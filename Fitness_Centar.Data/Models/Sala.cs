using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Centar.Data.Models
{
    public class Sala
    {
        [Key] public int SalaId { get; set; }
        [Required] public string Naziv { get; set; }
        public int BrojMjesta { get; set; }

    }
}
