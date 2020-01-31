using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.ViewModels
{
    public class HomeIndexVM
    {
        public List<Clan> Clanovi { get; set; }
        public List<Termin> Termini { get; set; }
        public List<Obavijest> Obavijesti { get; set; }
        public List<UplataClanarine> Uplate { get; set; }
    }
}
