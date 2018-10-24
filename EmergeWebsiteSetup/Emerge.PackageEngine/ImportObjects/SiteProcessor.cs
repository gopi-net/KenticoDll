using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using Emerge.PackageEngine.Infrastructure;
using Emerge.PackageEngine.Helpers;
using System.Text.RegularExpressions;

namespace Emerge.PackageEngine.ImportObjects
{
    /// <summary>
    /// Class for processing the site xml ie. Site/cms_site.xml.export
    /// </summary>
    public class SiteProcessor : ObjectProcessor
    {
        #region Constants

        private const string CMS_SITE = "cms_site";
        private const string SITENAME = "SiteName";
        private const string SITEDISPLAYNAME = "SiteDisplayName";
        private const string SITEGUID = "SiteGUID";

        #endregion

        public SiteProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.SITEFILEPATH); 
            }
        }

        /// <summary>
        /// Processes the xml for site xml
        /// </summary>
        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Site xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);

            //Process the cms_class elements
            var classItems = from item in xmlDoc.Descendants(CMS_SITE).Descendants(CMS_SITE)
                             select item;

            foreach (var item in classItems)
            {
                string value = item.Element(SITENAME).Value;
                value = GetNewValue(value);
                item.SetElementValue(SITENAME, value);

                value = PackageHelper.NewSiteName;
                item.SetElementValue(SITEDISPLAYNAME, value);

                value = item.Element(SITEGUID).Value;
                value = GetNewValue(value);
                item.SetElementValue(SITEGUID, value);

            }

            xmlDoc.Save(filePath);
            LogProgress(string.Format("Site xml - {0} processed successfully.", FileName));
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
