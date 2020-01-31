using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness_Centar.Data.Models
{
    public class ObjaveClanova
    {
        public int ObjaveClanovaId { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumObjave { get; set; }

        public int ClanId { get; set; }
        public virtual Clan Clan { get; set; }
    }
}
