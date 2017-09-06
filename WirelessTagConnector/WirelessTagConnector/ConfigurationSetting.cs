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
        public static readonly string AuthApiSubAddess = GetConfigData("AuthApiSubAddress");
        public static readonly string BaseAddress = GetConfigData("BaseAddress");

        public static readonly string UFL_Api_Address = GetConfigData("UFLApiAddress");
        public static readonly string UFL_Api_Username = GetConfigData("UFLApiUsername");
        public static readonly string UFL_Api_Password = GetConfigData("UFLApiPassword");
        public static readonly bool IsPosterService = Convert.ToBoolean(GetConfigData("IsPosterService"));


        private static string GetConfigData(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
