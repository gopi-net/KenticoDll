<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Career_Jobs_List.aspx.cs" 
    Inherits="CMSModules_CMS_Career_PagesCareer_Jobs_List" 
    EnableEventValidation="false"  MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Jobs - Data List"
    Theme="Default" %>


<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="SearchControl" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="Popup" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:SearchControl ID="SearchControl" runat="server" />
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>
