using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using Pollination.Domain.Interfaces;

namespace Pollination.Domain.Concrete
{
    public class PollinationRepository : IPollinationRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public PollinationRepository()
        {
            _sessionFactory = BuildFactory();
        }

        /// <summary>
        /// Получить все записи в таблице T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> Get<T>()
        {
            List<T> items;
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    items = session.Query<T>().ToList();

                    transact.Commit();
                }
            }
            return items;
        }

        /// <summary>
        /// Добавить пчелу к улью
        /// </summary>
        /// <param name="beehiveId"></param>
        /// <param name="bee"></param>
        public void AddBeToBeehive(int beehiveId, Bee bee)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    var beehive = session.Get<Beehive>(beehiveId);

                    if (beehive != null)
                    {
                        beehive.Bees.Add(bee);
                        bee.Beehive = beehive;

                        session.Update(beehive);
                    }

                    transact.Commit();
                }
            }
        }

        /// <summary>
        /// Добавить королеву к улью
        /// </summary>
        /// <param name="beehiveId"></param>
        /// <param name="queen"></param>
        public void AddQueenToBeehive(int beehiveId, Queen queen)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    var beehive = session.Get<Beehive>(beehiveId);

                    if (beehive != null)
                    {
                        beehive.Queen = queen;
                        queen.Beehive = beehive;

                        session.Update(beehive);
                    }

                    transact.Commit();
                }
            }
        }

        /// <summary>
        /// Добавить пчелу к растению
        /// </summary>
        /// <param name="plantId"></param>
        /// <param name="bee"></param>
        public void AddBeeToPlant(int plantId, Bee bee)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    var plant = session.Get<Plant>(plantId);

                    if (plant != null && bee != null)
                    {
                        plant.Bees.Add(bee);
                        session.Save(plant);
                    }

                    transact.Commit();
                }
            }
        }

        /// <summary>
        /// Добавить растение к пчеле
        /// </summary>
        /// <param name="beeId"></param>
        /// <param name="plant"></param>
        public void AddPlantToBee(int beeId, Plant plant)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    var bee = session.Get<Bee>(beeId);

                    if (plant != null && bee != null)
                    {
                        bee.Plants.Add(plant);
                        session.Save(bee);
                    }

                    transact.Commit();
                }
            }
        }

        /// <summary>
        /// Добавить улей
        /// </summary>
        /// <param name="beehive"></param>
        public void AddBeehive(Beehive beehive)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    session.Save(beehive);
                    transact.Commit();
                }
            }
        }

        /// <summary>
        /// Выбрать наиболее медоносные растения
        /// </summary>
        /// <returns></returns>
        public List<HoneyPlant> GetMostHoneyPlants()
        {
            List<HoneyPlant> honeyPlants;
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    honeyPlants = session.Query<Plant>()
                        .GroupBy(plant => plant.Name)
                        .Select(x => new HoneyPlant
                        {
                            Name = x.First().Name,
                            Honey = x.Sum(c => c.HoneyPerDusting),
                        }).OrderByDescending(x => x.Honey).Take(3).ToList();

                    transact.Commit();
                }
            }

            return honeyPlants;
        }

        /// <summary>
        /// Выбрать количество ульев в каждом городе
        /// </summary>
        /// <returns></returns>
        public List<CityInfo> GetCountsOfBeehive()
        {
            List<CityInfo> cities;

            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    cities = session.Query<Beehive>()
                        .GroupBy(city => city.Address.City)
                        .Select(x => new CityInfo
                        {
                            City = x.First().Address.City,
                            Count = x.Count(),
                        }).OrderByDescending(x => x.Count).ToList();

                    transact.Commit();
                }
            }
            return cities;
        }

        /// <summary>
        /// Выбрать статистику ульев, количество пчел в которых превысило предел вместимости
        /// </summary>
        /// <returns></returns>
        public List<CrowdedBeehive> GetCrowdedBeehives()
        {
            List<CrowdedBeehive> beehives;
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    beehives = session.Query<Beehive>().Where(x => x.Bees.Count > x.Capacity).Select( x => 
                        new CrowdedBeehive
                        {
                            Name = x.Name,
                            Capacity = x.Capacity,
                            Bees = x.Bees.Count
                        }).OrderByDescending( x => x.Bees ).ToList();

                    transact.Commit();
                }
            }

            return beehives;
        }

        /// <summary>
        /// Выбрать процентное соотношение ролей пчел в каждом улье
        /// </summary>
        /// <returns></returns>
        public List<RolesStatistic> GetRolesStatistic()
        {
            List<RolesStatistic> rolesStatistics;
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    // Выполить весь запрос на уровне СУБД, не получилось,
                    // NH никак не хотел сгенерировать нужный синтаксис

                    // Получаем список пчел для каждого улея из СУБД
                    var beehives = session.Query<Beehive>()
                        .Select(beehive => new
                        {
                            beehive.Name,
                            Bees = beehive.Bees.ToList()
                        }).ToList();

                    rolesStatistics = (
                        from beehive in beehives
                        group beehive by beehive.Name
                        into bh
                        select new RolesStatistic
                        {
                            BeehiveName = bh.Key,
                            RolesInfo = (
                                from b in bh
                                from bee in b.Bees
                                group bee by new
                                {
                                    Name = bee.Role,
                                    BeesCount = b.Bees.Count
                                } into role
                                select new RoleInfo
                                {
                                    BeeRole = role.Key.Name,
                                    Percent = (role.Count() * 100 / role.Key.BeesCount )
                                }).ToList()
                        }).ToList();

                    transact.Commit();
                }
            }

            return rolesStatistics;
        }

        /// <summary>
        /// Выбрать подробную статистику трех ульев,
        /// пчелы которых принесли наибольшее количество меда
        /// </summary>
        /// <returns></returns>
        public List<BeehiveStatistic> GetTopBeehives()
        {
            List<BeehiveStatistic> beehiveStatistics;

            using (var session = _sessionFactory.OpenSession())
            {
                using (var transact = session.BeginTransaction())
                {
                    var result = session.Query<Beehive>().ToList();

                    // Не оптимизированная выборка,
                    // Пытался выполнить это в Query<T>, вложенныe методы Sum выдают ошибку
                    beehiveStatistics = (
                        from res in result
                        select new BeehiveStatistic
                        {
                            BeehiveName = res.Name,
                            Address = res.Address.ToString(),
                            Capacity = res.Capacity,
                            QueenIQ = (res.Queen != null) ? res.Queen.IQ : 0,
                            BeesCount = res.Bees.Count,
                            HoneySum = res.Bees.Sum(y => y.Plants.Sum(z => z.HoneyPerDusting))
                        }).OrderByDescending(x => x.HoneySum).Take(3).ToList();
                               

                    transact.Commit();
                }
            }
            return beehiveStatistics;
        }

        private static ISessionFactory BuildFactory()
        {
            var cfg = new Configuration();
            cfg.Configure("../../AppConfig/hibernate.cfg.xml");
            return cfg.BuildSessionFactory();
        }
    }
}