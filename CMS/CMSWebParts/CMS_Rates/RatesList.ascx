<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RatesList.ascx.cs" Inherits="CMSWebParts_CMS_Rates_RatesList" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="panelRatesList" runat="server">
    <cms:CMSRepeater ID="repRatesList" runat="server">
    </cms:CMSRepeater>
</asp:Panel>
