using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Termin
    {
        [Key] public int TerminId { get; set; }
        public DateTime DatumTermina { get; set; }
        [Required] public string VrijemePocetak { get; set; }
        [Required] public string VrijemeKraj { get; set; }

        public virtual VrstaTreninga VrstaTreninga { get; set; }
        public int VrstaTreningaId { get; set; }

        public virtual Sala Sala { get; set; }
        public int SalaId { get; set; }

        public virtual Trener Trener { get; set; }
        public int TrenerId { get; set; }
    }
}
