using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness_Centar.Data.Models
{
    public class Notifikacija
    {
        public int NotifikacijaId { get; set; }
        public string Sadrzaj { get; set; }
        public int SourceClanId { get; set; }
        public int DestClanId { get; set; }
        public int ObjavaId { get; set; }
        public bool Seen { get; set; }
        public bool Read { get; set; }
    }
}
