namespace FootballMatches.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Number { get; set; }

        public string Position { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}