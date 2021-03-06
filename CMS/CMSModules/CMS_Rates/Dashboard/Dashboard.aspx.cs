﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.LicenseProvider;
using CMS.Modules;
using CMS.PortalEngine;
using CMS.UIControls;
using Bluespire.Emerge.Components.Rates.Pages;
using CMS.SiteProvider;
using CMS.Base;
using CMS.DocumentEngine;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using System.Web.UI.WebControls;

public partial class CMSModules_CMS_Rates_Dashboard : RatesDashboardPage
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