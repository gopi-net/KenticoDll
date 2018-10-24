<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventsCalendar_Events_VolunteerUsers.aspx.cs"
    Inherits="CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_VolunteerUsers"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Events Schedule Form" %>

<%@ Register Src="~/CMSModules/CMS_EventsCalendar/Controls/EventCalendarFormControl.ascx" TagName="EventForm" TagPrefix="Emerge" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="Popup" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">

        <Emerge:EventForm runat="server" ID="EventFormControl"></Emerge:EventForm>

        <asp:Button ID="SaveButton" CssClass="btn btn-primary" runat="server" Text="Save" />
    </asp:PlaceHolder>
</asp:Content>
