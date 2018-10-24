using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;
public partial class CMSWebParts_CMS_StaffDirectory_SearchResult : StaffDirectorySearchResultWebpart
{
    IDictionary<string, string> parameters;
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
    public string PhysicianRedirectPage
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("PhysicianRedirectPage"), "");
        }
        set
        {
            SetValue("PhysicianRedirectPage", value);
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

    #region [Page events]
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

        try
        {
            btnSearch.Click += btnSearch_Click;
            btnSearch.Text = EmergeResHelper.GetString("emerge.SD.SearchResult.SearchButtonText");
            repResults.ItemDataBound += repResults_ItemDataBound;

            if (!IsPostBack)
            {
                LoadListControls(false);
            }
            LoadPhysician();
        }
        catch (Exception ex)
        {
            ExceptionHandler.HandleException(ex);
        }
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
                    repeater.DataSource = GetRelationTableData(relationName, hdnItemId.Value);
                    repeater.DataBind();
                }
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EmergeURLHelper.Redirect(NewSearchUrl);
    }
    #endregion

    #region [Private functions]
    private void LoadPhysician()
    {
        InitializeRepeater();
        repResults.DataSource = GetPhysicians();
        repResults.DataBind();
        EmergeSessionHelper.SetValue("StaffDirectoryResultPage", EmergeURLHelper.CurrentURL);
        lblPageResult.Text = String.Format(EmergeResHelper.GetString("Emerge.SD.SearchResult.PageResult"), repResults, repResults.Items.Count);
    }

    private void InitializeRepeater()
    {
        SetPageSize();
        repResults.ZeroRowsText = EmergeResHelper.GetString("Emerge.StaffDirectory.SearchResultNotFound");
        repResults.TransformationName = TransformationName;
    }

    private void SetPageSize()
    {
        if (PageSize.SelectedItem.Value != "-1")
            repResults.PageSize = Convert.ToInt32(PageSize.SelectedItem.Value);
    }



    #endregion
}
