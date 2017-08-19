using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WirelessTagConnector
{
    public class ConfigurationSetting
    {
        public static readonly string ConnectionString = GetConfigData("PiConnectionString");
        public static readonly string Client_Id = GetConfigData("ClientId");
        public static readonly string Client_Secret = GetConfigData("ClientSecret");
        public static readonly string Client_Code = GetConfigData("ClientCode");

        private static string GetConfigData(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
