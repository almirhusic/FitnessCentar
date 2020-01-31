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

namespace Fitness_Centar.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(recepcionar: false, trener: true, clan: false)]
    [Area("ModulTrener")]
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

            if (h1 != null)
            {
                model.DatumIzmjene = h1.DatumIzmjene;
                model.IzmjeneObavijesti = h;
            }
            return View("Detalji", model);
        }

        public IActionResult Dodaj()
        {
            ObavijestUrediVM model = new ObavijestUrediVM { };

            int logKorId = GetLogKorisnikId();
            int zapId = _ctx.Treneri.Find(logKorId).ZaposlenikId;

            model.ZaposlenikId = zapId;
            model.Zaposlenici = _ctx.Zaposlenici.Where(x => x.ZaposlenikId == zapId).ToList();
            
            ViewData["view_uredi"] = null;

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            Obavijest o = _ctx.Obavijesti.Find(id);
            ObavijestUrediVM model = new ObavijestUrediVM
            {
                ObavijestId = o.ObavijestId,
                Zaposlenici = _ctx.Zaposlenici.Where(x => x.ZaposlenikId == o.ZaposlenikId).ToList(),
                Naslov = o.Naslov,
                DatumObjave = o.DatumObjave,
                Sadrzaj = o.Sadrzaj,
                Zaposlenik = o.Zaposlenik,
                ZaposlenikId = o.ZaposlenikId,
                ImePrezimeZaposlenika = o.Zaposlenik.Ime.ToString() + " " + o.Zaposlenik.Prezime.ToString()
            };

            ViewData["view_uredi"] = "Uredi";
            _ctx.SaveChanges();

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Snimi(ObavijestUrediVM model)
        {
            Korisnik korisnik = new Korisnik();
            korisnik.Zaposlenik = _ctx.Zaposlenici.Where(x => x.ZaposlenikId == model.ZaposlenikId).First();
            model.Zaposlenik = korisnik.Zaposlenik;

            if (ModelState.IsValid)
            {
                Obavijest o;
                if (model.ObavijestId != 0)
                {
                    o = _ctx.Obavijesti.Find(model.ObavijestId);
                    o.Zaposlenik = model.Zaposlenik;

                    if (model.Naslov != o.Naslov || model.Sadrzaj != o.Sadrzaj)
                    {
                        HistorijaIzmjenaObavijesti historija = new HistorijaIzmjenaObavijesti
                        {
                            Obavijest = o,
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
                    o.Zaposlenik = model.Zaposlenik;
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

        private int GetLogKorisnikId()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            if (k.Trener != null)
            {
                return k.Trener.TrenerId;
            }
            return -1;
        }
    }
}
