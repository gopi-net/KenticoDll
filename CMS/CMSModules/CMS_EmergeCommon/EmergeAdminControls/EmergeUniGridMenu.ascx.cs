using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Helpers;
using CMS.Base.Web.UI;

public partial class CMSFormControls_EmergeAdminControls_EmergeUnigrid_Menu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string menuId = ContextMenu.MenuID;
        string parentElemId = ContextMenu.ParentElementClientID;

        string parameterScript = "GetContextMenuParameter('" + menuId + "')";        

        //string actionPattern = "UG_Export_" + parentElemId + "('{0}', " + parameterScript + ");";
        string actionPattern = "window.CMS.UG_Export_" + parentElemId + ".ugExport('{0}', " + parameterScript + ");";

        // Initialize menu
        imgExcel.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/excelexport.png");
        lblExcel.Text = ResHelper.GetString("export.exporttoexcel");
        pnlExcel.Attributes.Add("onclick",ScriptHelper.GetDisableProgressScript() + String.Format(actionPattern, DataExportFormatEnum.XLSX));        

        imgAdvancedExport.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/advancedexport.png");
        lblAdvancedExport.Text = ResHelper.GetString("export.advancedexport");
        pnlAdvancedExport.Attributes.Add("onclick", string.Format(actionPattern, "advancedexport"));        
    }
}