using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using NSubstitute;
using NUnit.Framework;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.nUnitTests.Services
{
    [TestFixture]
    public class TeamCityServiceTests
    {
        private TeamCityService _teamCityService;

        [SetUp]
        public void Arrange()
        {

            _teamCityService = Substitute.For<TeamCityService>();
            _teamCityService.LoadXmlFromUrl(Arg.Any<string>()).ReturnsForAnyArgs(new XmlDocument());

        }

        [Test]
        public void When_I_Call_GetBuildStatus_And_There_Is_No_Data_The_Inconclusive_Status_Is_Returned()
        {
            //Act
            var status = _teamCityService.GetBuildStatus("123");

            //Assert
            Assert.AreEqual("INCONCLUSIVE",status);
        }

        [Test]
        public void When_I_Call_GetBuildStatus_And_There_Is_Data_The_Status_Will_Be_CorectlyRead()
        {
            var testDocument = new XmlDocument();
            string xmlStatus = "SUCCESS";
            testDocument.LoadXml(string.Format("<body status='{0}'></body>", xmlStatus));
            _teamCityService.LoadXmlFromUrl(Arg.Any<string>()).ReturnsForAnyArgs(testDocument);

            //Act
            var status = _teamCityService.GetBuildStatus("123");

            //Assert
            Assert.AreEqual(xmlStatus, status);
        }
    }
}