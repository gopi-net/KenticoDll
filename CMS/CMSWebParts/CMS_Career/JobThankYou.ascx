<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobThankYou.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobThankYou" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlJobDetails" runat="server" DefaultButton="Submit">
          <div class="clearfix">
            <h1>
                <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
        <div class="jobDetails">
            <cms:CMSEditableRegion ID="editableRegion" runat="server" RegionType="HtmlEditor" RegionTitle="Default Text" />
            <div class="btnWrapper">
                <asp:Button ID="Submit" Text="Apply" runat="server" />
            </div>
        </div>

    </asp:Panel>
    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>

