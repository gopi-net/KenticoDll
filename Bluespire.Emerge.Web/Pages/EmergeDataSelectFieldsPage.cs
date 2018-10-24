using System;
using System.Collections;
using System.Web.UI.WebControls;
using CMS.FormEngine;
using CMS.UIControls;
using Bluespire.Emerge.Web.Pages.BasePages;
using Bluespire.Emerge.Common;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.Base;
using CMS.Membership;
using Bluespire.Emerge.CommonService;
using CMS.Base.Web.UI.ActionsConfig;
using CMS.Base.Web.UI;

namespace Bluespire.Emerge.Web.Pages
{
    /// <summary>
    /// Base class for data select fields page.
    /// </summary>
    public class EmergeDataSelectFieldsPage : EmergeModalPage
    {
        private DataClassInfo dci = null;

        private bool? isSiteManager;
        /// <summary>
        ///Gets whether this page is being accessed from site manager.
        /// </summary>
        protected bool IsSiteManager
        {
            get
            {
                if (!isSiteManager.HasValue)
                    isSiteManager = (QueryHelper.GetInteger("sm", 0) == 1);
                return isSiteManager.Value;
            }
        }

        private int? itemID;
        /// <summary>
        /// Gets the id of the current item.
        /// </summary>
        protected int ItemID
        {
            get
            {
                if (!itemID.HasValue)
                    itemID = QueryHelper.GetInteger("itemid", 0);
                return itemID.Value;
            }
        }

        private int? customTableID;
        /// <summary>
        /// Gets the id of the current custom table.
        /// </summary>
        protected int CustomTableID
        {
            get
            {
                if (!customTableID.HasValue)
                    customTableID = QueryHelper.GetInteger("customtableid", 0);
                if (customTableID == 0)
                {
                    string customTableName = QueryHelper.GetString("customtablename", string.Empty);
                    if (!String.IsNullOrEmpty(customTableName))
                    {
                        string className = string.Format(customTableName, SiteContext.CurrentSiteName);
                        DataClassInfo classInfo = CustomTableDataHelper.GetCustomTableClassInfo(className);
                        customTableID = classInfo.ClassID;
                    }
                }
                return customTableID.Value;
            }
        }

        private ListItemCollection items = new ListItemCollection();
        /// <summary>
        /// Gets or sets the Item collection.
        /// </summary>
        protected ListItemCollection Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            RequireSite = false;
            base.OnInit(e);
        }

        /// <summary>
        /// OnPageLoad method. This method should be called from page_load event in the page.
        /// </summary>
        protected void OnPageLoad()
        {
            CurrentMaster.Title.TitleText = ResHelper.GetString("customtable.data.selectdisplayedfields");
           //CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/selectfields.png");
            CurrentMaster.DisplayActionsPanel = true;

            dci = DataClassInfoProvider.GetDataClassInfo(CustomTableID);
            
            EditedObject = dci;

            // If class exists
            if (dci != null)
            {
                // Check 'Read' permission
                if (CheckForPermissions())
                {
                    ShowError(String.Format(GetString("customtable.permissiondenied.read"), dci.ClassName));
                    return;
                }

                HeaderActions.AddAction(new HeaderAction()
                {
                  //  ImageUrl = GetImageUrl("Design/Controls/UI/selectall.png"),
                    Text = ResHelper.GetString("UniSelector.SelectAll"),
                    OnClientClick = "ChangeFields(true); return false;"
                });

                HeaderActions.AddAction(new HeaderAction()
                {
                    //ImageUrl = GetImageUrl("Design/Controls/UI/deselectall.png"),
                    Text = ResHelper.GetString("UniSelector.DeselectAll"),
                    OnClientClick = "ChangeFields(false); return false;"
                });

                Hashtable reportFields = new Hashtable();

                FormInfo fi = null;

                if (!RequestHelper.IsPostBack())
                {
                    // Get report fields
                    if (!String.IsNullOrEmpty(dci.ClassShowColumns))
                    {
                        reportFields.Clear();

                        foreach (string field in dci.ClassShowColumns.Split(';'))
                        {
                            // Add field key to hastable
                            reportFields[field] = null;
                        }
                    }

                    // Get columns names
                    fi = FormHelper.GetFormInfo(dci.ClassName, false);
                    var columnNames = fi.GetColumnNames();

                    if (columnNames != null)
                    {
                        FormFieldInfo ffi = null;
                        ListItem item = null;
                        ListItemCollection items = new ListItemCollection();
                        foreach (string name in columnNames)
                        {
                            ffi = fi.GetFormField(name);

                    //Check control is emerge group control or emerge file upload control if not then only add into select display fields
                            if ((null == ffi.Settings["controlname"]) || ((!ffi.Settings["controlname"].ToString().ToLower().Equals(Constants.GROUP_CONTROL_CODE_NAME.ToLower())) && (!ffi.Settings["controlname"].ToString().Equals(Constants.MEDIA_FILE_UPLOADER_CONTROL_CODE_NAME))))
                            {

                                // Add checkboxes to the list
                                item = new ListItem(HTMLHelper.HTMLEncode(ResHelper.LocalizeString(GetFieldCaption(ffi, name))), name);
                                if (reportFields.Contains(name))
                                {
                                    // Select checkbox if field is reported
                                    item.Selected = true;
                                }
                                items.Add(item);
                            }
                        }
                        Items = items;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the click event of the OK button.
        /// </summary>
        protected void OnBtnOKClick()
        {
            if (dci != null)
            {
                string reportFields = null;

                foreach (ListItem item in Items)
                {
                    // Display only selected fields
                    if (item.Selected)
                    {
                        reportFields += item.Value + ";";
                    }
                }


                if (!string.IsNullOrEmpty(reportFields))
                {
                    // Remove ending ';'
                    reportFields = reportFields.TrimEnd(';');
                }


                // Save report fields
                if (String.Compare(dci.ClassShowColumns, reportFields, true) != 0)
                {
                    dci.ClassShowColumns = reportFields;
                    DataClassInfoProvider.SetDataClassInfo(dci);
                }

                // Close dialog window
                ScriptHelper.RegisterStartupScript(this, typeof(string), "CustomTable_SelectFields", "CloseAndRefresh();", true);
            }
        }

        /// <summary>
        /// Checks if the current user has the required permissions to access the resources on this page.
        /// </summary>
        /// <returns></returns>
        protected bool CheckForPermissions()
        {
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                    !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
            {
                return true;
            }
            return false;
        }

        private string GetFieldCaption(FormFieldInfo ffi, string columnName)
        {
            // Get field caption        
            return ((ffi == null) || (ffi.Caption == string.Empty)) ? columnName : ffi.Caption;
        }

    }
}
