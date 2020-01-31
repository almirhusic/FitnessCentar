using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulTrener.ViewModels
{
    public class SviClanoviVM
    {
        public int ClanId { get; set; }
        public virtual Clan Clan { get; set; }
        public string LicniClan { get; set; }
    }
}
