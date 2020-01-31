using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class DropInInfo
    {
        [Key] public int DropInInfoId { get; set; }
        public int BrojDolazaka { get; set; }
        public int PreostaliBrojDolazaka { get; set; }
        public DateTime DatumIsteka { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }
    }
}
