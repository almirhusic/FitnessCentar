using Fitness_Centar.Data;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.Helper
{
    
    public class NotificationHub : Hub
    {
        private readonly MyContext _ctx;
        public NotificationHub(MyContext context)
        {
            _ctx = context;
        }


        
    }
}
