<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Rates_Tools_Rates_Data_EditItem" Theme="Default" ValidateRequest="false" 
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Cheer Card table data - Edit item"
    EnableEventValidation="false" CodeFile="Rates_Data_EditItem.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeForm.ascx" TagName="RatesForm"
    TagPrefix="Emerge" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergePopupDialog.ascx" TagName="EmergePopup"
    TagPrefix="Emerge" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <Emerge:RatesForm ID="customTableForm" runat="server" />
       <Emerge:EmergePopup ID="EmergePopup" runat="server" HeaderText="Are you Sure?" 
        ShowCancelButton="true" Width="200" XCoordiante="550" YCoordiante="80" OKButtonText="Yes" CancelButtonText="No"> 
        </Emerge:EmergePopup >
    </asp:PlaceHolder>
</asp:Content>
