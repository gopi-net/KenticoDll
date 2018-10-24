<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DonationConfirmation.ascx.cs" Inherits="CMSWebParts_CMS_Donation_DonationConfirmation" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="ThankYouPanel" runat="server">
    <h1>Online Donation</h1>
                     <hr>
                    <h3>Thank you for your contribution!</h3>
                    <p>You will receive a confirmation email shortly. Please save it for your records.</p>
</asp:Panel>