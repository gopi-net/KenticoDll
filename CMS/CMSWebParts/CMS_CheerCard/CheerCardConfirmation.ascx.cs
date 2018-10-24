using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.CheerCard.WebParts;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.CheerCard.WebParts;
using System.Text;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;


public partial class CMSWebParts_CMS_CheerCard_CheerCardConfirmation : CheerCardConfirmationWebPart
{

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

    #region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        SendAnotherCard.Click += SendAnotherCard_Click;

        if (StopProcessing)
        {
            panelThankYou.Visible = false;
            return;
        }


        if (!EmergeRequestHelper.IsPostBack())
        {
            if (EmergeCMSContext.IsLiveMode() || EmergeCMSContext.IsPreviewMode())
            {

                try
                {

                    litThankYouMessage.ResourceString = GetThankYouMessage();
                    litThankYouMessage.Visible = true;
                    EmergeSessionHelper.Remove(CheerCardConstants.SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD);
                }
                catch (NullReferenceException)
                {
                    ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_ERRORMESSAGE_CHEERCARD_NULLREFERENCEEXCEPTION));
                }
                catch (FormatException)
                {
                    ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_ERRORMESSAGE_CHEERCARD_FORMATEXCEPTION));
                }
                catch (OverflowException)
                {

                    ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_ERRORMESSAGE_CHEERCARD_OVERFLOWEXCEPTION));
                }
                catch (CheerCardItemNotFoundException)
                {
                    ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_ERRORMESSAGE_CHEERCARD_CHEERCARDITEMNOTFOUNDEXCEPTION));
                }
                catch (SessionDataMissingException)
                {
                    EmergeURLHelper.Redirect(CheerCardListingPageURL);
                }
            }


        }
    }
    #endregion "Page Events"

    #region "Control Events"
    void SendAnotherCard_Click(object sender, EventArgs e)
    {
        EmergeSessionHelper.Remove(CheerCardConstants.SESSION_KEY_NAME_FOR_ITEMID_OF_NEWLY_SAVED_CHEER_CARD);
        EmergeURLHelper.Redirect(CheerCardListingPageURL);
    }
    #endregion "Control Events"
}