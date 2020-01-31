using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels;
using Fitness_Centar.Web.Helper;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.Controllers
{
    [Autorizacija(recepcionar: true, trener: false, clan: false)]
    [Area("ModulRecepcionar")]
    public class ObavijestController : Controller
    {
        private readonly MyContext _ctx;

        public ObavijestController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index(DateTime? DatumOd, DateTime? DatumDo)
        {
            if (DatumOd == null)
                DatumOd = DateTime.MinValue;
            if (DatumDo == null)
                DatumDo = DateTime.MaxValue;

            List<Obavijest> model = _ctx.Obavijesti
                                    .Where(x => (x.DatumObjave >= DatumOd && x.DatumObjave <= DatumDo))
                                    .Include(x => x.Zaposlenik)
                                    .OrderByDescending(x => x.DatumObjave)
                                    .ToList();
            ViewData["_ctx"] = _ctx;

            return View(model);
        }

        public IActionResult Detalji(int id)
        {
            Obavijest o = _ctx.Obavijesti.Where(x => x.ObavijestId == id).Include(x => x.Zaposlenik).FirstOrDefault();
            List<HistorijaIzmjenaObavijesti> h = _ctx.HistorijaIzmjenaObavijesti.Where(x => x.ObavijestId == id).OrderByDescending(x => x.DatumIzmjene).ToList();
            HistorijaIzmjenaObavijesti h1 = h.Where(x => x.ObavijestId == o.ObavijestId).FirstOrDefault();
            ObavijestDetaljiVM model = new ObavijestDetaljiVM();
            
            model.Naslov = o.Naslov;
            model.Sadrzaj = o.Sadrzaj;
            model.DatumObjave = o.DatumObjave;
            model.ObavijestId = o.ObavijestId;
            model.ZaposlenikId = o.ZaposlenikId;
            model.Zaposlenik = o.Zaposlenik;

            if(h1 != null)
            {
                model.DatumIzmjene = h1.DatumIzmjene;
                model.IzmjeneObavijesti = h;
            }
            return View("Detalji", model);
        }

        public IActionResult PrikaziHistoriju(int id)
        {
            List<HistorijaIzmjenaObavijesti> model = _ctx.HistorijaIzmjenaObavijesti
                                                        .Where(x => x.ObavijestId == id)
                                                        .OrderByDescending(x => x.DatumIzmjene)
                                                        .ToList();
            ViewData["_ctx"] = _ctx;

            return View("Historija", model);
        }

        public IActionResult Obrisi(int id)
        {
            Obavijest o = _ctx.Obavijesti.Find(id);
            List<HistorijaIzmjenaObavijesti> historija = _ctx.HistorijaIzmjenaObavijesti
                                                         .Where(x => x.ObavijestId == id)
                                                         .ToList();
            if(historija != null)
            {
                foreach (HistorijaIzmjenaObavijesti h in historija)
                {
                    _ctx.HistorijaIzmjenaObavijesti.Remove(h);
                }
            }            

            _ctx.Obavijesti.Remove(o);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            ObavijestUrediVM model = new ObavijestUrediVM { };
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            model.Zaposlenik = k.Zaposlenik;
            model.Zaposlenici = new List<Zaposlenik>();
            model.Zaposlenici.Add(k.Zaposlenik);

            ViewData["view_uredi"] = null;

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            Obavijest o = _ctx.Obavijesti.Where(x => x.ObavijestId == id).Include(x => x.Zaposlenik).FirstOrDefault();
            ObavijestUrediVM model = new ObavijestUrediVM();

            model.ObavijestId = o.ObavijestId;
            model.Zaposlenici = null;
            model.Naslov = o.Naslov;
            model.DatumObjave = o.DatumObjave;
            model.Sadrzaj = o.Sadrzaj;
            model.Zaposlenik = o.Zaposlenik;
            model.ZaposlenikId = o.ZaposlenikId;
            model.ImePrezimeZaposlenika = o.Zaposlenik.Ime.ToString() + " " + o.Zaposlenik.Prezime.ToString();

            model.Zaposlenici = new List<Zaposlenik>();
            model.Zaposlenici.Add(model.Zaposlenik);

            ViewData["view_uredi"] = "Uredi";
            _ctx.SaveChanges();

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Snimi(ObavijestUrediVM model)
        {
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
            model.Zaposlenik = korisnik.Zaposlenik;

            if (ModelState.IsValid)
            {
                Obavijest o;
                if (model.ObavijestId != 0)
                {
                    o = _ctx.Obavijesti.Find(model.ObavijestId);

                    if(model.Naslov != o.Naslov || model.Sadrzaj != o.Sadrzaj)
                    {
                        HistorijaIzmjenaObavijesti historija = new HistorijaIzmjenaObavijesti
                        {
                            ObavijestId = o.ObavijestId,
                            DatumIzmjene = DateTime.Now,
                            StariNaslov = o.Naslov,
                            StariSadrzaj = o.Sadrzaj
                        };

                        _ctx.HistorijaIzmjenaObavijesti.Add(historija);
                        _ctx.SaveChanges();

                        ViewData["porukaUspjesno"] = "Uspješno ste uredili obavijest.";
                    }
                }
                else
                {
                    o = new Obavijest();
                    _ctx.Obavijesti.Add(o);
                    o.ZaposlenikId = model.ZaposlenikId;
                    o.DatumObjave = model.DatumObjave;

                    ViewData["porukaUspjesno"] = "Uspješno ste dodali obavijest.";
                }

                o.Naslov = model.Naslov;
                o.Sadrzaj = model.Sadrzaj;

                _ctx.SaveChanges();

                model.Zaposlenici = new List<Zaposlenik>();
                model.Zaposlenici.Add(korisnik.Zaposlenik);
                model.ImePrezimeZaposlenika = korisnik.Zaposlenik.Ime + " " + korisnik.Zaposlenik.Prezime;

                ViewData["view_uredi"] = "Uredi";
                return View("Dodaj", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                model.Zaposlenici = new List<Zaposlenik>();
                model.Zaposlenici.Add(korisnik.Zaposlenik);

                return View("Dodaj", model);
            }
        }
    }
}
