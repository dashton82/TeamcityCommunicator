namespace TeamCityCommunicator.Services
{
    public interface ITeamCityService : ITeamCityBaseService
    {
        string GetBuildStatus(string buildId);
    }
}