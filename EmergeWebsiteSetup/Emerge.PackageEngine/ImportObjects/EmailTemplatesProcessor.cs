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
    public class EmailTemplatesProcessor : ObjectProcessor
    {
        private const string CMS_EMAILTEMPLATE = "cms_emailtemplate";
        private const string EMAILTEMPLATENAME = "EmailTemplateName";
        private const string EMAILTEMPLATEGUID = "EmailTemplateGUID";
        private const string EMAILTEMPLATEDISPLAYNAME = "EmailTemplateDisplayName";
        
        

        public EmailTemplatesProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.EMAILTEMPLATESFILEPATH); 
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Email template xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);
            processEmailTemplateXML(xmlDoc);
            xmlDoc.Save(filePath);
            LogProgress(string.Format("Email template xml {0} processed successfully.", FileName));
        }

        private void processEmailTemplateXML(XDocument xmlDoc)
        {
            string oldSiteName = PackageHelper.OldSiteName;

            var items = from item in xmlDoc.Descendants(CMS_EMAILTEMPLATE).Descendants(CMS_EMAILTEMPLATE)
                        where Regex.IsMatch((item.Element(EMAILTEMPLATENAME) != null) ? item.Element(EMAILTEMPLATENAME).Value : string.Empty, PackageHelper.OldSiteName, RegexOptions.IgnoreCase) 
                        select item;

            foreach (var item in items)
            {
                LogProgress(string.Format("Email template - {0}.", item.Element(EMAILTEMPLATEDISPLAYNAME).Value));
                string value = item.Element(EMAILTEMPLATENAME).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(EMAILTEMPLATENAME, value);

                Guid templateGUID = Guid.NewGuid();
                item.SetElementValue(EMAILTEMPLATEGUID, templateGUID.ToString());
            }
        }

        //private string getNewValue(string value)
        //{
        //    string oldWebsite = ImportProcessor.OldSiteName;
        //    value = Regex.Replace(value, oldWebsite + "_", ImportProcessor.NewSiteName + "_", RegexOptions.IgnoreCase);
        //    //value = value.Replace(oldWebsite + "_", ImportProcessor.NewSiteName + "_");
        //    return value;
        //}
    }
}
