using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emerge.PackageEngine.Helpers
{
    public class PackageSettingsHelper
    {
        private const string PACKAGEFILE = "PackageFile";
        private const string KEY = "Key";
        private const string VALUE = "Value";
        private const string WEBSITENAME = "WebsiteName";
        private const string CUSTOMTABLE = "CustomTable";

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
                return Path.Combine(AssemblyDirectory, Constants.PACKAGESETTINGSFILE);
            }
        }

        public static string GetPackageFile(string key)
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var values = from item in xmlDoc.Descendants(PACKAGEFILE)
                        where item.Element(KEY).Value == key
                        select item.Element(VALUE).Value;
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

        public static IEnumerable<string> GetCustomTableDataFiles()
        {
            if (xmlDoc == null)
                xmlDoc = XDocument.Load(SettingsFilePath);

            var items = from item in xmlDoc.Descendants(CUSTOMTABLE)
                        select (string)item.Value.Replace("{0}", PackageHelper.OldSiteName).ToLower();

            if (items != null)
                return items;
            return null;
        }
    }
}
