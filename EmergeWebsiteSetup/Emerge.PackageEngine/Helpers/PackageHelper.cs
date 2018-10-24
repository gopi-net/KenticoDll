using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Emerge.PackageEngine.Helpers
{
    public static class PackageHelper
    {

        private static string newSiteName = string.Empty;
        public static string NewSiteName
        {
            get
            {
                return newSiteName;
            }
        }

        private static string oldSiteName = string.Empty;
        public static string OldSiteName
        {
            get
            {
                if (String.IsNullOrEmpty(oldSiteName))
                {
                    oldSiteName = PackageSettingsHelper.GetOldWebsiteName();
                    if (String.IsNullOrEmpty(oldSiteName))
                        throw new Exception("Resources.Messages.OldWebsiteNameNotSpecified");
                }
                return oldSiteName;
            }
        }


        private static string packageFileName = string.Empty;
        public static string PackageFileName
        {
            get
            {
                return packageFileName;
            }
        }

        private static string packageFilePath = string.Empty;
        public static string PackageFilePath
        {
            get
            {
                return packageFilePath;
            }
        }

        private static string targetFolder = string.Empty;
        public static string TargetFolder
        {
            get
            {
                return targetFolder;
            }
        }

        private static string newPackageFilePath = string.Empty;
        private static string originalPackagePath = string.Empty;
        public static string NewPackageFilePath
        {
            get
            {
                if (!string.IsNullOrEmpty(newPackageFilePath))
                    return newPackageFilePath;
                return originalPackagePath;
            }
            set
            {
                newPackageFilePath = value;
            }
        }


        private static void SetPathAndFileName(string packagePath)
        {
            string[] elements = packagePath.Split(new char[] { '\\' });
            packageFileName = elements.Last();
            packageFilePath = packagePath.Substring(0, packagePath.LastIndexOf('\\') + 1);
            targetFolder = PackageFilePath + "Temp" + "\\";
        }

        public static void Init(PackageSettings settings)
        {
            newSiteName = settings.WebsiteCodeName;
            SetPathAndFileName(settings.PackagePath);
            originalPackagePath = settings.PackagePath;
        }


    }
}
