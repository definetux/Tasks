using System.Collections.Generic;
using FootballMatches.Models.Concrete;

namespace FootballMatches.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string City  { get; set; }

        public int Place { get; set; }

        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public virtual ICollection<Goal> Goals { get; set; } 
    }
}