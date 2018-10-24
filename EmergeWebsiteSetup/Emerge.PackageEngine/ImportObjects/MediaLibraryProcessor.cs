using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.PackageEngine.Infrastructure;
using System.Configuration;
using System.Xml.Linq;
using Emerge.PackageEngine.Helpers;
using System.IO;
using System.Text.RegularExpressions;

namespace Emerge.PackageEngine.ImportObjects
{
    public class MediaLibraryProcessor : ObjectProcessor
    {
        private const string MEDIA_LIBRARY = "media_library";
        private const string LIBRARYNAME = "LibraryName";
        private const string LIBRARYDISPLAYNAME = "LibraryDisplayName";
        private const string LIBRARYDESCRIPTION = "LibraryDescription";
        private const string LIBRARYFOLDER = "LibraryFolder";
        private const string LIBRARYGUID = "LibraryGUID";

        public MediaLibraryProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.MEDIALIBRARYFILEPATH);
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Media Library xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);
            processMediaLibraryXML(xmlDoc);
            xmlDoc.Save(filePath);
            LogProgress("Media library xml processed successfully.");
        }

        private void processMediaLibraryXML(XDocument xmlDoc)
        {
            string oldSiteName = PackageHelper.OldSiteName;

            var mediaItems = from item in xmlDoc.Descendants(MEDIA_LIBRARY).Descendants(MEDIA_LIBRARY)
                             where item.Element(LIBRARYNAME).Value.Contains(oldSiteName)
                             select item;

            foreach (var item in mediaItems)
            {
                string library = item.Element(LIBRARYNAME).Value;
                LogProgress(string.Format("Processing media library - {0}.", library));
                string value = string.Empty;
                value = item.Element(LIBRARYNAME).Value;
                value = GetNewValue(value);
                item.SetElementValue(LIBRARYNAME, value);

                value = item.Element(LIBRARYDESCRIPTION).Value;
                value = GetNewValue(value);
                item.SetElementValue(LIBRARYDESCRIPTION, value);

                value = item.Element(LIBRARYDISPLAYNAME).Value;
                value = GetNewValue(value);
                item.SetElementValue(LIBRARYDISPLAYNAME, value);

                string oldLibraryFolder = item.Element(LIBRARYFOLDER).Value;
                value = GetNewValue(oldLibraryFolder);
                item.SetElementValue(LIBRARYFOLDER, value);
                processLibraryFolder(oldLibraryFolder, value);

                Guid libraryGUID = Guid.NewGuid();
                string oldLibraryGUID = item.Element(LIBRARYGUID).Value;
                item.SetElementValue(LIBRARYGUID, libraryGUID.ToString());
                processMediaFilesXML(libraryGUID.ToString(), oldLibraryGUID);
                LogProgress(string.Format("Media Library - {0} processed successfully.", library));
            }
            
        }

        private void processLibraryFolder(string oldLibraryFolder, string newLibraryFolder)
        {
            LogProgress(string.Format("Processing media library folder - {0}.", oldLibraryFolder));
            
            string path = PackageSettingsHelper.GetPackageFile(Constants.LIBRARYFOLDERPATH);
            string libraryFolderPath = PackageHelper.TargetFolder + Path.Combine(path, oldLibraryFolder);
            string newLibraryFolderPath = PackageHelper.TargetFolder + Path.Combine(path, newLibraryFolder);
            if (Directory.Exists(libraryFolderPath))
                Directory.Move(libraryFolderPath, newLibraryFolderPath);

            LogProgress(string.Format("Media library folder - {0} processed successfully.", oldLibraryFolder));
        }

        private void processMediaFilesXML(string libraryGUID, string oldLibraryGUID)
        {
            string fileName = PackageSettingsHelper.GetPackageFile(Constants.MEDIAFILESFILEPATH);
            string filePath = PackageHelper.TargetFolder + string.Format(fileName, oldLibraryGUID);
            LogProgress(string.Format("Processing media library xml file - {0}.", string.Format(fileName, oldLibraryGUID)));
            XDocument xmlDoc = XDocument.Load(filePath);

            string rootName = xmlDoc.Root.Name.LocalName;
            rootName = rootName.Replace(oldLibraryGUID.ToLower(), libraryGUID.ToLower());
            xmlDoc.Root.Name = rootName;

            string newFilePath = PackageHelper.TargetFolder + string.Format(fileName, libraryGUID);
            xmlDoc.Save(newFilePath);

            File.Delete(filePath);
            LogProgress(string.Format("Media library xml file - {0} processed successfully.", string.Format(fileName, oldLibraryGUID)));
        }

        public override string GetNewValue(string value)
        {
            string oldWebsite = PackageHelper.OldSiteName;
            value = Regex.Replace(value, oldWebsite, PackageHelper.NewSiteName, RegexOptions.IgnoreCase);
            //value = value.Replace(oldWebsite, ImportProcessor.NewSiteName);
            return value;
        }
    }
}
