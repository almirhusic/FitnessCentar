using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.ViewModels
{
    public class ProfilVM
    {
        public int BrojKomentara { get; set; }
        public int BrojLajkova { get; set; }
        public string KoJeLajkao { get; set; }
        public ObjaveClanova Objava { get; set; }
        public List<Lajkovi> Lajkovi { get; set; }
        public List<KomentariObjavaClanova> Komentari { get; set; }
    }
}
