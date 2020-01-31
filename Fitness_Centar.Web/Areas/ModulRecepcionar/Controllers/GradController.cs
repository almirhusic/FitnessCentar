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
    public class GradController : Controller
    {
        private readonly MyContext _ctx;

        public GradController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<GradIndexVM> model = new List<GradIndexVM>();

            foreach (var x in _ctx.Gradovi)
            {
                GradIndexVM m = new GradIndexVM();
                int brojClanova = _ctx.Clanovi.Count(y => y.GradId == x.GradId);
                int brojZaposlenika = _ctx.Zaposlenici.Count(y => y.GradId == x.GradId);
                int ukupno = brojClanova + brojZaposlenika;

                m.GradId = x.GradId;
                m.Naziv = x.Naziv;
                m.BrojOsoba = ukupno;
                m.Gradovi = getGradovi();
                model.Add(m);
            }

            ViewData["_ctx"] = _ctx;

            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            Grad grad = _ctx.Gradovi.Find(id);
            _ctx.Gradovi.Remove(grad);
            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            GradUrediVM model = new GradUrediVM { };

            return View("Dodaj", model);
        }

        public IActionResult Uredi(int id)
        {
            Grad g = _ctx.Gradovi.Find(id);
            GradUrediVM model = new GradUrediVM();

            model.GradId = g.GradId;
            model.Naziv = g.Naziv;

            return View("Dodaj", model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(GradUrediVM model)
        {
            if (model.GradId == 0)
            {
                foreach (Grad grad in _ctx.Gradovi.ToList())
                {
                    if (grad.Naziv == model.Naziv)
                        ViewData["NazivGreska"] = "Grad sa tim nazivom već postoji.";
                }
            }

            if (ModelState.IsValid && ViewData["NazivGreska"] == null)
            {
                Grad g;

                if (model.GradId != 0)
                {
                    g = _ctx.Gradovi.Find(model.GradId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o gradu.";
                }
                else
                {
                    g = new Grad();
                    _ctx.Gradovi.Add(g);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali grad.";
                }
                g.Naziv = model.Naziv;

                _ctx.SaveChanges();
                return View("Dodaj", model);
            }
            else
            {
                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";
                return View("Dodaj", model);
            }
        }

        private List<Grad> getGradovi()
        {
            return _ctx.Gradovi.ToList();
        }


        //// GET: ModulRecepcionar/Grad
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Gradovi.ToListAsync());
        //}

        //// GET: ModulRecepcionar/Grad/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grad = await _context.Gradovi
        //        .FirstOrDefaultAsync(m => m.GradId == id);
        //    if (grad == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grad);
        //}

        //// GET: ModulRecepcionar/Grad/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ModulRecepcionar/Grad/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GradId,Naziv")] Grad grad)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(grad);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(grad);
        //}

        //// GET: ModulRecepcionar/Grad/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grad = await _context.Gradovi.FindAsync(id);
        //    if (grad == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(grad);
        //}

        //// POST: ModulRecepcionar/Grad/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("GradId,Naziv")] Grad grad)
        //{
        //    if (id != grad.GradId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(grad);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!GradExists(grad.GradId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(grad);
        //}

        //// GET: ModulRecepcionar/Grad/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var grad = await _context.Gradovi
        //        .FirstOrDefaultAsync(m => m.GradId == id);
        //    if (grad == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(grad);
        //}

        //// POST: ModulRecepcionar/Grad/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var grad = await _context.Gradovi.FindAsync(id);
        //    _context.Gradovi.Remove(grad);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool GradExists(int id)
        //{
        //    return _context.Gradovi.Any(e => e.GradId == id);
        //}
    }
}
