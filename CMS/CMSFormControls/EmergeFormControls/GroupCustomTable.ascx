<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupCustomTable.ascx.cs" Inherits="CMSFormControls_EmergeFormControls_GroupCustomTable" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>


<div>
    <cms:CustomTableForm ID="CustomTableForm" runat="server" MarkRequiredFields="true" />

</div>
<div class="content-block-25">
<asp:Button ID="btnSave" runat="server" Text="Add" CssClass="btn" />
<asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" CssClass="btn" />
<asp:HiddenField ID="hdnItemIdsForUpdate" runat="server" />
</div>
<div>
    <cms:CustomTableDataSource ID="CustomTableDataSource" runat="server" />
    <cms:UniGrid ID="CustomTableGrid" runat="server">
        <GridActions>
            <ug:Action Name="delete" Caption="$General.Delete$" Confirmation="$GroupControl.EmergeGrid.DeleteConfirmation$" CommandArgument="ItemID" FontIconClass="icon-bin" FontIconStyle="critical" />
            <ug:Action Name="edit" CommandArgument="itemid" ExternalSourceName="edit" Caption="$General.Edit$" FontIconClass="icon-edit" FontIconStyle="allow" />
        </GridActions>
        <GridColumns>
        </GridColumns>
        <PagerConfig />
    </cms:UniGrid>
</div>
