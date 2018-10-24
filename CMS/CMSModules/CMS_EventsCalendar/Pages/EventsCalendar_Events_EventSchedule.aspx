<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventsCalendar_Events_EventSchedule.aspx.cs"
    Inherits="CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_EventSchedule"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Events Schedule Form" %>

<%@ Register Src="~/CMSModules/CMS_EventsCalendar/Controls/EventCalendarFormControl.ascx" TagName="EventForm" TagPrefix="Emerge" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="Popup" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">

        <Emerge:EventForm runat="server" ID="EventFormControl"></Emerge:EventForm>

        <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-primary" Text="Save" />
        <asp:Button ID="BackButton" runat="server" CssClass="btn" Text="Back" />
    </asp:PlaceHolder>
    <Emerge:Popup ID="EmergePopup" runat="server" HeaderText="Blackout date clashes"
        ShowCancelButton="true" CancelButtonText="Cancel" Width="500" XCoordiante="450" YCoordiante="150" OKButtonText="Done" />
    <Emerge:Popup ID="InformationPopup" runat="server" HeaderText="Event Schedule"
        ShowCancelButton="false" Width="500" XCoordiante="450" YCoordiante="150" OKButtonText="OK" />
    <Emerge:Popup ID="RegistrationsPopup" runat="server" HeaderText="Move Registrations"
        Width="700" XCoordiante="200" YCoordiante="50" OKButtonText="Move Registrations" CancelButtonText="Cancel" />
    <Emerge:Popup ID="RegistrationConflictPopup" runat="server" HeaderText="Registrations Conflicts"
        ShowCancelButton="false" Width="700" XCoordiante="250" YCoordiante="150" OKButtonText="Back" />
    <Emerge:Popup ID="DeadlinePopup" runat="server" HeaderText="Registration Deadline clash"
        ShowCancelButton="true" CancelButtonText="No" Width="500" XCoordiante="450" YCoordiante="150" OKButtonText="Yes" />
</asp:Content>
