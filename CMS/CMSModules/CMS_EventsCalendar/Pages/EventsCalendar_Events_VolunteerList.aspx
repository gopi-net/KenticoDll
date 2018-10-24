<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventsCalendar_Events_VolunteerList.aspx.cs" 
    Inherits="CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_VolunteerList" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Events Schedule - Data List" Theme="Default" %>


<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="SearchControl" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:SearchControl ID="SearchControl" runat="server" />
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>