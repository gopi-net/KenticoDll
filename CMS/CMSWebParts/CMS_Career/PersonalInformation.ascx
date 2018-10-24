<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalInformation.ascx.cs" Inherits="CMSWebParts_CMS_Career_PersonalInformation" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlPersonalInformation" runat="server" DefaultButton="Submit">

        <div class="clearfix">
            <h1><cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right"><i><cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page1" runat="server" /></i></div>
        </div>
        <asp:Panel ID="pnlPostionDetails" runat="server" Visible="false">
            <div class="positionDetails jobDetails">
                <h3><cms:LocalizedLiteral ID="litPositionDetails" ResourceString="Emerge.CR.PositionDetails" runat="server" />:</h3>
                <hr>
                <dl>
                    <dt><cms:LocalizedLiteral ID="litPositionAppliedFor" ResourceString="Emerge.CR.PositionAppliedFor" runat="server" />:</dt>
                    <dd>
                        <asp:Label ID="Position" runat="server"></asp:Label></dd>
                    <dt><cms:LocalizedLiteral ID="litApplicationDate" ResourceString="Emerge.CR.ApplicationDate" runat="server" />:</dt>
                    <dd>
                        <asp:Label ID="ApplicationDate" runat="server"></asp:Label></dd>
                    <dt><cms:LocalizedLiteral ID="litPositionLocation" ResourceString="Emerge.CR.PositionLocation" runat="server" />:</dt>
                    <dd>
                        <asp:Label ID="Location" runat="server"></asp:Label></dd>
                </dl>
            </div>
        </asp:Panel>
        <div class="personalInfo">
            <h3><cms:LocalizedLiteral ID="litPresonalInformation" ResourceString="Emerge.CR.PresonalInformation" runat="server" />:</h3>
            <hr>
            <div class="clearfix">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litName" ResourceString="Emerge.CR.Name" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12 name">
                        <asp:TextBox ID="ApplicantFirstName" MaxLength="30" runat="server" placeholder="First Name" />

                        <asp:TextBox ID="ApplicantMiddleName" MaxLength="30" runat="server" placeholder="Middle Name"  />

                        <asp:TextBox ID="ApplicantLastName" MaxLength="30" runat="server"  placeholder="Last Name" />
                        <asp:RequiredFieldValidator ID="rfvApplicantFirstName" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantFirstName" ErrorMessage="Required" Style="position: relative;" />
                        <asp:RequiredFieldValidator ID="rfvApplicantLastName" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantLastName" ErrorMessage="Required" Style="position: relative; left: 255px;" />


                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litAddress" ResourceString="Emerge.CR.Address" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantAddress" TextMode="MultiLine" Style="padding: 6px 12px;" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantAddress" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantAddress" ErrorMessage="Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litCity" ResourceString="Emerge.CR.City" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantCity" MaxLength="50" runat="server" />
                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvApplicantCity" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantCity" ErrorMessage="Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litState" ResourceString="Emerge.CR.State" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="ApplicantState" DataTextField="State" DataValueField="ItemId" />
                        <cms:CMSQueryDataSource ID="ApplicantState_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_CR_States.GetStates" />
                        <asp:RequiredFieldValidator Display="Dynamic" ID="rfvApplicantState" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" InitialValue="-1" runat="server" ControlToValidate="ApplicantState" ErrorMessage="Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litZip" ResourceString="Emerge.CR.Zip" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantZip" runat="server" CssClass="zipField" MaxLength="5" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantZip" runat="server" CssClass="ErrorMessage" ControlToValidate="ApplicantZip" ValidationExpression="[0-9]*" ErrorMessage="Invalid Input" SetFocusOnError="true" ValidationGroup="CRPersonalInfo" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litTelHome" ResourceString="Emerge.CR.TelHome" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantPhoneHome" MaxLength="14" CssClass="phoneMask" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantPhoneHome" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantPhoneHome" ErrorMessage="Required" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantPhoneHome" runat="server" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantPhoneHome" ValidationExpression="^[(]\d{3}[)][ ]\d{3}-\d{4}$" ErrorMessage="Invalid Input" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litTelCellBeeperOther" ResourceString="Emerge.CR.TelCellBeeperOther" runat="server" />
                            <span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantPhoneOther" MaxLength="14" CssClass="phoneMask" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantPhoneOther" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantPhoneOther" ErrorMessage="Required" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantPhoneOther" runat="server" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantPhoneOther" ValidationExpression="^[(]\d{3}[)][ ]\d{3}-\d{4}$" ErrorMessage="Invalid Input" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litEmail" ResourceString="Emerge.CR.Email" runat="server" />
                            <span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmail" MaxLength="100" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantEmail" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantEmail" ErrorMessage="Required" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantEmail" ValidationGroup="CRPersonalInfo" runat="server" CssClass="ErrorMessage" ControlToValidate="ApplicantEmail" ValidationExpression="\b[a-zA-Z0-9._%+!#$*/_-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,4}\b" ErrorMessage="Invalid Input" SetFocusOnError="true" />
                    </div>
                </div>
            </div>

            <div class="halfContent">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litApplicantCanFurnishWorkPermit" ResourceString="Emerge.CR.ApplicantCanFurnishWorkPermit" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="ApplicantCanFurnishWorkPermit" RepeatDirection="Horizontal" CssClass="rbl" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" />
                        </cms:LocalizedRadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litApplicantElegibleForEmploymentInCountry" ResourceString="Emerge.CR.ApplicantElegibleForEmploymentInCountry" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="ApplicantElegibleForEmploymentInCountry" RepeatDirection="Horizontal" CssClass="rbl" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" Selected="True" />
                            <asp:ListItem Text="No" Value="No" />
                        </cms:LocalizedRadioButtonList>

                        <asp:RequiredFieldValidator ID="rfvApplicantElegibleForEmploymentInCountry" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantElegibleForEmploymentInCountry" ErrorMessage="Required" />
                    </div>
                </div>
            </div>
            <div><cms:LocalizedLiteral ID="litInCaseOfEmergencyNotify" ResourceString="Emerge.CR.InCaseOfEmergencyNotify" runat="server" />:</div>
            <div class="clearfix">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litRelName" ResourceString="Emerge.CR.Name" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmergengyContactName" MaxLength="90" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantEmergengyContactName" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantEmergengyContactName" ErrorMessage="Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litRelationship" ResourceString="Emerge.CR.Relationship" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmergengyContactRelationship" MaxLength="50" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantEmergengyContactRelationship" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantEmergengyContactRelationship" ErrorMessage="Required" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litRelTelHome" ResourceString="Emerge.CR.TelHome" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmergengyContactPhoneHome" MaxLength="14" CssClass="phoneMask" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantEmergengyContactPhoneHome" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantEmergengyContactPhoneHome" ErrorMessage="Required" />
                        <asp:RegularExpressionValidator ID="revApplicantEmergengyContactPhoneHome" Display="Dynamic" runat="server" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantEmergengyContactPhoneHome" ValidationExpression="^[(]\d{3}[)][ ]\d{3}-\d{4}$" ErrorMessage="Invalid Input" CssClass="ErrorMessage" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litRelTelCell" ResourceString="Emerge.CR.TelCell" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmergengyContactPhoneCell" MaxLength="14" CssClass="phoneMask" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvApplicantEmergengyContactPhoneCell" Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CRPersonalInfo" runat="server" ControlToValidate="ApplicantEmergengyContactPhoneCell" ErrorMessage="Required" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantEmergengyContactPhoneCell" runat="server" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantEmergengyContactPhoneCell" ValidationExpression="^[(]\d{3}[)][ ]\d{3}-\d{4}$" CssClass="ErrorMessage" ErrorMessage="Invalid Input" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label><cms:LocalizedLiteral ID="litRelTelWork" ResourceString="Emerge.CR.TelWork" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ApplicantEmergengyContactPhoneWorkNum" MaxLength="14" CssClass="phoneMask" runat="server" />
                        <label><cms:LocalizedLiteral ID="litRelWorkExt" ResourceString="Emerge.CR.Ext" runat="server" />:</label>
                        <asp:TextBox ID="ApplicantEmergengyContactPhoneWorkExt" MaxLength="4" CssClass="extn" runat="server" />
                        <asp:TextBox ID="ApplicantEmergengyContactPhoneWork" runat="server" Visible="false" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantEmergengyContactPhoneWorkNum" runat="server" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantEmergengyContactPhoneWorkNum" ValidationExpression="^[(]\d{3}[)][ ]\d{3}-\d{4}$" CssClass="ErrorMessage" ErrorMessage="Invalid Input" SetFocusOnError="true" />
                        <asp:RegularExpressionValidator Display="Dynamic" ID="revApplicantEmergengyContactPhoneWorkExt" runat="server" ValidationGroup="CRPersonalInfo" ControlToValidate="ApplicantEmergengyContactPhoneWorkExt" ValidationExpression="^\d{4}$" ErrorMessage="Invalid Input" CssClass="ErrorMessage" SetFocusOnError="true" />
                    </div>
                </div>
            </div>

            <div class="clearfix">
               <div class="btnWrapper clearfix pull-left">
                <asp:LinkButton ID="Submit" Text="Save" ValidationGroup="CRPersonalInfo" runat="server" />
                <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
            </div>
               <div class="btnWrapper clearfix pull-right btn-prev">
                <asp:LinkButton ID="Next" Text="Save & Next<span class='icon-rightArrowGrey'></span>" CssClass="pull-right" ValidationGroup="CRPersonalInfo" runat="server" />
                <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" CssClass="pull-right" runat="server" />
            </div>
            </div>
        </div>

    </asp:Panel>
    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>
<script type="text/javascript">
    jQuery(document).ready(function ($) {
        InitializeCareer();
        jQuery('input[type=text], textarea').placeholder();
    });
</script>
