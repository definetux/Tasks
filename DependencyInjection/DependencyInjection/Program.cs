using System;
using DependencyInjection.Infrastructure;
using DependencyInjection.Interfaces;
using Ninject;

namespace DependencyInjection
{
    class Program
    {
        static void Main()
        {
            // Создаем ядро DI, используя пользовательский модуль 
            var kernel = new StandardKernel(new MyNinjectModule());

            // Получаем инстанс движка калькулятора на основе связываний
            var calculatorEngine = kernel.Get<ICalculationEngine>();

            Console.WriteLine( "Enter number: ");

            try
            {
                // Устанавливаем начальное значение для калькулятора
                calculatorEngine.Set(Double.Parse(Console.ReadLine()));

                // Выполняем базовые операции
                calculatorEngine.Add(5);
                calculatorEngine.Sub(11);
                calculatorEngine.Multiply(3);
                calculatorEngine.Divide(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message); 
            }

            Console.ReadLine();
        }
    }
}
