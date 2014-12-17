using System;

namespace TeamCityCommunicator.Services
{
    public interface IBuildStatusDataStorageService
    {
        Boolean StoreSanityBuildInformation(string buildNumber, string status);
    }
}