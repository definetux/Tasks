using System.Collections.Generic;

namespace Pollination.Domain.Concrete
{
    public class Plant
    {
        public virtual int Id { get; set; }

        public virtual int HoneyPerDusting { get; set; }

        public virtual string Name { get; set; }

        public virtual PlantType PlantType { get; set; }

        public virtual ICollection<Bee> Bees { get; set; }
    }
}