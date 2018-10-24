using System;
using System.Collections.Generic;
using System.Web.UI;
using Bluespire.Emerge.Components.Careers.WebParts;
using Bluespire.Emerge.Components.Career;
using System.Collections;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_Career_JobAffidevit : JobAffidevitWebpart
{
    public string PrevPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("PrevPageUrl"), string.Empty);
        }
    }
    public string NextPageUrl
    {
        get
        {
            //return EmergeURLHelper.GetHandledUrl(EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty));
            return EmergeValidationHelper.GetString(GetValue("NextPageUrl"), string.Empty);
        }
    }
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = pnlAffidavit;
        LoadListControls(false);
        Save.Click += Save_Click;
        Clear.Click += Clear_Click;
        Previous.Click += Previous_Click;
        Next.Click += Next_Click;
        if (NextPageUrl.Equals(string.Empty))
            Next.Visible = false;
        if (PrevPageUrl.Equals(string.Empty))
            Previous.Visible = false;
        SetMinMaxDates();
       
    }

    

    protected void Next_Click(object sender, EventArgs e)
    {
        if (SaveJobAffidevit())
        {
            if (Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER] != null)
                Response.Redirect(Request.QueryString[CareerConstants.CAREER_QUERYSTRING_URL_REFERRER].ToString());
            Response.Redirect(NextPageUrl);
        }
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }



    protected void Previous_Click(object sender, EventArgs e)
    {
        Response.Redirect(PrevPageUrl);
    }



    protected void Clear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        if (SaveJobAffidevit())
            ShowMessage(CMS.Base.Web.UI.MessageTypeEnum.Confirmation, CareerConstants.CAREER_MESSAGE_SUCCESS, string.Empty, string.Empty, false);
        else
            ShowError(CareerConstants.CAREER_MESSAGE_FAILURE);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            pnlAffidavit.Visible = false;
            return;
        }
        if (!Page.IsPostBack)
        {
            IDictionary itemDictionary;
            itemDictionary = GetAffidevitInformation();
            FillUserInformation(pnlAffidavit, (Dictionary<string, object>)itemDictionary);
        }
    }

    private void SetMinMaxDates()
    {
        Date.MaxDate = System.DateTime.Today;
    }
}