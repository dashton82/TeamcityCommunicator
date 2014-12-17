using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TeamCityCommunicator.Installers
{
    public class ServicesInstaller :IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Classes
                .FromThisAssembly()
                .InNamespace("TeamCityCommunicator.Services")
                .LifestylePerWebRequest());

        }
    }
}