using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Obavijest
    {
        [Key] public int ObavijestId { get; set; }
        [Required] public string Naslov { get; set; }
        public DateTime DatumObjave { get; set; }
        [Required] public string Sadrzaj { get; set; }

        public virtual Zaposlenik Zaposlenik { get; set; }
        public int ZaposlenikId { get; set; }
    }
}
