namespace TeamCityCommunicator.Common.Helpers
{
    public interface IEncryptionHelper : ITeamCityBaseHelper
    {
        string Encrypt(string data);

        string Decrypt(string data);

    }
}