using DependencyInjection.Concrete;
using DependencyInjection.Interfaces;
using Ninject.Modules;

namespace DependencyInjection.Infrastructure
{
    public class MyNinjectModule:NinjectModule
    {
        public override void Load()
        {
            // Связывание выполняется сверху вниз, поэтому располагаем бинды по порядку
            Bind<ILogger>().To<Logger>();
            Bind<ICalculator>().To<Calculator>();
            Bind<ICalculationEngine>().To<CalculationEngine>();
        }
    }
}