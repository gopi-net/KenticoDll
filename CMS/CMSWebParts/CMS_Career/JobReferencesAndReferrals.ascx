<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobReferencesAndReferrals.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobReferencesAndReferrals" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDateTimeControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>
<section class="contentInner careerWrap">
    <asp:Panel ID="pnlMain" runat="server">

        <div class="clearfix">
            <h1>
                <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right">
                <i>
                    <cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page5" runat="server" /></i>
            </div>
        </div>
        <asp:Panel ID="pnlReference" runat="server" DefaultButton="Add">
            <div class="personalInfo employmentHistory">
                <h3>
                    <cms:LocalizedLiteral ID="litReferences" ResourceString="Emerge.CR.References" runat="server" />:</h3>
                <hr>
                <cms:CMSRepeater ID="repReferences" runat="server">
                </cms:CMSRepeater>
                <hr>

                <div  class="referenceFields">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesReferenceType" ResourceString="Emerge.CR.ReferencesReferenceType" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="ReferenceType" runat="server" CssClass="rbl">
                                <asp:ListItem Text="Business" Value="Business"></asp:ListItem>
                                <asp:ListItem Text="Character" Value="Character"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvReferenceType" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="ReferenceType" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesSupervisorName" ResourceString="Emerge.CR.ReferencesSupervisorName" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12 name">

                            <asp:TextBox ID="FirstName" MaxLength="30" runat="server" placeholder="First Name" />
                            <asp:TextBox ID="LastName" MaxLength="30" runat="server" placeholder="Last Name"/>
                            <asp:RequiredFieldValidator ID="rfvFirstName" ValidationGroup="CRReferences" SetFocusOnError="true" CssClass="ErrorMessage" runat="server" ControlToValidate="FirstName" ErrorMessage="Required" />
                            <asp:RequiredFieldValidator ID="rfvLastName" ValidationGroup="CRReferences" SetFocusOnError="true" CssClass="ErrorMessage" runat="server" ControlToValidate="LastName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesCompanyName" ResourceString="Emerge.CR.ReferencesCompanyName" runat="server" />
                                :</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="CompanyName" MaxLength="100" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesAddress" ResourceString="Emerge.CR.ReferencesAddress" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="Address" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvAddress" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="Address" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesCity" ResourceString="Emerge.CR.ReferencesCity" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="City" MaxLength="50" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvCity" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="City" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesState" ResourceString="Emerge.CR.ReferencesState" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:DropDownList ID="State" runat="server" DataTextField="State" DataValueField="ItemId"></asp:DropDownList>
                            <cms:CMSQueryDataSource ID="State_DataSource" runat="server"
                                                    QueryName="customtable.Emerge_{0}_CR_States.GetStates" />
                            <asp:RequiredFieldValidator ID="rfvState" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="State" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesZip" ResourceString="Emerge.CR.ReferencesZip" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="Zip" runat="server" CssClass="zipField" MaxLength="5" />
                            <asp:RequiredFieldValidator ID="rfvZip" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="Zip" ErrorMessage="Required" />
							<asp:RegularExpressionValidator ID="revZip" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="Zip" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid zip."
                                                        ValidationExpression="^([0-9]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesBusinessPhoneNum" ResourceString="Emerge.CR.ReferencesBusinessPhoneNum" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12 name">
                            <asp:TextBox ID="BusinessPhoneNum" MaxLength="14" CssClass="phoneMask" runat="server" />
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesBusinessPhoneExt" ResourceString="Emerge.CR.ReferencesBusinessPhoneExt" runat="server" />
                                :</label>
                            <asp:TextBox ID="BusinessPhoneExt" CssClass="extn" runat="server" MaxLength="4" />
                            <asp:TextBox ID="BusinessPhone" Visible="false" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvBusinessPhoneNum" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="BusinessPhoneNum" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesCell" ResourceString="Emerge.CR.ReferencesCell" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="Cell" MaxLength="14" CssClass="phoneMask" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvCell" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="Cell" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferencesHomePhone" ResourceString="Emerge.CR.ReferencesHomePhone" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="HomePhone" MaxLength="14" CssClass="phoneMask" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvHomePhone" ValidationGroup="CRReferences" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="HomePhone" ErrorMessage="Required" />
                        </div>
                    </div>

                    
                </table>
            </div>
            <div class="btnWrapper">
                <asp:LinkButton CssClass="referenceFields" ID="Add" Text="Add" ValidationGroup="CRReferences" runat="server" />
            </div>
        </asp:Panel>
        <div class="message_box">
            <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcReference" BasicStyles="true" runat="server" />
        </div>
        <asp:Panel ID="pnlReferral" runat="server" DefaultButton="Save">
            <div class="personalInfo employmentHistory">
                <h3>
                    <cms:LocalizedLiteral ID="litReferrals" ResourceString="Emerge.CR.Referrals" runat="server" />
                    :</h3>
                <hr>
                <div  class="referrals">
                    
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <asp:CheckBoxList ID="Referral" runat="server" RepeatDirection="Horizontal" RepeatColumns="2" CssClass="rbl ReferralCheckBoxList">
                                <asp:ListItem Text="Newspaper" Value="NewspaperName"></asp:ListItem>
                                <asp:ListItem Text="School" Value="SchoolName"></asp:ListItem>
                                <asp:ListItem Text="Open House" Value="OpenHouse"></asp:ListItem>
                                <asp:ListItem Text="Career/Job Fair" Value="JobFair"></asp:ListItem>
                                <asp:ListItem Text="Internet Posting" Value="InternetPosting"></asp:ListItem>
                                <asp:ListItem Text="Walk In" Value="Walk In"></asp:ListItem>
                                <asp:ListItem Text="Employment Agency" Value="EmploymentAgency"></asp:ListItem>
                                <asp:ListItem Text="Professional Journal" Value="ProfessionalJournal"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                <asp:ListItem Text="Employee Referral" Value="EmployeeReferral"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    
                </div>
                <div  class="referrals-form">
                    
                    <div class="checkBoxDependentTextbox row" id="trNewspaperName">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsNewspaperName" ResourceString="Emerge.CR.ReferralsNewspaperName" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="NewspaperName" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvNewspaperName" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvNewspaperName" runat="server" ControlToValidate="NewspaperName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trSchoolName">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsSchoolName" ResourceString="Emerge.CR.ReferralsSchoolName" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="SchoolName" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvSchoolName" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvSchoolName" runat="server" ControlToValidate="SchoolName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trOpenHouse">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsOpenHouse" ResourceString="Emerge.CR.ReferralsOpenHouse" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OpenHouse" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvOpenHouse" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvOpenHouse" runat="server" ControlToValidate="OpenHouse" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trJobFair">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsJobFair" ResourceString="Emerge.CR.ReferralsJobFair" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="JobFair" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvJobFair" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvJobFair" runat="server" ControlToValidate="JobFair" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trInternetPosting">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsInternetPosting" ResourceString="Emerge.CR.ReferralsInternetPosting" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="InternetPosting" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvInternetPosting" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvInternetPosting" runat="server" ControlToValidate="InternetPosting" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trEmploymentAgency">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsEmploymentAgency" ResourceString="Emerge.CR.ReferralsEmploymentAgency" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="EmploymentAgency" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvEmploymentAgency" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvEmploymentAgency" runat="server" ControlToValidate="EmploymentAgency" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trProfessionalJournal">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsProfessionalJournal" ResourceString="Emerge.CR.ReferralsProfessionalJournal" runat="server" />
                                <span style="color: red">*</span>:</label></div>

                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ProfessionalJournal" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvProfessionalJournal" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvProfessionalJournal" runat="server" ControlToValidate="ProfessionalJournal" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trOther">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsOther" ResourceString="Emerge.CR.ReferralsOther" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="Other" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvOther" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvOther" runat="server" ControlToValidate="Other" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="checkBoxDependentTextbox row" id="trEmployeeReferral">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litReferralsEmployeeReferral" ResourceString="Emerge.CR.ReferralsEmployeeReferral" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="EmployeeReferral" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvEmployeeReferral" ValidationGroup="CRReferrals" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage referralValidator rfvEmployeeReferral" runat="server" ControlToValidate="EmployeeReferral" ErrorMessage="Required" />
                        </div>
                    </div>
                    
                </div>
            </div>
        </asp:Panel>
         <div class="clearfix">
        <div class="btnWrapper cearfix pull-left">
            <asp:LinkButton ID="Save" Text="Save Referral" ValidationGroup="CRReferrals" runat="server" />
            <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
            </div>
         <div class="btnWrapper clearfix pull-right btn-prev">
            <asp:LinkButton ID="Next" Text="Next <span class='icon-rightArrowGrey'>" runat="server" CssClass="pull-right" />
            <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" runat="server" CssClass="pull-right" />

        </div>
        </div>
        <script>
            function showHideReferral(detailId) {
                jQuery('#detail' + detailId).slideToggle("slow", function () {
                    jQuery('#showDetailButton' + detailId).toggleClass('openWrap');
                });
            }
            jQuery('#AddNewReference').click(function () {
                jQuery('.referenceFields').slideToggle("slow", function () {
                });
            });
            jQuery(document).ready(function () {
                jQuery('input[type=text], textarea').placeholder();
                InitializeCareer();

            });
        </script>
    </asp:Panel>
    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>
