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
    public class VrstaClanarineController : Controller
    {
        private readonly MyContext _ctx;

        public VrstaClanarineController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<VrstaClanarine> model = _ctx.VrstaClanarine.ToList();
            ViewData["_ctx"] = _ctx;

            return View(model);
        }

        public ActionResult Obrisi(int id)
        {
            VrstaClanarine VrsteClanarine = _ctx.VrstaClanarine.Find(id);
            _ctx.VrstaClanarine.Remove(VrsteClanarine);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            VrstaClanarineUrediVM model = new VrstaClanarineUrediVM();

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            VrstaClanarine v = _ctx.VrstaClanarine.Find(id);
            VrstaClanarineUrediVM model = new VrstaClanarineUrediVM();
            model.VrstaClanarineId = v.VrstaClanarineId;
            model.Naziv = v.Naziv;
            model.Cijena = v.Cijena;
            model.Opis = v.Opis;

            _ctx.SaveChanges();

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(VrstaClanarineUrediVM model)
        {
            if (model.VrstaClanarineId == 0)
            {
                foreach (VrstaClanarine vrsta in _ctx.VrstaClanarine.ToList())
                {
                    if (vrsta.Naziv == model.Naziv)
                        ViewData["nazivGreska"] = "Vrsta članarine sa tim nazivom već postoji.";
                }
            }

            if (ModelState.IsValid && ViewData["nazivGreska"] == null)
            {
                VrstaClanarine v;
                if (model.VrstaClanarineId != 0)
                {
                    v = _ctx.VrstaClanarine.Find(model.VrstaClanarineId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o vrsti članarine.";
                }
                else
                {
                    v = new VrstaClanarine();
                    _ctx.VrstaClanarine.Add(v);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali vrstu članarine.";
                }
                v.Naziv = model.Naziv;
                v.Cijena = model.Cijena;
                v.Opis = model.Opis;
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
