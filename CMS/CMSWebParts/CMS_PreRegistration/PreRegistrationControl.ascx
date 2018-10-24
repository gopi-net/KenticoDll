<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreRegistrationControl.ascx.cs" Inherits="CMSWebParts_CMS_PreRegistration_PreRegistrationControl" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDatePickerControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="pnlStep1" runat="server" DefaultButton="btnNext">
    <asp:HiddenField ID="IsOnlyForFamilyBirthCenter" runat="server" Value="Yes" />
    <asp:HiddenField ID="ServiceOrCenter" runat="server" Value="" />
    <asp:HiddenField ID="Status" runat="server" Value="Pending" />
    <asp:HiddenField ID="IsActive" runat="server" Value="Yes" />
    <div>
        <%-- Patient Information--%>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHPatientInformation" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HPatientInformation" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
           <%-- <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">--%>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLiteral ID="litEducationTraining" ResourceString="Emerge.PR.Label.FirstName" runat="server" />:<font color="red">*</font>
                    </div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="FirstName" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="First Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvFirstName" Display="dynamic" ControlToValidate="FirstName"
                                                    SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf1" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revFirstName" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="FirstName"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMiddleName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MiddleName"
                                            DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="MiddleInitial" runat="server" CssClass="text" MaxLength="1"
                                     ToolTip="Middle Initial"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revMiddle" ValidationExpression="^([a-zA-Z\'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="MiddleInitial"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                      <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litLastName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.LastName"
                                            DisplayColon="false" />:<font color="red">*</font>
                    </div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="LastName"
                                     CssClass="fright tbox text" runat="server" MaxLength="30" ToolTip="Last Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="LastName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revLastName" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="LastName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z \'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPMaidenName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PMaidenName"
                                            DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="MaidenName"
                                     CssClass="text" runat="server" MaxLength="30" ToolTip="Maiden Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPMaidenName" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="MaidenName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPMaidenName" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="MaidenName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z\'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPDOB" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PDOB"
                                            DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="DateOfBirth" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" NeedsValidation="true" />
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litEmail" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Email"
                                            DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="Email" CssClass="text" runat="server" MaxLength="100" ToolTip="Email Address"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvEmail" CssClass="ErrorMessage" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="Email" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" CssClass="ErrorMessage" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="Email" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Email Address"
                                                        ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litBirthStateOrCountry" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.BirthStateOrCountry" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BirthStateOrCountry"
                                     CssClass="text" runat="server" MaxLength="30" ToolTip="Maiden Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvBirthStateOrCountry" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="BirthStateOrCountry" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revBirthStateOrCountry" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="BirthStateOrCountry" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Commas, Dashes, Periods or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \#\,\-\.]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSSN" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SSN" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SSN"
                                     onkeyup="FormatSSN(this);" CssClass="fright tbox text" runat="server" MaxLength="11"
                                     ToolTip="Social Security Number"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="SSN" Display="dynamic" SetFocusOnError="true"
                                                    ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSSN" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="SSN" Display="dynamic" SetFocusOnError="true"
                                                        ErrorMessage="Invalid Social Security Number" ValidationExpression="^([0-9]{3}[-][0-9]{2}[-][0-9]{4}|xxx-xx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMailingAddress" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MailingAddress" DisplayColon="false" />:<font color="red">*</font></div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="MailingAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Mailing Address" onchange="CheckSameAsAboveChecked()"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="rfvMailingAddress" runat="server" ValidationGroup="reggroupf1"
                                                    Display="Dynamic" ControlToValidate="MailingAddress" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revMailingAddress" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="MailingAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litCity" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.City" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="City" CssClass="text" runat="server" MaxLength="30" ToolTip="City" onchange="CheckSameAsAboveChecked()"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="City" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revCity" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="City" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litState" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.State" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="State" DataTextField="StateName" DataValueField="ItemID" onchange="CheckSameAsAboveChecked();UpdateZipValidationByState();" />
                        <cms:CMSQueryDataSource ID="State_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="State" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required"
                                                    InitialValue="-1" CssClass="ErrorMessage">
                        </asp:RequiredFieldValidator>


                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litZip" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Zip" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="Zip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip" onchange="CheckSameAsAboveChecked();"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="rfvZip" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="Zip" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>

                        <asp:CustomValidator ID="cvZip" runat="server"
                                             ControlToValidate="Zip" Display="dynamic" SetFocusOnError="true"
                                             ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                        </asp:CustomValidator>
                    </div>
                </div>
               <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-10">
                        <cms:LocalizedLabel ID="litResidencySameAsAbove" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ResidencySameAsAbove" DisplayColon="false" />:</div>
                   <div class="col-md-6 col-sm-6 col-xs-2">
                        <asp:CheckBox ID="ResidencySameAsAbove" runat="server" onchange="CheckSameAsAboveChecked()" />
                    </div>
                </div>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litResAddress" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ResAddress" DisplayColon="false" />:<font color="red">*</font></div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ResidencyAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Residence"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator runat="server" ID="rfvResidencyAddress" ValidationGroup="reggroupf1" Display="Dynamic" ErrorMessage="Required" SetFocusOnError="true" CssClass="ErrorMessage" ControlToValidate="ResidencyAddress"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revResAddress" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="ResidencyAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litResCity" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ResCity" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ResidencyCity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="rfvResCity" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="ResidencyCity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revResCity" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="ResidencyCity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litCounty" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.County" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ResidencyCounty" CssClass="text" runat="server" MaxLength="30" ToolTip="County"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revCountry" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="ResidencyCounty" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
               <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litResState" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ResState" DisplayColon="false" />:<font color="red">*</font></div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="ResidencyState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByResState();" />
                        <cms:CMSQueryDataSource ID="ResidencyState_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvResState" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="ResidencyState" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required"
                                                    InitialValue="-1" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litResZip" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ResZip" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ResidencyZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="refResZip" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="ResidencyZip" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvResZip" runat="server"
                                             ControlToValidate="ResidencyZip" Display="dynamic" SetFocusOnError="true"
                                             ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByResState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                        </asp:CustomValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHomePhone" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HomePhone" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="HomePhone"
                                     onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                                     ToolTip="Home/Cell Phone"></asp:TextBox><div class="clear">
                    </div>
                        <asp:RequiredFieldValidator ID="rfvHomePhone" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="HomePhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revHomePhone" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="HomePhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Home/Cell Phone"
                                                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litRace" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Race" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="Race" runat="server" CssClass="rbl-form" ToolTip="Race/Ethnicity" DataTextField="Race" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="Race_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_Race.GetRace" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="revRace" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="Race" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOfHispanic" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OfHispanic" DisplayColon="false" />:<font color="red">*</font></div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="OfHispanic" runat="server" CssClass="rbl-form radio" ToolTip="Of Hispanic origin or descent?" onclick="CheckOfHispanic();">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvOfHispanic" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="OfHispanic" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
               <div class="row" id="IfOfHispanictr">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litIfOfHispanic" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.IfOfHispanic" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="IfOfHispanic" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc."></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revIfOfHispanic" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="IfOfHispanic"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litReligion" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Religion" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="Religion" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Religion"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revReligion" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="Religion"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMaritalStatus" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MaritalStatus" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="MaritalStatus" runat="server" CssClass="rbl-form" ToolTip="Marital Status" DataTextField="MaritalStatus" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="MaritalStatus_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_MaritalStatus.GetMaritalStatus" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvMaritalStatus" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="MaritalStatus" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHighestGrade" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HighestGrade" DisplayColon="false" />:<font color="red">*</font></div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="HighestGrade" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Highest Grade of School Completed"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvHighestGrade" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="HighestGrade" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revHighestGrade" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="HighestGrade"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litEmploymentStatus" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.EmploymentStatus" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="EmploymentStatus" runat="server" CssClass="rbl-form" DataTextField="EmploymentStatus" DataValueField="EmploymentStatus" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="EmploymentStatus_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_EmploymentStatus.GetEmploymentStatus" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvEmploymentStatus" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="EmploymentStatus" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
               <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOccupation" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Occupation" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="Occupation" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Occupation (Within Last Year)"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvOccupation" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="Occupation" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revOccupation" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="Occupation"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litTypeOfBusiness" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.TypeOfBusiness" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="TypeOfBusiness" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Type of Business or Industry"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revTypeOfBusiness" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="TypeOfBusiness"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litEmployer" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.Employer" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="Employer" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Employer (Within Last Year)"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvEmployer" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="Employer" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmployer" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="Employer"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litWorkPhone" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.WorkPhone" DisplayColon="false" />:</div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="WorkPhone"
                                     onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                                     ToolTip="Work/Cell Phone"></asp:TextBox><div class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revWorkPhone" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="WorkPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Work/Cell Phone"
                                                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMotherParticipatedIn" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MotherParticipatedIn" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedCheckBoxList ID="MotherParticipatedIn" runat="server" CssClass="rbl-form" DataTextField="MotherParticipatedIn" DataValueField="ItemID">
                        </cms:LocalizedCheckBoxList>
                        <cms:CMSQueryDataSource ID="MotherParticipatedIn_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_MotherParticipatedIn.GetMotherParticipatedIn" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litDidMotherSmoke" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.DidMotherSmoke" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="DidMotherSmoke" runat="server" CssClass="rbl-form radio" ToolTip="Did mother smoke at any time during pregnancy?">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvDidMotherSmoke" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="DidMotherSmoke" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litAdvanceDirectives" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.AdvanceDirectives" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="AdvanceDirectives" runat="server" CssClass="rbl-form radio" ToolTip="Advance Directives (Living Will or Durable Power of Attorney for Health Care)">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvAdvanceDirectives" runat="server" ValidationGroup="reggroupf1"
                                                    ControlToValidate="AdvanceDirectives" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
          <%--  </table>--%>
        </div>
        <%-- Guarantor Information--%>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHResponsibleParty" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HResponsibleParty" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <%--<table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">--%>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litFirstNameGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.FirstNameGi" DisplayColon="false" />:
                    </div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIFirstName" runat="server" CssClass="text" MaxLength="30" ToolTip="First Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revFirstNameGi" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="GIFirstName"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMiddleGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MiddleGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIMiddleInitial" runat="server" CssClass="text" MaxLength="1" ToolTip="Middle Initial"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revMiddleGi" ValidationExpression="^([a-zA-Z\'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="GIMiddleInitial"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litLastNameGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.LastNameGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GILastName" CssClass="fright tbox text" runat="server" MaxLength="30" ToolTip="Last Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revLastNameGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GILastName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z \'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litRelationshipGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.RelationshipGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIRelationship" CssClass="fright tbox text" runat="server" MaxLength="30" ToolTip="Relationship"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revRelationshipGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GIRelationship" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z\'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPDOBGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PDOBGi" DisplayColon="false" />:</div>
                   <div class="col-md-6 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="GIDateOfBirth" runat="server" EditTime="false" DisplayNow="false" IsRequired="false" NeedsValidation="true" class="icon-calendar" />
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSSNGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SSNGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GISSN" onkeyup="FormatSSN(this);" CssClass="fright tbox text" runat="server" MaxLength="11"
                                     ToolTip="Social Security Number"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revSSNGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GISSN" Display="dynamic" SetFocusOnError="true"
                                                        ErrorMessage="Invalid Social Security Number" ValidationExpression="^([0-9]{3}[-][0-9]{2}[-][0-9]{4}|xxx-xx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMailingAddressGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MailingAddressGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revMailingAddressGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GIAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litCityGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.CityGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GICity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revCityGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GICity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litStateGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.StateGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="GIState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByGIState();" />
                        <cms:CMSQueryDataSource ID="GIState_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litZipGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ZipGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:CustomValidator ID="cvGIZip" runat="server"
                                             ControlToValidate="GIZip" Display="dynamic" SetFocusOnError="true"
                                             ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByGIState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                        </asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litGenderGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.GenderGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="GIGender" runat="server" CssClass="rbl-form radio">
                            <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOtherLegalNamesGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OtherLegalNamesGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIOtherLegalNames" CssClass="fright tbox text" runat="server" MaxLength="50" ToolTip="Other Legal Names"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revOtherLegalNamesGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GIOtherLegalNames" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z \'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litRaceGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.RaceGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="GIRace" runat="server" CssClass="rbl-form" ToolTip="Race/Ethnicity" DataTextField="Race" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="GIRace_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_Race.GetRace" />
                    </div>
                </div>
                <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litReligionGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ReligionGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIReligion" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Religion"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revReligionGi" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="GIReligion"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
               <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMaritalStatusGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MaritalStatusGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="GIMaritalStatus" runat="server" CssClass="rbl-form" ToolTip="Marital Status" DataTextField="MaritalStatus" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="GIMaritalStatus_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_MaritalStatus.GetMaritalStatus" />
                    </div>
                </div>
                <div class="row">
                   <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litEmploymentStatusGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.EmploymentStatusGi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="GIEmploymentStatus" runat="server" CssClass="rbl-form" DataTextField="EmploymentStatus" DataValueField="EmploymentStatus" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="GIEmploymentStatus_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_EmploymentStatus.GetEmploymentStatus" />
                    </div>
                </div>
                <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOccupationGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OccupationGi" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIOccupation" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Occupation (Within Last Year)"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revOccupationGi" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="GIOccupation"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litEmployerGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.EmployerGi" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIEmployer" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Employer (Within Last Year)"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revEmployerGi" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="GIEmployer"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litWorkPhoneGi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.WorkPhoneGi" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="GIWorkPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                                     ToolTip="Work/Cell Phone"></asp:TextBox><div class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revWorkPhoneGi" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="GIWorkPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Work/Cell Phone"
                                                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
          <%--  </table>--%>
        </div>
        <%-- Employer Information--%>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHEmployer" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HEmployer" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
           <%-- <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">--%>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litCompanyEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.CompanyEmp" DisplayColon="false" />:
                    </div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ECompany" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Company"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revCompanyEmp" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="ECompany"
                                                        ValidationGroup="reggroupf1" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMailingAddressEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MailingAddressEmp" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="EAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revMailingAddressEmp" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="EAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litCityEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.CityEmp" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ECity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revCityEmp" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="ECity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litStateEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.StateEmp" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="EState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByEState();" />
                        <cms:CMSQueryDataSource ID="EState_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litZipEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ZipEmp" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="EZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                            class="clear">
                    </div>
                        <asp:CustomValidator ID="cvEZip" runat="server"
                                             ControlToValidate="EZip" Display="dynamic" SetFocusOnError="true"
                                             ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByEState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                        </asp:CustomValidator>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPhoneEmp" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PhoneEmp" DisplayColon="false" />:</div>
                     <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="EPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                                     ToolTip="Phone"></asp:TextBox><div class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revPhoneEmp" runat="server" ValidationGroup="reggroupf1"
                                                        ControlToValidate="EPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Phone"
                                                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    
                     <div class="col-md-3 col-sm-6 col-xs-12 box-button text-right">
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="FormButton" ValidationGroup="reggroupf1" OnClick="btnNext_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="FormButton" OnClick="btnClear_Click" />
                    </div>
                </div>
          <%--  </table>--%>
        </div>
    </div>
</asp:Panel>
<script type="text/javascript">
    HideOfHispanic();
    CheckOfHispanic();
    IfSameAsAboveChecked();
    function DisableTextboxes() {
        document.getElementById('<%= ResidencyAddress.ClientID %>').disabled = true;
        document.getElementById('<%= ResidencyCity.ClientID %>').disabled = true;
        document.getElementById('<%= ResidencyZip.ClientID %>').disabled = true;
        document.getElementById('<%= ResidencyState.ClientID %>').disabled = true;
    }
    function AssignResidencySameAsAboveValues() {
        document.getElementById('<%= ResidencyAddress.ClientID %>').value = document.getElementById('<%= MailingAddress.ClientID %>').value;
        document.getElementById('<%= ResidencyCity.ClientID %>').value = document.getElementById('<%= City.ClientID %>').value;
        document.getElementById('<%= ResidencyZip.ClientID %>').value = document.getElementById('<%= Zip.ClientID %>').value;
        document.getElementById('<%= ResidencyState.ClientID %>').selectedIndex = document.getElementById('<%= State.ClientID %>').selectedIndex;
    }
    function EnableTextBoxes() {
        document.getElementById('<%= ResidencyAddress.ClientID %>').disabled = false;
        document.getElementById('<%= ResidencyCity.ClientID %>').disabled = false;
        document.getElementById('<%= ResidencyZip.ClientID %>').disabled = false;
        document.getElementById('<%= ResidencyState.ClientID %>').disabled = false;
    }
    function EnableValidators() {
        var valResAddress = document.getElementById("<%=rfvResidencyAddress.ClientID%>");
        var valResCity = document.getElementById("<%=rfvResCity.ClientID%>");
        var valResState = document.getElementById("<%=rfvResState.ClientID%>");
        var valResZip = document.getElementById("<%=refResZip.ClientID%>");
        ValidatorEnable(valResAddress, true);
        ValidatorEnable(valResCity, true);
        ValidatorEnable(valResState, true);
        ValidatorEnable(valResZip, true);
    }
    function DisableValidators() {
        var valResAddress = document.getElementById("<%=rfvResidencyAddress.ClientID%>");
        var valResCity = document.getElementById("<%=rfvResCity.ClientID%>");
        var valResState = document.getElementById("<%=rfvResState.ClientID%>");
        var valResZip = document.getElementById("<%=refResZip.ClientID%>");
        ValidatorEnable(valResAddress, false);
        ValidatorEnable(valResCity, false);
        ValidatorEnable(valResState, false);
        ValidatorEnable(valResZip, false);
    }
    function CheckSameAsAboveChecked() {
        var valResAddress = document.getElementById("<%=rfvResidencyAddress.ClientID%>");
        var valResCity = document.getElementById("<%=rfvResCity.ClientID%>");
        var valResState = document.getElementById("<%=rfvResState.ClientID%>");
        var valResZip = document.getElementById("<%=refResZip.ClientID%>");

        if (document.getElementById('<%= ResidencySameAsAbove.ClientID %>').checked) {
            AssignResidencySameAsAboveValues();
            DisableTextboxes();
            DisableValidators();
        }
        else {
            document.getElementById('<%= ResidencyAddress.ClientID %>').value = '';
            document.getElementById('<%= ResidencyCity.ClientID %>').value = '';
            document.getElementById('<%= ResidencyZip.ClientID %>').value = '';
            document.getElementById('<%= ResidencyState.ClientID %>').selectedIndex = 0;
            EnableTextBoxes();
            EnableValidators();
        }
    }
    function CheckSameAsAboveChecked2() {
        var valResAddress = document.getElementById("<%=rfvResidencyAddress.ClientID%>");
        var valResCity = document.getElementById("<%=rfvResCity.ClientID%>");
        var valResState = document.getElementById("<%=rfvResState.ClientID%>");
        var valResZip = document.getElementById("<%=refResZip.ClientID%>");

        if (document.getElementById('<%= ResidencySameAsAbove.ClientID %>').checked) {
            AssignResidencySameAsAboveValues();
            DisableTextboxes();
            DisableValidators();
        }
        else {
            EnableTextBoxes();
            EnableValidators();
        }
    }
    function CheckOfHispanic() {
        var RB1 = document.getElementById("<%=OfHispanic.ClientID%>");
        var radio = RB1.getElementsByTagName("input");
        if (radio[0].checked) {
            document.getElementById("IfOfHispanictr").style.display = 'table-row';
        }
        else if (radio[1].checked) {
            document.getElementById("IfOfHispanictr").style.display = 'none';
            document.getElementById("<%=IfOfHispanic.ClientID%>").value = '';
        }
        else
            document.getElementById("<%=IfOfHispanic.ClientID%>").value = '';
    }
    function HideOfHispanic() {
        document.getElementById("IfOfHispanictr").style.display = 'none';
    }
    function IfSameAsAboveChecked() {
        if (document.getElementById('<%= ResidencySameAsAbove.ClientID %>').checked) {
            AssignResidencySameAsAboveValues();
            DisableTextboxes();
        }
    }

</script>
<script type="text/javascript">
    var stateBC = "British Columbia";
    function ValidateZipByState(source, arguments) {
        if (jQuery(document.getElementById('<%= State.ClientID %>')).children(':selected').text() == stateBC) {
            alert("1");
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipByResState(source, arguments) {
        if (jQuery(document.getElementById('<%= ResidencyState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipByEState(source, arguments) {
        if (jQuery(document.getElementById('<%= EState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipByGIState(source, arguments) {
        if (jQuery(document.getElementById('<%= GIState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function UpdateZipValidationByState() {
        ValidatorValidate(document.getElementById('<%= cvZip.ClientID %>'));
    }
    function UpdateZipValidationByResState() {
        ValidatorValidate(document.getElementById('<%= cvResZip.ClientID %>'));
    }
    function UpdateZipValidationByEState() {
        ValidatorValidate(document.getElementById('<%= cvEZip.ClientID %>'));
    }
    function UpdateZipValidationByGIState() {
        ValidatorValidate(document.getElementById('<%= cvGIZip.ClientID %>'));
    }

</script>