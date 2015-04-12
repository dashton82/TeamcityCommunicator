using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TeamCityCommunicator.Common.Helpers;

namespace TeamCityCommunicator.Common.nUnitTests
{
    [TestFixture]
    public class EncryptionHelperTests
    {
        private EncryptionHelper _encryptionHelper;
        private IConfigurationHelper _configurationHelper;

        [SetUp]
        public void Arrange()
        {
            _configurationHelper = Substitute.For<IConfigurationHelper>();
            _encryptionHelper = new EncryptionHelper(_configurationHelper);
        }

        [Test]
        public void When_I_Call_Encrypt_My_Data_Is_Encryped_Using_AES()
        {
            //Arrange
            var data = "My Data to Encrypt";

            //Act
            var returnedData = _encryptionHelper.Encrypt(data);

            //Assert
            Assert.AreNotEqual(data,returnedData);
        }

        [Test]
        public void When_I_Call_Decrypt_My_Data_Is_Decrypted_Using_AES()
        {
            //Arrange
            var data = "My Data to Encrypt";

            //Act
            var returnedData = _encryptionHelper.Encrypt(data);
            var unEncrypted = _encryptionHelper.Decrypt(returnedData);

            //Assert
            Assert.AreEqual(data, unEncrypted);
        }

        [Test]
        public void When_I_Encrypt_Data_The_Password_Is_Retrieved_From_Config_Entry()
        {
            //Arrange
            var data = "Some other data";
            

            var returnedData = _encryptionHelper.Encrypt(data);

            var someValue = _configurationHelper.Received(1).GetConfigurationValue("SecurityKey");
        }
    }
}
