using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TeamCityCommunicator.Installers
{
    public class HelpersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes
                .FromThisAssembly()
                .InNamespace("TeamCityCommunicator.Common.Helpers")
                .LifestyleSingleton());
        }
    }
}