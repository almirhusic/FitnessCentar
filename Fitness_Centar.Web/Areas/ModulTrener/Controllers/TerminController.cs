using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Helper;
using Fitness_Centar.Web.Areas.ModulTrener.ViewModels;

namespace Fitness_Centar.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(recepcionar: false, trener: true, clan: false)]
    [Area("ModulTrener")]
    public class TerminController : Controller
    {
        private readonly MyContext _ctx;
        public int trenerId;

        public TerminController(MyContext context)
        {
            _ctx = context;
            
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

        public IActionResult Index()
        {
            trenerId = GetLogKorisnikId();
            
            List<Termin> model = _ctx.Termini
                                .Where(x => x.DatumTermina >= DateTime.Today && x.TrenerId == trenerId)
                                .Include(x => x.VrstaTreninga)
                                .Include(x => x.Sala)
                                .Include(x => x.Trener)
                                .Include(x => x.Trener.Zaposlenik)
                                .OrderByDescending(x => x.DatumTermina)
                                .ToList();

            return View(model);
        }

        public IActionResult Dodaj()
        {
            trenerId = GetLogKorisnikId();
            TerminUrediVM model = new TerminUrediVM
            {
                SaleList = GetSale(),
                VrsteTreningaList = GetVrsteTreninga(),
                Trener = GetTrener(),
                DatumTermina = DateTime.Now,
                TrenerId = trenerId
            };            
            model.ImePrezime = model.Trener.Zaposlenik.Ime + " " + model.Trener.Zaposlenik.Prezime;

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            Termin t = _ctx.Termini.Where(x => x.TerminId == id)
                                   .Include(x => x.Sala)
                                   .Include(x => x.VrstaTreninga)
                                   .Include(x => x.Trener)
                                   .Include(x => x.Trener.Zaposlenik)
                                   .FirstOrDefault();
            TerminUrediVM model = new TerminUrediVM
            {
                TerminId = t.TerminId,
                SaleList = GetSale(),
                VrsteTreningaList = GetVrsteTreninga(),
                DatumTermina = t.DatumTermina,
                VrijemePocetak = t.VrijemePocetak,
                VrijemeKraj = t.VrijemeKraj,
                Sala = t.Sala,
                SalaId = t.SalaId,
                Trener = t.Trener,
                TrenerId = t.TrenerId,
                VrstaTreninga = t.VrstaTreninga,
                VrstaTreningaId = t.VrstaTreningaId,
                ImePrezime = t.Trener.Zaposlenik.Ime.ToString() + " " + t.Trener.Zaposlenik.Prezime.ToString()
            };

            _ctx.SaveChanges();
            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(TerminUrediVM model)
        {
            model.TrenerId = GetLogKorisnikId();
            if (ModelState.IsValid)
            {
                List<Termin> termini = _ctx.Termini.Where(x => x.DatumTermina >= model.DatumTermina).ToList();

                if (string.Compare(model.VrijemeKraj, model.VrijemePocetak) <= 0)
                {
                    ViewData["greskaSatiTermina"] = "Vrijeme kraja termina ne može biti manje ili jednako vremenu početka termina.";
                }

                if (model.DatumTermina < DateTime.Today)
                {
                    ViewData["datumTerminaGreska"] = "Ne možete dodati termin za datum koji je prošao.";
                }

                foreach (Termin termin in termini)
                {
                    if (model.TerminId != termin.TerminId)
                    {
                        if ((termin.DatumTermina == model.DatumTermina)
                            && (termin.VrijemePocetak == model.VrijemePocetak)
                            && (termin.SalaId == model.SalaId))
                        {
                            ViewData["salaGreska"] = "Termin u odabranoj sali je zauzet.";
                        }

                        if ((termin.DatumTermina == model.DatumTermina)
                            && (termin.VrijemePocetak == model.VrijemePocetak)
                            && (termin.TrenerId == model.TrenerId))
                        {
                            ViewData["trenerGreska"] = "Trener zauzet za odabrani termin.";
                        }
                    }
                }
            }

            if (ModelState.IsValid && ViewData["trenerGreska"] == null
                                   && ViewData["salaGreska"] == null
                                   && ViewData["greskaSatiTermina"] == null
                                   && ViewData["datumTerminaGreska"] == null)
            {
                Termin t;
                if (model.TerminId != 0)
                {
                    t = _ctx.Termini.Find(model.TerminId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o terminu.";
                }
                else
                {
                    t = new Termin();
                    _ctx.Termini.Add(t);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali termin.";
                }

                t.DatumTermina = model.DatumTermina;
                t.VrijemePocetak = model.VrijemePocetak;
                t.VrijemeKraj = model.VrijemeKraj;
                t.Sala = model.Sala;
                t.SalaId = model.SalaId;
                t.Trener = model.Trener;
                t.TrenerId = model.TrenerId;
                t.VrstaTreninga = model.VrstaTreninga;
                t.VrstaTreningaId = model.VrstaTreningaId;

                _ctx.SaveChanges();

                model.SaleList = GetSale();
                model.VrsteTreningaList = GetVrsteTreninga();

                return View("Dodaj", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                model.SaleList = GetSale();
                model.VrsteTreningaList = GetVrsteTreninga();
                return View("Dodaj", model);
            }
        }

        private List<Sala> GetSale()
        {
            return _ctx.Sale.OrderBy(x => x.Naziv).ToList();
        }

        private List<VrstaTreninga> GetVrsteTreninga()
        {
            return _ctx.VrstaTreninga.OrderBy(x => x.Naziv).ToList();
        }

        private Trener GetTrener()
        {
            trenerId = GetLogKorisnikId();
            return _ctx.Treneri.Include(x => x.Zaposlenik).Where(x => x.TrenerId == trenerId && x.Zaposlenik.IsTrener).OrderBy(x => x.Zaposlenik.Ime).FirstOrDefault();
        }
    }
}
