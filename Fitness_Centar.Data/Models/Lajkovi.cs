using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fitness_Centar.Data.Models
{
    public class Lajkovi
    {
        [Key] public int LajkoviId { get; set; }

        [Required] public int ObjaveClanovaId { get; set; }
        public virtual ObjaveClanova ObjaveClanova { get; set; }

        [Required] public int ClanId { get; set; }
        public virtual Clan Clan { get; set; }
    }
}
