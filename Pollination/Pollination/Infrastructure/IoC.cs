using Ninject.Modules;
using Pollination.Domain.Concrete;
using Pollination.Domain.Interfaces;

namespace Pollination.Infrastructure
{
    public class IoC : NinjectModule
    {
        public override void Load()
        {
            Bind<IPollinationRepository>().To<PollinationRepository>();
        }
    }
}