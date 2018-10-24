using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using CMS.FormEngine;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using CMS.CustomTables;
using CMS.Membership;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService.GridActions
{
    public class GridDeleteAction : IGridDeleteAction
    {
        #region IGridAction Members

        /// <summary>
        /// Processes Grid delete action. Method delete the actual record from custom table.
        /// </summary>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        /// <param name="CustomTableClassName">Custom Table Class Name</param>
        /// <param name="AfterActionRedirectToUrl">If Set then after action processed, control will be redirect to this Url</param>
        
        public bool ProcessAction(object actionArgument, string CustomTableClassName, string AfterActionRedirectToUrl)
        {
            
            if (CustomTableClassName != null && !CustomTableClassName.Equals(string.Empty))
            {
                CustomTableItem item = CustomTableItemProvider.GetItem(ValidationHelper.GetInteger(actionArgument, 0), CustomTableClassName);

                if (item != null)
                {
                    if (EmergeRelationHelper.IsActionFeasible(item, Constants.GridActions.Delete))
                    {
                        DeleteMediaFiles(CustomTableClassName,  item);
                        item.Delete();
                        
                        URLHelper.Redirect(RequestContext.URL.ToString());
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// method to delete the media files if exists in custom table item being deleted.
        /// </summary>
        /// <param name="CustomTableClassName"></param>
        /// <param name="item"></param>
        private static void DeleteMediaFiles(string CustomTableClassName, CustomTableItem item)
        {
            const string CONTROL_NAME_KEY = "controlname";

            FormInfo fi = FormHelper.GetFormInfo(CustomTableClassName, false);
            foreach (FormFieldInfo ffi in fi.GetFields(true, true, false))
            {
                object ColumnValue = item.GetValue(ffi.Name);

                if (null != ffi.Settings[CONTROL_NAME_KEY] && null != ColumnValue && !string.IsNullOrEmpty(ColumnValue.ToString().Trim()))
                {
                    if (ffi.Settings[CONTROL_NAME_KEY].ToString().ToLower().Equals(Constants.MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME.ToLower()))
                    {
                        MediaFileInfo deleteFile = MediaFileInfoProvider.GetMediaFileInfo(Guid.Parse(ColumnValue.ToString()), SiteContext.CurrentSiteName);
                        MediaFileInfoProvider.DeleteMediaFileInfo(deleteFile);
                    }
                }
            }
           
        }

        
        public object BeforeAction(params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public object AfterAction(params object[] parameters)
        {
        //throw new NotImplementedException();
            return true;
        }

        #endregion
    }
}
