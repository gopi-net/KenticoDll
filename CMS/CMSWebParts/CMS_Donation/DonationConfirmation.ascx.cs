using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.Donation;
using Bluespire.Emerge.Components.Donation.WebParts;
using CMS.Base.Web.UI;
using CMS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CMSWebParts_CMS_Donation_DonationConfirmation : DonationConfirmationWebpart
{
    #region Properties
    public string DonationFormURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("DonationFormURL"), string.Empty);
        }
        set
        {
            SetValue("DonationFormURL", value);
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
    protected override void OnInit(EventArgs e)
    {
        ControlPanel = ThankYouPanel;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ThankYouPanel.Visible = false;
            return;
        }

        if (!RequestHelper.IsPostBack())
        {
            SaveDonation();
        }
    }

    private void SaveDonation()
    {
        if (SessionHelper.GetValue(DonationConstants.SESSIONKEY_DONATIONINFO) == null)
            URLHelper.Redirect(DonationFormURL);
        try
        {
            bool isSuccess = base.SaveDonationInformation();
            try
            {
                if (isSuccess)
                {
                    SendEmail();
                }
                else
                {
                    plcMess.ShowMessage(MessageTypeEnum.Error, GetString(DonationConstants.STRINGCODE_DONATIONSAVEEXCEPTIONMESSAGE), string.Empty, string.Empty, true);
                    ThankYouPanel.Visible = false;
                }
            }
            catch(Exception ex)
            {
                EmergeLogWriter.WriteError("Donation:SendEmail", EventCode.EMERGE_EMAIL, ex.ToString());
                plcMess.ShowMessage(MessageTypeEnum.Error, GetString(DonationConstants.STRINGCODE_DONATIONSENDEMAILEXCEPTION), string.Empty, string.Empty, true);
                ThankYouPanel.Visible = false;
            }

        }
        catch(Exception ex)
        {
            EmergeLogWriter.WriteError("Donation:Save", EventCode.EMERGE_ADD, ex.ToString());
            plcMess.ShowMessage(MessageTypeEnum.Error, GetString(DonationConstants.STRINGCODE_DONATIONSAVEEXCEPTIONMESSAGE), string.Empty, string.Empty, true);
            ThankYouPanel.Visible = false;
        }
        FinalizeSave();
    }
}