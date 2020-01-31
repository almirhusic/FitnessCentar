using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulTrener.ViewModels
{
    public class ObavijestDetaljiVM
    {
        public int ObavijestId { get; set; }
        public string Naslov { get; set; }
        public DateTime DatumObjave { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public string Sadrzaj { get; set; }
        public string ImePrezimeZaposlenika { get; set; }

        public int ZaposlenikId { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }

        public List<HistorijaIzmjenaObavijesti> IzmjeneObavijesti { get; set; }
    }
}
