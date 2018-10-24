<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_EventsCalendar_Pages_EventsCalendar_Events_EventsScheduleList" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Events Schedule - Data List"
    Theme="Default" CodeFile="EventsCalendar_Events_ScheduleList.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="SearchControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="Popup" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:SearchControl ID="SearchControl" runat="server" />
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
    <Emerge:Popup ID="MessagePopup" runat="server" HeaderText="Event Schedule" 
        ShowCancelButton="false" Width="500" XCoordiante="450" YCoordiante="150" OKButtonText="OK" />
</asp:Content>
