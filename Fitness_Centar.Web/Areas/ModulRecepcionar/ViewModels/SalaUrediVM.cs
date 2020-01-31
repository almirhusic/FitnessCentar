using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class SalaUrediVM
    {
        public int SalaId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        [RegularExpression("^[A-ZŠŽČĆĐa-zšžčćđ0-9 ]*$", ErrorMessage = "Naziv smije sadržavati samo slova.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Broj mjesta je obavezno polje.")]
        [Range(1, 50, ErrorMessage = "Broj mjesta mora biti u rasponu od 1 do 50.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Broj mjesta smije sadržavati samo brojeve.")]
        public int BrojMjesta { get; set; }
    }
}
