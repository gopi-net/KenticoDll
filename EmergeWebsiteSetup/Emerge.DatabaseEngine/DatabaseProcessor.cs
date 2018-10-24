using CMS.DataEngine;
using Emerge.WebsiteSetupHelper;
using Emerge.WebsiteSetupHelper.Delegates;
using Emerge.WebsiteSetupHelper.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emerge.DatabaseEngine
{
    public class DatabaseProcessor : IProcessor
    {
        private DatabaseSettings settings;
        public event LogProgressHandler OnLogProgress;

        public DatabaseProcessor(DatabaseSettings databaseSettings)
        {
            settings = databaseSettings;
            ImportContextHelper.InitializeDatabaseSettings(settings.DatabaseServerName, settings.DatabaseName, settings.UserName, settings.Password);
        }

        public void Process()
        {
            CreateDatabase();
        }

        public void CreateDatabase()
        {
            if (!DatabaseHelper.DatabaseExists(ImportContextHelper.ConnectionString))
            {
                LogProgress("Installing the database setup...");
                string connectionString = ConnectionHelper.BuildConnectionString(SQLServerAuthenticationModeEnum.SQLServerAuthentication, settings.DatabaseServerName, String.Empty, settings.UserName, settings.Password, SqlInstallationHelper.DB_CONNECTION_TIMEOUT);
                SqlInstallationHelper.CreateDatabase(settings.DatabaseName, connectionString, null);
                InstallDatabase(Path.Combine(settings.WebsitePath, DatabaseSettingsHelper.GetScriptsPath()));
                LogProgress("Database setup completed successfully...");
            }
        }

        public void InstallHotfixScripts(List<string> websites)
        {
            LogProgress("Running the hotfix database scripts...");
            SQLScriptsProcessor scriptsProcessor = new SQLScriptsProcessor(Path.Combine(settings.WebsitePath, DatabaseSettingsHelper.GetHotfixScriptsPath()));
            scriptsProcessor.OnLogProgress += LogProgress;
            if (scriptsProcessor.ProcessScripts(websites))
                InstallDatabase(scriptsProcessor.NewPackagePath);

            LogProgress("Hotfix scripts completed successfully...");
        }

        private void InstallDatabase(string scriptsPath)
        {
            Thread.Sleep(5000);
            if (DatabaseHelper.DatabaseExists(ImportContextHelper.ConnectionString))
                SqlInstallationHelper.InstallDatabase(ImportContextHelper.ConnectionString, scriptsPath, "", "", Log, "dbo");
        }

        private void Log(string message, MessageTypeEnum type)
        {
            LogProgress(message);
            if (type == MessageTypeEnum.Error)
                throw new Exception("An exception occurred while installing database....");
        }

        private void LogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }
    }
}
