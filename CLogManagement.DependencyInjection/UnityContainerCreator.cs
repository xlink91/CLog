using CLogManagement.Abstractions.Resolvers;
using CLogManagement.Configuration;
using CLogManagement.Resolvers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Unity;

namespace CLogManagement.DependencyInjection
{
    public static class UnityContainerCreator
    {
        public static IUnityContainer Create()
        {
            return new UnityContainer()
                    .RegisterInstance(new CfgInformation(
                        ConfigurationManager.AppSettings["LiteDbPath"]))
                    .RegisterType<ILogResolver, LogResolver>()
                    .AddExtension(new Diagnostic());
           
        }
    }
}