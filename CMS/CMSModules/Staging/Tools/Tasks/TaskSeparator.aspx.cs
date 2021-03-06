using System;

using CMS.Base;
using CMS.Helpers;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.Base.Web.UI;
using CMS.Synchronization.Web.UI;

[UIElement("CMS.Staging", "Documents")]
public partial class CMSModules_Staging_Tools_Tasks_TaskSeparator : CMSStagingPage
{
    #region "Properties"

    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check enabled servers
        if (!ServerInfoProvider.IsEnabledServer(SiteContext.CurrentSiteID))
        {
            ShowInformation(GetString("ObjectStaging.NoEnabledServer"));
        }
        else
        {
            // Check DLL required for for staging
            if (SystemContext.IsFullTrustLevel)
            {
                URLHelper.Redirect("Frameset.aspx");
            }

            ShowInformation(GetString("objectstaging.fulltrustrequired"));
        }
    }
}