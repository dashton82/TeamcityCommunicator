namespace TeamCityCommunicator.Services
{
    public interface ITestEnvironmentService : ITeamCityBaseService
    {
        string GetBuildVersion(string environmentNumber);
    }
}