namespace TeamCityCommunicator.Services
{
    public interface ITestEnvironmentService
    {
        string GetBuildVersion(string environmentNumber);
    }
}