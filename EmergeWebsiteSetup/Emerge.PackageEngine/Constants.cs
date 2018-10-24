using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerge.PackageEngine
{
    public enum ImportObjectType
    {
        CustomTable,
        Site,
        CustomTableData,
        MediaLibrary,
        EmailTemplates,
        Webpart,
        FormControls,
        PageTemplates,
        AdHocPageTemplates
        
    }
    /// <summary>
    /// Class for declaring all the constants in the system
    /// </summary>
    public static class Constants
    {

        public const string CUSTOMTABLEFILEPATH = "CustomTableFilePath";
        public const string SITEFILEPATH = "SiteFilePath";
        public const string CUSTOMTABLEDATAFILEPATH = "CustomTableDataPath";
        public const string EMAILTEMPLATESFILEPATH = "EmailTemplatesFilePath";
        public const string MEDIALIBRARYFILEPATH = "MediaLibraryFilePath";
        public const string MEDIAFILESFILEPATH = "MediaFilesFilePath";
        public const string LIBRARYFOLDERPATH = "LibraryFolderPath";
        public const string PACKAGESETTINGSFILE = "PackageSettings.xml";
        public const string WEBPARTFILEPATH = "WebpartFilePath";
        public const string FORMCONTROLSFILEPATH = "FormControlsFilePath";
        public const string EMERGE = "Emerge_";
        public const string OLDWEBSITE = "OldWebsite";
        public const string PAGETEMPLATESFILEPATH = "PageTemplatesFilePath";
        public const string ADHOCPAGETEMPLATESFILEPATH = "AdHocPageTemplatesFilePath";

    }
}
