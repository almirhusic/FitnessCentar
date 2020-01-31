using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class ObracunPlata
    {
        [Key] public int ObracunPlataId { get; set; }
        public float Satnica { get; set; }
        public float BrojRadnihSati { get; set; }
        public int BrojRadnihDana { get; set; }
        public float IznosPlate { get; set; }

        public virtual ObracunskiPeriod ObracunskiPeriod { get; set; }
        public int ObracunskiPeriodId { get; set; }

        public virtual Zaposlenik Zaposlenik { get; set; }
        public int ZaposlenikId { get; set; }

    }
}
