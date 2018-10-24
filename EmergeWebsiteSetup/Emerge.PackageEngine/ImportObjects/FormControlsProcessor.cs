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
    public class FormControlsProcessor : ObjectProcessor
    {
        private const string CMS_FORMUSERCONTROL = "cms_formusercontrol";
        private const string USERCONTROLPARAMETERS = "UserControlParameters";
        private const string USERCONTROLDISPLAYNAME = "UserControlDisplayName";
        
        public FormControlsProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.FORMCONTROLSFILEPATH); 
            }
        }

        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Form controls xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);
            processFormControlsXML(xmlDoc);
            xmlDoc.Save(filePath);
            LogProgress(string.Format("Form controls xml - {0} processed successfully.", FileName));
        }

        private void processFormControlsXML(XDocument xmlDoc)
        {
            string oldSiteName = PackageHelper.OldSiteName;

            var items = from item in xmlDoc.Descendants(CMS_FORMUSERCONTROL).Descendants(CMS_FORMUSERCONTROL)
                        where Regex.IsMatch((item.Element(USERCONTROLPARAMETERS) != null) ? item.Element(USERCONTROLPARAMETERS).Value : string.Empty, PackageHelper.OldSiteName, RegexOptions.IgnoreCase)
                        select item;

            foreach (var item in items)
            {
                LogProgress(string.Format("Form control - {0}.", item.Element(USERCONTROLDISPLAYNAME).Value));
                string value = item.Element(USERCONTROLPARAMETERS).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(USERCONTROLPARAMETERS, value);
            }
        }

        public override string GetNewValue(string value)
        {
            value = Regex.Replace(value, PackageHelper.OldSiteName, PackageHelper.NewSiteName, RegexOptions.IgnoreCase);
            return value;
        }
    }
}
