using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Services;
using Bluespire.Emerge.Components.EventsCalendar.WebParts;
using CMS.PortalEngine;
using CMS.Helpers;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_EventsCalendar_EventCart : EventCartWebPart
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

    #region "Webpart Properties"
    /// <summary>
    /// This Property will be used to set Header Template Text for Event Cart Repeater.
    /// </summary>
    public string EventCartHeaderTemplateText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartHeaderTemplateText"), string.Empty);
        }
        set
        {
            SetValue("EventCartHeaderTemplateText", value);
        }
    }

    /// <summary>
    /// if Events Cart is Empty then it will redirect to Events Calendar Page.
    /// </summary>
    public string EventsCalendarPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventsCalendarPageURL"), string.Empty);
        }
        set
        {
            SetValue("EventsCalendarPageURL", value);
        }
    }



    /// <summary>
    /// if Events Cart is Empty then it will redirect to Events Calendar Page.
    /// </summary>
    public string EventCartRegistrationPageURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventCartRegistrationPageURL"), string.Empty);
        }
        set
        {
            SetValue("EventCartRegistrationPageURL", value);
        }
    }


    /// <summary>
    /// Transformation Name set to Events Cart.
    /// </summary>
    public string EventsCartTransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventsCartTransformationName"), EventCartRepeater.TransformationName);
        }
        set
        {
            SetValue("EventsCartTransformationName", value);

        }
    }


    #endregion

    protected override void OnLoad(EventArgs e)
    {
        if (StopProcessing)
        {
            panelCart.Visible = false;
            return;
        }

        RegisterEvents();

        if (PortalContext.ViewMode == CMS.PortalEngine.ViewModeEnum.LiveSite || PortalContext.ViewMode == CMS.PortalEngine.ViewModeEnum.Preview)
        {
            if (CartService.GetItems().Count == 0)
            {
                URLHelper.Redirect(EventsCalendarPageURL);
            }
            LoadCartRepeater();
        }


    }

    private void RegisterEvents()
    {
        cmdBrowseEvents.Click += cmdBrowseEvents_Click;
        cmdProceedToRegistration.Click += cmdProceedToRegistration_Click;

        EventCartRepeater.ItemCommand += EventCartRepeater_ItemCommand;
        EventCartRepeater.ItemDataBound += EventCartRepeater_ItemDataBound;
    }

    void EventCartRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            setFields(e);
        }

    }

    private void setFields(RepeaterItemEventArgs e)
    {

        EventOccurence occurrence = getEventOccurrence(e);
        if (!occurrence.Schedule.IsPaidSchedule)
        {
            setDiscountField(e, false);
            setAmountField(e);
            setCostField(e);
        }
        else
        {
            setDiscountField(e, (true && !String.IsNullOrEmpty(occurrence.Schedule.Discount)));
        }
    }

    private EventOccurence getEventOccurrence(RepeaterItemEventArgs e)
    {
        LocalizedHidden hdnOccuranceID = (LocalizedHidden)e.Item.Controls[0].FindControl("hdnOccuranceID");
        int occurrenceID = 0;

        if (hdnOccuranceID != null)
            occurrenceID = ValidationHelper.GetInteger(hdnOccuranceID.Value, 0);
        EventOccurence occurrence = EventsCalendarHelper.GetEventOccurenceByID(occurrenceID);
        return occurrence;
    }

    private void setDiscountField(RepeaterItemEventArgs e, bool show)
    {
        TextBox discountCoupon = (TextBox)e.Item.Controls[0].FindControl("DiscountCouponTextbox");
        LocalizedLinkButton updateButton = (LocalizedLinkButton)e.Item.Controls[0].FindControl("cmdUpdate");
        LocalizedLiteral DiscountCodeNA = (LocalizedLiteral)e.Item.Controls[0].FindControl("DiscountCodeNA");
        if (!show)
        {
            if (discountCoupon != null)
                discountCoupon.Visible = false;
            if (updateButton != null)
                updateButton.Visible = false;
            if (DiscountCodeNA != null)
            {
                DiscountCodeNA.Visible = true;
                DiscountCodeNA.Text = "N/A";
            }
        }
        else
        {
            discountCoupon.Visible = updateButton.Visible = true;
            DiscountCodeNA.Visible = false;
        }

    }

    private void setCostField(RepeaterItemEventArgs e)
    {
        LocalizedLiteral costLiteral = (LocalizedLiteral)e.Item.Controls[0].FindControl("CostLiteral");
        if (costLiteral != null)
            costLiteral.Text = "N/A";
    }

    private void setAmountField(RepeaterItemEventArgs e)
    {
        LocalizedLiteral amountLiteral = (LocalizedLiteral)e.Item.Controls[0].FindControl("AmountLiteral");
        if (amountLiteral != null)
            amountLiteral.Text = "N/A";
    }



    void cmdProceedToRegistration_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EventCartRegistrationPageURL))
        {
            URLHelper.ResponseRedirect(EventCartRegistrationPageURL);
        }
    }

    void cmdBrowseEvents_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(EventsCalendarPageURL))
        {
            URLHelper.ResponseRedirect(EventsCalendarPageURL);
        }
    }


    void EventCartRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            if (e.CommandName == "RemoveEventOccuranceFromCart")
            {

                CartService.RemoveItem(Convert.ToInt32(e.CommandArgument));

                if (CartService.GetItems().Count == 0)
                {
                    EventCartRepeater.Visible = false;
                    EventCartHeaderLiteral.Visible = false;
                    TotalLiteral.Visible = false;
                    cmdProceedToRegistration.Visible = false;
                    ShowError(ResHelper.GetString("Emerge.EC.Message.EmptyCart"));
                }
                LoadCartRepeater();

            }
            else if (e.CommandName == "DiscountCouponChanged")
            {

                TextBox discountCoupon = (TextBox)e.Item.Controls[0].FindControl("DiscountCouponTextbox");
                LocalizedLiteral AmountLiteral = (LocalizedLiteral)e.Item.Controls[0].FindControl("AmountLiteral");
                LocalizedHidden hdnOccuranceID = (LocalizedHidden)e.Item.Controls[0].FindControl("hdnOccuranceID");
                LocalizedHidden hdnCost = (LocalizedHidden)e.Item.Controls[0].FindControl("hdnCost");
                LocalizedLiteral CostLiteral = (LocalizedLiteral)e.Item.Controls[0].FindControl("CostLiteral");

                if (discountCoupon != null && AmountLiteral != null)
                {
                    try
                    {
                        double discountedCost = Math.Round(EventsCalendarHelper.GetDiscountedCostbyCodeAndScheduleID(discountCoupon.Text.Trim(), Convert.ToInt32(e.CommandArgument)), 2);
                        AmountLiteral.Text = discountedCost.ToString();
                        CartService.UpdateDiscountedCostForItem(discountCoupon.Text.Trim(), discountedCost, Convert.ToInt32(hdnOccuranceID.Value));
                        ShowError(ResHelper.GetString("Emerge.EC.Message.TotalCostChangedAsPerDiscountCoupon"));

                    }
                    catch (InvalidDiscountCodeException)
                    {
                        AmountLiteral.Text = hdnCost.Value;
                        CartService.UpdateDiscountedCostForItem(string.Empty, Convert.ToDouble(hdnCost.Value), Convert.ToInt32(hdnOccuranceID.Value));
                        discountCoupon.CssClass = EventsConstants.CSSCLASS_FOR_TEXTBOX_WITH_REDBORDER;
                        ShowError(ResHelper.GetString("Emerge.EC.Message.InValidDiscountCoupon"));
                    }

                }

            }

            UpdateTotalCost();
        }
    }

    private void UpdateTotalCost()
    {
        TotalLiteral.Text = "$"+ CartService.GetTotalCost().ToString("0.00");
    }

    private void LoadCartRepeater()
    {
        ShowMessage();
        EventCartRepeater.TransformationName = EventsCartTransformationName;
        EventCartRepeater.DataSource = CartService.GetItems();
        EventCartRepeater.DataBind();
        EventCartHeaderLiteral.Text = EventCartHeaderTemplateText;
        UpdateTotalCost();
    }

    private void ShowMessage()
    {
        if (null != SessionHelper.GetValue(EventsConstants.SESSIONKEY_SAVEREGISTRATIONSTATUS))
        {
            SaveRegistrationStatus status = (SaveRegistrationStatus)SessionHelper.GetValue(EventsConstants.SESSIONKEY_SAVEREGISTRATIONSTATUS);
            string message = string.Empty;
            switch (status)
            {
                case SaveRegistrationStatus.DISCOUNTCODEUSED:
                    message = GetString(EventsConstants.STRINGCODE_CARTDISCOUNTCODEUSEDBEFORE);
                    break;
                case SaveRegistrationStatus.DISCOUNTCODEUSEDINCART:
                    message = GetString(EventsConstants.STRINGCODE_CARTDISCOUNTCODEUSED);
                    break;
                case SaveRegistrationStatus.DUPLICATEREGISTRATIONS:
                    message = GetString(EventsConstants.STRINGCODE_CARTDUPLICATEREGISTRATIONBEFORE);
                    break;
                case SaveRegistrationStatus.DUPLICATEREGISTRATIONSINCART:
                    message = GetString(EventsConstants.STRINGCODE_CARTDUPLICATEREGISTRATION);
                    break;
                case SaveRegistrationStatus.REGISTRATIONLIMITREACHED:
                    message = GetString(EventsConstants.STRINGCODE_CARTREGISTRATIONLIMITREACHED);
                    break;
                case SaveRegistrationStatus.INVALIDDISCOUNTCODE:
                    message = GetString(EventsConstants.STRINGCODE_CARTINVALIDDISCOUNTCODE);
                    break;
            }
            if (!string.IsNullOrEmpty(message))
            {
                plcMess.ShowMessage(MessageTypeEnum.Error, message, string.Empty, string.Empty, true);
                SessionHelper.Remove(EventsConstants.SESSIONKEY_SAVEREGISTRATIONSTATUS);
            }
        }
    }


}