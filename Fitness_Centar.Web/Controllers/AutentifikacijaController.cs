using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Fitness_Centar.Web.Helper;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Centar.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private MyContext _ctx;

        public AutentifikacijaController(MyContext db)
        {
            _ctx = db;
        }

        public IActionResult Index()
        {
            ViewData["context"] = _ctx;
            return View("Index", new AutentifikacijaVM() { ZapamtiMe = true });
        }
        
        [HttpPost]
        public IActionResult Login(AutentifikacijaVM model)
        {
            Korisnik k = new Korisnik();

            if (model.Uloga == "Recepcionar")
            {
                k.Zaposlenik = _ctx.Zaposlenici.FirstOrDefault(x => x.KorisnickoIme == model.KorisnickoIme
                                                               && x.Lozinka == model.Lozinka
                                                               && x.IsRecepcionar);
            }
            else if (model.Uloga == "Trener")
            {
                k.Trener = _ctx.Treneri.Include(x => x.Zaposlenik).FirstOrDefault(x => x.Zaposlenik.KorisnickoIme == model.KorisnickoIme
                                                        && x.Zaposlenik.Lozinka == model.Lozinka
                                                        && x.Zaposlenik.IsTrener);
                
            }
            else if (model.Uloga == "Clan")
            {
                k.Clan = _ctx.Clanovi.FirstOrDefault(x => x.KorisnickoIme == model.KorisnickoIme
                                                        && x.Lozinka == model.Lozinka);

            }

            if (ModelState.IsValid && (k.Zaposlenik != null || k.Trener != null || k.Clan != null))
            {
                HttpContext.SetLogiraniKorisnik(k, model.ZapamtiMe);

                if (k.Zaposlenik != null)
                    return RedirectToAction("Index", "Home", new { area = "" });
                if (k.Trener != null)
                    return RedirectToAction("Index", "Clan", new { area = "ModulTrener" });
                if (k.Clan != null)
                    return RedirectToAction("Index", "Clan", new { area = "ModulClan" });
            }

            ViewData["porukaGreska"] = "Niste unijeli validne podatke.";
            return View("Index", model);
        }

        public ActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return RedirectToAction("Index");
        }
    }
}