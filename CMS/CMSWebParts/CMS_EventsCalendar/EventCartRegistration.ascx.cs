using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using System.Data;
using CMS.Helpers;
using CMS.Base.Web.UI;
public partial class CMSWebParts_CMS_EventsCalendar_EventCartRegistration : EventCartRegistrationWebPart
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

    public string CalendarPageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CalendarPageUrl"), string.Empty);
        }
        set
        {
            SetValue("CalendarPageUrl", value);
        }
    }


    public string EventCartPageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartPageUrl"), string.Empty);
        }
        set
        {
            SetValue("EventCartPageUrl", value);
        }
    }

    public string PaymentGatewayUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("PaymentGatewayUrl"), string.Empty);
        }
        set
        {
            SetValue("PaymentGatewayUrl", value);
        }
    }

    public string EventCartRegistrationConfirmationPageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartRegistrationConfirmationPageUrl"), string.Empty);
        }
        set
        {
            SetValue("EventCartRegistrationConfirmationPageUrl", value);
        }
    }

    protected override void OnInit(EventArgs e)
    {
        ControlPanel = CartRegistrationPanel;
    }

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            CartRegistrationPanel.Visible = false;
            return;
        }

        if (CartService.GetItems().Count == 0 )
        {
            URLHelper.Redirect(CalendarPageUrl);
        }

        RegisterEvents();
    }

    private void RegisterEvents()
    {
        btnBackToCart.Click += btnBackToCart_Click;
        btnClear.Click += btnClear_Click;
        btnProceedToPayment.Click += btnProceedToPayment_Click;
    }

    void btnProceedToPayment_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (SessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART) == null)
            {
                CreateFormParameters();
                SessionHelper.SetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART, FormParameters);
            }
            else
            {
                SetFormFieldsFromDictionary(((Dictionary<string, object>)SessionHelper.GetValue(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART)));
            }
            List<Dictionary<string, object>> registrations = EventsCalendarHelper.GetRegistrationsFromCart();
            SaveRegistrationStatus status = EventsCalendarHelper.ValidateRegistrations(registrations);
            if (status == SaveRegistrationStatus.VALID)
            {
                //URLHelper.ResponseRedirect(PaymentGatewayUrl);
                if (!string.IsNullOrEmpty(EventCartRegistrationConfirmationPageUrl))
                    URLHelper.ResponseRedirect(EventCartRegistrationConfirmationPageUrl);
            }
            else
            {
                SessionHelper.SetValue(EventsConstants.SESSIONKEY_SAVEREGISTRATIONSTATUS, status);
                SessionHelper.Remove(EventsConstants.SESSIONKEY_REGISTRATIONINFO_CART);
                if (!string.IsNullOrEmpty(EventCartPageUrl))
                    URLHelper.Redirect(EventCartPageUrl);
            }

        }
    }
    

    void btnClear_Click(object sender, EventArgs e)
    {
        ClearFormFields();
    }

    void btnBackToCart_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EventCartPageUrl))
            URLHelper.Redirect(EventCartPageUrl);
    }
}