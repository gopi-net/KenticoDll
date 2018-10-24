<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmergeUniGridMenu.ascx.cs" Inherits="CMSFormControls_EmergeAdminControls_EmergeUnigrid_Menu" %>
<%@ Register Assembly="CMS.UIControls" Namespace="CMS.UIControls" TagPrefix="cms" %>
<asp:Panel runat="server" ID="pnlUniGridMenu" CssClass="PortalContextMenu WebPartContextMenu"
    EnableViewState="false">
    <asp:Panel runat="server" ID="pnlExcel" CssClass="Item">
        <asp:Panel runat="server" ID="pnlExcelPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgExcel" CssClass="Icon" EnableViewState="false" />
            <cms:LocalizedLabel runat="server" ID="lblExcel" CssClass="Name" EnableViewState="false"
                ResourceString="export.exporttoexcel" />
        </asp:Panel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlAdvancedExport" CssClass="Item">
        <asp:Panel runat="server" ID="pnlAdvancedExportPadding" CssClass="ItemPadding">
            <asp:Image runat="server" ID="imgAdvancedExport" CssClass="Icon" EnableViewState="false" />
            <cms:LocalizedLabel runat="server" ID="lblAdvancedExport" CssClass="Name" EnableViewState="false"
                ResourceString="export.advancedexport" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>

