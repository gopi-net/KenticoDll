using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory;
using Bluespire.Emerge.Components.StaffDirectory.WebParts;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_StaffDirectory_PhysicianSearch : StaffDirectoryAdvanceSearchWebpart
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

    public bool ShowZipSearch
    {
        get
        {

            return EmergeValidationHelper.GetBoolean(GetValue("ShowZipSearch"), true);
        }
        set
        {
            SetValue("ShowZipSearch", value);
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
        trZip.Visible = trMiles.Visible = ShowZipSearch;
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
            {
                LoadListControls(false);
                LoadMilesControl();
            }

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
        LoadMilesControl();
		ClearFormFields();
		Zip.Text=string.Empty;
		
		
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        trErrorForMiles.Visible = (Miles.SelectedValue.Equals("-1") && Zip.Text.Length > 0);
        trErrorForZip.Visible = (!Miles.SelectedValue.Equals("-1") && Zip.Text.Length == 0);
        if (!trErrorForMiles.Visible && !trErrorForZip.Visible)
        {
            ControlPanel = SearchPanel;
            parameters = new Dictionary<string, string>();
            parameters.Add(StaffDirectoryConstants.STAFF_COLUMN_STAFFTYPE, StaffType);
            parameters.Add(StaffDirectoryConstants.MILES_CONTROL_ID, Miles.SelectedValue);
            parameters.Add(StaffDirectoryConstants.ZIP_CONTROL_ID, Zip.Text.Trim());
            parameters = SetSearchParameters(parameters);
            SaveSearchParametersToSession(parameters);
            EmergeURLHelper.Redirect(SearchResultPage);
        }
    }
    protected void AlphaPhysicianSearch(object sender, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)sender;
        parameters = new Dictionary<string, string>();
        parameters.Add(AlphabeticSearchColumn, lbtn.Text);
        SaveSearchParametersToSession(parameters);
        EmergeURLHelper.Redirect(SearchResultPage);
    }

    protected void LoadMilesControl()
    {
        Miles_DataSource.QueryName = EmergeStaticHelper.SetSiteName(Miles_DataSource.QueryName);
        Miles.DataSource = Miles_DataSource.DataSource;
        Miles.DataBind();
    }
    #endregion
}