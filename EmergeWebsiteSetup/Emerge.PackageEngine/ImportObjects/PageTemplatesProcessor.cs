using Emerge.PackageEngine.Helpers;
using Emerge.PackageEngine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Emerge.PackageEngine.ImportObjects
{
    public class PageTemplatesProcessor : ObjectProcessor
    {

        private const string CMS_PAGETEMPLATE = "cms_pagetemplate";
        private const string PAGETEMPLATEWEBPARTS = "PageTemplateWebParts";
        private const string PAGETEMPLATEDISPLAYNAME = "PageTemplateDisplayName";

        public PageTemplatesProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.PAGETEMPLATESFILEPATH);
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Page Templates xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);
            processPageTemplateXML(xmlDoc);

            xmlDoc.Save(filePath);
            LogProgress(string.Format("Page Templates xml - {0} processed successfully.", FileName));
        }

        private void processPageTemplateXML(XDocument xmlDoc)
        {
            var items = from item in xmlDoc.Descendants(CMS_PAGETEMPLATE).Descendants(CMS_PAGETEMPLATE)
                        where Regex.IsMatch((item.Element(PAGETEMPLATEWEBPARTS) != null) ? item.Element(PAGETEMPLATEWEBPARTS).Value : string.Empty, PackageHelper.OldSiteName, RegexOptions.IgnoreCase)
                        select item;

            foreach (var item in items)
            {
                LogProgress(string.Format("Page Template - {0}.", item.Element(PAGETEMPLATEDISPLAYNAME).Value));
                string value = item.Element(PAGETEMPLATEWEBPARTS).Value;
                value = GetNewValue(value);
                item.SetElementValue(PAGETEMPLATEWEBPARTS, value);
            }
        }

        public override string GetNewValue(string value)
        {
            value = Regex.Replace(value, PackageHelper.OldSiteName, PackageHelper.NewSiteName, RegexOptions.IgnoreCase);
            return value;
        }
    }
}
