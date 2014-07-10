using System.Collections.Generic;

namespace FootballMatches.Models
{
    public class Goal
    {
        public int GoalId { get; set; }

        public int Minute { get; set; }

        public string LastName { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}