using System;
using System.Collections.Generic;

namespace FootballMatches.Models.Concrete
{
    public class Match
    {
        public int MatchId { get; set; }

        public string City { get; set; }

        public string Stadium { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

    }
}