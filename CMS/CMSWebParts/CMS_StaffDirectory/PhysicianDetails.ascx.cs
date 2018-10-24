using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory.WebParts;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;

public partial class CMSWebParts_CMS_StaffDirectory_PhysicianDetails : StaffDirectorySearchResultWebpart
{

    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    #region [Webparts Properties]
    public string TransformationName
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("TransformationName"), repResults.TransformationName);
        }
        set
        {
            SetValue("TransformationName", value);
            repResults.TransformationName = value;
        }
    }
    public string StaffDefaultImagePath
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("StaffDefaultImagePath"), "");
        }
        set
        {
            SetValue("StaffDefaultImagePath", value);
        }
    }
    public string BackToResultPage
    {
        get
        {

            return EmergeValidationHelper.GetString(GetValue("BackToResultPage"), NewSearchUrl);
        }
        set
        {
            if (EmergeSessionHelper.GetValue("StaffDirectoryResultPage") != null)
            {
                SetValue("BackToResultPage", Convert.ToString(EmergeSessionHelper.GetValue("StaffDirectoryResultPage")));
            }
            else
            {
                SetValue("BackToResultPage", NewSearchUrl);
            }
        }
    }
    public string NewSearchUrl
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("NewSearchUrl"), "");
        }
        set
        {
            SetValue("NewSearchUrl", value);
        }

    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = ResultPanel;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ResultPanel.Visible = false;
            return;
        }

        if (EmergeSessionHelper.GetValue("StaffDirectoryResultPage") != null)
            BackToResultPage = Convert.ToString(EmergeSessionHelper.GetValue("StaffDirectoryResultPage"));
        else
            BackToResultPage = NewSearchUrl;
        if (!IsPostBack)
            LoadListControls(false);
        LoadPhysician();
    }
    protected override void OnPreRender(EventArgs e)
    {
        RenderMetaTags();
    }
    protected void btnBackToResult_Click(object sender, EventArgs e)
    {
        EmergeURLHelper.Redirect(BackToResultPage);
    }
    protected void btnNewSearch_Click(object sender, EventArgs e)
    {
        EmergeURLHelper.Redirect(NewSearchUrl);
    }
    protected void repResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnItemId = (HiddenField)e.Item.Controls[0].FindControl("hdnItemId");
            if (hdnItemId != null)
            {
                foreach (CMSRepeater repeater in e.Item.Controls[0].Controls.OfType<CMSRepeater>())
                {
                    string relationName = repeater.ID.ToLower().Replace("repeater_", string.Empty);
                    repeater.TransformationName = EmergeStaticHelper.SetSiteName(repeater.TransformationName);
                    DataSet ds = GetRelationTableData(relationName, hdnItemId.Value);
                    repeater.DataSource = ds;
                    repeater.Visible = !EmergeDataHelper.DataSourceIsEmpty(ds);
                    repeater.DataBind();
                }
            }
        }
    }
    #endregion

    #region Private functions
    private void LoadPhysician()
    {
        repResults.ZeroRowsText = EmergeResHelper.GetString("Emerge.StaffDirectory.SearchResultNotFound");
        repResults.TransformationName = TransformationName;
        if (EmergeCMSContext.IsLiveMode() || EmergeCMSContext.IsPreviewMode())
        {
            repResults.DataSource = GetPhysicianDetails();
            repResults.DataBind();
        }
    }
    #endregion
}
