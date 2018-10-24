<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheerCardListing.ascx.cs" Inherits="CMSWebParts_CMS_CheerCard_Mobile_CheerCardListing_Mobile" %>

<asp:Panel ID="panCheerCardList" runat="server" DefaultButton="NextButton">
   
    <script>
        webpartFor = "Mobile"; // Used in cheercard.js to identify webpart used for.
    </script>

    <label class="noCard">

        <cms:LocalizedRadioButton ID="rbNoCard" rel="NoImage" CssClass="cheerCardRadio" GroupName="CardGroupName" Checked='<%# IsNoImageSelected %>' runat="server" />

        <span>No card design</span>
    </label>

    <div class="accordionWrapper">
        <div id="accordion" class="panel-group">

            <cms:LocalizedHidden ID="hdnSelectedImageGuid" runat="server" />
            <cms:CMSRepeater ID="repCheerCardCategories" runat="server">
            </cms:CMSRepeater>

        </div>
    </div>
    <section class="btnWrapper">



        <asp:LinkButton ID="NextButton" class="btn btn-default" runat="server">
            Next
            <cms:LocalizedLiteral ID="NextButtonLit" runat="server" ResourceString="Emerge.CC.Mobile.CheerCardList.NextButton.Text">
            </cms:LocalizedLiteral>

        </asp:LinkButton>



    </section>

</asp:Panel>
<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>
