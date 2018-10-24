<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobAffidevit.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobAffidevit" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDateTimeControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlAffidavit" runat="server" DefaultButton="Save">

        <div class="clearfix">
            <h1> <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right"><i><cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page6" runat="server" /></i></div>
        </div>
        
        <div class="personalInfo employmentHistory">
            <h3>
                <cms:LocalizedLiteral ID="litAffidavitAuthorization" ResourceString="Emerge.CR.AffidavitAuthorization" runat="server" />:</h3>
            <hr>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litAffidavitAuthorizationAuthorizationDetails" ResourceString="Emerge.CR.AffidavitAuthorizationAuthorizationDetails" runat="server" />
                              <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="AuthorizationDetails" TextMode="MultiLine" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAuthorizationDetails" ValidationGroup="CRAffidavit" runat="server" ErrorMessage="Required" ControlToValidate="AuthorizationDetails" SetFocusOnError="true" CssClass="ErrorMessage" />
                        </div>
                    </div>
                     <div class="row">
                         <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litAffidavitAuthorizationApplicantSignature" ResourceString="Emerge.CR.AffidavitAuthorizationApplicantSignature" runat="server" />
                               <span style="color: red">*</span>:</label></div>

                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ApplicantSignature" MaxLength="30" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvApplicantSignature" ValidationGroup="CRAffidavit" runat="server" ErrorMessage="Required" ControlToValidate="ApplicantSignature" SetFocusOnError="true" CssClass="ErrorMessage" />
                            <asp:RegularExpressionValidator ID="revApplicantSignature" ControlToValidate="ApplicantSignature" runat="server" ErrorMessage="Invalid Input" SetFocusOnError="true" ValidationGroup="CRAffidavit" ValidationExpression="[a-zA-Z .,-]*" CssClass="ErrorMessage" />

                        </div>
                    </div>
                     <div class="row">
                         <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litAffidavitAuthorizationDate" ResourceString="Emerge.CR.AffidavitAuthorizationDate" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                         <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="Date" runat="server" DisplayNow="false" IsRequired="true" EditTime="false" />
                        </div>
                    </div>
        </div>

        <div class="clearfix">
        <div class="btnWrapper clearfix pull-left">
            <asp:LinkButton ID="Save" Text="Save" ValidationGroup="CRAffidavit" runat="server" />
            <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
            </div>
             <div class="btnWrapper clearfix pull-right btn-prev">
            <asp:LinkButton ID="Next" Text="Save & Next <span class='icon-rightArrowGrey'>" ValidationGroup="CRAffidavit" runat="server" CssClass="pull-right" />
            <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" runat="server" CssClass="pull-right" />

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