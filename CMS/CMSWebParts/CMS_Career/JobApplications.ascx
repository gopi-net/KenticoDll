<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobApplications.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobApplications" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="pnlJobSearch" runat="server" DefaultButton="Search">
    <section class="contentInner careerWrap">
        <div class="clearfix">
            <h1>Careers</h1>
        </div>
        <div class="searchResult">
            <h3>Job Applications</h3>
            <ul>
                <cms:CMSRepeater ID="repJobs" runat="server"  />
            </ul>
        </div>
        <div class="btnWrapper">
            <asp:Button ID="Search" Text="Go To Job Search" runat="server" />
            <asp:Button ID="ApplicationForm" Text="My Application Form" CssClass="flRight" runat="server" />
        </div>
    </section>

</asp:Panel>
<script type="text/javascript" >
    jQuery(document).ready(function ($) {
        InitializeCareer();
    });
</script>
