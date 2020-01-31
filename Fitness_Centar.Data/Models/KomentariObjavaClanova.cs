using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness_Centar.Data.Models
{
    public class KomentariObjavaClanova
    {
        public int KomentariObjavaClanovaId { get; set; }
        public string SadrzajKomentara { get; set; }
        public DateTime DatumObjaveKomentara { get; set; }

        public int ObjaveClanovaId { get; set; }
        public virtual ObjaveClanova ObjaveClanova { get; set; }

        public int ClanId { get; set; }
        public virtual Clan Clan { get; set; }
    }
}
