using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class LicniClanovi
    {
        [Key] public int LicniClanoviId { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }

        public virtual Trener Trener { get; set; }
        public int TrenerId { get; set; }
    }
}
