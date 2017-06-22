using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; }

        public static IConfiguration Configuration { get; set; }

        public static T Get<T>(string key)
        {
            if (Configuration == null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                var configuration = builder.Build();
                Configuration = configuration.GetSection("ConnectionStrings");
            }

            return (T)Convert.ChangeType(Configuration[key], typeof(T));
        }
    }
}
