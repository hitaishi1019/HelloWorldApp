using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace HelloWorldUtilities.Services
{
    public class ConfigAppSetting : IAppSettings
    {
        public string Get(string name)
        {
            return ConfigurationManager.AppSettings.Get(name);
        }
    }
}
