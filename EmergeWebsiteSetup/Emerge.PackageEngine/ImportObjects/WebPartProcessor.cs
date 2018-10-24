using Emerge.PackageEngine.Helpers;
using Emerge.PackageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Emerge.PackageEngine.ImportObjects
{
    public class WebPartProcessor : ObjectProcessor
    {
        private const string CMS_WEBPART = "cms_webpart";
        private const string WEBPARTPROPERTIES = "WebPartProperties";
        private const string WEBPARTDISPLAYNAME = "WebPartDisplayName";
        
        public WebPartProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.WEBPARTFILEPATH); 
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Webpart xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);
            processWebpartXML(xmlDoc);

            xmlDoc.Save(filePath);
            LogProgress(string.Format("Webpart xml - {0} processed successfully.", FileName));
        }

        private void processWebpartXML(XDocument xmlDoc)
        {
            string oldSiteName = PackageHelper.OldSiteName;

            var items = from item in xmlDoc.Descendants(CMS_WEBPART).Descendants(CMS_WEBPART)
                        where Regex.IsMatch(item.Element(WEBPARTPROPERTIES).Value, PackageHelper.OldSiteName, RegexOptions.IgnoreCase)
                        select item;

            foreach (var item in items)
            {
                LogProgress(string.Format("Webpart - {0}.", item.Element(WEBPARTDISPLAYNAME).Value));
                string value = item.Element(WEBPARTPROPERTIES).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(WEBPARTPROPERTIES, value);
            }
        }

        public override string GetNewValue(string value)
        {
            value = Regex.Replace(value, PackageHelper.OldSiteName, PackageHelper.NewSiteName, RegexOptions.IgnoreCase);
            return value;
        }
    }
}
