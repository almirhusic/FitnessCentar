using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class ClanUrediVM
    {
        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }


        [Required(ErrorMessage = "Ime je obavezno polje.")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Ime smije sadržavati samo slova.")]
        public string Ime { get; set; }


        [Required(ErrorMessage = "Prezime je obavezno polje.")]
        [RegularExpression("^[A-ZČĆŽŠĐa-zčćžšđ]{3,}", ErrorMessage = "Ime smije sadržavati samo slova.")]
        public string Prezime { get; set; }


        [Required(ErrorMessage = "Korisničko ime je obavezno polje.")]
        [RegularExpression("^[a-z0-9.]{4,18}", ErrorMessage = "Korisničko ime nije u validnom formatu.")]
        public string KorisnickoIme { get; set; }


        [Required(ErrorMessage = "Lozinka je obavezno polje.")]
        [RegularExpression("((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20})", ErrorMessage = "Lozinka mora sadržavati barem jedan broj, malo i veliko slovo! Veličina lozinke mora biti u rasponu 6-20 karaktera.")]
        public string Lozinka { get; set; }


        [Required(ErrorMessage = "JMBG je obavezno polje.")]
        [RegularExpression("^[0-9]{13}", ErrorMessage = "JMBG može sadržavati samo brojeve.")]
        public string JMBG { get; set; }

        [Required(ErrorMessage = "Datum rođenja je obavezno polje.")]
        [DataType(DataType.Date, ErrorMessage = "Datum nije u validnom formatu.")]
        public DateTime DatumRodjenja { get; set; }


        [Required(ErrorMessage = "Adresa je obavezno polje.")]
        public string Adresa { get; set; }


        [Required(ErrorMessage = "Broj telefona je obavezno polje.")]
        [RegularExpression("([06]{2}[0-6][0-9]{3}[0-9]{3,4})", ErrorMessage = "Broj telefona nije validan.")]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "Email je obavezno polje.")]
        [EmailAddress(ErrorMessage = "Email nije u validnom formatu.")]
        public string Email { get; set; }


        [RegularExpression("^((?!contain).)*(Muško|Žensko)", ErrorMessage = "Morate odabrati spol.")]
        public string Spol { get; set; }


        [Required(ErrorMessage = "Datum učlanjivanja je obavezno polje.")]
        [DataType(DataType.Date, ErrorMessage = "Datum nije u validnom formatu.")]
        public DateTime DatumUclanjivanja { get; set; }


        [Range(1, Int32.MaxValue, ErrorMessage = "Broj članske kartice mora biti veći od 1.")]
        public int BrojClanskeKartice { get; set; }


        [Range(1, Int32.MaxValue, ErrorMessage = "Niste odabrali grad.")]
        public int GradId { get; set; }
        public virtual Grad Grad { get; set; }
        public List<Grad> Gradovi { get; set; }


        [Range(1, Int32.MaxValue, ErrorMessage = "Niste odabrali vrstu članarine.")]
        public int VrstaClanarineId { get; set; }
        public virtual VrstaClanarine VrsteClanarine { get; set; }
        public List<VrstaClanarine> Clanarine { get; set; }
    }
}
