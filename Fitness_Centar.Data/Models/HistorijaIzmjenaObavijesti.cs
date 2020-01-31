using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class HistorijaIzmjenaObavijesti
    {
        [Key] public int HistorijaIzmjenaObavijestiId { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public string StariNaslov { get; set; }
        public string StariSadrzaj { get; set; }

        public virtual Obavijest Obavijest { get; set; }
        public int ObavijestId { get; set; }
    }
}
