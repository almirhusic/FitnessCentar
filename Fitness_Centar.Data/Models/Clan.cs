using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Clan
    {
        [Key] public int ClanId { get; set; }
        [Required] public string Ime { get; set; }
        [Required] public string Prezime { get; set; }
        [Required] public string KorisnickoIme { get; set; }
        [Required] public string Lozinka { get; set; }
        [Required] public string JMBG { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        [Required] public string Telefon { get; set; }
        public string Email { get; set; }
        public string Spol { get; set; }
        public DateTime DatumUclanjivanja { get; set; }
        public int BrojClanskeKartice { get; set; }

        public virtual VrstaClanarine VrstaClanarine { get; set; }
        public int VrstaClanarineId { get; set; }

        public virtual Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}
