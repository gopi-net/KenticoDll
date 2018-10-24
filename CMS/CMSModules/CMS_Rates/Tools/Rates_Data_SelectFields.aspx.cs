using System;
using System.Collections;
using System.Web.UI.WebControls;

using CMS.Base;
using CMS.DataEngine;
using CMS.Base.Web.UI.ActionsConfig;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.MacroEngine;
using CMS.Membership;
using CMS.SiteProvider;
using CMS.UIControls;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Rates;
[UIElement("CMS.CustomTables", "CustomTables", false, false)]
public partial class CMSModules_Rates_Tools_Rates_Data_SelectFields : RatesDataSelectFieldsPage
{

    #region "Page events"

    protected void Page_Init(object sender, EventArgs e)
    {
        Save += btnOK_Click;
        RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();
            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (!EmergeRequestHelper.IsPostBack())
            {
                foreach (ListItem item in Items)
                {
                    chkListFields.Items.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Button OK clicked.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (ListItem item in chkListFields.Items)
            {
                Items.Add(item);
            }
            base.OnBtnOKClick();
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }
    #endregion
}