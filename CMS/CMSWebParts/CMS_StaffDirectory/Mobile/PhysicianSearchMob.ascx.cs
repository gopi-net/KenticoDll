using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory.WebParts;
using CMS.Base.Web.UI;
using Bluespire.Emerge.Components.StaffDirectory;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
public partial class CMSWebParts_CMS_StaffDirectory_Mobile_PhysicianSearchMob : StaffDirectoryAdvanceSearchWebpart
{
    IDictionary<string, string> parameters;
    #region Webpart Properties
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return MessagesPlaceHolder1;
        }
    }
    public string SearchResultPage
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("SearchResultPage"), string.Empty);
        }
        set
        {
            SetValue("SearchResultPage", value);
        }
    }
    public string Transformation
    {
        get;
        set;
    }
    public string StaffType
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("StaffType"), "0");
        }
        set
        {
            SetValue("StaffType", value);
        }
    }
    public bool ShowAlphabeticSearch
    {
        get
        {

            return EmergeValidationHelper.GetBoolean(GetValue("ShowAlphabeticSearch"), true);
        }
        set
        {
            SetValue("ShowAlphabeticSearch", value);
        }
    }
    public string AlphabeticSearchColumn
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("AlphabeticSearchColumn") + StaffDirectoryConstants.LINKBUTTON_SEPERATOR, StaffDirectoryConstants.LAST_NAME + StaffDirectoryConstants.LINKBUTTON_SEPERATOR);
        }
        set
        {
            SetValue("AlphabeticSearchColumn", Convert.ToString(value) + StaffDirectoryConstants.LINKBUTTON_SEPERATOR);
        }
    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = SearchPanel;
        btnSearch.Click += btnSearch_Click;
        btnClear.Click += btnClear_Click;
        divAlphaSearch.Visible = ShowAlphabeticSearch;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            SearchPanel.Visible = false;
            return;
        }
        try
        {
            if (!IsPostBack)
                LoadListControls(false);
        }
        catch (Exception ex)
        {
            string message = EmergeResHelper.GetString("Emerge.SD.Search.LoadControl.Error");
            ExceptionHandler.HandleException(ex, ref message);
            ShowError(message);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ControlPanel = SearchPanel;
        parameters = new Dictionary<string, string>();
        parameters.Add(StaffDirectoryConstants.STAFF_COLUMN_STAFFTYPE, StaffType);
        parameters = SetSearchParameters(parameters);
        SaveSearchParametersToSession(parameters);
        EmergeURLHelper.Redirect(SearchResultPage);
    }
    protected void AlphaPhysicianSearch(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        parameters = new Dictionary<string, string>();
        parameters.Add(AlphabeticSearchColumn, lbtn.Text);
        SaveSearchParametersToSession(parameters);
        EmergeURLHelper.Redirect(SearchResultPage);
    }
    #endregion
}