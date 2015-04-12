namespace TeamCityCommunicator.Common.Helpers
{
    public interface IConfigurationHelper : ITeamCityBaseHelper
    {
        string GetConfigurationValue(string key);
    }
}