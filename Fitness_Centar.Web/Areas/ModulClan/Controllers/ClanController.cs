using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Helper;
using Fitness_Centar.Web.Areas.ModulClan.ViewModels;

namespace Fitness_Centar.Web.Areas.ModulClan.Controllers
{
    [Autorizacija(recepcionar: false, trener: false, clan: true)]
    [Area("ModulClan")]
    public class ClanController : Controller
    {
        private readonly MyContext _ctx;
        private Korisnik k;
        public ClanController(MyContext context)
        {
            _ctx = context;
        }
        
        public IActionResult Index()
        {
            List<TimelineVM> model = new List<TimelineVM>();
            List<ObjaveClanova> objave = _ctx.ObjaveClanova.OrderByDescending(x => x.DatumObjave).Include(x => x.Clan).ToList();
            
            foreach (var item in objave) 
            {
                TimelineVM t = new TimelineVM();
                t.Objava = item;
                t.BrojKomentara = GetBrKomentara(item.ObjaveClanovaId);
                t.Komentari = GetKomentari(item.ObjaveClanovaId);
                t.Lajkovi = GetLajkovi(item.ObjaveClanovaId);
                t.KoJeLajkao = GetKoJeLajkao(t.Lajkovi, t.Objava.ObjaveClanovaId);

                model.Add(t);
            }
                        
            ViewData["_ctx"] = _ctx;

            return View(model);
        }

        public IActionResult DodajObjavu(string sadrzaj)
        {
            k = HttpContext.GetLogiraniKorisnik();
            if (k.Clan == null)
                return RedirectToAction("Index", "Autentifikacija", new { area = "" });

            if (string.IsNullOrEmpty(sadrzaj))
                return RedirectToAction("Index");

            ObjaveClanova o = new ObjaveClanova();
            
            _ctx.ObjaveClanova.Add(o);
            
            o.ClanId = k.Clan.ClanId;
            o.DatumObjave = DateTime.Now;
            o.Sadrzaj = sadrzaj;

            sadrzaj = string.Empty;

            _ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ObrisiPost(int id)
        {
            ObjaveClanova o = _ctx.ObjaveClanova.Find(id);
            if(o != null)
            {
                List<KomentariObjavaClanova> k = _ctx.KomentariObjavaClanova.Where(x => x.ObjaveClanovaId == id).ToList();
                if (k.Count() > 0)
                {
                    foreach (var item in k)
                    {
                        _ctx.KomentariObjavaClanova.Remove(item);
                    }
                }

                List<Lajkovi> l = _ctx.Lajkovi.Where(x => x.ObjaveClanovaId == id).ToList();
                if (l.Count() > 0)
                {
                    foreach (var item in l)
                    {
                        _ctx.Lajkovi.Remove(item);
                    }
                }

                _ctx.ObjaveClanova.Remove(o);
                _ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserInfo(int id)
        {
            UserInfoVM model = new UserInfoVM();
            Clan c = _ctx.Clanovi.Include(x => x.Grad).Include(x => x.VrstaClanarine).Where(x => x.ClanId == id).FirstOrDefault();
            if(c != null)
            {
                model.ClanId = c.ClanId;
                model.Email = c.Email;
                model.ImePrezime = c.Ime + " " + c.Prezime;
                model.DatumRodjenja = c.DatumRodjenja;
                model.Grad = c.Grad.Naziv;
                model.Followers = GetFollowers(id, "followers");
                model.Following = GetFollowers(id, "following");
            }
            return View("User-info", model);
        }

        public IActionResult Profil(int? id)
        {
            if (id == null)
            {
                Korisnik k = HttpContext.GetLogiraniKorisnik();
                if(k.Clan != null)
                {
                    id = k.Clan.ClanId;
                }
            }
            ViewData["_ctx"] = _ctx;
            List<ProfilVM> model = new List<ProfilVM>();
            Clan c = _ctx.Clanovi.Include(x => x.Grad).Where(x => x.ClanId == id).FirstOrDefault();
            ViewData["clan"] = c;
            if (c != null)
            {
                List<ObjaveClanova> objave = _ctx.ObjaveClanova.OrderByDescending(x => x.DatumObjave).Include(x => x.Clan).Include(x => x.Clan.Grad).Where(x => x.ClanId == id).ToList();

                ViewData["followers"] = GetFollowers((int)id, "followers");
                ViewData["following"] = GetFollowers((int)id, "following");
                foreach (var item in objave)
                {
                    ProfilVM temp = new ProfilVM();
                    temp.Objava = item;
                    temp.BrojKomentara = GetBrKomentara(item.ObjaveClanovaId);
                    temp.Komentari = GetKomentari(item.ObjaveClanovaId);
                    temp.Lajkovi = GetLajkovi(item.ObjaveClanovaId);
                    temp.KoJeLajkao = GetKoJeLajkao(temp.Lajkovi, item.ObjaveClanovaId);
                    
                    model.Add(temp);
                }

                return View("Profil", model);
            }
            return RedirectToAction("Index");
        }

        private string GetKoJeLajkao(List<Lajkovi> lajkovi, int objavaId)
        {
            string koJeLajkao = null;
            if (lajkovi.Count() > 0)
            {
                foreach (var item in lajkovi)
                {
                    if (item.ObjaveClanovaId == objavaId)
                    {
                        if (lajkovi.IndexOf(item) >= 2)
                        {
                            koJeLajkao = koJeLajkao.Substring(0, koJeLajkao.Length - 2);
                            koJeLajkao += " i još " + (lajkovi.Count() - 2) + " drugih";
                            break;
                        }
                        else
                        {
                            koJeLajkao += item.Clan.Ime + " " + item.Clan.Prezime + ", ";
                        }
                    }
                }
            }
            if (lajkovi.Count() > 0 && lajkovi.Count() < 3)
            {
                koJeLajkao = koJeLajkao.Substring(0, koJeLajkao.Length - 2);
            }
            return koJeLajkao;
        }

        private IEnumerable<ObjaveClanova> GetObjave(int id)
        {
            return _ctx.ObjaveClanova.Where(x => x.ClanId == id).Include(x => x.Clan).ToList();
        }

        private int GetFollowers(int id, string f)
        {
            if(f == "followers")
                return _ctx.Followeri.Where(x => x.ZapraceniClanId == id).Count();
            else
                return _ctx.Followeri.Where(x => x.PratiteljClanId == id).Count();
        }

        private List<Lajkovi> GetLajkovi(int id)
        {
            return _ctx.Lajkovi.Include(x => x.Clan)
                               .Include(x => x.ObjaveClanova)
                               .OrderByDescending(x => x.LajkoviId)
                               .Where(x => x.ObjaveClanovaId == id).ToList();
        }
        private int GetBrKomentara(int id)
        {
            return _ctx.KomentariObjavaClanova.Where(x => x.ObjaveClanovaId == id).Count(); ;
        }
        private List<KomentariObjavaClanova> GetKomentari(int id)
        {
            return _ctx.KomentariObjavaClanova
                       .Include(x => x.Clan)
                       .Include(x => x.ObjaveClanova)
                       .Where(x => x.ObjaveClanovaId == id)
                       .OrderByDescending(x => x.DatumObjaveKomentara).ToList();
        }
    }
}
