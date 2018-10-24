using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emerge.PackageEngine.Infrastructure;
using Emerge.PackageEngine.ImportObjects;

namespace Emerge.PackageEngine
{
    /// <summary>
    /// Factory class for objects to be created for processing
    /// </summary>
    public class ObjectProcessorFactory
    {
        /// <summary>
        /// Returns the instance of object processor depending by type
        /// </summary>
        /// <param name="type">Type of object</param>
        /// <returns>Instance of object processor</returns>
        public static ObjectProcessor GetObjectProcessor(ImportObjectType type)
        {
            ObjectProcessor objectProcessor = null;
            switch (type)
            {
                case ImportObjectType.CustomTable:
                    objectProcessor = new CustomTableProcessor();
                    break;
                case ImportObjectType.Site:
                    objectProcessor = new SiteProcessor();
                    break;
                case ImportObjectType.CustomTableData:
                    objectProcessor = new CustomTableDataProcessor(); 
                    break;
                case ImportObjectType.MediaLibrary:
                    objectProcessor = new MediaLibraryProcessor();
                    break;
                case ImportObjectType.EmailTemplates:
                    objectProcessor = new EmailTemplatesProcessor();
                    break;
                case ImportObjectType.FormControls:
                    objectProcessor = new FormControlsProcessor();
                    break;
                case ImportObjectType.Webpart:
                    objectProcessor = new WebPartProcessor();
                    break;
                case ImportObjectType.PageTemplates:
                    objectProcessor = new PageTemplatesProcessor();
                    break;
                case ImportObjectType.AdHocPageTemplates:
                    objectProcessor = new AdHocPageTemplateProcessor();
                    break;

            }
            return objectProcessor;
        }
    }
}
