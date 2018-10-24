<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StaffAdvanceSearchConfig.ascx.cs" Inherits="CMSFormControls_EmergeFormControls_StaffAdvanceSearchConfig" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>

<asp:Panel ID="pnlRelationTable" runat="server">
    <cms:LocalizedDropDownList runat="server" ID="OtherTables" DataTextField="TableName" AutoPostBack="true"
         OnSelectedIndexChanged="OtherTables_SelectedIndexChanged" DataValueField="TableName"></cms:LocalizedDropDownList>
    <cms:CMSQueryDataSource ID="OtherTables_DataSource"  runat="server"  QueryName="customtable.Emerge_{0}_SD_Staff.GetRelationTableList"   />

    <cms:LocalizedCheckBoxList runat="server" ID="ColumnList" RepeatColumns="4" DataTextField="column_name" DataValueField="column_name" RepeatDirection="Horizontal"></cms:LocalizedCheckBoxList>
    <cms:CMSQueryDataSource ID="Columns_DataSource"  runat="server"   />
     <asp:GridView ID="CustomTableGrid" runat="server"  >
                    
                 
                </asp:GridView>
    <asp:Button ID="btnSetColumnList" Text="Add Column In Search" runat="server" CausesValidation="false" />
    <asp:HiddenField ID="hdnIsPostBack" runat="server" Value="false" />
</asp:Panel>
