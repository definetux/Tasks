using System.Collections;
using System.Collections.Generic;
using Pollination.Domain.Concrete;

namespace Pollination.Domain.Interfaces
{
    public interface IPollinationRepository
    {
        IEnumerable<T> Get<T>();

        void AddBeToBeehive(int beehiveId, Bee bee);

        void AddQueenToBeehive(int beehiveId, Queen queen);

        void AddBeeToPlant(int plantId, Bee bee);

        void AddPlantToBee(int beeId, Plant plant);

        void AddBeehive(Beehive beehive);

        List<HoneyPlant> GetMostHoneyPlants();

        List<CityInfo> GetCountsOfBeehive();

        List<CrowdedBeehive> GetCrowdedBeehives();

        List<RolesStatistic> GetRolesStatistic();

        List<BeehiveStatistic> GetTopBeehives();

    }
}