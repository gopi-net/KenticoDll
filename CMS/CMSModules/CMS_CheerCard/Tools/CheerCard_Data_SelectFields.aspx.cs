using System;
using System.Collections;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.CheerCard;

public partial class CMSModules_CheerCard_Tools_CheerCard_Data_SelectFields : CheerCardDataSelectFieldsPage
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