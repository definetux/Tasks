using System;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using Ninject;
using Pollination.Domain.Concrete;
using Pollination.Domain.Interfaces;
using Pollination.Infrastructure;

namespace Pollination
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NHibernateProfiler.Initialize();

            var kernel = new StandardKernel(new IoC());

            var repo = kernel.Get<IPollinationRepository>();

            Console.WriteLine("Most honey plants:");
            var honeyPlants = repo.GetMostHoneyPlants();
            foreach (var honeyPlant in honeyPlants)
            {
                Console.WriteLine("{0} - {1}", honeyPlant.Name, honeyPlant.Honey);
            }
            Console.WriteLine("=====================\n");

            Console.WriteLine("Count of beehives:");
            var citiesInfo = repo.GetCountsOfBeehive();
            foreach (var cityInfo in citiesInfo)
            {
                Console.WriteLine("{0} - {1}", cityInfo.City, cityInfo.Count);
            }
            Console.WriteLine("=====================\n");

            Console.WriteLine("Crowded beehives:");
            var crowdedBeehives = repo.GetCrowdedBeehives();
            foreach (var crowdedBeehive in crowdedBeehives)
            {
                Console.WriteLine("Beehive: {0} Capacity: {1} Bees: {2}", crowdedBeehive.Name, crowdedBeehive.Capacity, crowdedBeehive.Bees);
            }
            Console.WriteLine("=====================\n");

            Console.WriteLine("Roles statistic by beehives:");
            var rolesStatistic = repo.GetRolesStatistic();
            foreach (var beehive in rolesStatistic)
            {
                Console.WriteLine("{0}", beehive.BeehiveName);
                foreach (var roleInfo in beehive.RolesInfo)
                {
                    Console.WriteLine("\t{0}: {1}%", roleInfo.BeeRole, roleInfo.Percent);
                }
            }
            Console.WriteLine("=====================\n");


            Console.WriteLine("Top beehives:");
            var beehiveStatistics = repo.GetTopBeehives();
            foreach (var beehive in beehiveStatistics)
            {
                Console.WriteLine("Beehive: {0},\n \tQueen Name: {1},\n\tQueen IQ: {2},\n\tCapacity: {3}\n\tCount of bees: {4},\n\tSum of Honey: {5}",
                    beehive.BeehiveName, beehive.QueenName, beehive.QueenIQ, beehive.Capacity, beehive.BeesCount, beehive.HoneySum);
            }
            Console.WriteLine("=====================\n");

            Console.ReadLine();
        }
    }
}

