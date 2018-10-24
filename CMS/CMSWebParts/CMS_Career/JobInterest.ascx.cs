using System;
using System.Collections.Generic;
using Bluespire.Emerge.Components.Careers.WebParts;
using System.Collections;
using Bluespire.Emerge.Components.Career;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_Career_JobInterest : JobInterestWebpart
{
    #region Webpart and Public Properties
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
    
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
       
        IDictionary itemDictionary;
        BindEventMethods();
        ControlPanel = pnlJobInterest;
		AvailabilityDate.MinDate = DateTime.Now;
            
			AvailabilityDate.NeedsValidation=true;
        if (!IsPostBack)
        {
            LoadListControls(false);
            itemDictionary = GetUserJobInterestInformation();
            FillUserInformation(pnlJobInterest, (Dictionary<string, object>)itemDictionary);
			AvailabilityDate.MinDate = DateTime.Now;
			AvailabilityDate.NeedsValidation=true;
        }
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlJobInterest.Visible = false;
            return;
        }
    }

    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER] != null)
                Response.Redirect(Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER].ToString());
            Response.Redirect(NextPageUrl);
        }
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (SaveData())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    #endregion

    #region Private Methods
    private void BindEventMethods()
    {
        Submit.Click += Submit_Click;
        Clear.Click += Clear_Click;
        Next.Click += Next_Click;
        Previous.Click += Previous_Click;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
    }
    private bool SaveData()
    {
        SetOptionalValidations();
        if (SaveJobInterest())
            return true;
        return false;
    }
    private void SetOptionalValidations()
    {
        SetMinMaxValues();
        SetValidation(WasHiredEarlier, PrevEmploymentFrom);
        SetValidation(WasHiredEarlier, PrevEmploymentTo);
        SetValidation(HasAnyTrafficViolations, TrafficViolationDate);
        SetValidation(HasOffense, OffenseDate);
    }
    private void SetMinMaxValues()
    {
        OffenseDate.MaxDate = System.DateTime.Today;
        PrevEmploymentFrom.MaxDate = System.DateTime.Today;
        PrevEmploymentTo.MinDate = PrevEmploymentFrom.SelectedDateTime;
        TrafficViolationDate.MaxDate = System.DateTime.Today;
		AvailabilityDate.MinDate =System.DateTime.Today;
        
    }
    #endregion

}