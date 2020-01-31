using System;
using System.Collections.Generic;
using System.Text;

namespace Fitness_Centar.Data.Models
{
    public class Followers
    {
        public int FollowersId { get; set; }
        public int PratiteljClanId { get; set; }
        public virtual Clan PratiteljClan { get; set; }

        public int ZapraceniClanId { get; set; }
        public virtual Clan ZapraceniClan { get; set; }

    }
}
