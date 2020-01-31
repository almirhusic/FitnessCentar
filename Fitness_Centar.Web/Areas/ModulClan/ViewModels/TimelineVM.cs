using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.ViewModels
{
    public class TimelineVM
    {
        public ObjaveClanova Objava { get; set; }
        public int BrojKomentara { get; set; }
        public List<KomentariObjavaClanova> Komentari { get; set; }

        public List<Lajkovi> Lajkovi { get; set; }
        public string KoJeLajkao { get; set; }
    }
}
