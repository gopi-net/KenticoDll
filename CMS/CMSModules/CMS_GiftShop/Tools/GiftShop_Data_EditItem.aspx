<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_GiftShop_Tools_GiftShop_Data_EditItem" Theme="Default" ValidateRequest="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Gift Shop table data - Edit item"
    EnableEventValidation="false" CodeFile="GiftShop_Data_EditItem.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeForm.ascx" TagName="CustomTableForm"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableForm ID="customTableForm" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
