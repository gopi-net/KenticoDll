using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Components.Careers.WebParts;
using CMS.Base.Web.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class CMSWebParts_CMS_Career_JobReferencesAndReferrals : JobReferencesAndReferralsWebpart
{
    MessagesPlaceHolder placeHolder;
    #region Webpart Properties
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
    public int MinReferences
    {
        get
        {
            return EmergeValidationHelper.GetInteger(GetValue("MinReferences"), 0);
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
        ControlPanel = pnlReference;
        LoadListControls(false);
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
    protected void repReferences_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        PerformItemCommandAction(e, GetReferenceItemId(e));
        BindUserData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        SetMessagePlaceHolder(plcReference);
        if (SaveReferenceData())
        {
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
            ClearFormFields();
            Add.Text = CareerConstants.CAREER_BUTTON_TEXT_ADD ;
        }
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
        BindUserData();
    }
    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        SetMessagePlaceHolder(plcReference);
        if (IsMinimunReferencesAdded())
        {
            if (Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER] != null)
                Response.Redirect(Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER].ToString());
            Response.Redirect(NextPageUrl);
        }
        else
            ShowError(string.Format(CareerConstants.CAREER_MESSAGE_MINIMUM_REFERENCES_FAILED, MinReferences));
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        ControlPanel = pnlReference;
        ClearFormFields();
        ControlPanel = pnlReferral;
        ClearFormFields();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        SetMessagePlaceHolder(plcMess);
        ControlPanel = pnlReferral;
        if (SaveReferrals())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    #endregion

    #region Private Methods
    private void BindEventMethods()
    {
        Save.Click += Save_Click;
        Clear.Click += Clear_Click;
        Next.Click += Next_Click;
        Previous.Click += Previous_Click;
        Add.Click += Add_Click;
        repReferences.ItemCommand += repReferences_ItemCommand;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
    }
    private bool SaveReferenceData()
    {
        ControlPanel = pnlReference;
        SetBusinessPhone();
        int itemId = Convert.ToInt32(EmergeSessionHelper.GetValue(CareerConstants.CAREER_SESSION_JOB_REFERENCE_ID));
        EmergeSessionHelper.Remove(CareerConstants.CAREER_SESSION_JOB_REFERENCE_ID);
        return SaveReferences(itemId);
    }
    private void PerformItemCommandAction(RepeaterCommandEventArgs e, int itemId)
    {
        if (e.CommandName == CareerConstants.CAREER_ITEM_COMMAND_DELETE)
            DeleteReferenceItem(itemId);
        else if (e.CommandName == CareerConstants.CAREER_ITEM_COMMAND_EDIT)
            FillReferenceItemForEdit(itemId);
    }
    private void FillReferenceItemForEdit(int itemId)
    {
        IDictionary itemDictionary;
        itemDictionary = GetReferencesItem(itemId);
        FillUserInformation(pnlReference, (Dictionary<string, object>)itemDictionary);
        EmergeSessionHelper.SetValue(CareerConstants.CAREER_SESSION_JOB_REFERENCE_ID, itemId);
        Add.Text=CareerConstants.CAREER_BUTTON_TEXT_UPDATE;
        GetBusinessPhoneAndExtension();
    }
    private int GetReferenceItemId(RepeaterCommandEventArgs e)
    {
        return Convert.ToInt32(e.CommandArgument.ToString());
    }
    private void BindUserData()
    {
        BindReferencesToRepeater();
        GetReferralsData();
    }
    private void GetBusinessPhoneAndExtension()
    {
        BusinessPhoneNum.Text = GetPhoneNumber(BusinessPhone.Text);
        BusinessPhoneExt.Text = GetExtension(BusinessPhone.Text);
    }
    private void SetBusinessPhone()
    {
        BusinessPhone.Text = string.Format(CareerConstants.CAREER_CONCATENATE_PHONE_EXTENSION, BusinessPhoneNum.Text, BusinessPhoneExt.Text);
    }
    private void GetReferralsData()
    {
        IDictionary itemDictionary;
        itemDictionary = GetUserReferrals();
        FillUserInformation(pnlReferral, (Dictionary<string, object>)itemDictionary);
    }
    private void BindReferencesToRepeater()
    {
        repReferences.ZeroRowsText = "No Reference added";
        repReferences.TransformationName = string.Format("customtable.Emerge_{0}_CR_References.Default", EmergeCMSContext.CurrentSiteName);
        repReferences.DataSource = GetUserReferences();
        repReferences.DataBind();
    }
    private bool IsMinimunReferencesAdded()
    {
        return MinReferences <= repReferences.Items.Count;
    }
    private void SetMessagePlaceHolder(MessagesPlaceHolder holder)
    {
        placeHolder = holder;
    }
    #endregion
}