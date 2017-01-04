using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Tests
{
    public class Settings
    {
        private static Lazy<IConfigurationRoot> _config = new Lazy<IConfigurationRoot>(() => GetConfig());
        
        public static string Token
        {
            get
            {
                return _config.Value["Token"];
            }
        }

        public static string FbUri
        {
            get
            {
                return _config.Value["fogbugzUri"];
            }
        }

        private static IConfigurationRoot GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true);

            return builder.Build();
        }
    }
}
