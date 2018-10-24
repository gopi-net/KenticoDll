<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
     CodeFile="PreRegistration_Data_EditItem.aspx.cs" 
    Inherits="PreRegistration_Data_EditItem" Title="Pre-Registration - Edit item" Theme="Default" ValidateRequest="false"
    EnableEventValidation="false"%>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeForm.ascx" TagName="CustomTableForm"
    TagPrefix="Emerge" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <Emerge:CustomTableForm ID="customTableForm" runat="server" />
    </asp:PlaceHolder>
</asp:Content>

