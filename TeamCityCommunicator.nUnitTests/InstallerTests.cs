using System;
using System.Linq;
using System.Web.Http;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;
using TeamCityCommunicator.Controllers;
using TeamCityCommunicator.Installers;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.nUnitTests
{
    [TestFixture]
    public class InstallerTests
    {
        private IWindsorContainer _containerWithControllers;
        private IWindsorContainer _containerWithServices;

        [SetUp]
        public void Arrange()
        {
            _containerWithControllers = new WindsorContainer().Install(new WebApiInstaller());
            _containerWithServices = new WindsorContainer().Install(new ServicesInstaller());
        }

        [Test]
        public void All_Api_Controllers_Implemtent_ApiController()
        {
            var allHandlers = GetAllHanders(_containerWithControllers);
            var allKnownTypes = GetHandlersFor(typeof(ApiController), _containerWithControllers);

            Assert.IsNotNull(allHandlers);
            Assert.AreEqual(allHandlers,allKnownTypes);
        }

        [Test]
        public void All_ApiControllers_Are_Registered()
        {
            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<ApiController>());
            var registeredControllers = GetImplementationTypesFor(typeof (ApiController), _containerWithControllers);

            Assert.AreEqual(allControllers,registeredControllers);
        }

        [Test]
        public void All_Services_Implement_ITeamCityBaseService()
        {
            var allServices = GetAllHanders(_containerWithServices);
            var registeredServices = GetHandlersFor(typeof(ITeamCityBaseService), _containerWithServices);

            Assert.AreEqual(allServices, registeredServices);
        }

        [Test]
        public void All_Serivces_Are_Registered_In_The_Container()
        {
            var allServices = GetPublicClassesFromApplicationAssembly(c => c.Is<ITeamCityBaseService>());
            var registeredServices = GetImplementationTypesFor(typeof(ITeamCityBaseService), _containerWithServices);

            Assert.AreEqual(allServices, registeredServices);
        }
        
        private IHandler[] GetAllHanders(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }

        private IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        private Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(UpdateBuildStatusController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
    }

    
}