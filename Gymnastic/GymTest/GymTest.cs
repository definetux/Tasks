using System;
using System.Collections.Generic;
using System.Linq;
using Gymnastic;
using Gymnastic.Domain;
using Moq;
using NUnit.Framework;

namespace GymTest
{
    [TestFixture]
    public class GymTest
    {
        private Gym _gym;

        private List<Exercise> _cardioProgram;

        private List<Exercise> _lightAthletic;

        private List<Exercise> _hardAthletic;

        /// <summary>
        /// Задаю стандартный набор тренажеров и программ
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var mock = new Mock<ITrainerRepository>();
            mock.Setup(x => x.GetTrainers())
                .Returns(new List<Trainer>
                {
                    new Trainer {Name = "Беговая дорожка"},
                    new Trainer {Name = "Велотренажер"},
                    new Trainer {Name = "Брусья"},
                    new Trainer {Name = "Степпер"},
                    new Trainer {Name = "Перекладина"},
                    new Trainer {Name = "Лавка Скотта"},
                    new Trainer {Name = "Скамья для пресса"},
                    new Trainer {Name = "Скамья для жима"},
                    new Trainer {Name = "Упор для отжимания"},
                    new Trainer {Name = "Тренажер Гаккеншмидта"}
                });

            _gym = new Gym(mock.Object);



            _cardioProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 30},
                new Exercise{ Trainer = new Trainer { Name = "Велотренажер"}, Time = 20},
                new Exercise{ Trainer = new Trainer { Name = "Степпер"}, Time = 10}
            };

            _lightAthletic = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Брусья"}, Time = 20},
                new Exercise{ Trainer = new Trainer { Name = "Перекладина"}, Time = 10},
                new Exercise{ Trainer = new Trainer { Name = "Упор для отжимания"}, Time = 20},
                new Exercise{ Trainer = new Trainer { Name = "Скамья для пресса" }, Time = 25 },
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка" }, Time = 40 },
            };

            _hardAthletic = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Лавка Скотта"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Скамья для жима"}, Time = 10},
                new Exercise{ Trainer = new Trainer { Name = "Тренажер Гаккешмидта"}, Time = 15}
            };
        }

        /// <summary>
        /// Проверка добавления типовой программы
        /// </summary>
        [Test]
        public void AddSampleProgramTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgramName = "Легкая атлетика";
            const string thirdProgramName = "Тяжелая атлетика";

            var badProgram1 = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Плохой тренажер"}, Time = 10},
                new Exercise{ Trainer = new Trainer { Name = "Плохой тренажер"}, Time = 20},
            };

            var badProgram2 = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Плохой тренажер"}, Time = -1},
                new Exercise{ Trainer = new Trainer { Name = "Плохой тренажер"}, Time = -2},
            };

            // act
            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgramName, _lightAthletic);
            _gym.AddSampleProgram(thirdProgramName, _hardAthletic);
            

            // assert
            Assert.Throws <ArgumentException>(() => _gym.AddSampleProgram("Плохая программа", badProgram1));
            Assert.Throws<ArgumentException>(() => _gym.AddSampleProgram("Плохая программа 2", badProgram2));
            Assert.Throws<ArgumentNullException>(() => _gym.AddSampleProgram("", null));

            Assert.NotNull(_gym.GetSamplePrograms());
            Assert.AreEqual(3, _gym.GetSamplePrograms().Count());

            Assert.AreEqual(firstProgramName, _gym.GetSamplePrograms().FirstOrDefault().Name);
            Assert.AreEqual(thirdProgramName, _gym.GetSamplePrograms().LastOrDefault().Name);

        }

        /// <summary>
        /// Проверка добавления клиентской программы
        /// </summary>
        [Test]
        public void AddClientProgramTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgramName = "Легкая атлетика";
            const string thirdProgramName = "Тяжелая атлетика";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgramName, _lightAthletic);
            _gym.AddSampleProgram(thirdProgramName, _hardAthletic);

            var firstProgramsList = new List<string> { firstProgramName, secondProgramName, thirdProgramName };

            var secondProgramsList = new List<string> { secondProgramName, thirdProgramName };

            var thirdPrgramList = new List<string> {"Другие упражнения"};

            // act
            _gym.AddClientProgram("Иван", firstProgramsList);

            _gym.AddClientProgram("Игорь", secondProgramsList);

            // assert
            Assert.Throws<ArgumentException>(() => _gym.AddClientProgram("Олег", thirdPrgramList), "Такой программы не существует");
            Assert.Throws<ArgumentNullException>(() => _gym.AddClientProgram("Олег", null));

            Assert.NotNull(_gym.GetClientPrograms());
            Assert.AreEqual(2, _gym.GetClientPrograms().Count());
            Assert.AreEqual("Иван", _gym.GetClientPrograms().FirstOrDefault().Name);
            Assert.AreEqual(3, _gym.GetClientPrograms().FirstOrDefault().Programs.Count());
        }

        /// <summary>
        /// Проверка изменения длительность на тренажере
        /// </summary>
        [Test]
        public void EditClientTrainersTimeTest()
        {
            // arrange
            const string firstProgramName = "Кардио";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);

            var firstProgramsList = new List<string> { firstProgramName };

            _gym.AddClientProgram("Иван", firstProgramsList);

            // act

            _gym.EditClientTrainersTime("Иван", firstProgramName, "Беговая дорожка", 20);

            // assert
            Assert.Throws<ArgumentException>(() =>
                _gym.EditClientTrainersTime("Иван", firstProgramName, "Беговая дорожка", -1), "Длительность меньше нуля");

            Assert.Throws<ArgumentException>(() =>
                _gym.EditClientTrainersTime("Олег", firstProgramName, "Беговая дорожка", 10), "Клиента не существует");

            Assert.Throws<ArgumentException>(() =>
                _gym.EditClientTrainersTime("Иван", firstProgramName, "Скакалка", 10), "Тренажера не существует");

            Assert.Throws<ArgumentException>(() =>
                _gym.EditClientTrainersTime("Иван", "Bad program", "Беговая дорожка", 10), "Программы не существует");

            var trainer = _cardioProgram.FirstOrDefault(x => x.Trainer.Name == "Беговая дорожка");

            var clientTraining = _gym.GetClientPrograms()
                .FirstOrDefault()
                .Programs
                .FirstOrDefault()
                .Program.SingleOrDefault(p => p.Trainer == trainer.Trainer);

            var sampleTraining = _gym.GetSamplePrograms()
                .FirstOrDefault()
                .Program
                .SingleOrDefault(p => p.Trainer == trainer.Trainer);

            Assert.AreNotEqual(clientTraining.Time, sampleTraining.Time);

            Assert.AreEqual(20, clientTraining.Time);
            Assert.AreEqual(30, sampleTraining.Time);

        }

        /// <summary>
        /// Проверка удаления тренажера из программ
        /// </summary>
        [Test]
        public void DeleteTrainerTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgram = "Легкая атлетика";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgram, _lightAthletic);

            var firstProgramsList = new List<string> { firstProgramName, secondProgram };

            _gym.AddClientProgram("Иван", firstProgramsList);
            _gym.AddClientProgram("Олег", firstProgramsList);

            var trainer = _gym.GetTrainers().FirstOrDefault(x => x.Name == "Беговая дорожка");

            // act
            _gym.DeleteTrainer(trainer);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.DeleteTrainer(null));

            // Проверяем остались ли старые значения
            var sampleProgramTrainers = _gym.GetSamplePrograms()
                .Select( sProgram => sProgram.Program.Where( ex => ex.Trainer.Name == trainer.Name ));
            var clientProgramTrainers = _gym.GetClientPrograms()
                .Select(
                    cPrograms =>
                        cPrograms.Programs.Select(
                            sPrograms => sPrograms.Program.Where(ex => ex.Trainer.Name == trainer.Name)));

            Assert.That(sampleProgramTrainers.All(sp => !sp.Any()), "Тренажер остался в типовых упражнениях");
            Assert.That(clientProgramTrainers.All(cProgram => cProgram.All( sp => !sp.Any())), "Тренажер остался в клиентских упражнениях");
        }

        /// <summary>
        /// Проверка изменения тренажера во всех программах
        /// </summary>
        [Test]
        public void EditTrainerTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgram = "Легкая атлетика";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgram, _lightAthletic);

            var firstProgramsList = new List<string> { firstProgramName, secondProgram };

            _gym.AddClientProgram("Иван", firstProgramsList);
            _gym.AddClientProgram("Олег", firstProgramsList);

            var oldTrainer = _gym.GetTrainers().FirstOrDefault(x => x.Name == "Беговая дорожка");

            var newTrainer = new Trainer { Name = "Скакалка" };
            const int newTime = 40;

            // act
            _gym.EditTrainer(oldTrainer, newTrainer, newTime);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.EditTrainer(null, null, 10));
            Assert.Throws<ArgumentException>(() => _gym.EditTrainer(oldTrainer, newTrainer, -1));

            // Проверяем осталось ли старое значение
            var oldSampleProgramTrainers = _gym.GetSamplePrograms()
                .Select(sProgram => sProgram.Program.Where(ex => ex.Trainer.Name == oldTrainer.Name));
            var oldClientProgramTrainers = _gym.GetClientPrograms()
                .Select(
                    cPrograms =>
                        cPrograms.Programs.Select(
                            sPrograms => sPrograms.Program.Where(ex => ex.Trainer.Name == oldTrainer.Name)));

            Assert.That(oldSampleProgramTrainers.All(sp => !sp.Any()), "Тренажер остался в типовых упражнениях");
            Assert.That(oldClientProgramTrainers.All(cProgram => cProgram.All(sp => !sp.Any())), "Тренажер остался в клиентских упражнениях");

            // Проверяем появилось ли новое значение
            var newSampleProgramTrainers = _gym.GetSamplePrograms()
                .Select(sProgram => sProgram.Program.Where(ex => ex.Trainer.Name == newTrainer.Name));
            var newClientProgramTrainers = _gym.GetClientPrograms()
                .Select(
                    cPrograms =>
                        cPrograms.Programs.Select(
                            sPrograms => sPrograms.Program.Where(ex => ex.Trainer.Name == newTrainer.Name)));

            Assert.That(newSampleProgramTrainers.All(sp => sp.Any()), "Тренажер остался в типовых упражнениях");
            Assert.That(newClientProgramTrainers.All(cProgram => cProgram.All(sp => sp.Any())), "Тренажер остался в клиентских упражнениях");
        }

        /// <summary>
        /// Проверка вставки тренажера в конкретную программу
        /// </summary>
        [Test]
        public void InsertTrainerToSampleTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            _gym.AddSampleProgram(firstProgramName, _cardioProgram);

            var trainer = new Trainer { Name = "Скакалка" };
            const int time = 20;
            var sampleProgram = _gym.GetSamplePrograms().FirstOrDefault();
            var badTrainer = _gym.GetTrainers().FirstOrDefault(x => x.Name == "Перекладина");

            // act
            sampleProgram.InsertTrainer("Беговая дорожка", "Велотренажер", trainer, time);

            sampleProgram.InsertTrainer("Беговая дорожка", "Степпер", badTrainer, 20 );

            // assert
            Assert.Throws<ArgumentNullException>(
                () => sampleProgram.InsertTrainer("Беговая дорожка", trainer.Name, null, time));
            Assert.Throws<ArgumentException>(
                () => sampleProgram.InsertTrainer("Беговая дорожка", trainer.Name, trainer, -1));

            var newTrainer = sampleProgram.Program.ElementAt(1);
            Assert.AreSame(newTrainer.Trainer, trainer);

            var bTrainer = sampleProgram.Program.FirstOrDefault(x => x.Trainer.Name == badTrainer.Name);

            Assert.Null(bTrainer, "Неверная вставка");
        }

        /// <summary>
        /// Проверка кол-ва типовых программ, в которые входит тренажер
        /// </summary>
        [Test]
        public void CheckCountOfSampleProgramForTrainer()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgram = "Легкая атлетика";
            const string testProgram = "Тестовая программа";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgram, _lightAthletic);

            var newProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 20},
            };

            _gym.AddSampleProgram(testProgram, newProgram);

            var trainer = new Trainer {Name = "Беговая дорожка"};

            // act

            int count = _gym.GetSampleProgramsCount(trainer);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.GetSampleProgramsCount(null));

            Assert.AreEqual(4, count);
        }

        /// <summary>
        /// Проверка кол-ва клиентских программ, в которые входит тренажер
        /// </summary>
        [Test]
        public void CheckCountOfClientProgramForTrainer()
        {
            // arrange
            const string testProgram = "Тестовая программа";

            var newProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 20},
            };

            _gym.AddSampleProgram(testProgram, newProgram);


            _gym.AddClientProgram("Иван", new List<string>{ testProgram });

            var trainer = new Trainer { Name = "Беговая дорожка" };

            // act
            int count = _gym.GetClientProgramsCount(trainer);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.GetClientProgramsCount(null));

            Assert.AreEqual(2, count);
        }

        /// <summary>
        /// Проверка суммарного времени на тренажере в типовых программах
        /// </summary>
        [Test]
        public void CheckSumOfSampleProgramsTime()
        {
            // arrange
            const string testProgram = "Тестовая программа";

            var newProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 20},
            };

            _gym.AddSampleProgram(testProgram, newProgram);

            var trainer = new Trainer {Name = "Беговая дорожка"};

            // act
            int sum = _gym.GetSampleProgramSumTime(trainer);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.GetSampleProgramSumTime(null));
            Assert.AreEqual(35, sum);
        }

        /// <summary>
        /// Проверка суммарного времени на тренажере в клиентских программах
        /// </summary>
        [Test]
        public void CheckSumOfClientProgramsTime()
        {
            // arrange
            const string testProgram = "Тестовая программа";

            var newProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 20},
            };

            _gym.AddSampleProgram(testProgram, newProgram);
            _gym.AddClientProgram("Иван", new List<string> { testProgram });

            var trainer = new Trainer { Name = "Беговая дорожка" };

            // act
            int sum = _gym.GetClientProgramsSumTime(trainer);

            // assert
            Assert.Throws<ArgumentNullException>(() => _gym.GetClientProgramsSumTime(null));
            Assert.AreEqual(35, sum);
        }

        /// <summary>
        /// Проверка клиентов, которые занимаются дольше всего
        /// </summary>
        [Test]
        public void CheckClientWithMaxTime()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgram = "Легкая атлетика";
            const string thirdProgram = "Тяжелая атлетика";
            const string testProgram = "Тестовая программа";

            var newProgram = new List<Exercise>
            {
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 15},
                new Exercise{ Trainer = new Trainer { Name = "Беговая дорожка"}, Time = 20},
            };

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgram, _lightAthletic);
            _gym.AddSampleProgram(thirdProgram, _hardAthletic);
            _gym.AddSampleProgram(testProgram, newProgram);

            // Длительность _cardio = 60
            // Длительность _lightAthletic = 105
            // Длительность _hardAthletic = 40
            // Длительность newProgram = 35

            _gym.AddClientProgram("Иван", new List<string> { firstProgramName }); // 60
            _gym.AddClientProgram("Олег", new List<string> { firstProgramName, secondProgram }); // 175
            _gym.AddClientProgram("Игорь", new List<string> { firstProgramName, thirdProgram }); // 100
            _gym.AddClientProgram("Алексей", new List<string> { secondProgram }); // 115
            _gym.AddClientProgram("Петр", new List<string> { firstProgramName, testProgram }); // 95
            _gym.AddClientProgram("Николай", new List<string> { thirdProgram, testProgram }); // 75

            // act
            var clients = _gym.GetClientWithMaxTime();

            // assert
            Assert.AreEqual(3, clients.Count());
            Assert.AreEqual("Олег", clients.First());
            Assert.AreEqual("Игорь", clients.Last());
        }

        /// <summary>
        /// Проверка последовательность треажер - длительность
        /// </summary>
        [Test]
        public void GetSequenceExercisesTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            var sampleProgram = _gym.GetSamplePrograms().FirstOrDefault(x => x.Name == firstProgramName);

            // act
            var sequence = sampleProgram.GetSequenceOfExercises(true);
            var reverseSequence = sampleProgram.GetSequenceOfExercises(false);

            // assert
            Assert.AreEqual("Беговая дорожка - 30, Велотренажер - 20, Степпер - 10, ", sequence);
            Assert.AreEqual("Степпер - 10, Велотренажер - 20, Беговая дорожка - 30, ", reverseSequence);
        }

        /// <summary>
        /// Проверка неизменяемой программы
        /// </summary>
        [Test]
        public void GetUnchangedSampleProgramTest()
        {
            // arrange
            const string firstProgramName = "Кардио";
            const string secondProgram = "Легкая атлетика";

            _gym.AddSampleProgram(firstProgramName, _cardioProgram);
            _gym.AddSampleProgram(secondProgram, _lightAthletic);

            _gym.AddClientProgram("Иван", new List<string> { firstProgramName, secondProgram }); // 60
            _gym.AddClientProgram("Олег", new List<string> { firstProgramName, secondProgram }); // 175
            _gym.AddClientProgram("Игорь", new List<string> { firstProgramName, secondProgram }); // 100

            _gym.EditClientTrainersTime("Иван", "Кардио", "Беговая дорожка", 10);
            _gym.EditClientTrainersTime("Олег", "Кардио", "Беговая дорожка", 10);
            _gym.EditClientTrainersTime("Игорь", "Легкая атлетика", "Брусья", 30);

            // act
            var sampleProgram = _gym.GetUnchagedProgram();
            // assert
            Assert.AreEqual("Легкая атлетика", sampleProgram.Name);
        }
    }
}
