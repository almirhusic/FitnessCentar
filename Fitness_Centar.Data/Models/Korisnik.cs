using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Korisnik
    {
        [Key] public int KorisnikId { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public virtual Trener Trener { get; set; }
        public virtual Clan Clan { get; set; }
    }
}
