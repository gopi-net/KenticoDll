<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="EventsCalendar_Events_Form.aspx.cs"
    Inherits="CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_Form"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Events Calendar Events Form" %>

<%@ Register Src="~/CMSModules/CMS_EventsCalendar/Controls/EventCalendarFormControl.ascx" TagName="EventForm" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">

        <Emerge:EventForm runat="server" ID="EventFormControl"></Emerge:EventForm>

        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-primary" Text="Save" />
        <asp:Button ID="SaveNext" runat="server" CssClass="btn btn-primary" Text="Save & Next" />
    </asp:PlaceHolder>
</asp:Content>
