using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.PackageEngine.Infrastructure;
using System.Configuration;
using System.Xml.Linq;
using Emerge.PackageEngine.Helpers;
using System.Text.RegularExpressions;

namespace Emerge.PackageEngine.ImportObjects
{
    /// <summary>
    /// Class for processing custom table xml ie. Objects/cms_customtable.xml.export
    /// </summary>
    public class CustomTableProcessor : ObjectProcessor
    {
        #region Constants

        private const string CMS_CLASS = "cms_class";
        private const string CLASSDISPLAYNAME = "ClassDisplayName";
        private const string CLASSNAME = "ClassName";
        private const string CLASSTABLENAME = "ClassTableName";
        private const string CLASSGUID = "ClassGUID";
        private const string CLASSFORMDEFINITION = "ClassFormDefinition";
        private const string CLASSXMLSCHEMA = "ClassXmlSchema";
        
        
        private const string CMS_QUERY = "cms_query";
        private const string QUERYTEXT = "QueryText";
        private const string QUERYGUID = "QueryGUID";
        private const string QUERYNAME = "QueryName";
        

        private const string CMS_TRANSFORMATION = "cms_transformation";
        private const string TRANSFORMATIONGUID = "TransformationGUID";
        private const string TRANSFORMATIONCODE = "TransformationCode";
        private const string TRANSFORMATIONNAME = "TransformationName";

        #endregion

        public CustomTableProcessor()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = PackageSettingsHelper.GetPackageFile(Constants.CUSTOMTABLEFILEPATH);
            }
        }

        /// <summary>
        /// Processes the xml for custom table.
        /// </summary>
        public override void ProcessXML()
        {
            LogProgress(string.Format("Processing Custom Tables xml - {0}.", FileName));
            string filePath = PackageHelper.TargetFolder + FileName;
            XDocument xmlDoc = XDocument.Load(filePath);

            processCustomTableXML(xmlDoc);
            processQueryXML(xmlDoc);
            processTransformationXML(xmlDoc);

            xmlDoc.Save(filePath);
            LogProgress(string.Format("Custom Tables xml - {0} processed successfully...", FileName));
        }

        private void processCustomTableXML(XDocument xmlDoc)
        {
            LogProgress("Processing Custom Tables..");
            //Process the cms_class elements
            var classItems = from item in xmlDoc.Descendants(CMS_CLASS)
                             where item.Element(CLASSNAME).Value.Contains(Constants.EMERGE)
                             select item;

            foreach (var item in classItems)
            {
                string displayname = item.Element(CLASSDISPLAYNAME).Value;
                LogProgress(string.Format("Custom table - {0}.", displayname));
                string value = item.Element(CLASSDISPLAYNAME).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(CLASSDISPLAYNAME, value);

                value = item.Element(CLASSNAME).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(CLASSNAME, value);

                value = item.Element(CLASSTABLENAME).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(CLASSTABLENAME, value);

                value = item.Element(CLASSFORMDEFINITION).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(CLASSFORMDEFINITION, value);

                value = item.Element(CLASSXMLSCHEMA).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(CLASSXMLSCHEMA, value);

                Guid classGUID = Guid.NewGuid();
                item.SetElementValue(CLASSGUID, classGUID.ToString());
            }
            LogProgress("Custom Tables processed successfully...");
        }

        private void processQueryXML(XDocument xmlDoc)
        {
            LogProgress("Processing Queries..");
            //Process the cms_query elements
            var queryItems = from item in xmlDoc.Descendants(CMS_QUERY)
                             where Regex.IsMatch((item.Element(QUERYTEXT) != null) ? item.Element(QUERYTEXT).Value : string.Empty, PackageHelper.OldSiteName, RegexOptions.IgnoreCase) 
                             select item;

            foreach (var item in queryItems)
            {
                LogProgress(string.Format("Query - {0}.", item.Element(QUERYNAME).Value));
                string value = item.Element(QUERYTEXT).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(QUERYTEXT, value);

                Guid queryGUID = Guid.NewGuid();
                item.SetElementValue(QUERYGUID, queryGUID.ToString());
            }
            LogProgress("Queries processed successfully..");
        }

        private void processTransformationXML(XDocument xmlDoc)
        {
            LogProgress("Processing Transformations..");
            var transformationItems = from item in xmlDoc.Descendants(CMS_TRANSFORMATION)
                             where item.Element(TRANSFORMATIONCODE).Value.Contains(Constants.EMERGE)
                             select item;

            foreach (var item in transformationItems)
            {
                LogProgress(string.Format("Transformation - {0}.", item.Element(TRANSFORMATIONNAME).Value));
                string value = item.Element(TRANSFORMATIONCODE).Value;
                value = GetNewValue(value);//getNewValue(value);
                item.SetElementValue(TRANSFORMATIONCODE, value);

                Guid transGUID = Guid.NewGuid();
                item.SetElementValue(TRANSFORMATIONGUID, transGUID.ToString());
            }
            LogProgress("Transformations processed successfully..");
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
