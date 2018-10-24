using CMS.Helpers;
using CMS.DataEngine;
using CMS.IO;
using CMS.Base;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerge.WebsiteSetupHelper
{
    public static class ImportContextHelper
    {
        private static string connectionString;
        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }

        public static bool TestServerConnection(string serverName, string databaseName, string userName, string password)
        {
            string error = ConnectionHelper.TestConnection(SQLServerAuthenticationModeEnum.SQLServerAuthentication,
               serverName, "", userName, password);

            if (!String.IsNullOrEmpty(error))
                throw new Exception(error);

            return true;
        }

        public static void SetConnectionString(string serverName, string databaseName, string userName, string password)
        {
            connectionString = ConnectionHelper.BuildConnectionString(SQLServerAuthenticationModeEnum.SQLServerAuthentication,
            serverName, databaseName, userName, password, 240, null,false);
            ConnectionHelper.ConnectionString = ConnectionString;
        }

        public static void SetHashStringSalt()
        {
            if (string.IsNullOrEmpty(ValidationHelper.GetDefaultHashStringSalt()))
            {
                ValidationHelper.HashStringSalt = ConnectionString;
            }
        }

        public static void InitializeCMSContext()
        {
            CMSApplication.Init();
        }

        public static void InitializeDatabaseSettings(string serverName, string databaseName, string userName, string password)
        {
            if (TestServerConnection(serverName, databaseName, userName, password))
                SetConnectionString(serverName, databaseName, userName, password);

            SetHashStringSalt();
            
        }

        public static void SetConnectionStringInConfig(string websitePath)
        {
            SystemContext.WebApplicationPhysicalPath = websitePath;
            SettingsHelper.SetConnectionString(ConnectionHelper.ConnectionStringName, ConnectionString);
        }
    }
}
