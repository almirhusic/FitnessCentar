using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.Helper
{
    public class LajkHub : Hub
    {
        private readonly MyContext _ctx;
        public LajkHub(MyContext context)
        {
            _ctx = context;
        }

        public class User
        {
            public string Id;
            public HashSet<string> connectionIds;
        }
        private static readonly ConcurrentDictionary<string, User> ConnectedUsers = new ConcurrentDictionary<string, User>();

        public async Task Lajkaj(int userId, int postId)
        {
            Lajkovi lajk = new Lajkovi();
            Lajkovi temp = _ctx.Lajkovi.Where(x => x.ObjaveClanovaId == postId && x.ClanId == userId).FirstOrDefault();

            if(temp != null)
            {
                _ctx.Lajkovi.Remove(temp);
                
                //Remove Notification on Unlike post...
                Notifikacija n = _ctx.Notifikacije.Where(x => x.ObjavaId == postId && x.SourceClanId == userId).FirstOrDefault();
                if (n != null)
                    _ctx.Notifikacije.Remove(n);
            }
            else
            {
                _ctx.Lajkovi.Add(lajk);

                lajk.ClanId = userId;
                lajk.ObjaveClanovaId = postId;                
            }

            _ctx.SaveChanges();

            if (temp == null)
                await AddNotification(userId, postId);

            int brLajkova = _ctx.Lajkovi.Where(x => x.ObjaveClanovaId == postId).Count();
            string lajkovi = "";
            List<Lajkovi> likeList = _ctx.Lajkovi.Include(x => x.Clan).Where(x => x.ObjaveClanovaId == postId).ToList();
            foreach (var d in likeList)
            {
                if (likeList.Count() > 2)
                {
                    if (likeList.IndexOf(d) < 2)
                    {
                        lajkovi += d.Clan.Ime + " " + d.Clan.Prezime + ", ";
                    }
                    else
                    {
                        lajkovi = lajkovi.Substring(0, lajkovi.Length - 1);
                        lajkovi += " i još " + (likeList.Count() - 2) + " drugih";
                        break;
                    }
                }
                else
                {
                    lajkovi += d.Clan.Ime + " " + d.Clan.Prezime + ",";
                }
            }

            if(likeList.Count() > 0 && likeList.Count() < 3)
            {
                lajkovi = lajkovi.Substring(0, lajkovi.Length - 1);
            }

            await Clients.All.SendAsync("ReceiveMessage", brLajkova, postId, userId, lajkovi);
        }

        public async Task AddNotification(int userId, int postId)
        {
            ObjaveClanova o = _ctx.ObjaveClanova.Find(postId);
            Notifikacija n = new Notifikacija();
            _ctx.Notifikacije.Add(n);

            n.Sadrzaj = " je lajkao vašu objavu ";
            n.SourceClanId = userId;
            n.DestClanId = o.ClanId;
            n.ObjavaId = postId;
            n.Seen = false;
            n.Read = false;

            _ctx.SaveChanges();

            await Notify(o.ClanId);
        }


        public async Task Notify(int uId)
        {
            User receiver;
            if (ConnectedUsers.TryGetValue(uId.ToString(), out receiver))
            {
                IEnumerable<string> allReceivers;
                lock (receiver.connectionIds)
                {
                    allReceivers = receiver.connectionIds;
                }

                foreach (var cid in allReceivers)
                {
                    await Clients.Client(cid).SendAsync("ReceiveMsg");
                }
            }
        }

        public override Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            Korisnik k = httpContext.GetLogiraniKorisnik();

            string id = k.Clan?.ClanId.ToString();
            string connectionId = Context.ConnectionId;

            var user = ConnectedUsers.GetOrAdd(id, _ => new User
            {
                Id = id,
                connectionIds = new HashSet<string>()
            });

            lock (user.connectionIds)
            {
                user.connectionIds.Add(connectionId);
            }

            return base.OnConnectedAsync();
        }
    }
}
