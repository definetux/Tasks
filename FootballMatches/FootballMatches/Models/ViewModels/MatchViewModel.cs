using System;
using System.ComponentModel.DataAnnotations;

namespace FootballMatches.Models.ViewModels
{
    public class MatchViewModel
    {
        public int MatchId { get; set; }

        public string City { get; set; }

        public string Stadium { get; set; }
    }
}