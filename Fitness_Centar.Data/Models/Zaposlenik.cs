using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Zaposlenik
    {
        [Key] public int ZaposlenikId { get; set; }
        public bool IsTrener { get; set; }
        public bool IsMenadzer { get; set; }
        public bool IsRecepcionar { get; set; }
        [Required] public string KorisnickoIme { get; set; }
        [Required] public string Lozinka { get; set; }
        [Required] public string Ime { get; set; }
        [Required] public string Prezime { get; set; }
        public string Spol { get; set; }
        [Required] public string JMBG { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        [Required] public string Telefon { get; set; }
        public string Email { get; set; }
        [Required] public string Adresa { get; set; }
        [Required] public string PocetakRada { get; set; }
        [Required] public string KrajRada { get; set; }

        public virtual Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}
