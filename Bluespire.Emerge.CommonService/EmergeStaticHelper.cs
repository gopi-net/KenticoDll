using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing.Imaging;
using System.Web;
using Bluespire.Emerge.Common;
using CMS.SiteProvider;
using Bluespire.Emerge.CommonService.GridActions;
using System.Reflection;
using Bluespire.Emerge.Common.Exceptions;
using CMS.MembershipProvider;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;
using CMS.Membership;
using CMS.CustomTables;
using CMS.DataEngine;
using System.Data;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;
namespace Bluespire.Emerge.CommonService
{
    public static class EmergeStaticHelper
    {

        public static Dictionary<string, Func<IGridAction>> GridActions = new Dictionary<string, Func<IGridAction>>{
            {"delete", () => new GridDeleteAction()},
            {"deactivate", () => new GridDeactivateAction()},
            {"activate", () => new GridActivateAction()},
            {"moveup", () => new GridMoveUpAction()},
            {"movedown", () => new GridMoveDownAction()},
            {"edit", () => new GridCustomTableEditAction()},
            {"deletelicense",() => new GridDeleteLicenseAction()}
        };

        /// <summary>
        /// Create datatable to html.
        /// </summary>
        /// <param name="targetTable"></param>
        /// <returns></returns>
        public static string ConvertDataTableToHtml(DataTable targetTable)
        {
            string htmlString = string.Empty;

            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
            }

            StringBuilder htmlBuilder = new StringBuilder();

            //Create Top Portion of HTML Document
            htmlBuilder.Append("<html>");
            htmlBuilder.Append("<head>");
            htmlBuilder.Append("<title>");
            htmlBuilder.Append("Page-");
            htmlBuilder.Append(Guid.NewGuid().ToString());
            htmlBuilder.Append("</title>");
            htmlBuilder.Append("</head>");
            htmlBuilder.Append("<body>");
            htmlBuilder.Append("<table cellpadding='5' cellspacing='0' border='0' BORDERCOLOR='black' >");
            //htmlBuilder.Append("style='border: solid 1px Black; font-size: small;'>");

            //Create Header Row
            htmlBuilder.Append("<tr align='left' valign='top' font-size='4'>");

            foreach (DataColumn targetColumn in targetTable.Columns)
            {
                htmlBuilder.Append("<td  align='left' valign='top'><b>");
                htmlBuilder.Append(targetColumn.ColumnName);
                htmlBuilder.Append("</b></td>");
            }

            htmlBuilder.Append("</tr>");

            //Create Data Rows
            foreach (DataRow myRow in targetTable.Rows)
            {
                htmlBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn targetColumn in targetTable.Columns)
                {
                    htmlBuilder.Append("<td align='left' valign='top'>");
                    if (targetColumn.ColumnName == "ItemCost" || targetColumn.ColumnName == "ItemTotal")
                    {
                        htmlBuilder.Append(string.Format("{0:c2}", myRow[targetColumn.ColumnName]));
                    }
                    else
                        htmlBuilder.Append(myRow[targetColumn.ColumnName].ToString());
                    htmlBuilder.Append("</td>");
                }

                htmlBuilder.Append("</tr>");
            }

            //Create Bottom Portion of HTML Document
            htmlBuilder.Append("</table>");
            htmlBuilder.Append("</body>");
            htmlBuilder.Append("</html>");

            //Create String to be Returned
            htmlString = htmlBuilder.ToString();

            return htmlString;
        }

        /// <summary>
        /// Return the desired configuration setting from Web.config. If the value was not found, 
        /// the function will return the "defaultValue" value.
        /// </summary>
        /// <param name="configurationName">Name of the key from the AppSettings section of Web.config</param>
        /// <param name="defaultValue">Value to return if the key was not found.</param>
        /// <returns>String containing the value from Web.config or the defaultValue.</returns>
        public static string GetConfigurationString(string configurationName, string defaultValue)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[configurationName];
            if ((value == null) || (value == string.Empty))
                value = defaultValue;

            return value;
        }

        public static string GetCustomConfigString(string configurationName, string defaultValue)
        {
            string value = String.Empty;
            NameValueCollection configSect = (NameValueCollection)ConfigurationManager.GetSection("customServiceProvider");
            foreach (string Key in configSect)
            {
                value = configSect.Get(configurationName);
                if ((value == null) || (value == string.Empty))
                    value = defaultValue;
            }
            return value;
        }
        /// <summary>
        /// Reads the file specified and returns all its content as a single string
        /// </summary>
        /// <param name="fullpath">path of the specified file </param>
        /// <returns>contents of the file specified </returns>
        public static string GetTemplateFileContent(string fullpath)
        {
            string data = "";

            try
            {
                StreamReader sr = new StreamReader(fullpath);
                data = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            catch (Exception ex)
            {
                data = "";
            }
            return data;
        }

        /// <summary>
        /// Make selections in the provided list control from the given list of strings
        /// </summary>
        /// <param name="list">Any ASP.NET control that has a ListItem child</param>
        /// <param name="selectionTextList">A list of strings. Items in list that correspond to items in this parameter will be marked selected.</param>
        public static void SetListSelection(ListControl list, List<string> selectionTextList)
        {
            bool isSingleSelection = false;
            list.ClearSelection();

            if ((list.GetType() == typeof(DropDownList)) || (list.GetType() == typeof(RadioButtonList)))
            {
                isSingleSelection = true;
            }

            foreach (ListItem item in list.Items)
            {
                foreach (string s in selectionTextList)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;
                        if (isSingleSelection)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public static void SetValidationGroup(UserControl control, string group)
        {
            foreach (object item in control.Controls)
            {
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RegularExpressionValidator"))
                    ((RegularExpressionValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RequiredFieldValidator"))
                    ((RequiredFieldValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CompareValidator"))
                    ((CompareValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CustomValidator"))
                    ((CustomValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RangeValidator"))
                    ((RangeValidator)item).ValidationGroup = group;
                if (((Control)item).Controls.Count > 0)
                {
                    SetValidationGroup((Control)item, group);
                }
            }
        }

        public static void SetValidationGroup(Control control, string group)
        {
            foreach (object item in control.Controls)
            {
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RegularExpressionValidator"))
                    ((RegularExpressionValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RequiredFieldValidator"))
                    ((RequiredFieldValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CompareValidator"))
                    ((CompareValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CustomValidator"))
                    ((CustomValidator)item).ValidationGroup = group;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RangeValidator"))
                    ((RangeValidator)item).ValidationGroup = group;
                if (((Control)item).Controls.Count > 0)
                {
                    SetValidationGroup((Control)item, group);
                }
            }
        }


        public static void ResetValue(UserControl control)
        {
            foreach (object item in control.Controls)
            {
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    ((TextBox)item).Text = "";
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                    ((DropDownList)item).SelectedIndex = -1;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.ListBox"))
                    ((ListBox)item).SelectedIndex = -1;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RadioButton"))
                    ((RadioButton)item).Checked = false;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                    ((CheckBox)item).Checked = false;
                if (((Control)item).Controls.Count > 0)
                {
                    ResetValue((Control)item);
                }
            }
        }

        public static void ResetValue(Control control)
        {
            foreach (object item in control.Controls)
            {

                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                    ((TextBox)item).Text = "";
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                    ((DropDownList)item).SelectedIndex = -1;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.ListBox"))
                    ((ListBox)item).SelectedIndex = -1;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.RadioButton"))
                    ((RadioButton)item).Checked = false;
                if (item.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                    ((CheckBox)item).Checked = false;
                if (((Control)item).Controls.Count > 0)
                {
                    ResetValue((Control)item);
                }
            }
        }

        public static void ResetValue(Panel control)
        {
            foreach (object item in control.Controls)
            {
                string itemType = item.GetType().ToString();
                if (((Control)item).Controls.Count > 0)
                {
                    ResetValue((Control)item);
                }
                switch (itemType)
                {
                    case "System.Web.UI.WebControls.TextBox": ((TextBox)item).Text = string.Empty;
                        break;
                    case "System.Web.UI.WebControls.DropDownList": ((DropDownList)item).SelectedIndex = -1;
                        break;
                    case "System.Web.UI.WebControls.ListBox": ((ListBox)item).SelectedIndex = -1;
                        break;
                    case "System.Web.UI.WebControls.RadioButton": ((RadioButton)item).Checked = false;
                        break;
                    case "System.Web.UI.WebControls.CheckBox": ((CheckBox)item).Checked = false;
                        break;
                    case "CMS.Base.Web.UI.CMSTextBox": ((CMSTextBox)item).Text = string.Empty;
                        break;
                    case "CMS.Base.Web.UI.LocalizedDropDownList": ((LocalizedDropDownList)item).SelectedIndex = -1;
                        break;
                    case "CMS.Base.Web.UI.LocalizedRadioButton": ((LocalizedRadioButton)item).Checked = false;
                        break;
                    case "CMS.Base.Web.UI.LocalizedCheckBox": ((LocalizedCheckBox)item).Checked = false;
                        break;

                }


            }
        }




        /// <summary>
        /// Resize the Image
        /// </summary>        
        /// <param name="ImageWidth">Image Width</param>
        /// <param name="ImageURL">Image URL</param>
        public static void ResizeImage(int ImageWidth, string ImageURL)
        {
            System.Drawing.Image g = System.Drawing.Image.FromFile(ImageURL);
            ImageFormat thisformat = g.RawFormat;
            int iWidth = g.Width;
            int iHeight = g.Height;
            iHeight = iHeight * ImageWidth / iWidth;
            //if (iWidth > ImageWidth)
            //{
            Bitmap imgOutput = new Bitmap(g, ImageWidth, iHeight);
            g.Dispose();
            System.IO.File.Delete(ImageURL);
            // save the resized image
            imgOutput.Save(ImageURL, thisformat);
            //tidy up
            imgOutput.Dispose();
            //}
        }


        /// <summary>
        /// Delete file from Server
        /// </summary>
        /// <param name="FileUrl"></param>
        public static void DeleteFile(string FileUrl)
        {
            try
            {
                FileInfo DeleteFile = new FileInfo(FileUrl);
                if (DeleteFile.Exists)
                {
                    File.Delete(FileUrl);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        /// <summary>
        /// Accept file name and grid view object and create excel file
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <param name="gv">Pass Grid view Object</param>
        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  include the gridline settings
                    table.GridLines = gv.GridLines;

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }
                else if (current is Label)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as Label).Text));
                }
                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }


        /// <summary>
        /// Get value of node from XML file
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetValueFromXMLFile(string nodeId, string filePath)
        {
            string strMessage = "";
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.RemoveAll();
                xmldoc.Load(filePath);
                XmlNodeList xmlnodelist = xmldoc.DocumentElement["content"].ChildNodes;
                for (int i = 0; i < xmlnodelist.Count; i++)
                {
                    if (xmlnodelist[i].Attributes[0].Value == nodeId)
                    {
                        strMessage = xmlnodelist[i].Attributes[1].Value;
                        break;
                    }
                }
            }
            catch
            {
                strMessage = "";
            }
            return strMessage;
        }

        /// <summary>
        /// get the month name from integer
        /// </summary>
        /// <param name="iMonth">valid month number</param>
        /// <returns>Respective month name</returns>
        public static string GetMonth(int iMonth)
        {
            string sMonth = null;
            switch (iMonth)
            {
                case 1: sMonth = "January"; break;
                case 2: sMonth = "February"; break;
                case 3: sMonth = "March"; break;
                case 4: sMonth = "April"; break;
                case 5: sMonth = "May"; break;
                case 6: sMonth = "June"; break;
                case 7: sMonth = "July"; break;
                case 8: sMonth = "August"; break;
                case 9: sMonth = "September"; break;
                case 10: sMonth = "October"; break;
                case 11: sMonth = "November"; break;
                case 12: sMonth = "December"; break;


            }

            return sMonth;
        }

        /// <summary>
        /// Pass Control and get the control value
        /// </summary>
        /// <param name="control">control</param>
        /// <returns>control value return in a string datatype if value not found or got error then return value is blank text.</returns>
        public static string GetControlValue(Control control)
        {


            string key = control.ID;
            string value = string.Empty;

            switch (control.GetType().ToString())
            {
                case Constants.TYPE_TEXTBOX:
                    value = ((TextBox)control).Text;
                    break;
                case Constants.TYPE_DROPDOWNLIST:

                    if (((DropDownList)control).SelectedItem != null)
                    {
                        value = ((DropDownList)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;
                case Constants.TYPE_LISTBOX:
                    if (((ListBox)control).SelectedItem != null)
                    {
                        value = ((ListBox)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;
                case Constants.TYPE_CHECKBOX:
                    value = ((CheckBox)control).Checked.ToString();

                    break;
                case Constants.TYPE_CHECKBOXLIST:
                    if (((CheckBoxList)control).SelectedItem != null)
                    {
                        value = ((CheckBoxList)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;

                case Constants.TYPE_RADIOBUTTON:
                    value = ((RadioButton)control).Checked.ToString();
                    break;

                case Constants.TYPE_RADIOBUTTONLIST:
                    if (((RadioButtonList)control).SelectedItem != null)
                    {
                        value = ((RadioButtonList)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;

                case Constants.TYPE_CMS_TEXTBOX:
                    value = ((CMSTextBox)control).Text;
                    break;
                case Constants.TYPE_LOCALIZED_DROPDOWNLIST:
                    if (((LocalizedDropDownList)control).SelectedItem != null)
                    {
                        value = ((LocalizedDropDownList)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;
                case Constants.TYPE_LOCALIZED_RADIOBUTTONLIST:
                    if (((LocalizedRadioButtonList)control).SelectedItem != null)
                    {
                        value = ((LocalizedRadioButtonList)control).SelectedItem.Value;
                        if (value == "0" || value == "-1")
                        {
                            value = string.Empty;
                        }
                    }
                    break;

                case Constants.TYPE_LOCALIZED_RADIOBUTTON:
                    value = ((LocalizedRadioButton)control).Checked.ToString();
                    break;
                case Constants.TYPE_LOCALIZED_CHECKBOX:
                    value = ((LocalizedCheckBox)control).Checked.ToString();
                    break;

            }


            return value;

        }

        /// <summary>
        /// replace "_EmergeWebsite_" or {0} word with current site name
        /// </summary>
        /// <param name="value">string contain "EmergeWebSite" word or {0} </param>
        /// <returns></returns>
        public static string SetSiteName(string value)
        {
            if (value.Contains("{0}"))
            {
                value = string.Format(value, SiteContext.CurrentSiteName);
            }
            value = value.Replace(Constants.EMERGE_WEBSITE.ToLower(), SiteContext.CurrentSiteName);
            return value;
        }

        /// <summary>
        /// Method to load datasource into ListControl
        /// </summary>
        /// <param name="setDefaultValue">allow to set default value</param>

        public static void LoadListControls(bool allowDefault, Control ControlPanel)
        {
            Control datasource = null;
            if (ControlPanel != null)
            {
                foreach (Control control in ControlPanel.Controls.OfType<ListControl>())
                {

                    string ID = ((ListControl)control).ID + "_" + "Datasource";
                    datasource = ControlPanel.FindControl(ID);
                    ((CMSQueryDataSource)datasource).QueryName = EmergeStaticHelper.SetSiteName(((CMSQueryDataSource)datasource).QueryName);// string.Format(((CMSQueryDataSource)datasource).QueryName, CMS.CMSHelper.CMSContext.CurrentSiteName);
                    if (datasource != null)
                    {
                        ((ListControl)control).DataSource = ((CMSQueryDataSource)datasource).DataSource;
                        ((ListControl)control).DataBind();
                        if (allowDefault)
                            ((ListControl)control).Items.Insert(0, new ListItem(Constants.DROPDOWN_DEFAULT_TEXT, Constants.DROPDOWN_DEFAULT_VALUE));
                    }


                }
            }
        }


        /// <summary>
        /// retunrs querystring
        /// </summary>
        /// <param name="QueryStringParameters">Dictionary item for Query String Parameters.</param>
        /// <returns></returns>
        public static string FormQueryString(Dictionary<string, string> QueryStringParameters)
        {
            string parameter = string.Empty;
            string QueryString = string.Empty;
            foreach (KeyValuePair<string, string> queryStringParameter in QueryStringParameters)
            {
                parameter += queryStringParameter.Key + "," + queryStringParameter.Value;
                parameter += ",";
            }

            QueryString = QueryHelper.BuildQuery(parameter.Split(new char[] { ',' }));
            return QueryString;
        }

        /// <summary>
        /// Checks whether the current property has the attribute
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool HasAttribute(PropertyInfo property, Type type)
        {
            if (property.GetCustomAttributes(type, false).Length > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Gets the customized name for the property having attribute
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetFieldNameForProperty(PropertyInfo field)
        {
            object[] exportattr = field.GetCustomAttributes(typeof(CustomTableFieldAttribute), false);
            if (exportattr.Length > 0)
            {
                if (exportattr[0] != null)
                {
                    CustomTableFieldAttribute attribute = ((CustomTableFieldAttribute)exportattr[0]);
                    return attribute.FieldName;
                }
            }
            return field.Name;
        }

        /// <summary>
        /// Gets the fields for the custom table represented by the entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetFieldsOf<T>()
        {
            List<string> fields = new List<string>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (HasAttribute(property, typeof(CustomTableFieldAttribute)))
                    fields.Add(GetFieldNameForProperty(property));
            }

            return fields;
        }

        public static string[] GetPropertyNameByCustomAttribute<T, R>(Func<R, bool> attributePredicate) where R : Attribute
        {
            if (attributePredicate == null)
            {
                throw new ArgumentNullException("attributePredicate");
            }
            else
            {
                List<string> propertyNames = new List<string>();

                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    if (propertyInfo.GetCustomAttributes(typeof(R), true).Any(currentAttribute => attributePredicate((R)currentAttribute)))
                    {
                        propertyNames.Add(propertyInfo.Name);
                    }
                }

                return propertyNames.ToArray();
            }
        }

        /// <summary>
        /// Method will convert Dictionary into 2 D array of strings.
        /// </summary>
        /// <param name="sourceDictionary"></param>
        /// <returns>2 D String Array containing all element available in Dictionary</returns>
        public static string[,] GetMacrosForEmailTemplate(Dictionary<string, object> sourceDictionary)
        {

            string[,] macros = new string[sourceDictionary.Keys.Count, 2];
            int itemCounter = 0;

            foreach (KeyValuePair<string, object> item in sourceDictionary)
            {
                macros[itemCounter, 0] = item.Key;
                macros[itemCounter, 1] = item.Value.ToString();
                itemCounter++;
            }

            return macros;
        }


        public static bool CheckUserbyUserName(string userName, int userID)
        {
            UserInfo user = UserInfoProvider.GetUserInfo(userName);

            if (user != null && user.UserID != userID)
            {
                EmergeLogWriter.WriteError("EmergeStaticHelper:CheckUserbyUserName", EventCode.EMERGE_USEREXISTS, String.Format("User with user name {0} already exists.", userName));
                throw new UserWithUserNameExistsException(String.Format("User with user name {0} already exists.", userName));
            }
            return false;
        }

        public static bool CheckUserByEmail(string email, int userID)
        {
            string where = "Email like '%" + email + "%'";
            ObjectQuery<UserInfo> ds = UserInfoProvider.GetUsers();
            ds = ds.Where(where);
            bool isExists = ds.ToList<UserInfo>().Any<UserInfo>(a => a.Email == email && a.UserID != userID);
            if (isExists)
            {
                EmergeLogWriter.WriteError("EmergeStaticHelper:CheckUserByEmail", EventCode.EMERGE_USEREXISTS, String.Format("User with email {0} already exists.", email));
                throw new UserWithEmailExistsException(String.Format("User with email {0} already exists.", email));
            }
            return false;
        }

        public static RoleInfo CreateRole(string displayName, string roleName, int siteID)
        {
            RoleInfo roleInfo = new RoleInfo();
            roleInfo.RoleName = roleName;
            roleInfo.RoleDisplayName = displayName;
            roleInfo.SiteID = siteID;
            RoleInfoProvider.SetRoleInfo(roleInfo);
            return roleInfo;
        }

        public static bool IsFieldExcludedForViewPage(string columnName, string className)
        {
            string whereCondition = Constants.FIELDS_VIEWPAGEEXCLUDEDFIELDS_COLUMNNAME + " = '" + columnName + "' AND " + Constants.FIELDS_VIEWPAGEEXCLUDEDFIELDS_CUSTOMTABLE + " = '" + className + "'";
            List<CustomTableItem> items = CustomTableDataHelper.GetCustomTableItemsByCriteria(String.Format(Constants.CUSTOMTABLE_VIEWPAGEEXCLUDEDFIELDS, SiteContext.CurrentSiteName), whereCondition);
            if (items.Count > 0)
                return true;

            return false;
        }

        public static List<string> GetMandatoryFields(string className)
        {
            string whereCondition = Constants.FIELDS_MANDATORYFIELDS_CUSTOMTABLE + " = '" + className + "'";
            List<CustomTableItem> items = CustomTableDataHelper.GetCustomTableItemsByCriteria(String.Format(Constants.CUSTOMTABLE_MANDATORYFIELDS, SiteContext.CurrentSiteName), whereCondition);
            List<string> fields = new List<string>();
            foreach (CustomTableItem item in items)
                fields.Add(ValidationHelper.GetString(item.GetValue(Constants.FIELDS_MANDATORYFIELDS_FIELDNAME), string.Empty));

            return fields;
        }
    }
}
