<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_StaffDirectory_Tools_StaffDirectory_Data_ViewItem" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="StaffDirectory_Data_ViewItem.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeViewItem.ascx" TagName="CustomTableViewItem"
    TagPrefix="Emerge" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <Emerge:CustomTableViewItem ID="customTableViewItem" runat="server" />
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" ResourceString="general.close"
            OnClientClick="return CloseDialog();" EnableViewState="false" />
    </div>
</asp:Content>