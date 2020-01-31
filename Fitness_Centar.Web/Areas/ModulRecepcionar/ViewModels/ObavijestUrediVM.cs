using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class ObavijestUrediVM
    {
        public int ObavijestId { get; set; }
        [Required(ErrorMessage = "Naslov je obavezno polje.")]
        public string Naslov { get; set; }
        [Required(ErrorMessage = "Datum objave je obavezno polje.")]
        public DateTime DatumObjave { get; set; }
        [Required(ErrorMessage = "Sadržaj je obavezno polje.")]
        [MaxLength(10000, ErrorMessage = "Sadržaj može sadržavati max 1000 karaktera.")]
        public string Sadrzaj { get; set; }
        public string ImePrezimeZaposlenika { get; set; }

        [Required(ErrorMessage = "Morate odabrati zaposlenika.")]
        public int ZaposlenikId { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }

        public List<HistorijaIzmjenaObavijesti> IzmjeneObavijesti { get; set; }
    }
}
