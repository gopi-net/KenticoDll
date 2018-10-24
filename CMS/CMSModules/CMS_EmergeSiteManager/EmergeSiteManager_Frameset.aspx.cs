using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.Ecommerce;
using CMS.UIControls;
using CMS.Helpers;

public partial class CMSModules_Emerge_Pages_Tools_EmergeSiteManager_Frameset : CMSDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frameMenu.Attributes.Add("src", "EmergeSiteManagerHeader.aspx" + RequestContext.URL.Query);
    }
}