using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.PackageEngine.Infrastructure;
using Emerge.PackageEngine.Helpers;
using System.IO;
using System.Configuration;
using Emerge.WebsiteSetupHelper.Delegates;
using Emerge.WebsiteSetupHelper.Infrastructure;
using Emerge.WebsiteSetupHelper;

namespace Emerge.PackageEngine
{
    /// <summary>
    /// Manager class for processing the pre-import
    /// </summary>
    public class PackageProcessor : IProcessor
    {
        public event LogProgressHandler OnLogProgress;
        public PackageProcessor(PackageSettings settings)
        {
            PackageHelper.Init(settings);
        }

        public string GetNewPackageFilePath()
        {
            return PackageHelper.NewPackageFilePath;
        }

        /// <summary>
        /// Start method for the pre-import process
        /// </summary>
        public void Process()
        {
            raiseLogProgress("Uncompressing the package...");
            if (ZIPHelper.UnCompress(PackageHelper.PackageFilePath, PackageHelper.PackageFileName, PackageHelper.TargetFolder))
            {
                foreach (ImportObjectType type in Enum.GetValues(typeof(ImportObjectType)).Cast<ImportObjectType>())
                {
                    ObjectProcessor objectProcessor = ObjectProcessorFactory.GetObjectProcessor(type);

                    if (objectProcessor != null)
                    {
                        objectProcessor.OnLogProgress += raiseLogProgress;
                        objectProcessor.ProcessXML();
                    }
                }
                raiseLogProgress("Compressing the package...");
                PackageHelper.NewPackageFilePath = ZIPHelper.CompressPackage(PackageHelper.PackageFilePath, GetNewPackageName(), PackageHelper.TargetFolder);
                raiseLogProgress("The package processing has been completed...");
            }
        }

        private void raiseLogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }

        private string GetNewPackageName()
        {
            string newPackageName = string.Empty;
            newPackageName = "export_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".zip";
            return newPackageName;
        }

    }
}
