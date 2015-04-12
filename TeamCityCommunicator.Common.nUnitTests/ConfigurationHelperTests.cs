using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TeamCityCommunicator.Common.Helpers;

namespace TeamCityCommunicator.Common.nUnitTests
{
    [TestFixture]
    public class ConfigurationHelperTests
    {
        private ConfigurationHelper _configurationHelper;

        [SetUp]
        public void Arrange()
        {
            _configurationHelper = new ConfigurationHelper();
        }

        [Test]
        public void When_I_Call_GetConfigurationValue_The_Key_Is_Returned_From_The_App_Config()
        {
            //Arrange
            ConfigurationManager.AppSettings["myValue"] = "Config Value";

            //Act
            var myValue = _configurationHelper.GetConfigurationValue("myValue");

            //Assert
            Assert.AreEqual("Config Value",myValue);
        }
    }
}
