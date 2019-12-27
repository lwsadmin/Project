using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Project.Common
{
    /// <summary>
    /// 读配置文件
    /// </summary>
    public class UtilConf
    {
        private static IConfiguration config;

        /// <summary>
        /// 加载配置文件
        /// </summary>
        public static IConfiguration Configuration
        {
            get
            {
                if (config != null) return config;
                config = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
                return config;
            }
            set => config = value;
        }
    }
}
