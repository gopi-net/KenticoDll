<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheerCardConfirmation.ascx.cs" Inherits="CMSWebParts_CMS_CheerCard_CheerCardConfirmation" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />

<asp:Panel ID="panelThankYou" runat="server" DefaultButton="SendAnotherCard">
    <cms:LocalizedLiteral ID="litThankYouMessage" runat="server" ResourceString="Emerge.CC.CheerCardConfirmation.ThankyouMessage" Visible="false" />

    <section class="btnWrapper">



        <asp:LinkButton ID="SendAnotherCard" runat="server">
            <cms:LocalizedLiteral ID="SendAnotherCardLit" runat="server" ResourceString="Emerge.CC.CheerCardList.SendAnotherCard.Text">
            </cms:LocalizedLiteral>

        </asp:LinkButton>


    </section>
</asp:Panel>
