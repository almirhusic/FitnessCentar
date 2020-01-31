using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class UplataClanarineDodajVM
    {
        public int UplataClanarineId { get; set; }

        [Required(ErrorMessage = "Iznos je obavezno polje.")]
        [Range(1, 10000, ErrorMessage = "Cijena mora biti veća od 1.")]
        //[RegularExpression("^[0-9,]*$", ErrorMessage = "Iznos ne smije sadržavati slova.")]
        public decimal Iznos { get; set; }

        [Required(ErrorMessage = "Datum uplate je obavezno polje.")]
        [DataType(DataType.Date, ErrorMessage = "Datum nije u validnom formatu.")]
        public DateTime DatumUplate { get; set; }


        public List<Clan> Clanovi { get; set; }
        public virtual Clan Clan { get; set; }

        [Required(ErrorMessage = "Morate odabrati člana.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Morate odabrati člana.")]
        public int ClanId { get; set; }
    }
}
