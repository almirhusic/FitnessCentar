using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Fitness_Centar.Web.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool recepcionar, bool trener, bool clan)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { recepcionar, trener, clan };
        }
    }

    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool recepcionar, bool trener, bool clan)
        {
            _recepcionar = recepcionar;
            _trener = trener;
            _clan = clan;
        }
        private readonly bool _recepcionar;
        private readonly bool _trener;
        private readonly bool _clan;

        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            Korisnik k = filterContext.HttpContext.GetLogiraniKorisnik();

            if(k == null)
            {
                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
                return;
            }
            
            MyContext db = filterContext.HttpContext.RequestServices.GetService<MyContext>();

            if(k.Zaposlenik != null)
            {
                if (_recepcionar && db.Zaposlenici.Any(x => x.ZaposlenikId == k.Zaposlenik.ZaposlenikId))
                {
                    await next();
                    return;
                }
            }
            
            if(k.Trener != null)
            {
                if (_trener && db.Treneri.Any(x => x.TrenerId == k.Trener.TrenerId))
                {
                    await next();
                    return;
                }
            }
            
            if(k.Clan != null)
            {
                if(_clan && db.Clanovi.Any(x => x.ClanId == k.Clan.ClanId))
                {
                    await next();
                    return;
                }
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}
