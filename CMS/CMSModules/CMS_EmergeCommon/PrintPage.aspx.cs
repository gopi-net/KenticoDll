using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Web.Pages.BasePages;
using CMS.PortalEngine;

public partial class CMSModules_CMS_EmergeCommon_PrintPage : EmergeModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = PortalContext.CurrentSiteStylesheetName;
    }
    
}