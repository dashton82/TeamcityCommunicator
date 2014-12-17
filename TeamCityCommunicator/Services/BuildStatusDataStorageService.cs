using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamCityCommunicator.Services
{
    public class BuildStatusDataStorageService : IBuildStatusDataStorageService
    {
        public Boolean StoreSanityBuildInformation(string buildNumber, string status)
        {
            if (!string.IsNullOrEmpty(buildNumber) && !string.IsNullOrEmpty(status))
            {

                //Persist the data somehow
                return true;
            }
            return false;
        }
    }
}