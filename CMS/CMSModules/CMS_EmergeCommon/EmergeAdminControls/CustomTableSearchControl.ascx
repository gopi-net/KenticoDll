<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomTableSearchControl.ascx.cs" Inherits="CMSFormControls_EmergeAdminControls_CustomTableSearchControl" %>
<cms:MessagesPlaceHolder ID="plcMess" runat="server" />
<asp:Panel ID="panSearchFields" runat="server" DefaultButton="btnSearch">
</asp:Panel>
<br />
<div>
    <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary" runat="server" />
    <asp:Button ID="btnClear" Text="Clear" CssClass="btn btn-default" runat="server" />
</div>
<style>
    .cms-bootstrap td {
        padding: 3px;
    }
</style>
<br />
<br />
