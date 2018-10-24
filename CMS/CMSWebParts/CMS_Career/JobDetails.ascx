<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobDetails.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobDetails" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlJobDetails" runat="server" DefaultButton="Submit">

        <div class="clearfix">
            <h1>Careers</h1>
        </div>
                <div class="jobDetails">
            <dl>
                <cms:CMSRepeater ID="JobDetails" runat="server">
                </cms:CMSRepeater>
            </dl>
            <asp:Label ID="lblNoDataFound" Text="No Data Found" runat="server" Visible="false"></asp:Label>

            <div class="btnWrapper">
                <asp:Button ID="Submit" Text="Apply" runat="server" />
                <asp:Button ID="FillForm" Text="Application Form" CssClass="pull-right" runat="server" />
                <asp:Button ID="Back" Text="Back" runat="server" />
            </div>
        </div>

    </asp:Panel>
    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>

<script type="text/javascript" >
    jQuery(document).ready(function ($) {
        InitializeCareer();
    });
</script>
