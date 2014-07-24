using System;
using System.Collections.Generic;

namespace Pollination.Domain.Concrete
{
    public class Bee
    {
        public virtual int Id { get; set; }

        public virtual ComplexName Name { get; set; }

        public virtual ICollection<Plant> Plants { get; set; }

        public virtual BeeRole Role { get; set; }

        public virtual Beehive Beehive { get; set; }
    }
}