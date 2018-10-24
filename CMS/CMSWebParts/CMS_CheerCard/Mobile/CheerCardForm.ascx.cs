using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.Email;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.Components.CheerCard.Services;
using Bluespire.Emerge.Components.CheerCard.WebParts;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;
using CMS.Base.Web.UI;


public partial class CMSWebParts_CMS_CheerCard_Mobile_CheerCardForm : CheerCardFormWebPart
{

    protected const string ControlIDHiddenMailToPatient = "hdnIsMailToPatient";

    #region properties


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

    #endregion properties

    # region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panelCheerCardForm;
        Environment = Constants.Environments.Mobile;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            cheercardMV.Visible = false;
            return;
        }
        cmdBackToCheerCardListing.Click += cmdBackToCheerCardListing_Click;
        cmdPreview.Click += cmdPreview_Click;
        cmdBackToForm.Click += cmdBackToForm_Click;
        cmdSend.Click += cmdSend_Click;
        cmdClear.Click += cmdClear_Click;

        if (!EmergeRequestHelper.IsPostBack())
        {
            SetupCheerCardForm();
            cheercardMV.SetActiveView(CheerCardFormView);
        }
    }

    void cmdClear_Click(object sender, EventArgs e)
    {
        ClearCheerCardFormFields();
    }

    # endregion "Page Events"

    # region "Control Events"

    /// <summary>
    /// method to return back to the Cheer card Listing page. (The page can be set through Web part properties.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void cmdBackToCheerCardListing_Click(object sender, EventArgs e)
    {
        string QueryString = string.Empty;
        EmergeURLHelper.Redirect(CheerCardSelectionPageURL + EmergeStaticHelper.FormQueryString(GetListingPageQueryString()));
    }

    /// <summary>
    /// Method to create Preview of Cheer card.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void cmdPreview_Click(object sender, EventArgs e)
    {
        ResetValidators(SendMailToPatient());

        if (Page.IsValid)
        {
            try
            {
                CreateFormParameters();
              
                EmergeSessionHelper.SetValue(CheerCardConstants.SESSION_KEY_NAME_FOR_CHEER_CARD_FORM_FIELDS, FormParameters);
                previewCheerCard.Text = GetPreviewHtml();

                cheercardMV.SetActiveView(CheerCardPreview);
            }
            catch (CheerCardConfigurationItemNotFound ex)
            {
                base.OnError(ex);
            }
            catch (CheerCardPreviewHtmlItemNotFound ex)
            {
                base.OnError(ex);
            }
        }
    }

    private bool SendMailToPatient()
    {
        if (null != ControlPanel.FindControl(ControlIDHiddenMailToPatient))
        {
            return EmergeValidationHelper.GetBoolean(((LocalizedHidden)ControlPanel.FindControl(ControlIDHiddenMailToPatient)).Value, false);
        }
        return sendMailToPatient;
    }

    /// <summary>
    /// Method to show "Cheer card form" from "Cheer card Preview"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void cmdBackToForm_Click(object sender, EventArgs e)
    {
        cheercardMV.SetActiveView(CheerCardFormView);
    }

    /// <summary>
    /// Method to save the cheer card request, send email on successfull operation.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void cmdSend_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            CreateFormParameters();
            
            if (!SendMailToPatient())
            {
                FormParameters.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_SENDCHEERCARDTO, EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_SENDCHEERCARDTO_HOSPITALFORDELIVERY));
                FormParameters.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_DELIVERYSTATUS, CheerCardConstants.DeliveryStatus.Pending.ToString());
            }
            else
            {
                FormParameters.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_SENDCHEERCARDTO, EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_SENDCHEERCARDTO_PATIENTEMAILFORDELIVERY));
                FormParameters.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_DELIVERYSTATUS, CheerCardConstants.DeliveryStatus.Delivered.ToString());
            }

            if (!FormParameters.ContainsKey(CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID))
            {
                FormParameters.Add(CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID, SelectedCheerCardImage);
            }

            ResetPatientEmail(SendMailToPatient());

            bool isSendCheerCardAsAttachement = IsSendEmailWithCheerCardAsAttachment();
            if (isSendCheerCardAsAttachement)
                CreateCheerCardImage();

            _ItemID = SaveCheerCardDetails();

            if (_ItemID == 0)
            {
                ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_FAILEDTOINSERTMESSAGE));
            }
            else
            {
                SendCheerCardEmail(isSendCheerCardAsAttachement, SendMailToPatient());
                if (isSendCheerCardAsAttachement)
                    DeleteCheerCardImage();
                SetSession();

                if (!string.IsNullOrEmpty(CheerCardConfirmationPageURL))
                    EmergeURLHelper.Redirect(CheerCardConfirmationPageURL);
            }
        }
    }


    # endregion "Control Events"


}