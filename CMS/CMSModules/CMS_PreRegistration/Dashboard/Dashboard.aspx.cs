using CMS.UIControls;
using System;
using Bluespire.Emerge.Components.PreRegistration.Pages;
using System.Web.UI.WebControls;
using CMS.LicenseProvider;
using CMS.Helpers;

public partial class CMSModules_CMS_PreRegistration_Dashboard : PreRegistrationDashboardPage
{
    #region "Page events"
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterEvents();
        RequestContext.ClientApplication.Add("applicationName", ResHelper.GetString("Dashboard"));
        LicenseValidationEnum licenseCheck = LicenseHelper.ValidateLicenseForDomain(RequestContext.CurrentDomain);
        if (licenseCheck != LicenseValidationEnum.Valid)
        {
            URLHelper.ResponseRedirect(URLHelper.ResolveUrl("~/CMSMessages/invalidlicensekey.aspx"));
        }
        SetupDashboard();
    }

    private void RegisterEvents()
    {
        repOuter.ItemDataBound += repOuter_ItemDataBound;
    }

    void repOuter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Repeater repInner = (Repeater)e.Item.FindControl("repInner") as Repeater;
        repInner.ItemDataBound += repeater_ItemDataBound;
        repInner.ItemCommand += repeater_ItemCommand;
        HiddenField hdnElementID = (HiddenField)e.Item.FindControl("hdnElementId") as HiddenField;
        int itemId = Convert.ToInt32(hdnElementID.Value);
        SetupInnerRepeater(repInner, itemId);
    }

    void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        base.repeater_ItemDataBound(sender, e);
    }

    void repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        base.repeater_ItemCommand(source, e);
    }

    #endregion

    #region "Private methods"
    private void SetupDashboard()
    {
        var applications = GetApplications(Request.Url.AbsolutePath);

        if ((applications == null) || DataHelper.DataSourceIsEmpty(applications))
        {
            plcDashboard.Visible = false;
            plcEmpty.Visible = true;
        }
        else
        {
            repOuter.DataSource = applications.Tables[0].DefaultView;
            repOuter.DataBind();
            plcDashboard.Visible = true;
            plcEmpty.Visible = false;
        }
    }
    #endregion
}