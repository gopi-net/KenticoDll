<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
     CodeFile="PreRegistration_Data_PreRegistrationList.aspx.cs" EnableEventValidation="false"
    Inherits="PreRegistration_Data_PreRegistrationList" Title="Pre-Registration - Data List" Theme="Default" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="CustomTableSearch" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableSearch ID="customTableSearch" runat="server" />
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>
