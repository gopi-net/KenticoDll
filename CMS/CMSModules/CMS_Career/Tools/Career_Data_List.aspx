<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Career_Tools_Career_Data_List" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Cheer Card - Data List"
    Theme="Default" CodeFile="Career_Data_List.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="CustomTableSearch" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableSearch ID="customTableSearch" runat="server" />
        <cms:CustomTableDataList ID="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>
