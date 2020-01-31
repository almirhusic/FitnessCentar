using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Trener
    {
        [Key] public int TrenerId { get; set; }
        public int? GodineIskustva { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public int ZaposlenikId { get; set; }

        public static implicit operator Trener(List<Trener> v)
        {
            throw new NotImplementedException();
        }
    }
}
