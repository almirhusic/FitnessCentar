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
    public class TerminController : Controller
    {
        private readonly MyContext _ctx;

        public TerminController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<Termin> model = _ctx.Termini
                                     .Where(x => x.DatumTermina >= DateTime.Today)
                                     .Include(x => x.VrstaTreninga)
                                     .Include(x => x.Sala)
                                     .Include(x => x.Trener)
                                     .Include(x => x.Trener.Zaposlenik)
                                     .OrderByDescending(x => x.DatumTermina)
                                     .ToList();

            return View(model);
        }

        public async Task<IActionResult> Dodaj()
        {
            TerminUrediVM model = new TerminUrediVM { };
            model.SaleList = await GetSale();
            model.VrsteTreningaList = await GetVrsteTreninga();
            model.Trener = new Trener();
            model.Trener.Zaposlenik = new Zaposlenik();
            model.TreneriList = await GetTrener();
            model.DatumTermina = DateTime.Now;

            return View("Dodaj", model);
        }

        public async Task<IActionResult> Uredi(int id)
        {
            Termin t = await _ctx.Termini.FindAsync(id);
            TerminUrediVM model = new TerminUrediVM
            {
                TerminId = t.TerminId,
                SaleList = await GetSale(),
                TreneriList = await GetTrener(),
                VrsteTreningaList = await GetVrsteTreninga(),
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

            await _ctx.SaveChangesAsync();
            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Snimi(TerminUrediVM model)
        {
            if (ModelState.IsValid)
            {
                List<Termin> termini = await _ctx.Termini.Where(x => x.DatumTermina >= model.DatumTermina).ToListAsync();

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
                        if((termin.DatumTermina == model.DatumTermina) 
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
                    t = await _ctx.Termini.FindAsync(model.TerminId);
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

                await _ctx.SaveChangesAsync();

                model.SaleList = await GetSale();
                model.VrsteTreningaList = await GetVrsteTreninga();
                model.TreneriList = await GetTrener();

                return View("Dodaj", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                model.SaleList = await GetSale();
                model.VrsteTreningaList = await GetVrsteTreninga();
                model.TreneriList = await GetTrener();
                return View("Dodaj", model);
            }
        }

        private async Task<List<Sala>> GetSale()
        {
            return await _ctx.Sale.OrderBy(x => x.Naziv).ToListAsync();
        }

        private async Task<List<VrstaTreninga>> GetVrsteTreninga()
        {
            return await _ctx.VrstaTreninga.OrderBy(x => x.Naziv).ToListAsync();
        }

        private async Task<List<Trener>> GetTrener()
        {
            return await _ctx.Treneri.Include(x => x.Zaposlenik).Where(x => x.Zaposlenik.IsTrener).OrderBy(x => x.Zaposlenik.Ime).ToListAsync();
        }
    }
}
