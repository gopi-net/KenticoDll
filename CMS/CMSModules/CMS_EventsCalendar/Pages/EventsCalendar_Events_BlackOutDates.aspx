<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventsCalendar_Events_BlackOutDates.aspx.cs" 
    Inherits="CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_BlackOutDates" 
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Blackout Dates Form" %>

<%@ Register Src="~/CMSModules/CMS_EventsCalendar/Controls/EventCalendarFormControl.ascx" TagName="EventForm" TagPrefix="Emerge" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="Popup" TagPrefix="Emerge" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
        <asp:PlaceHolder ID="plcContent" runat="server">
            
            <Emerge:EventForm runat="server" ID="EventFormControl"></Emerge:EventForm>

             <asp:Button ID="SaveButton" runat="server" CssClass="btn btn-primary" Text="Save" />
    </asp:PlaceHolder>
   
    
    <Emerge:Popup ID="EmergePopup" runat="server" HeaderText="BlackOut dates" 
        ShowCancelButton="false" Height="180" Width="400" XCoordiante="450" YCoordiante="150" OKButtonText="OK" />
    
    </asp:Content>

  
      
