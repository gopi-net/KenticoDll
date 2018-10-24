using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

namespace Emerge.DatabaseEngine
{
    public static class DatabaseSettingsHelper
    {
        private const string SCRIPTSPATH = "ScriptsPath";
        private const string DATABASESETTINGSFILE = "DatabaseSettings.xml";
        private const string WEBSITENAME = "WebsiteName";
        private const string HOTFIXFILES = "HotfixFiles";
        private const string HOTFIXSCRIPTPATH = "HotfixScriptPath";

        private static XDocument xmlDoc;
        static string assemblyDirectory = string.Empty;
        static string AssemblyDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(assemblyDirectory))
                {
                    string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);
                    assemblyDirectory = Path.GetDirectoryName(path);
                }
                return assemblyDirectory;
            }
        }

        static string SettingsFilePath
        {
            get
            {
                return Path.Combine(AssemblyDirectory, DATABASESETTINGSFILE);
            }
        }

        public static string GetScriptsPath()
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var values = from item in xmlDoc.Descendants(SCRIPTSPATH)
                         select item.Value;
            if (values != null)
                return values.First();
            return null;
        }

        public static string GetOldWebsiteName()
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var values = from item in xmlDoc.Descendants(WEBSITENAME)
                         select item.Value;

            if (values != null)
                return values.First();
            return null;
        }

        public static string GetHotfixFiles()
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var values = from item in xmlDoc.Descendants(HOTFIXFILES)
                         select item.Value;

            if (values != null)
                return values.First();
            return null;
        }

        public static string GetHotfixScriptsPath()
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var values = from item in xmlDoc.Descendants(HOTFIXSCRIPTPATH)
                         select item.Value;
            if (values != null)
                return values.First();
            return null;
        }
    }
}
