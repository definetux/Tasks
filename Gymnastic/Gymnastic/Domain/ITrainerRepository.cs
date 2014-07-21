using System.Collections.Generic;

namespace Gymnastic.Domain
{
    public interface ITrainerRepository
    {
        List<Trainer> GetTrainers();
    }
}