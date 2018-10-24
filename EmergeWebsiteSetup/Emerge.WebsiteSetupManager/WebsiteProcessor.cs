using Emerge.DatabaseEngine;
using Emerge.ImportEngine;
using Emerge.ImportEngine.Entities;
using Emerge.PackageEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.WebsiteSetupHelper.Delegates;
using Emerge.WebsiteSetupHelper.Infrastructure;
using Emerge.WebsiteSetupHelper;

namespace Emerge.WebsiteSetupManager
{
    public delegate void onLogProgressHandler(string message);

    public class WebsiteProcessor
    {
        public event onLogProgressHandler OnLogProgress;
        private WebsiteSettings settings;
        public WebsiteProcessor(WebsiteSettings websiteSettings)
        {
            settings = websiteSettings;
        }

        public void StartProcess()
        {
            try
            {
                PackageSettings packageSettings = buildPackageSettingsObject();
                ProcessPackage(packageSettings);

                DatabaseSettings databaseSettings = buildDatabaseSettingsObject();
                ProcessDataBase(databaseSettings);

                ImportSettings importSettings = buildImportSettingsObject();
                ProcessImport(importSettings);

                LogProgress("Saving configuration settings in web.config file.");
                WebsiteHelper.SetConfigurationFileSettings(settings.WebsitePath, settings.WebsiteCodeName);
            }
            catch(Exception ex)
            {
                LogProgress(ex.ToString());
                throw ex;
            }
        }

        public void InstallHotfix()
        {
            try
            {
                DatabaseSettings databaseSettings = buildDatabaseSettingsObject();
                DatabaseProcessor processor = new DatabaseProcessor(databaseSettings);
                processor.OnLogProgress += LogProgress;

                string sites = WebsiteHelper.GetEmergeWebsitesFromAppSettings(settings.WebsitePath);
                List<string> websites = sites.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                processor.InstallHotfixScripts(websites);
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
                throw ex;
            }
        }

        private bool ProcessPackage(PackageSettings packageSettings)
        {
            PackageProcessor processor = new PackageProcessor(packageSettings);
            processor.OnLogProgress += LogProgress;
            processor.Process();
            settings.NewPackagePath = processor.GetNewPackageFilePath();
            return true;
        }

        private bool ProcessDataBase(DatabaseSettings settings)
        {
            DatabaseProcessor processor = new DatabaseProcessor(settings);
            processor.OnLogProgress += LogProgress;
            processor.Process();
            return true;
        }

        private bool ProcessImport(ImportSettings settings)
        {
            ImportProcessor processor = new ImportProcessor(settings);
            processor.OnLogProgress += LogProgress;
            processor.Process();
            return true;
        }
        private  PackageSettings buildPackageSettingsObject()
        {
            PackageSettings packageSettings = new PackageSettings();
            packageSettings.PackagePath = settings.PackagePath;
            packageSettings.WebsiteCodeName = settings.WebsiteCodeName;
            return packageSettings;
        }

        private  DatabaseSettings buildDatabaseSettingsObject()
        {
            DatabaseSettings databaseSettings = new DatabaseSettings();
            databaseSettings.DatabaseName = settings.DatabaseName;
            databaseSettings.DatabaseServerName = settings.DatabaseServerName;
            databaseSettings.Password = settings.Password;
            databaseSettings.UserName = settings.UserName;
            databaseSettings.WebsitePath = settings.WebsitePath;
            return databaseSettings;
        }


        private  ImportSettings buildImportSettingsObject()
        {
            ImportSettings importSettings = new ImportSettings();
            importSettings.PackagePath = settings.NewPackagePath;
            importSettings.WebsiteCodeName = settings.WebsiteCodeName;
            importSettings.WebsiteDisplayName = settings.WebsiteDisplayName;
            importSettings.WebsiteDomain = settings.WebsiteDomain;
            importSettings.WebsitePath = settings.WebsitePath;
            importSettings.DatabaseName = settings.DatabaseName;
            importSettings.DatabaseServerName = settings.DatabaseServerName;
            importSettings.Password = settings.Password;
            importSettings.UserName = settings.UserName;
            return importSettings;
        }


        void LogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }
    }
}
