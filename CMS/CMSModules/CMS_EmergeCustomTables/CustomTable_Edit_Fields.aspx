<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CMS_EmergeCustomTables_CustomTables_CustomTable_Edit_Fields"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Custom talble edit - Fields"
    CodeFile="CustomTable_Edit_Fields.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:FieldEditor ID="FieldEditor" IsLiveSite="false" runat="server" Visible="false" AllowDummyFields="true"/>
</asp:Content>