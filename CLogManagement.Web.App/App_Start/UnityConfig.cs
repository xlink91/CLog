using CLogManagement.DependencyInjection;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CLogManagement.Web.App
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = UnityContainerCreator
                .Create();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}