using System;
using System.Collections.Generic;
using System.Web.UI;
using Bluespire.Emerge.Components.Careers.WebParts;

using Bluespire.Emerge.Components.Career;
using System.Collections;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_Career_JobEducationAndTraining : JobEducationAndTrainingWebpart
{
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
        if (StopProcessing)
        {
            pnlMain.Visible = false;
            return;
        }
        IDictionary itemDictionary;
        BindEventMethods();
        ControlPanel = pnlProfessionalLicensure;
        LoadListControls(false);
        if (!Page.IsPostBack)
        {
            itemDictionary = GetUserEducationAndTrainingInformation();
            FillUserInformation(pnlEducationTraining, (Dictionary<string, object>)itemDictionary);
            itemDictionary = GetUserProfessionalLicensureInformation();
            FillUserInformation(pnlProfessionalLicensure, (Dictionary<string, object>)itemDictionary);
        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlMain.Visible = false;
            return;
        }    
    }

    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }
    protected void Next_Click(object sender, EventArgs e)
    {
        if (IsSaved())
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
        ControlPanel = pnlEducationTraining;
        ClearFormFields();
        ControlPanel = pnlProfessionalLicensure;
        ClearFormFields();
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        if (IsSaved())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }
    #endregion

    #region Methods
    private void BindEventMethods()
    {
        Save.Click += Save_Click;
        Clear.Click += Clear_Click;
        Next.Click += Next_Click;
        Previous.Click += Previous_Click;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
    }
    private bool IsSaved()
    {
        SetOptionalValidations();
        ControlPanel = pnlEducationTraining;
        if (SaveEducationAndTraining())
        {
            ControlPanel = pnlProfessionalLicensure;
            return SaveProfessionalLicensure();
        }
        return false;
    }
    private void SetOptionalValidations()
    {
        SetMinMaxValues();
        SetValidation(HaveLicenseInState, DateIssued1);
        SetValidation(HaveLicenseInState, DateIssued2);
        SetValidation(HaveLicenseInState, ExpirationDate1);
        SetValidation(HaveLicenseInState, ExpirationDate2);
        SetValidation(HasLicenseEverRevokedSuspendedTerminated, DateOfAction);
        SetValidation(HasLicenseEverRevokedSuspendedTerminated, ReactivationDate);
        SetValidation(HaveContactedBoardForConversion, ExpectedConversionDate);
        SetValidation(HaveContactedBoardForConversion, OutOfStateEffectiveDate);
        SetValidation(HaveContactedBoardForConversion, OutOfStateExpirationDate);
    }
    private void SetMinMaxValues()
    {
        ExpirationDate1.MinDate = DateIssued1.SelectedDateTime;
        ExpirationDate2.MinDate = DateIssued2.SelectedDateTime;
        ReactivationDate.MinDate = DateOfAction.SelectedDateTime;
        OutOfStateExpirationDate.MinDate = OutOfStateEffectiveDate.SelectedDateTime;
    }
    #endregion
}