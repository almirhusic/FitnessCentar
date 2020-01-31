using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class PlanTreninga
    {
        [Key] public int PlanTreningaId { get; set; }
        [Required] public string NazivVjezbe { get; set; }
        public int Trajanje { get; set; }
        public int BrojSerija { get; set; }
        public int BrojPonavljanja { get; set; }
        public string Opis { get; set; }

        public virtual Clan Clan { get; set; }
        public int ClanId { get; set; }
    }
}
