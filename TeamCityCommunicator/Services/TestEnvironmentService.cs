using System;
using System.Net;

namespace TeamCityCommunicator.Services
{
    public class TestEnvironmentService : ITestEnvironmentService
    {
        public string GetBuildVersion(string environmentNumber)
        {
            var buildInfoTxt = GetBuildInfoTextFromUrl(environmentNumber);

            if (string.IsNullOrEmpty(buildInfoTxt))
            {
                return "";
            }

            return buildInfoTxt.Remove(0, buildInfoTxt.IndexOf("Version Is", StringComparison.InvariantCultureIgnoreCase)).Replace("Version Is ", "");
        }

        public virtual string GetBuildInfoTextFromUrl(string environmentNumber)
        {
            return new WebClient().DownloadString(string.Format("http://www.{0}.test/buildinfo.txt", environmentNumber));
        }
    }
}