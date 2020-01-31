using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Centar.Web.Areas.ModulClan.ViewModels
{
    public class UserInfoVM
    {
        public int UserInfoVMId { get; set; }
        public int ClanId { get; set; }
        public string ImePrezime { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Grad { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
    }
}
