using System;
using System.Collections.Generic;
using System.Text;

namespace Gymnastic.Domain
{
    public class SampleProgram
    {
        public string Name { get; set; }

        public List<Exercise> Program { get; set; }

        /// <summary>
        /// Вставить тренажер
        /// </summary>
        /// <param name="firstTrainer"></param>
        /// <param name="secondTrainer"></param>
        /// <param name="newTrainer"></param>
        /// <param name="time"></param>
        public void InsertTrainer(string firstTrainer, string secondTrainer, Trainer newTrainer, int time)
        {
            if( newTrainer == null)
                throw new ArgumentNullException();

            if( time <= 0 )
                throw new ArgumentException();

            for (var i = 0; i < Program.Count - 1; i++)
            {
                if( Program[i].Trainer.Name == firstTrainer 
                    && Program[i + 1].Trainer.Name == secondTrainer)
                    Program.Insert(i + 1, new Exercise{ Time = time, Trainer = newTrainer});
            }
        }

        /// <summary>
        /// Получить последовательность упражнений в прямом или обратном порядке
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public string GetSequenceOfExercises(bool direction)
        {
            var list = Program.GetRange(0, Program.Count);

            if (direction == false)
                list.Reverse();

            var sb = new StringBuilder();
            list.ForEach( x => sb.AppendFormat("{0}, ", x.ToString()));

            return sb.ToString();
        }
    }
}