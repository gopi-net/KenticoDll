<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmergePopupDialog.ascx.cs"
    Inherits="CMSModules_CMS_EmergeCommon_Controls_EmergePopupDialog" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Button ID="HideButton" runat="server" style="display:none;"/>
<asp:Panel ID="PopupPanel" runat="server" CssClass="modalPopup">
    <div class="PopupOuter">
        <div class="PopupHeader" id="PopupHeader" runat="server">Header</div>
        <div class="PopupBody">
            <asp:Panel ID="InnerPanel" runat="server">
            </asp:Panel>
        </div>
    </div>

    <asp:Button ID="OKButton" runat="server" Text="OK" />
    <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
</asp:Panel>

<asp:ModalPopupExtender ID="ModalPopupExtender" TargetControlID="HideButton" runat="server" Enabled="True" BackgroundCssClass="ModalPopupBG"
    PopupControlID="PopupPanel" DropShadow="true">
</asp:ModalPopupExtender>

<style type="text/css">
        .ModalPopupBG
        {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .PopupOuter
        {
            min-width: 200px;
            min-height: 150px;
            max-height:600px;
            overflow:auto;
            background: white;
        }
        .PopupHeader
        {
            background-color: #808080;
            width: auto;
            height: 20px;
            padding-left: 10px;
            padding-top: 10px;
            font-weight: bold;
            font-family: Arial;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 2px;
            border-style: solid;
            border-color: black;
            width: 300px;
           
        }
        .PopupBody
        {
            padding-left: 10px;
            padding-top: 10px;
             padding-right: 10px;
            font-family: Arial;
        }
</style>