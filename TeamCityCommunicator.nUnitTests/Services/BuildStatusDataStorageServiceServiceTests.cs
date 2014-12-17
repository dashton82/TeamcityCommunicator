using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using TeamCityCommunicator.Services;

namespace TeamCityCommunicator.nUnitTests.Services
{
    [TestFixture]
    public class BuildStatusDataStorageServiceServiceTests
    {
        private BuildStatusDataStorageService _buildStatusDataStorageService;

        [SetUp]
        public void Arrange()
        {
            _buildStatusDataStorageService = new BuildStatusDataStorageService();
        }

        [Test]
        public void When_I_Call_StoreData_With_A_BuildNumber_And_Status_True_Is_Returned()
        {
            var status = "SUCCESS";
            string buildNumber = "96.0.0.0";

            var result = _buildStatusDataStorageService.StoreSanityBuildInformation(buildNumber,status);

            Assert.IsTrue(result);
        }

        [Test]
        public void When_I_Call_StoreData_Without_BuildNumber_And_Status_false_Is_Returned()
        {
            var status = string.Empty;
            string buildNumber = string.Empty;

            var result = _buildStatusDataStorageService.StoreSanityBuildInformation(buildNumber, status);

            Assert.IsFalse(result);
        }
    }
}