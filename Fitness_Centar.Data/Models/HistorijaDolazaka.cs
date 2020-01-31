using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class HistorijaDolazaka
    {
        [Key] public int HistorijaDolazakaId { get; set; }
        public DateTime DatumDolaska { get; set; }
        public string VrijemeDolaska { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }
    }
}
