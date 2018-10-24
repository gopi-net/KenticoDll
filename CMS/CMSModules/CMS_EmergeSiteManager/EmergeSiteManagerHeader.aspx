<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" AutoEventWireup="true"
    Inherits="CMSModules_Emerge_SiteManager_Pages_Tools_Header" Title="Emerge - Header"
    Theme="default" CodeFile="EmergeSiteManagerHeader.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIToolbar.ascx" TagName="UIToolbar"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu">
        <cms:frameresizer id="frmResizer" runat="server" minsize="6, *" vertical="True" />
        <asp:Panel runat="server" ID="pnlContentMenu" CssClass="ContentMenuLeft">
            <cms:uitoolbar id="uiToolbarElem" targetframeset="emergeContent" runat="server" RememberSelectedItem="true"
                modulename="CMS.EmergeSiteManager" queryparametername="resourcename" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
