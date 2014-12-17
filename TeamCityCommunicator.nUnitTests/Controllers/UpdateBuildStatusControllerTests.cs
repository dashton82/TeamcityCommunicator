using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Mvc;
using NSubstitute;
using NUnit.Framework;
using TeamCityCommunicator.Controllers;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.nUnitTests.Controllers
{
    [TestFixture]
    public class UpdateBuildStatusControllerTests
    {
        private UpdateBuildStatusController _updateBuildStatusController;
        private ITeamCityService _teamCityService;
        private ITestEnvironmentService _testEnvironmentService;
        private string _sucessBuildId;
        private IBuildStatusDataStorageService _buildStatusDataStorageService;

        [SetUp]
        public void Arrange()
        {
            _sucessBuildId = "827572";
            _teamCityService = Substitute.For<ITeamCityService>();
            _teamCityService.GetBuildStatus(_sucessBuildId).Returns("SUCCESS");
            _testEnvironmentService = Substitute.For<ITestEnvironmentService>();
            _buildStatusDataStorageService = Substitute.For<IBuildStatusDataStorageService>();
            _updateBuildStatusController = new UpdateBuildStatusController(_teamCityService,_testEnvironmentService,_buildStatusDataStorageService);
            SetupController(HttpMethod.Post);
        }

        [Test]
        public void When_I_Pass_A_Valid_Id_The_TeamcityService_Is_Called()
        {
            //Act
            _updateBuildStatusController.Post(_sucessBuildId);

            //Assert
            _teamCityService.Received(1).GetBuildStatus(_sucessBuildId);
        }

        [Test]
        public void When_I_Pass_A_Valid_Id_The_TestEnvironmentService_Is_Called()
        {
            //Act
            _updateBuildStatusController.Post(_sucessBuildId);

            //Assert
            _testEnvironmentService.Received(1).GetBuildVersion("135");
        }

        [Test]
        public void When_I_Pass_A_Valid_Id_The_BuildStatusDataStorageService_Is_Called()
        {
            //Arrange
            _testEnvironmentService.GetBuildVersion(Arg.Any<string>()).Returns("96.0.0.0");

            //Act
            _updateBuildStatusController.Post(_sucessBuildId);

            //Assert
            _buildStatusDataStorageService.Received(1).StoreSanityBuildInformation("96.0.0.0","SUCCESS");
        }

        [Test]
        public void When_I_Sucessfully_Call_SanityFinished_I_Get_A_201_Response_Back()
        {
            //Act
            var response = _updateBuildStatusController.Post(_sucessBuildId);
            
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void When_there_is_an_exception_a_400_Response_Is_Sent_Back()
        {
            //Arrange
            _teamCityService.GetBuildStatus(Arg.Any<string>()).Returns(x => { throw new Exception(); });

            //Act
            var response = _updateBuildStatusController.Post(_sucessBuildId);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private void SetupController(HttpMethod method)
        {
            _updateBuildStatusController.Configuration = new HttpConfiguration();
            var route = _updateBuildStatusController
                        .Configuration
                        .Routes
                        .MapHttpRoute(
                            "Default",
                            "{controller}/{action}/{id}",
                            new
                            {
                                controller = "Home", 
                                action = "Index", 
                                id = UrlParameter.Optional
                            });
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary
            {
                {"id", _sucessBuildId},
                {"controller", "UpdateBuildStatusController"}
            });

            _updateBuildStatusController.Request = new HttpRequestMessage(method, string.Format("http://localhost:56330/api/UpdateBuildStatus/{0}", _sucessBuildId));
            _updateBuildStatusController.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,_updateBuildStatusController.Configuration);
            _updateBuildStatusController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

        }
    }
}