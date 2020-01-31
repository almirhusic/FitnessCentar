using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class VrstaClanarine
    {
        [Key] public int VrstaClanarineId { get; set; }
        [Required] public string Naziv { get; set; }
        [Required] public decimal Cijena { get; set; }
        public string Opis { get; set; }
    }
}
