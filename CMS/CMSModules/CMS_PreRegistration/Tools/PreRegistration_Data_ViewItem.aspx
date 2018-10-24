<%@ Page Language="C#" AutoEventWireup="true"
     CodeFile="PreRegistration_Data_ViewItem.aspx.cs" Theme="Default"
     MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" ValidateRequest="false"
    Inherits="PreRegistration_Data_ViewItem" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeViewItem.ascx" TagName="CustomTableViewItem"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CustomTableViewItem ID="customTableViewItem" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" ResourceString="general.close"
            OnClientClick="return CloseDialog();" EnableViewState="false" />
    </div>
</asp:Content>

