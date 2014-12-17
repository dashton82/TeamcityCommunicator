using NSubstitute;
using NUnit.Framework;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.nUnitTests.Services
{
    [TestFixture]
    public class TestEnvironmentServiceTests
    {
        private TestEnvironmentService _testEnvironmentService;

        [SetUp]
        public void Arrange()
        {
            _testEnvironmentService = Substitute.For<TestEnvironmentService>();
            _testEnvironmentService.GetBuildInfoTextFromUrl(Arg.Any<string>()).ReturnsForAnyArgs("");
        }

        [Test]
        public void When_I_Call_GetBuildVersion_The_Build_Version_Is_Not_Returned_When_NotAvailable()
        {
            //Act
            var buildNumber = _testEnvironmentService.GetBuildVersion("123");

            //Assert
            Assert.AreEqual(string.Empty,buildNumber);

        }

        [Test]
        public void When_I_Call_GetBuildVersion_The_Build_Version_Is_Returned_When_Available()
        {
            //Arange
            const string versionNumber = "96.0.0.0";
            var buildInfoText = string.Format("Some Text Version Is {0}", versionNumber);
            _testEnvironmentService.GetBuildInfoTextFromUrl(Arg.Any<string>()).ReturnsForAnyArgs(buildInfoText);

            //Act
            var buildNumber = _testEnvironmentService.GetBuildVersion("123");

            //Assert
            Assert.AreEqual(versionNumber, buildNumber);

        }
    }
}