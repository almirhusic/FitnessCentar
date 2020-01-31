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
    public class UplataClanarineController : Controller
    {
        private readonly MyContext _ctx;

        public UplataClanarineController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<UplataClanarine> model = _ctx.UplataClanarine
                                            .OrderByDescending(x => x.DatumUplate)
                                            .Include(x => x.Clan)
                                            .ToList();
            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            UplataClanarine uplata = _ctx.UplataClanarine.Find(id);
            _ctx.UplataClanarine.Remove(uplata);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Dodaj(int? id)
        {
            UplataClanarineDodajVM model = new UplataClanarineDodajVM();
            if (id != null)
            {
                model.ClanId = (int)id;
                model.Clanovi = _ctx.Clanovi.Where(x => x.ClanId == id).ToList();
                return View("Dodaj", model);
            }
            else
            {
                model.Clanovi = GetClanovi();
                return View("Uredi", model);
            }
        }

        public IActionResult Uredi(int id)
        {
            UplataClanarine c = _ctx.UplataClanarine.Find(id);
            UplataClanarineDodajVM model = new UplataClanarineDodajVM();
            model.UplataClanarineId = c.UplataClanarineId;
            model.Clan = _ctx.Clanovi.Find(c.ClanId);
            model.Clanovi = _ctx.Clanovi.Where(x => x.ClanId == c.ClanId).ToList();
            model.Iznos = c.Iznos;
            model.DatumUplate = c.DatumUplate;
            model.ClanId = c.ClanId;

            _ctx.SaveChanges();
            return View("Uredi", model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(UplataClanarineDodajVM model, string clan = "ne")
        {
            if(ModelState.IsValid && ViewData["porukaUspjesno"] == null)
            {
                UplataClanarine u;
                if(model.UplataClanarineId != 0)
                {
                    u = _ctx.UplataClanarine.Find(model.UplataClanarineId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o uplati.";
                }
                else
                {
                    u = new UplataClanarine();
                    _ctx.UplataClanarine.Add(u);
                    u.DatumUplate = model.DatumUplate;
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali uplatu.";
                }

                model.Clan = _ctx.Clanovi.Find(model.ClanId);
                u.Clan = model.Clan;
                u.ClanId = model.ClanId;
                u.Iznos = model.Iznos;
                model.Clanovi = GetClanovi();

                _ctx.SaveChanges();
                if (clan == "da")
                {
                    return View("Dodaj", model);
                }
                return View("Uredi", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                if (model.ClanId != 0)
                {
                    model.Clan = _ctx.Clanovi.Find(model.ClanId);
                    model.Clanovi = _ctx.Clanovi.Where(x => x.ClanId == model.ClanId).ToList();
                }
                else
                {
                    model.Clanovi = GetClanovi();
                }
                if (clan == "da")
                {
                    return View("Dodaj", model);
                }
                return View("Uredi", model);
            }
        }

        private List<Clan> GetClanovi()
        {
            return _ctx.Clanovi.OrderBy(x => x.Ime).ToList();
        }
    }
}
