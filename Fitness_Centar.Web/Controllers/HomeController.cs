using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fitness_Centar.Web.ViewModels;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Helper;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Centar.Web.Controllers
{
    [Autorizacija(recepcionar: true, trener: false, clan: false)]
    public class HomeController : Controller
    {
        private MyContext _ctx;

        public HomeController(MyContext db)
        {
            _ctx = db;
        }
        public IActionResult Index()
        {
            HomeIndexVM model = new HomeIndexVM();

            model.Clanovi = GetClanovi();
            model.Obavijesti = GetObavijesti();
            model.Termini = GetTermini();
            model.Uplate = GetUplate();

            ViewData["context"] = _ctx;
            return View(model);
        }

        private List<UplataClanarine> GetUplate()
        {
            return _ctx.UplataClanarine.OrderByDescending(x => x.DatumUplate)
                                       .Include(x => x.Clan)
                                       .Take(5)
                                       .ToList();
        }

        private List<Termin> GetTermini()
        {
            return _ctx.Termini.Include(x => x.Trener)
                               .Include(x => x.Trener.Zaposlenik)
                               .Include(x => x.Sala)
                               .Include(x => x.VrstaTreninga)
                               .Where(x => x.DatumTermina == DateTime.Today)
                               .ToList();
        }

        private List<Obavijest> GetObavijesti()
        {
            return _ctx.Obavijesti.OrderByDescending(x => x.DatumObjave)
                                  .Include(x => x.Zaposlenik)
                                  .Take(5)
                                  .ToList();
        }

        private List<Clan> GetClanovi()
        {
            return _ctx.Clanovi.OrderByDescending(x => x.DatumUclanjivanja)
                               .Take(8)
                               .ToList();
        }


    }
}