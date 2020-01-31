using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Centar.Data;
using Fitness_Centar.Data.Models;
using Fitness_Centar.Web.Helper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Fitness_Centar.Web.Areas.ModulClan.Controllers;

namespace Fitness_Centar.Web.Areas.ModulClan.Helper
{
    public class KomentarHub : Hub
    {
        private readonly MyContext _ctx;
        public class User
        {
            public string Id;
            public HashSet<string> connectionIds;
        }
        private static readonly ConcurrentDictionary<string, User> ConnectedUsers = new ConcurrentDictionary<string, User>();
        public KomentarHub(MyContext context)
        {
            _ctx = context;
        }
        public async Task Komentiraj(int userId, int postId, string sadrzaj)
        {
            KomentariObjavaClanova k = new KomentariObjavaClanova();
            _ctx.KomentariObjavaClanova.Add(k);
            k.ClanId = userId;
            k.ObjaveClanovaId = postId;
            k.DatumObjaveKomentara = DateTime.Now;
            k.SadrzajKomentara = sadrzaj;

            ObjaveClanova o = _ctx.ObjaveClanova.Find(postId);

            _ctx.SaveChanges();

            AddNotification(userId, postId);

            int brKomentara = _ctx.KomentariObjavaClanova.Where(x => x.ObjaveClanovaId == postId).Count();
            string username = _ctx.Clanovi.Find(userId).Ime + " " + _ctx.Clanovi.Find(userId).Prezime;
            await Clients.All.SendAsync("ReceiveMessage", userId, postId, brKomentara, k);
            await Notify(o.ClanId);
        }

        public void AddNotification(int userId, int postId)
        {
            ObjaveClanova o = _ctx.ObjaveClanova.Find(postId);
            Notifikacija n = new Notifikacija();
            _ctx.Notifikacije.Add(n);

            n.Sadrzaj = " je komentarisao vašu objavu ";
            n.SourceClanId = userId;
            n.DestClanId = o.ClanId;
            n.ObjavaId = postId;
            n.Seen = false;
            n.Read = false;

            _ctx.SaveChanges();
        }

        public async Task ObrisiKomentar(int kId, int uId, int pId)
        {
            pId -= 148;

            KomentariObjavaClanova k = new KomentariObjavaClanova();
            k = _ctx.KomentariObjavaClanova.Find(kId);

            if(k != null && k.ClanId == uId)
            {
                _ctx.KomentariObjavaClanova.Remove(k);
                _ctx.SaveChanges();
            }

            Notifikacija n = _ctx.Notifikacije.Where(x => x.ObjavaId == pId && x.SourceClanId == uId).FirstOrDefault();
            if (n != null )
                _ctx.Notifikacije.Remove(n);
            await _ctx.SaveChangesAsync();

            int brKomentara = _ctx.KomentariObjavaClanova.Where(x => x.ObjaveClanovaId == k.ObjaveClanovaId).Count();           

            await Clients.All.SendAsync("ReceiveMessageObrisi", kId, uId, pId, brKomentara);
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
