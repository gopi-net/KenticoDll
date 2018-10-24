<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_StaffDirectory_Tools_StaffDirectory_Data_EditItem" Theme="Default" ValidateRequest="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Staff Directory table data - Edit item"
    EnableEventValidation="false" CodeFile="StaffDirectory_Data_EditItem.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeForm.ascx" TagName="CustomTableForm"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableForm ID="customTableForm" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
