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
    public class CustomTableDataProcessor : ObjectProcessor
    {
        #region Constants

        private const string CUSTOMTABLE = "CustomTable";
        private const string TABLE = "NewDataSet";

        #endregion

        public CustomTableDataProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.CUSTOMTABLEDATAFILEPATH);
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing custom table data xml files in folder {0}.", FileName));
            string folderPath = PackageHelper.TargetFolder + FileName;

            var items = PackageSettingsHelper.GetCustomTableDataFiles();
            
            foreach (string file in Directory.GetFiles(folderPath).Select(a => Path.GetFileName(a)).ToArray())
            {
                if (items.Contains(file))
                    processFile(file);
                else
                    File.Delete(Path.Combine(folderPath, file));
            }
            LogProgress("Custom table data xml files processed successfully.");
        }

        private void processFile(string file)
        {
            string oldFilename = file;
            LogProgress(string.Format("File - {0}.", oldFilename));
            string folderPath = PackageHelper.TargetFolder + FileName;
            string filePath = Path.Combine(folderPath, file);
            XDocument xmlDoc = XDocument.Load(filePath);
            string oldWebsiteName = PackageHelper.OldSiteName.ToLower();

            var classItems = from item in xmlDoc.Descendants(TABLE)
                             where item.Elements().Any(a => a.Name.LocalName.ToLower().Contains(oldWebsiteName + "_"))
                             select item;




            foreach (var element in classItems.Elements().Where(a => a.Name.LocalName.ToLower().Contains(oldWebsiteName + "_")))
            {
                string value = element.Name.LocalName;
                value = GetNewValue(value).ToLower();// getNewValue(value);
                element.Name = value.ToLower();
            }

            var tableItems =  from item in xmlDoc.Descendants(TABLE).Descendants()
                             where item.Elements().Any(a => a.Value.ToLower().Contains(oldWebsiteName + "_"))
                             select item;

            


            foreach (var element in tableItems.Elements())
            {
                //if(element..Where(x => x.Value.Contains(oldWebsiteName + "_")))
                if (element.Value.ToLower().Contains(oldWebsiteName))
                {
                    string value = element.Value;
                    value = GetNewValue(value);// getNewValue(value);
                    element.Value = value;
                }
            }

            string rootName = xmlDoc.Root.Name.LocalName;
            rootName = GetNewValue(rootName).ToLower();//getNewValue(rootName);
            xmlDoc.Root.Name = rootName.ToLower();
            
            File.Delete(Path.Combine(folderPath, file));
           // xmlDoc.ToString().Replace(oldWebsiteName, GetNewValue(oldFilename));
            file = file.ToLower().Replace(oldWebsiteName, PackageHelper.NewSiteName.ToLower());
            xmlDoc.Save(Path.Combine(folderPath, file));
            LogProgress(string.Format("File - {0} processed successfully.", oldFilename));
        }

        //private string getNewValue(string value)
        //{
        //    string oldWebsite = ImportProcessor.OldSiteName;
        //    value = Regex.Replace(value, oldWebsite + "_", ImportProcessor.NewSiteName + "_", RegexOptions.IgnoreCase);
        //    return value;
        //}
    }
}
