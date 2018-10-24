using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;

using CMS.FormEngine;

using CMS.UIControls;
using Bluespire.Emerge.Common;
using System.Collections.Generic;
using CMS.MediaLibrary;
using Bluespire.Emerge.Web;
using Bluespire.Emerge.CommonService;
using CMS.CustomTables;
using CMS.DataEngine;
using CMS.Base;
using CMS.Helpers;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.CMSHelper;

public partial class CMSModules_Maintenance_Controls_EmergeViewItem : CMSUserControl
{
    #region "Variables"

    private CustomTableItem mCustomTableItem;

    #endregion


    #region "Properties"

    public CustomTableItem CustomTableItem
    {
        get
        {
            return mCustomTableItem;
        }
        set
        {
            mCustomTableItem = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (CustomTableItem != null)
        {
            StringBuilder sb = new StringBuilder();

            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(CustomTableItem.CustomTableClassName);
            if (dci != null)
            {
                sb.Append("<table class=\"table table-hover\">");
                // Get class form definition
                FormInfo fi = FormHelper.GetFormInfo(dci.ClassName, false);
                string fieldCaption = "";

                FormFieldInfo ffi = null;
                IDataContainer item = CustomTableItem;
                List<FormFieldInfo> fields = fi.GetFields(true, true);
                // Table header
                string headerContent = "<tr class=\"unigrid-head\"><th>" + GetString("customtable.data.nametitle") + "</th><th class=\"main-column-100\">" + GetString("customtable.data.namevalue") + "</th></tr>";
                sb.Append(headerContent);

                // Go through the columns
                int i = 0;
                foreach (FormFieldInfo field in fields)
                {
                    string columnName = field.Name;
                    if (EmergeStaticHelper.IsFieldExcludedForViewPage(columnName, dci.ClassName))
                        continue;
                    // Get field caption
                    ffi = fi.GetFormField(columnName);
                    if (ffi == null)
                    {
                        fieldCaption = columnName;
                    }
                    else
                    {
                        if (ffi.Caption == "")
                        {
                            fieldCaption = columnName;
                        }
                        else
                        {
                            fieldCaption = ResHelper.LocalizeString(ffi.Caption);
                        }
                    }
                    string rowContent = "<tr><td><strong>{0}</strong></td><td class=\"wrap-normal\">{1}</td></tr>";

                    string value = getValue(ffi, item, field, columnName);
                    if (!String.IsNullOrEmpty(value))
                        sb.Append(String.Format(rowContent, fieldCaption, value));
                    ++i;
                }
                sb.Append("</table>");
            }

            string resultTable = sb.ToString();
            if (!string.IsNullOrEmpty(resultTable))
            {
                ltlContent.Text = resultTable;
            }
        }
        else
        {
            ltlContent.Text = GetString("editedobject.notexists");
            return;
        }
    }

    private string getValue(FormFieldInfo ffi, IDataContainer item, FormFieldInfo field, string columnName)
    {
        string value = string.Empty;
        if (!field.System)
        {
            if (null != ffi.Settings["controlname"] && (ffi.Settings["controlname"].ToString().ToLower() == Constants.CONTROL_ENCRYPTEDFIELDLABLE.ToLower() || ffi.Settings["controlname"].ToString().ToLower() == Constants.CONTROL_ENCRYPTEDFIELD.ToLower()) && item.GetValue(columnName) != null)
            {
                value = EmergeEncryptionHelper.DecryptData(item.GetValue(columnName).ToString()).ToString();
            }
            else if ((null != ffi.Settings["controlname"]) && (ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower())))
            {
                value = GetTableDetails(ffi);
            }
            else if ((null != ffi.Settings["controlname"]) && (ffi.Settings["controlname"].ToString().Equals(Constants.MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME)))
            {
                value = GetUploadedFileDetails(columnName);
            }
            else
                value = HTMLHelper.HTMLEncode(ValidationHelper.GetString(GetColumnValue(columnName, CustomTableItem.CustomTableClassName, field.DataType == FieldDataType.DateTime && !field.System && !String.IsNullOrEmpty(Convert.ToString(item.GetValue(columnName))) ? Convert.ToDateTime(item.GetValue(columnName)).ToString(Constants.EMERGE_DATEFORMAT) : Convert.ToString(item.GetValue(columnName))), ""));
        }
        else
        {
            if (columnName.ToLower() == "itemcreatedby" || columnName.ToLower() == "itemmodifiedby")
            {
                int userId = ValidationHelper.GetInteger(item.GetValue(columnName), 0);
                string userName = GetUserName(userId);
                value = HTMLHelper.HTMLEncode((userName == null) ? Convert.ToString(item.GetValue(columnName)) : userName);
            }
            else
                value = Convert.ToString(item.GetValue(columnName));
        }
        return value;
    }

    private string GetUserName(int userId)
    {
        string userName = null;

        if (userId != 0)
        {
            string key = "UserInfo_" + userId;
            // Get user name from request cache
            userName = RequestStockHelper.GetItem(key) as string;
            if (userName == null)
            {
                // Get user information
                DataSet users = UserInfoProvider.GetFullUsers("UserID=" + userId, null, 1, "UserName, FullName");
                if (!DataHelper.DataSourceIsEmpty(users))
                {
                    DataRow dr = users.Tables[0].Rows[0];
                    string usrName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "UserName"), null);
                    string usrFullName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "FullName"), null);
                    userName = Functions.GetFormattedUserName(usrName, usrFullName, IsLiveSite);
                    // Store to request cache
                    RequestStockHelper.Add(key, userName);
                }
            }
        }

        return userName;
    }



    #region [Emerge Code]

    /// <summary>
    /// Check Table and column is present in relation master table
    /// if exist get actual primary table column value
    /// </summary>
    /// <param name="columnName">foreign table column name</param>
    /// <param name="tableName">foreign table name</param>
    /// <param name="columnValue">actual value in foreign table column</param>
    /// <returns></returns>
    private string GetColumnValue(string columnName, string tableName, string columnValue)
    {
        string returnValue = EmergeRelationHelper.GetRelationColumnValue(tableName, columnName, columnValue);
        if (returnValue == string.Empty)
        {
            returnValue = columnValue;
        }

        return returnValue;
    }

    private bool CheckRelationExist(string tableName, string columnName)
    {
        bool isExist = false;

        return isExist;
    }

    /// <summary>
    /// Method to return Table html for Group Control.
    /// </summary>
    /// <param name="ffi">form field Info</param>
    private string GetTableDetails(FormFieldInfo ffi)
    {
        StringBuilder sb = new StringBuilder();
        List<string> columnNames = CustomTableDataHelper.GetCustomTableSelectedColumns(ffi.Settings[Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY].ToString());
        FormInfo fi = FormHelper.GetFormInfo(ffi.Settings[Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY].ToString(), false);


        if (columnNames.Count == 0)
            columnNames.AddRange(GetExistingColumns(false, fi).Take(5));

        FormFieldInfo ffiInner = null;
        Dictionary<string, FormFieldInfo> innerFormFields = new Dictionary<string, FormFieldInfo>();
        string where = ffi.Settings[Constants.GROUP_CONTROL_RELATIONCOLUMNNAME_PROPERTY].ToString() + " =" + QueryHelper.GetInteger("ItemID", 0);
        DataSet customTableItems = CustomTableItemProvider.GetItems(ffi.Settings[Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY].ToString(), where, null);

        customTableItems = EmergeRelationHelper.GetRelationShipData(customTableItems, ffi.Settings[Constants.GROUP_CONTROL_CUSTOMTABLENAME_PROPERTY].ToString());
        if (!DataHelper.DataSourceIsEmpty(customTableItems))
        {
            sb.Append("<table class=\"table table-hover\">");
            // Get class form definition
            string HeaderContent = "<tr class=\"unigrid-head\">";
            string tableContent = string.Empty;
            string rowContent = string.Empty;

            foreach (string columnName in columnNames)
            {
                innerFormFields.Add(columnName, fi.GetFormField(columnName));
                HeaderContent += "<th class=\"main-column-100\">" + (innerFormFields[columnName].Caption == null ? columnName : innerFormFields[columnName].Caption.Trim() == string.Empty ? columnName : innerFormFields[columnName].Caption) + "</th>";
            }
            HeaderContent += "</tr>";
            int i = 0;
            foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
            {
                rowContent = "<tr>";
                foreach (string columnName in columnNames)
                {
                    ffiInner = innerFormFields[columnName];
                    if ((null != ffiInner.Settings["controlname"]) && (ffiInner.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower())))
                        rowContent += "<td class=\"wrap-normal\">" + GetTableDetails(ffiInner) + "</td>";
                    else
                        rowContent += "<td class=\"wrap-normal\">" + customTableItemDr[columnName].ToString() + "</td>";
                }
                rowContent += "</tr>";
                i++;
                tableContent += rowContent;
            }
            tableContent += "</table>";
            sb.Append(HeaderContent);
            sb.Append(tableContent);
        }
        return sb.ToString();
    }

    /// <summary>
    /// Gets existing columns from form info
    /// </summary>
    /// <param name="sort">Indicates if the columns should be sorted</param>
    private List<string> GetExistingColumns(bool sort, FormInfo fi)
    {
        var existingColumns = fi.GetColumnNames();
        if (sort)
        {
            existingColumns.Sort(StringComparer.InvariantCultureIgnoreCase);
        }

        return existingColumns;
    }


    /// <summary>
    /// Method to return preview for media file uploade control. For image file method will return html for image tag. For document method will return ancnhor tag.
    /// </summary>
    /// <param name="columnName">Column Name</param>
    private string GetUploadedFileDetails(string columnName)
    {

        string displayText = string.Empty;


        DataClassInfo customTable = DataClassInfoProvider.GetDataClassInfo(CustomTableItem.CustomTableClassName);
        string where = " ItemID = " + CustomTableItem.ItemID.ToString();

        CustomTableItem updateCustomTableItem = CustomTableItemProvider.GetItem(CustomTableItem.ItemID, customTable.ClassName);
        if (updateCustomTableItem.GetValue(columnName) != null)
        {
            Guid mguid = ValidationHelper.GetGuid(updateCustomTableItem.GetValue(columnName).ToString(), Guid.Empty);
            if (mguid != null)
            {

                MediaFileInfo mediaFile = MediaFileInfoProvider.GetMediaFileInfo(mguid, EmergeCMSContext.CurrentSiteName);
                if (mediaFile != null)
                {
                    string fileURL = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(mediaFile.FileGUID, mediaFile.FileName);

                    if (mediaFile.FileMimeType.ToLower().Contains("image"))
                    {
                        string queryParam = string.Empty;
                        if (mediaFile.FileImageHeight > Constants.MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY)
                        {
                            queryParam = "?Height=" + Constants.MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY.ToString();
                            queryParam += "&Width=" + Convert.ToInt16(mediaFile.FileImageWidth * Constants.MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY / mediaFile.FileImageHeight).ToString();

                        }
                        else if (mediaFile.FileImageWidth > Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY)
                        {
                            queryParam = "?Width=" + Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY.ToString();
                            queryParam += "&Height=" + Convert.ToInt16(mediaFile.FileImageHeight * Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY / mediaFile.FileImageWidth).ToString();
                        }

                        fileURL += queryParam;

                        displayText = "<img src='" + fileURL + "' alt='" + mediaFile.FileName + "' title='" + mediaFile.FileName + "' />";
                    }
                    else
                    {
                        displayText = "<a href='" + fileURL + "' target='_blank' >" + mediaFile.FileName + " </a>";
                    }

                }
            }
        }



        return displayText;
    }

    #endregion
}