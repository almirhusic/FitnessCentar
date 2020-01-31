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
    public class VrstaTreningaController : Controller
    {
        private readonly MyContext _ctx;

        public VrstaTreningaController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<VrstaTreninga> model = _ctx.VrstaTreninga.ToList();
            ViewData["_ctx"] = _ctx;

            return View("Index", model);
        }

        public IActionResult Obrisi(int id)
        {
            VrstaTreninga v = _ctx.VrstaTreninga.Find(id);
            _ctx.VrstaTreninga.Remove(v);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            VrstaTreningaUrediVM model = new VrstaTreningaUrediVM();

            return View("Dodaj", model);
        }

        public ActionResult Uredi(int id)
        {
            VrstaTreninga v = _ctx.VrstaTreninga.Find(id);
            VrstaTreningaUrediVM model = new VrstaTreningaUrediVM();
            model.VrstaTreningaId = v.VrstaTreningaId;
            model.Naziv = v.Naziv;

            _ctx.SaveChanges();

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(VrstaTreningaUrediVM model)
        {
            if (model.VrstaTreningaId == 0)
            {
                foreach (VrstaTreninga vrsta in _ctx.VrstaTreninga.ToList())
                {
                    if (vrsta.Naziv == model.Naziv)
                        ViewData["nazivGreska"] = "Vrsta treninga sa tim nazivom već postoji.";
                }
            }

            if (ModelState.IsValid && ViewData["NazivGreska"] == null)
            {
                VrstaTreninga v;
                if (model.VrstaTreningaId != 0)
                {
                    v = _ctx.VrstaTreninga.Find(model.VrstaTreningaId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke od vrsti treninga.";
                }
                else
                {
                    v = new VrstaTreninga();
                    _ctx.VrstaTreninga.Add(v);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali vrstu treninga.";
                }
                v.Naziv = model.Naziv;
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
