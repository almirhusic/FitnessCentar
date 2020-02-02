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
    public class ClanController : Controller
    {
        private readonly MyContext _ctx;

        public ClanController(MyContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            List<Clan> clanovi = new List<Clan>();
            clanovi = _ctx.Clanovi
                          .Include(x => x.VrstaClanarine)
                          .Include(x => x.Grad)
                          .ToList();

            ViewData["_ctx"] = _ctx;

            return View(clanovi);
        }

        public IActionResult Detalji(int id)
        {
            Clan c = _ctx.Clanovi
                         .Include(x => x.VrstaClanarine)
                         .Include(x => x.Grad)
                         .Where(x => x.ClanId == id).First();

            return View("Detalji", c);
        }

        public IActionResult Obrisi(int id)
        {
            Clan clan = _ctx.Clanovi.Find(id);

            LicniClanovi lc = _ctx.LicniClanovi.FirstOrDefault(x => x.ClanId == id);
            List<Lajkovi> l = _ctx.Lajkovi.Where(x => x.ClanId == id).ToList();
            List<ObjaveClanova> o = _ctx.ObjaveClanova.Where(x => x.ClanId == id).ToList();
            List<KomentariObjavaClanova> k = _ctx.KomentariObjavaClanova.Where(x => x.ClanId == id).ToList();
            List<Followers> f = _ctx.Followeri.Where(x => x.PratiteljClanId == id || x.ZapraceniClanId == id).ToList();

            if(f.Count != 0)
            {
                foreach (var nn in f)
                {
                    _ctx.Followeri.Remove(nn);
                }
            }
            if (l != null)
            {
                foreach (var nn in l)
                {
                    _ctx.Lajkovi.Remove(nn);
                }
            }
            if(o != null)
            {
                foreach (var nn in o)
                {
                    _ctx.ObjaveClanova.Remove(nn);
                }
            }
            if(k != null)
            {
                foreach (var nn in k)
                {
                    _ctx.KomentariObjavaClanova.Remove(nn);
                }
            }
            if(lc != null)
            {
                _ctx.LicniClanovi.Remove(lc);
            }
            _ctx.Clanovi.Remove(clan);
            _ctx.SaveChanges();

            ViewData["porukaUspjesnoBrisanje"] = "Uspješno ste obrisali člana " + clan.Ime + " " + clan.Prezime;

            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            Clan c = _ctx.Clanovi.OrderByDescending(x => x.BrojClanskeKartice).First();
            ClanUrediVM model = new ClanUrediVM { };
            model.Gradovi = GetGradovi();
            model.Clanarine = GetVrsteClanarina();
            model.BrojClanskeKartice = ++c.BrojClanskeKartice;
            model.DatumRodjenja = DateTime.Now;
            
            return View(model);
        }

        public IActionResult Uredi(int id)
        {
            Clan c = _ctx.Clanovi.Find(id);
            ClanUrediVM model = new ClanUrediVM();            

            model.Gradovi = GetGradovi();
            model.Clanarine = GetVrsteClanarina();
            model.Clan = c;
            model.ClanId = c.ClanId;
            model.Ime = c.Ime;
            model.Prezime = c.Prezime;
            model.KorisnickoIme = c.KorisnickoIme;
            model.Lozinka = c.Lozinka;
            model.JMBG = c.JMBG;
            model.DatumRodjenja = c.DatumRodjenja;
            model.Adresa = c.Adresa;
            model.Telefon = c.Telefon;
            model.Email = c.Email;
            model.Spol = c.Spol;
            model.DatumUclanjivanja = c.DatumUclanjivanja;
            model.BrojClanskeKartice = c.BrojClanskeKartice;
            model.VrstaClanarineId = c.VrstaClanarineId;
            model.GradId = c.GradId;
            model.Grad = c.Grad;

            _ctx.SaveChanges();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public IActionResult Snimi(ClanUrediVM model)
        {
            DateTime d = model.DatumRodjenja;
            if(d.AddYears(15) > DateTime.Today)
            {
                ViewData["datumRodjenjaGreska"] = "Morate imati minimalno 15 godina da bi ste se učlanili.";
            }

            List<Clan> clanovi = _ctx.Clanovi.ToList();
            foreach(Clan c in clanovi)
            {
                if(c.ClanId != model.ClanId)
                {
                    if (c.Ime == model.Ime && c.Prezime == model.Prezime)
                    {
                        ViewData["clanGreska"] = "Član '" + model.Ime.ToString() + " " + model.Prezime.ToString() + "' je već učlanjen.";
                    }
                    if (c.BrojClanskeKartice == model.BrojClanskeKartice)
                    {
                        ViewData["karticaGreska"] = "Članska kartica sa brojem '" + model.BrojClanskeKartice.ToString() + "' je zauzeta.";
                    }
                    if (c.KorisnickoIme == model.KorisnickoIme)
                    {
                        ViewData["korisnickoImeGreska"] = "Korisničko ime '" + model.KorisnickoIme.ToString() + "' je zauzeto.";
                    }
                    if (c.Telefon == model.Telefon)
                    {
                        ViewData["telefonGreska"] = "Broj telefona '" + model.Telefon.ToString() + "' je zauzet.";
                    }
                    if (c.JMBG == model.JMBG)
                    {
                        ViewData["jmbgGreska"] = "Jmbg '" + model.JMBG.ToString() + "' već postoji.";
                    }
                }
            }

            if(ModelState.IsValid && ViewData["clanGreska"] == null && ViewData["karticaGreska"] == null
                && ViewData["korisnickoImeGreska"] == null && ViewData["telefonGreska"] == null &&
                ViewData["jmbgGreska"] == null && ViewData["datumRodjenjaGreska"] == null)
            {
                Clan c;
                if(model.ClanId != 0)
                {
                    c = _ctx.Clanovi.Find(model.ClanId);
                    ViewData["porukaUspjesno"] = "Uspješno ste uredili podatke o članu.";
                }
                else
                {
                    c = new Clan();
                    _ctx.Clanovi.Add(c);
                    ViewData["porukaUspjesno"] = "Uspješno ste dodali člana.";
                }

                c.Ime = model.Ime;
                c.Prezime = model.Prezime;
                c.KorisnickoIme = model.KorisnickoIme;
                c.Lozinka = model.Lozinka;
                c.JMBG = model.JMBG;
                c.DatumRodjenja = model.DatumRodjenja;
                c.Adresa = model.Adresa;
                c.Telefon = model.Telefon;
                c.Email = model.Email;
                c.Spol = model.Spol;
                c.DatumUclanjivanja = model.DatumUclanjivanja;
                c.BrojClanskeKartice = model.BrojClanskeKartice;
                c.VrstaClanarineId = model.VrstaClanarineId;
                c.GradId = model.GradId;

                _ctx.SaveChanges();

                model.Gradovi = GetGradovi();
                model.Clanarine = GetVrsteClanarina();

                return View("Uredi", model);
            }
            else
            {
                model.Gradovi = GetGradovi();
                model.Clanarine = GetVrsteClanarina();

                ViewData["porukaNeuspjesno"] = "Žao nam je. Podaci nisu validni.";

                if (model.ClanId != 0)
                    return View("Uredi", model);
                else
                    return View("Dodaj", model);
            }
        }

        private List<VrstaClanarine> GetVrsteClanarina()
        {
            return _ctx.VrstaClanarine.ToList();
        }

        private List<Grad> GetGradovi()
        {
            return _ctx.Gradovi.ToList();
        }
    }
}
