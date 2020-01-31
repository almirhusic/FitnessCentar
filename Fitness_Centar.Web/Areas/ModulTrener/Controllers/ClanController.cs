using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Areas.ModulTrener.ViewModels;
using Fitness_Centar.Web.Helper;

namespace Fitness_Centar.Web.Areas.ModulTrener.Controllers
{
    [Autorizacija(recepcionar: false, trener: true, clan: false)]
    [Area("ModulTrener")]
    public class ClanController : Controller
    {
        private readonly MyContext _ctx;

        public ClanController(MyContext context)
        {
            _ctx = context;
        }
        
        public IActionResult Index(string _obrisan, string _dodan, string _greska)
        {
            List<SviClanoviVM> model = new List<SviClanoviVM>();
            List<Clan> sviClanovi = _ctx.Clanovi
                                   .Include(x => x.VrstaClanarine)
                                   .Include(x => x.Grad)
                                   .ToList();
            List<LicniClanovi> licniClanovi = _ctx.LicniClanovi
                                                  .Include(x => x.Clan)
                                                  .Include(x => x.Trener)
                                                  .ToList();

            Korisnik k = HttpContext.GetLogiraniKorisnik();

            foreach (Clan x in sviClanovi)
            {
                LicniClanovi temp = new LicniClanovi();
                if (licniClanovi.Count != 0)
                {
                    temp = licniClanovi.Where(y => y.ClanId == x.ClanId && y.TrenerId == k.Trener.TrenerId).FirstOrDefault();
                }

                SviClanoviVM m = new SviClanoviVM();

                m.Clan = x;
                m.ClanId = x.ClanId;

                if (temp != null && x.ClanId == temp.ClanId)
                {
                    m.LicniClan = "Da";
                }
                else
                {
                    m.LicniClan = "Ne";
                }

                model.Add(m);
            }

            if(_obrisan != null)
            {
                ViewData["brisanje"] = _obrisan;
            }
            if (_dodan != null)
            {
                ViewData["dodavanje"] = _dodan;
            }
            if (_greska != null)
            {
                ViewData["greska"] = _greska;
            }
            _obrisan = _dodan = _greska = null;
            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            int trenerId = 0;
            string obrisan = null, greska = null;
            if (k.Trener != null)
            {
                trenerId = k.Trener.TrenerId;
            }

            LicniClanovi lc = _ctx.LicniClanovi.FirstOrDefault(x => x.ClanId == id && x.TrenerId == trenerId);

            if (lc != null)
            {
                _ctx.LicniClanovi.Remove(lc);
                _ctx.SaveChanges();

                Clan clan = _ctx.Clanovi.Find(id);

                obrisan = "Uspješno ste obrisali člana " + clan.Ime.ToString() + " " + clan.Prezime.ToString() + ".";
            }
            else
            {
                greska = "Došlo je do greške. Pokušajte ponovo.";
            }
            
            return RedirectToAction("Index", new { _obrisan = obrisan, _greska = greska });
        }

        public IActionResult Detalji(int id)
        {
            Clan c = _ctx.Clanovi
                         .Where(x => x.ClanId == id)
                         .Include(x => x.Grad)
                         .Include(x => x.VrstaClanarine)
                         .FirstOrDefault();

            return View(c);
        }

        public IActionResult Snimi(int id)
        {
            Korisnik korisnik = HttpContext.GetLogiraniKorisnik();
            var trenerId = korisnik.Trener.TrenerId;

            LicniClanovi lc = _ctx.LicniClanovi.FirstOrDefault(x => x.ClanId == id && x.TrenerId == trenerId);

            Clan clan = _ctx.Clanovi.Find(id);

            if (lc != null)
            {
                string greska = "Nije moguće dodati člana " + clan.Ime + " " + clan.Prezime;
                return RedirectToAction("Index", new { _greska = greska });
            }


            LicniClanovi c = new LicniClanovi();
            _ctx.LicniClanovi.Add(c);

            c.Clan = _ctx.Clanovi.Find(id);
            c.ClanId = id;
            c.TrenerId = trenerId;

            _ctx.SaveChanges();

            string dodan = "Uspješno ste dodali člana " + clan.Ime + " " + clan.Prezime;
            return RedirectToAction("Index", new { _dodan = dodan });
        }
    }
}
