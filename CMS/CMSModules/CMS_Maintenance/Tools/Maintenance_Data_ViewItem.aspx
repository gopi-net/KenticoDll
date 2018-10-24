<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Maintenance_Tools_Maintenance_Data_ViewItem" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="Maintenance_Data_ViewItem.aspx.cs" %>

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
