using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class UplataClanarine
    {
        [Key] public int UplataClanarineId { get; set; }
        public decimal Iznos { get; set; }
        public DateTime DatumUplate { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }
    }
}
