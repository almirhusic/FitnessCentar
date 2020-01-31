using Fitness_Centar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulRecepcionar.ViewModels
{
    public class GradIndexVM
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public int BrojOsoba { get; set; }
        public List<Grad> Gradovi { get; set; }
    }
}
