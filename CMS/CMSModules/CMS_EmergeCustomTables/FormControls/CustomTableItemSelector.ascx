<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CMS_EmergeCustomTables_CustomTables_FormControls_CustomTableItemSelector" CodeFile="CustomTableItemSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:UniSelector ID="uniSelector" runat="server" ResourcePrefix="customtableitemselector"
            SelectionMode="SingleDropDownList" AllowEmpty="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
