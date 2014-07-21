using System.Collections.Generic;

namespace Gymnastic.Domain
{
    public class Exercise
    {
        public Trainer Trainer { get; set; }

        public int Time { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Trainer.Name, Time);
        }
    }
}