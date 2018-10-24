<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CMS_Location_Tools_Location_Data_EditItem" Theme="Default" ValidateRequest="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Loaction table data - Edit item"
    EnableEventValidation="false" CodeFile="Location_Data_EditItem.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeForm.ascx" TagName="CustomTableForm"
    TagPrefix="Emerge" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <Emerge:CustomTableForm ID="customTableForm" runat="server" />
    </asp:PlaceHolder>
</asp:Content>
