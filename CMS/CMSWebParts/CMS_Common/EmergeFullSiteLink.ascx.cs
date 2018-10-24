using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

public partial class CMSWebParts_CMS_Common_EmergeFullSiteLink : CMSAbstractWebPart
{
    /// <summary>
    /// Small device redirection URL.
    /// </summary>
    public string FullSiteLink
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("FullSiteLink"), string.Empty);
        }
        set
        {
            SetValue("FullSiteLink", value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void fullsiteLinkButton_Click(object sender, EventArgs e)
    {
        CookieHelper.SetValue(Constants.COOKIE_NAME_MOBILEFULLSITELINK,"1", DateTime.Now.AddSeconds(4));
        if (!string.IsNullOrEmpty(FullSiteLink))
            EmergeURLHelper.Redirect(FullSiteLink);
    }
}