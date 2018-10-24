using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Web.Controls;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.Membership;
using CMS.Helpers;
using CMS.FormEngine.Web.UI;


public partial class CMSModules_CMS_EventsCalendar_Controls_EventCalendarFormControl : CMSUserControl
{

    #region "Private variables"

    private string editItemPage = string.Empty;
    private DataClassInfo dci = null;
    private string mEditItemPageAdditionalParams = null;

    #endregion

    #region Events
    public event EventHandler OnBeforeSave;
    public event EventHandler OnAfterSave;
    #endregion


    #region "Public properties"

    /// <summary>
    /// Custom table class id.
    /// </summary>
    public int CustomTableId
    {
        get
        {
            return customTableForm.CustomTableId;
        }
        set
        {
            customTableForm.CustomTableId = value;
        }
    }

    /// <summary>
    /// Custom table class id.
    /// </summary>
    public string EditItemPage
    {
        get
        {
            return editItemPage;
        }
        set
        {
            editItemPage = value;
        }
    }



    /// <summary>
    /// Primary key value of the item being edited.
    /// </summary>
    public int ItemId
    {
        get
        {
            return customTableForm.ItemID;
        }
        set
        {
            customTableForm.ItemID = value;
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for edit page.
    /// </summary>
    public string EditItemPageAdditionalParams
    {
        get
        {
            return mEditItemPageAdditionalParams;
        }
        set
        {
            mEditItemPageAdditionalParams = value;
        }
    }

    /// <summary>
    /// If true, colon(:) is placed behind field label.
    /// </summary>
    public bool UseColonBehindLabel
    {
        get
        {
            return customTableForm.UseColonBehindLabel;
        }
        set
        {
            customTableForm.UseColonBehindLabel = value;
        }
    }

    /// <summary>
    /// Gets the custom table form control.
    /// </summary>
    public CustomTableForm CustomTableForm
    {
        get
        {
            return this.customTableForm;
        }
    }


    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
     SetupControl();
    }


    #region methods

    private void SetupControl()
    {
        if (CustomTableId > 0)
        {
            // Get form info
            dci = DataClassInfoProvider.GetDataClassInfo(CustomTableId);

            if (dci != null)
            {
                customTableForm.ShowPrivateFields = true;

                if (dci.ClassEditingPageURL != String.Empty)
                {
                    editItemPage = dci.ClassEditingPageURL;
                }
            }
        }

        if (!RequestHelper.IsPostBack() && QueryHelper.GetBoolean("saved", false))
        {
            customTableForm.ShowChangesSaved();
        }

        UseColonBehindLabel = true;
    }


    protected void customTableForm_OnBeforeSave(object sender, EventArgs e)
    {
        // If editing item
        if (ItemId > 0)
        {
            // Check 'Modify' permission
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Modify") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Modify"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.modify"), dci.ClassName), null, null);
                customTableForm.StopProcessing = true;
            }
        }
        else
        {
            // Check 'Create' permission
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Create") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Create"))
            {
                // Show error message
                customTableForm.MessagesPlaceHolder.ClearLabels();
                customTableForm.ShowError(String.Format(GetString("customtable.permissiondenied.create"), dci.ClassName), null, null);
                customTableForm.StopProcessing = true;
            }
        }

        if (OnBeforeSave != null)
        {
            OnBeforeSave(sender, e);
        }
    }


    protected void customTableForm_OnAfterSave(object sender, EventArgs e)
    {
        if (OnAfterSave != null)
        {
            OnAfterSave(sender, e);
        }
        if (!this.CustomTableForm.StopProcessing)
        {
            processAfterSave();
        }
    }

    public void processAfterSave()
    {
        string param = "&saved=1";

        // Include additional parameters if any
        param += (String.IsNullOrEmpty(EditItemPageAdditionalParams) ? String.Empty : "&" + EditItemPageAdditionalParams);

        // Reflect new in query string
        param += (QueryHelper.GetString("new", String.Empty) != String.Empty) ? "&new=1" : String.Empty;

        //Redirect to edit page with saved parameter and new itemId (new item)
        URLHelper.Redirect(URLHelper.GetAbsoluteUrl(editItemPage) + "?customtableid=" + CustomTableId + "&itemid=" + customTableForm.ItemID + param);

    }


    /// <summary>
    /// Check Allow empty false and set '*' after the form field caption.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void customTableForm_OnAfterDataLoad(object sender, EventArgs e)
    {
        customTableForm.SubmitButton.Visible = false;
        //Changed Ifield to IDataDefinitionItem
        List<IDataDefinitionItem> formitems = customTableForm.FormInformation.ItemsList;
        List<string> fields = new List<string>();
        dci = DataClassInfoProvider.GetDataClassInfo(CustomTableId);
        fields = EmergeStaticHelper.GetMandatoryFields(dci.ClassName);
        foreach (IField item in formitems)
        {
            FormFieldInfo fieldInfo = ((CMS.FormEngine.FormFieldInfo)(item));
            if (!fieldInfo.AllowEmpty || fields.Contains(fieldInfo.Name))
            {
                fieldInfo.Caption = fieldInfo.Caption + Constants.FORM_REQUIRED_FIELD_ASTERISK;
            }

        }
    }

    #endregion methods
}