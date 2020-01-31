using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class GradUrediVM
    {
        public int GradId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        [RegularExpression("^[A-ZŠŽČĆĐa-zšžčćđ ]*$", ErrorMessage = "Naziv treba sadržavati samo slova.")]
        public string Naziv { get; set; }
        public List<Grad> Gradovi { get; set; }
    }
}
