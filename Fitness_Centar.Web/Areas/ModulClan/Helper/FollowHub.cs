using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.Helper
{
    public class FollowHub : Hub
    {
        private readonly MyContext _ctx;
        public FollowHub(MyContext context)
        {
            _ctx = context;
        }

        public async Task Follow(int pratitelj, int zapraceni)
        {
            string action = "";
            Followers f = new Followers();
            f = _ctx.Followeri.Where(x => x.PratiteljClanId == pratitelj && x.ZapraceniClanId == zapraceni).FirstOrDefault();
            if (f != null)
            {
                _ctx.Followeri.Remove(f);
                action = "unfollow";
            }
            else
            {
                f = new Followers();
                _ctx.Followeri.Add(f);

                f.PratiteljClanId = pratitelj;
                f.ZapraceniClanId = zapraceni;
                action = "follow";
            }

            _ctx.SaveChanges();

            await Clients.All.SendAsync("ReceiveMessage", pratitelj, zapraceni, action);
        }
    }
}
