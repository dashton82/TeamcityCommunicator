using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TeamCityCommunicator.Common.Helpers
{
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly IConfigurationHelper _configurationHelper;

        public EncryptionHelper(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public string Encrypt(string data)
        {
            var key = GetKey();

            using (var encryptionAlogrithm = new AesManaged {Key = key.GetBytes(16), IV = key.GetBytes(16)})
            {
                var dataBytes = Encoding.UTF8.GetBytes(data);

                using (var sourceStream = new MemoryStream(dataBytes))
                using (var destinationStream = new MemoryStream())
                using (var crypto = new CryptoStream(sourceStream,encryptionAlogrithm.CreateEncryptor(),CryptoStreamMode.Read))
                {
                    MoveBytes(crypto, destinationStream);
                    return Convert.ToBase64String(destinationStream.ToArray());
                }
            }
        }

        private Rfc2898DeriveBytes GetKey()
        {
            return new Rfc2898DeriveBytes(_configurationHelper.GetConfigurationValue("SecurityKey"), Encoding.Unicode.GetBytes("Random"));
        }

        public string Decrypt(string data)
        {
            
            var key = GetKey();

            using (var encryptionAlogrithm = new AesManaged { Key = key.GetBytes(16), IV = key.GetBytes(16) })
            {
                var dataBytes = Convert.FromBase64String(data);

                using (var sourceStream = new MemoryStream(dataBytes))
                using (var destinationStream = new MemoryStream())
                using (var crypto = new CryptoStream(sourceStream, encryptionAlogrithm.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    MoveBytes(crypto, destinationStream);
                    var decryptedBytes = destinationStream.ToArray();
                    return new UTF8Encoding().GetString(decryptedBytes);
                }
            }
        }

        private void MoveBytes(Stream source, Stream dest)
        {
            var bytes = new byte[2048];
            var count = source.Read(bytes, 0, bytes.Length);
            while (0 != count)
            {
                dest.Write(bytes, 0, count);
                count = source.Read(bytes, 0, bytes.Length);
            }
        }
    }
}
