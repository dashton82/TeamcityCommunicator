using System;
using System.Xml;

namespace TeamCityCommunicator.Services
{
    public class TeamCityService : ITeamCityService, ITeamCityBaseService
    {
        public string GetBuildStatus(string buildId)
        {
            var url = string.Format("http://teamcity/guestAuth/api/builds/id:{0}", buildId);
            var xmlDocument = LoadXmlFromUrl(url);
            var status = "INCONCLUSIVE";
            if (xmlDocument.DocumentElement != null)
            {
                status = xmlDocument.DocumentElement.Attributes["status"].Value;
            }

            return status;
        }

        public virtual XmlDocument LoadXmlFromUrl(string url)
        {
            var xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(url);
            }
            catch
            {
                //Unable to load xml document
            }
            
            return xmlDocument;
        }
    }
}