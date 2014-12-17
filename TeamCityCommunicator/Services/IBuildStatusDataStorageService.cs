using System;

namespace TeamCityCommunicator.Services
{
    public interface IBuildStatusDataStorageService : ITeamCityBaseService
    {
        Boolean StoreSanityBuildInformation(string buildNumber, string status);
    }
}