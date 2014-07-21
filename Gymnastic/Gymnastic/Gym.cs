using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Gymnastic.Domain;

namespace Gymnastic
{
    public class Gym
    {
        // Список тренажеров
        private readonly List<Trainer> _trainers;

        // Список типовых программ
        private readonly List<SampleProgram> _samplePrograms;

        // Список клиентских программ
        private readonly List<ClientPrograms> _clientPrograms;

        public Gym(ITrainerRepository repository)
        {
            _trainers = repository.GetTrainers();

            _samplePrograms = new List<SampleProgram>();
            _clientPrograms = new List<ClientPrograms>();
        }

        /// <summary>
        ///  Добавить типовую программу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="program"></param>
        public void AddSampleProgram(string name, List<Exercise> program)
        {
            if( program == null)
                throw new ArgumentNullException();

            if( program.Any(x => x.Time <= 0) )
                throw new ArgumentException(message: "Длительность меньше или равна 0");

            // Проверяем есть ли в зале нужные тренажеры
            var res = from t in _trainers
                    from p in program
                    where t.Name == p.Trainer.Name
                    select t;

            if( !res.Any())
                throw new ArgumentException();

            _samplePrograms.Add(new SampleProgram {Name = name, Program = program});
        }

        /// <summary>
        /// Добавляем клиентскую программу
        /// </summary>
        /// <param name="name"> Имя клиента </param>
        /// <param name="programsName"> Имена типовых программ </param>
        public void AddClientProgram(string name, List<string> programsName)
        {
            if( programsName == null)
                throw new ArgumentNullException();

            // Проверяем есть ли нужные типовые программы
            var programs = _samplePrograms.Where(x => programsName.Contains(x.Name)).ToList();

            if( !programs.Any())
                throw new ArgumentException();

            var clientPrograms = new ClientPrograms
            {
                Name = name, Programs = new List<SampleProgram>()
            };

            // Создаем список программ клиента 
            programs.ForEach(program =>
            {
                var spProgram = new SampleProgram
                {
                    Name = program.Name,
                    Program = new List<Exercise>()
                };
                program.Program.ForEach(trainer
                    => spProgram.Program.Add(new Exercise
                    {
                        Time = trainer.Time,
                        Trainer = trainer.Trainer
                    }));
                clientPrograms.Programs.Add(spProgram);
            });          

            _clientPrograms.Add(clientPrograms);
        }

        /// <summary>
        /// Редактирует программу клиента - изменяет длительность
        /// </summary>
        /// <param name="clientName"> Имя клиента </param>
        /// <param name="programName"> Имя программы </param>
        /// <param name="trainerName"> Тренажер </param>
        /// <param name="time"> Длительность </param>
        public void EditClientTrainersTime(string clientName, string programName, string trainerName, int time)
        {
            if (time <= 0)
                throw new ArgumentException();

            // Проверяем существование клиента
            var client = _clientPrograms.Where(x => x.Name == clientName).ToList();

            if (!client.Any())
                throw new ArgumentException();

            // Проверяем существование программы
            var clientProgram = client
                .Select(x => x.Programs
                    .Where(y => y.Name == programName))
                .FirstOrDefault();

            if (!clientProgram.Any())
                throw new ArgumentException();

            // Проверяем существование тренажера
            var clientsTrainer = clientProgram.Select(s => s.Program.Where(e => e.Trainer.Name == trainerName).ToList()).ToList();

            if (clientsTrainer.All(c => !c.Any()))
                throw new ArgumentException();

            // Меняем длительность
            clientsTrainer.ForEach( exercise => exercise.ToList().ForEach( e => e.Time = time ));
        }

        /// <summary>
        /// Удалить тренажер
        /// </summary>
        /// <param name="trainer"></param>
        public void DeleteTrainer(Trainer trainer)
        {
            if (trainer == null)
                throw new ArgumentNullException();

            // Удялаем тренажер из типовых упражнений
            _samplePrograms.ForEach( s => s.Program.ForEach(t =>
            {
                if (t.Trainer.Name == trainer.Name)
                    s.Program.Remove(t);
            }));

            // Удаляем тренажер из клиентских упражнений
            _clientPrograms.ForEach( cProgram => cProgram.Programs
                .ForEach( sProgram => sProgram.Program
                    .ForEach(ex =>
                    {
                        if (ex.Trainer.Name == trainer.Name)
                            sProgram.Program.Remove(ex);
                    })));
        }

        /// <summary>
        /// Изменить тренажер
        /// </summary>
        /// <param name="oldTrainer"></param>
        /// <param name="newTrainer"></param>
        /// <param name="time"></param>
        public void EditTrainer(Trainer oldTrainer, Trainer newTrainer, int time)
        {
            if (oldTrainer == null || newTrainer == null)
                throw new ArgumentNullException();
            if( time <= 0)
                throw new ArgumentException();

            // Обновляем тренажер в типовых упражнений
            _samplePrograms.ForEach(s => s.Program.ForEach(t =>
            {
                if (t.Trainer.Name == oldTrainer.Name)
                {
                    t.Trainer.Name = newTrainer.Name;
                    t.Time = time;
                }
            }));

            // Обновляем тренажер в клиентских упражнениях
            _clientPrograms.ForEach(cProgram => cProgram.Programs
                .ForEach(sProgram => sProgram.Program
                    .ForEach(ex =>
                    {
                        if (ex.Trainer.Name == oldTrainer.Name)
                        {
                            ex.Trainer.Name = newTrainer.Name;
                            ex.Time = time;
                        }
                    })));
        }

        /// <summary>
        /// Получить количество типовых программ, в которые входит тренажер
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
        public int GetSampleProgramsCount(Trainer trainer)
        {
            if( trainer == null )
                throw new ArgumentNullException();

            return (from sp in _samplePrograms.Select(p => p.Program)
                from prog in sp
                where prog.Trainer.Name == trainer.Name
                select prog).Count();
        }

        /// <summary>
        /// Получить количество клиентских программ, в которые входит тренажер
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
        public int GetClientProgramsCount(Trainer trainer)
        {
            if (trainer == null)
                throw new ArgumentNullException();

            return (from clientProg in _clientPrograms.Select(p => p.Programs)
                    from sampleProg in clientProg.Select( sp => sp.Program)
                    from prog in sampleProg
                    where prog.Trainer.Name == trainer.Name
                    select prog).Count();
        }

        /// <summary>
        /// Получить суммарное время занятий на тренажере в типовых программах
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
        public int GetSampleProgramSumTime(Trainer trainer)
        {
            if( trainer == null )
                throw new ArgumentNullException();

            return (from sp in _samplePrograms.Select(p => p.Program)
                    from prog in sp
                    where prog.Trainer.Name == trainer.Name
                    select prog.Time).Sum();
        }

        /// <summary>
        /// Получить суммарное время занятий на тренажере в клиентских программах
        /// </summary>
        /// <param name="trainer"></param>
        /// <returns></returns>
        public int GetClientProgramsSumTime(Trainer trainer)
        {
            if (trainer == null)
                throw new ArgumentNullException();

            return (from clientProg in _clientPrograms.Select(p => p.Programs)
                    from sampleProg in clientProg.Select(sp => sp.Program)
                    from prog in sampleProg
                    where prog.Trainer.Name == trainer.Name
                    select prog.Time).Sum();
        }

        /// <summary>
        /// Получить трех клиентов, которые занимаются дольше всего
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetClientWithMaxTime()
        {
            return (from cPrograms in _clientPrograms
                    select new
                    {
                        Name = cPrograms.Name,
                        Time = (from spProgram in cPrograms.Programs
                                from exercise in spProgram.Program
                                select exercise.Time).Sum()
                    }).OrderByDescending( x => x.Time).Select( x => x.Name).Take(3);
        }

        /// <summary>
        /// Выбрать типовую программу, которая входит в большее кол-во клиентских программ
        /// </summary>
        /// <returns></returns>
        public SampleProgram GetUnchagedProgram()
        {
            // В этом методе возникли трудности с LINQ, поэтому написал нативно.

            // Выбираем клиентские программы
            var clientPrograms = (from clientProgram in _clientPrograms
                          from scProgram in clientProgram.Programs
                          select scProgram).ToList();

            SampleProgram unchagedProgram = null;

            int max = 0;

            // Для каждой типовой программы проверяем соответствие с клиентскими,
            // если программа полностью совпадает с клиентской, увеличиваем ее счетчик, 
            // который отвечает за неизменяемость, если значение текущего счетчика больше 
            // максимального, то сохраняем ссылку на типовую программу 
            foreach (var sampleProgram in _samplePrograms)
            {
                int unchagedProgramCount = 0;
                foreach (var clientProgram in clientPrograms)
                {
                    int unchagedExercise = 0;
                    for (int i = 0; i < sampleProgram.Program.Count; i++)
                    {
                        var sampleExercise = sampleProgram.Program[i];
                        var clientExercise = clientProgram.Program[i];

                        if (!(sampleExercise.Time == clientExercise.Time
                            && sampleExercise.Trainer.Name == clientExercise.Trainer.Name))
                            break;
                        unchagedExercise++;
                    }
                    if (unchagedExercise == sampleProgram.Program.Count)
                        unchagedProgramCount++;
                }
                if (unchagedProgramCount > max)
                {
                    max = unchagedProgramCount;
                    unchagedProgram = sampleProgram;
                }
            }
            return unchagedProgram;
        }

        public IEnumerable<Trainer> GetTrainers()
        {
            return _trainers;
        }

        public IEnumerable<SampleProgram> GetSamplePrograms()
        {
            return _samplePrograms;
        }

        public IEnumerable<ClientPrograms> GetClientPrograms()
        {
            return _clientPrograms;
        }
    }
}