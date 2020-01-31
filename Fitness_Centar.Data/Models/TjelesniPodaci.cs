using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class TjelesniPodaci
    {
        [Key] public int TjelesniPodaciId { get; set; }
        public float Visina { get; set; }
        public float Tezina { get; set; }
        public string Mjere { get; set; }
        
        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }
    }
}
