namespace Pollination.Domain.Concrete
{
    public class Queen
    {
        public virtual int Id { get; set; }

        public virtual ComplexName Name { get; set; }

        public virtual int  IQ { get; set; }

        public virtual Beehive Beehive { get; set; }
    }
}