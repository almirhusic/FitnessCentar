using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class VrstaClanarineUrediVM
    {
        public int VrstaClanarineId { get; set; }

        [Required(ErrorMessage = "Naziv je obavezno polje.")]
        [RegularExpression("^[A-ZŠŽČĆĐa-zšžčćđ ]{4,30}", ErrorMessage = "Naziv smije sadržavati samo slova. Mora imati minimalno 4 karaktera.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Cijena je obavezno polje.")]
        [Range(1, 1000, ErrorMessage = "Cijena mora biti veća od 1.")]
        [RegularExpression("^[0-9,]*$", ErrorMessage = "Cijena smije sadržavati samo brojeve.")]
        public decimal Cijena { get; set; }
        public string Opis { get; set; }
        public List<VrstaClanarine> VrsteClanarine { get; set; }
    }
}
