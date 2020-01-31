using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class VrstaTreningaUrediVM
    {
        public int VrstaTreningaId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        [RegularExpression("^[A-ZŠŽČĆĐa-zšžčćđ ]{3,30}", ErrorMessage = "Naziv smije sadržavati samo slova i mora imati minimalno 3 karaktera.")]
        public string Naziv { get; set; }
    }
}
