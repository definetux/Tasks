using System;
using System.Data.Entity;
using FootballMatches.Models.Concrete;

namespace FootballMatches.Models
{
    public class EFDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Goal> Goals { get; set; }
    }
}