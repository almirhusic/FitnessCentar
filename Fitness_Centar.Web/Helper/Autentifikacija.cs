using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, Korisnik korisnik, bool zapamtiMe = false)
        {
            context.Session.SetObjectAsJson(LogiraniKorisnik, korisnik);

            if (zapamtiMe)
                context.Response.SetCookieJson(LogiraniKorisnik, korisnik);
            else
                context.Response.SetCookieJson(LogiraniKorisnik, null);
        }

        public static Korisnik GetLogiraniKorisnik(this HttpContext context)
        {
            Korisnik k = context.Session.GetObjectFromJson<Korisnik>(LogiraniKorisnik);
            if(k == null)
            {
                k = context.Request.GetCookieJson<Korisnik>(LogiraniKorisnik);
                context.Session.SetObjectAsJson(LogiraniKorisnik, k);
            }
            return k;
        }
    }
}
