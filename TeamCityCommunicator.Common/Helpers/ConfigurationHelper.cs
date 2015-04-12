using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityCommunicator.Common.Helpers
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public string GetConfigurationValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
