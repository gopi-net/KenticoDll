using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using CMS.Base.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class CMSWebParts_CMS_Career_JobEmploymentHistory : JobEmploymentHistoryWebpart
{
    MessagesPlaceHolder placeHolder;
    #region Webpart Properties80
    public string NextPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty);
        }
    }
    public string PrevPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty);
        }
    }
    public int MinEmploymentHistory {
        get
        {
            return EmergeValidationHelper.GetInteger(GetValue("MinEmploymentHistory"), 0);
        }
    }
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return placeHolder;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = pnlEmploymentHistory;
        BindEventMethods();
        BindUserData();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlMain.Visible = false;
            return;
        }
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        SetMessagePlaceHolder(plcHistory);
        if (SaveEmploymentHistoryData())
        {
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
            ClearFormFields();
            Add.Text = CareerConstants.CAREER_BUTTON_TEXT_ADD;
        }
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
        BindUserData();
    }
    protected void repEmplymentHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {        
        PerformItemCommandAction(e, GetEmploymentHistoryId(e));
        BindUserData();
    }
    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        SetMessagePlaceHolder(plcHistory);
        if (IsMinimumNumberOfItemsAdded())
        {
            if (Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER] != null)
                Response.Redirect(Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER].ToString());
            Response.Redirect(NextPageUrl);
        }
        else
            ShowError(string.Format(CareerConstants.CAREER_MESSAGE_MINIMUM_EMPLOYMENT_HISTORY_FAILED, MinEmploymentHistory));
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        ControlPanel = pnlMilitaryRecord;
        SetMessagePlaceHolder(plcMess);
        SetMinMaxValuesMilitaryRecord();
        if (SaveMilitaryRecord())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
        BindUserData();
    }
    #endregion

    #region Private Methods
    private bool SaveEmploymentHistoryData()
    {
        ControlPanel = pnlEmploymentHistory;
        SetPhoneNumber();
        SetMinMaxValuesEmploymentHistroy();
        int itemId = Convert.ToInt32(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_EMPLOYMENT_HISTORY_ID));
        EmergeSessionHelper.Remove(CareerConstants.CAREER_SESSION_EMPLOYMENT_HISTORY_ID);
        
        return SaveJobEmploymentHistory(itemId);
    }
    private void SetMessagePlaceHolder(MessagesPlaceHolder holder)
    {
        placeHolder = holder;
    }
    private void PerformItemCommandAction(RepeaterCommandEventArgs e, int itemId)
    {
        if (e.CommandName == CareerConstants.CAREER_ITEM_COMMAND_DELETE)
            DeleteEmploymentHistoryItem(itemId);
        else if (e.CommandName == CareerConstants.CAREER_ITEM_COMMAND_EDIT)
            FillEmplymentHistoryForEdit(itemId);
    }
    private int GetEmploymentHistoryId(RepeaterCommandEventArgs e)
    {
        return Convert.ToInt32(e.CommandArgument.ToString());
    }
    private void FillEmplymentHistoryForEdit(int itemId)
    {
        IDictionary itemDictionary;
        itemDictionary = GetEmploymentHistoryItem(itemId);
        FillUserInformation(pnlEmploymentHistory, (Dictionary<string, object>)itemDictionary);
        GetPhoneNumberAndExtension();
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_EMPLOYMENT_HISTORY_ID, itemId);
        
        Add.Text = CareerConstants.CAREER_BUTTON_TEXT_UPDATE;
    }
    private void BindEventMethods()
    {
        Add.Click += Add_Click;
        Save.Click += Save_Click;
        Clear.Click += Clear_Click;
        Next.Click += Next_Click;
        Previous.Click += Previous_Click;
        repEmplymentHistory.ItemCommand += repEmplymentHistory_ItemCommand;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
    }
    private void GetPhoneNumberAndExtension()
    {
        PhoneNumberNum.Text = GetPhoneNumber(PhoneNumber.Text);
        PhoneNumberExt.Text = GetExtension(PhoneNumber.Text);
    }
    private void SetPhoneNumber()
    {
        PhoneNumber.Text = string.Format(CareerConstants.CAREER_CONCATENATE_PHONE_EXTENSION, PhoneNumberNum.Text, PhoneNumberExt.Text);
    }
    private void BindUserData()
    {
        BindEmploymentHistoryDataToRepeater();
        GetMilitaryRecordData();
    }
    private void GetMilitaryRecordData()
    {
        IDictionary itemDictionary;
        itemDictionary = GetUserMilitaryRecord();
        FillUserInformation(pnlMilitaryRecord, (Dictionary<string, object>)itemDictionary);
    }
    private void BindEmploymentHistoryDataToRepeater()
    {
        repEmplymentHistory.ZeroRowsText = "No Employment History added";
        repEmplymentHistory.TransformationName = string.Format("customtable.Emerge_{0}_CR_ApplicantEmploymentHistory.Default", EmergeCMSContext.CurrentSiteName); 
        repEmplymentHistory.DataSource = GetUserEmploymentHistoryInformation();
        repEmplymentHistory.DataBind();
    }
    private bool IsMinimumNumberOfItemsAdded()
    {
        return repEmplymentHistory.Items.Count >= MinEmploymentHistory;
    }
    private void SetMinMaxValuesEmploymentHistroy()
    {
		EmploymentFrom.MaxDate = System.DateTime.Today.AddDays(-1);
        EmploymentTo.MinDate = EmploymentFrom.SelectedDateTime;
		
    }
    private void SetMinMaxValuesMilitaryRecord()
    {
        ServiceTo.MinDate = ServiceFrom.SelectedDateTime;
    }
    #endregion
}