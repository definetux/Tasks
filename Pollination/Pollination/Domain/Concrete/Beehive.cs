using System.Collections.Generic;

namespace Pollination.Domain.Concrete
{
    public class Beehive
    {
        public virtual int Id { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Bee> Bees { get; set; }

        public virtual int Capacity { get; set; }

        public virtual string Name { get; set; }

        public virtual Queen Queen { get; set; }
    }
}