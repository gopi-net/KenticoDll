using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.WebsiteSetupHelper.Infrastructure;
using Emerge.DatabaseEngine;
using Emerge.ImportEngine;
using Emerge.PackageEngine;
using Emerge.ImportEngine.Entities;
using System.Configuration;
using System.Data.SqlClient;
using Emerge.WebsiteSetupHelper;

namespace Emerge.WebsiteSetupManager
{
    public class WebsiteHelper
    {
        const string WEBCONFIG = "web.config";
        const string CMSCONNECTIONSTRING = "CMSConnectionString";
        const string EMERGEWEBSITES = "EmergeWebsites";

        public static Configuration GetConfig(string websitePath)
        {
            WebsiteSettings settings = new WebsiteSettings();
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.IO.Path.Combine(websitePath, WEBCONFIG);
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            return config;
        }

        public static WebsiteSettings BuildWebsiteSettingsFromConfig(string websitePath)
        {
            Configuration config = GetConfig(websitePath);
            string connectionString = string.Empty;
            if (config.ConnectionStrings.ConnectionStrings[CMSCONNECTIONSTRING] != null)
                connectionString = config.ConnectionStrings.ConnectionStrings[CMSCONNECTIONSTRING].ConnectionString;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

            WebsiteSettings settings = buildWebsiteSettings(builder);
            settings.WebsitePath = websitePath;
            return settings;
        }

        private static WebsiteSettings buildWebsiteSettings(SqlConnectionStringBuilder builder)
        {
            WebsiteSettings settings = new WebsiteSettings();
            settings.DatabaseName = builder.InitialCatalog;
            settings.DatabaseServerName = builder.DataSource;
            settings.UserName = builder.UserID;
            settings.Password = builder.Password;
            return settings;
        }

        public static void SetConfigurationFileSettings(string websitePath, string websiteCodeName)
        {
            ImportContextHelper.SetConnectionStringInConfig(websitePath);
            SetAppSettingsKeyForEmergeWebsites(websitePath, websiteCodeName);
        }

        public static void SetAppSettingsKeyForEmergeWebsites(string websitePath, string value)
        {
            Configuration config = GetConfig(websitePath);
            string emergeWebsites = string.Empty;
            if (config.AppSettings.Settings[EMERGEWEBSITES] != null)
            {
                emergeWebsites = config.AppSettings.Settings[EMERGEWEBSITES].Value;
                if (!String.IsNullOrEmpty(emergeWebsites))
                    emergeWebsites += "|" + value.Trim();
                else
                    emergeWebsites = value.Trim();

                config.AppSettings.Settings[EMERGEWEBSITES].Value = emergeWebsites;
            }
            else
                config.AppSettings.Settings.Add(EMERGEWEBSITES, value);
           
            config.Save();
        }

        public static string GetEmergeWebsitesFromAppSettings(string websitePath)
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = System.IO.Path.Combine(websitePath, WEBCONFIG);
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            string emergeWebsites = string.Empty;
            if (config.AppSettings.Settings[EMERGEWEBSITES] != null)
            {
                return config.AppSettings.Settings[EMERGEWEBSITES].Value;
            }
            else
                return string.Empty;
        }

    }
}
