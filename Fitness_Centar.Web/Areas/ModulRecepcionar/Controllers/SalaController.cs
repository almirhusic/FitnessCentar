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
    public class SalaController : Controller
    {
        private readonly MyContext _ctx;

        public SalaController(MyContext context)
        {
            _ctx = context;
        }
        
        public IActionResult Index()
        {
            ViewData["_ctx"] = _ctx;

            List<Sala> model = _ctx.Sale.ToList();

            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            Sala s = _ctx.Sale.Find(id);
            _ctx.Sale.Remove(s);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            SalaUrediVM model = new SalaUrediVM();

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            Sala s = _ctx.Sale.Find(id);

            SalaUrediVM model = new SalaUrediVM();
            model.SalaId = s.SalaId;
            model.Naziv = s.Naziv;
            model.BrojMjesta = s.BrojMjesta;

            _ctx.SaveChanges();

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public ActionResult Snimi(SalaUrediVM model)
        {
            if (model.SalaId == 0)
            {
                foreach (Sala sala in _ctx.Sale.ToList())
                {
                    if (model.Naziv == sala.Naziv)
                        ViewData["nazivGreska"] = "Sala sa tim nazivom već postoji.";
                }
            }

            if (ModelState.IsValid && ViewData["nazivGreska"] == null)
            {
                Sala s;
                if (model.SalaId != 0)
                {
                    s = _ctx.Sale.Find(model.SalaId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o sali.";
                }
                else
                {
                    s = new Sala();
                    _ctx.Sale.Add(s);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali salu.";
                }

                s.Naziv = model.Naziv;
                s.BrojMjesta = model.BrojMjesta;

                _ctx.SaveChanges();

                return View("Dodaj", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                return View("Dodaj", model);
            }
        }
    }
}
