using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class RezervacijaGrupnihTermina
    {
        [Key] public int RezervacijaGrupnihTerminaId { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }

        public virtual Termin Termin { get; set; }
        public int TerminId { get; set; }
    }
}
