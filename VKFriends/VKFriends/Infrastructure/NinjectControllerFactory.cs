using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using VKFriends.Models.Concrete;
using VKFriends.Models.Interfaces;

namespace VKFriends.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;
        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)_ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {

            _ninjectKernel.Bind<IDataContext>().To<VkFriendsDataContext>();
            _ninjectKernel.Bind<IRepository>().To<Repository>();
        }
    } 
}