using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class TerminUrediVM
    {
        public int TerminId { get; set; }
        [Required(ErrorMessage = "Datum termina je obavezno polje.")]
        [DataType(DataType.Date, ErrorMessage = "Datum nije u validnom formatu.")]
        public DateTime DatumTermina { get; set; }

        [Required(ErrorMessage = "Vrijeme početka termina je obavezno polje.")]
        [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Vrijeme početka termina nije u validnom formatu.")]
        public string VrijemePocetak { get; set; }

        [Required(ErrorMessage = "Vrijeme kraja termina je obavezno polje.")]
        [RegularExpression("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Vrijeme kraja termina nije u validnom formatu.")]
        public string VrijemeKraj { get; set; }


        public List<VrstaTreninga> VrsteTreningaList { get; set; }
        public virtual VrstaTreninga VrstaTreninga { get; set; }

        [Required(ErrorMessage = "Morate odabrati vrstu treninga.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Niste odabrali vrstu treninga.")]
        public int VrstaTreningaId { get; set; }

        public List<Sala> SaleList { get; set; }
        public virtual Sala Sala { get; set; }

        [Required(ErrorMessage = "Morate odabrati salu.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Niste odabrali salu.")]
        public int SalaId { get; set; }


        public List<Trener> TreneriList { get; set; }
        public virtual Trener Trener { get; set; }

        [Required(ErrorMessage = "Morate odabrati trenera")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Niste odabrali trenera.")]
        public int TrenerId { get; set; }

        public string ImePrezime { get; set; }
    }
}
