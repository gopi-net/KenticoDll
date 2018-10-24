<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreRegistrationStep3.ascx.cs" Inherits="CMSWebParts_CMS_PreRegistration_PreRegistrationStep3" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDatePickerControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="pnlStep3" runat="server" DefaultButton="btnNext">
    <div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHVisitInformation" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HVisitInformation" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPDOBSD" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PDOBSD" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="ScheduledDateOfDelivery" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" NeedsValidation="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHealthcareProviderNameVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HealthcareProviderNameVi" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="ProvidersName" runat="server" CssClass="text" MaxLength="40"
                                     ToolTip="Healthcare Provider’s Name for this Pregnancy"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvHealthcareProviderNameVi" Display="dynamic" ControlToValidate="ProvidersName"
                                                    SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf3" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revHealthcareProviderNameVi" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="ProvidersName"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPrimaryCarePhyNameVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PrimaryCarePhyNameVi" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PCPhysiciansName" runat="server" CssClass="text" MaxLength="40"
                                     ToolTip="Primary Care Physician’s Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvPrimaryCarePhyNameVi" Display="dynamic" ControlToValidate="PCPhysiciansName"
                                                    SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf3" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revPrimaryCarePhyNameVi" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="PCPhysiciansName"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPediatricianNameVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PediatricianNameVi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PediatriciansName" runat="server" CssClass="text" MaxLength="40"
                                     ToolTip="Pediatrician’s Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revPediatricianNameVi" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="PediatriciansName"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHaveYouVisitedBeforeVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HaveYouVisitedBeforeVi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="VisitedBefore" runat="server" CssClass="rbl-form radio" ToolTip="Have you visited before?">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litNameIfChangedVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.NameIfChangedVi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="NameIfChanged" runat="server" CssClass="text" MaxLength="40"
                                     ToolTip="Your name then if changed"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revNameIfChangedVi" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="NameIfChanged"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHowFindOutUsVi" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HowFindOutUsVi" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="HowYouFindUs" runat="server" CssClass="rbl-form" DataTextField="HowFindOutAboutUs" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="HowYouFindUs_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_HowFindOutAboutUs.GetHowFindOutAboutUs" />
                    </div>
                </div>
        </div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHBabysParentInfo" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HBabysParentInfo" DisplayColon="false" /></h3>
            <hr />
        </div>
        <div class="formSectn">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litFirstNameBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.FirstNameBPI" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIFirstName" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="First Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvFirstNameBPI" Display="dynamic" ControlToValidate="BPIFirstName"
                                                    SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf3" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revFirstNameBPI" ValidationExpression="^([a-zA-Z \'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPIFirstName"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litMiddleBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MiddleBPI" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIMiddleInitial" runat="server" CssClass="text" MaxLength="1"
                                     ToolTip="Middle Initial"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revMiddleBPI" ValidationExpression="^([a-zA-Z\'\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPIMiddleInitial"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litLastNameBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.LastNameBPI" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPILastName"
                                     CssClass="fright tbox text" runat="server" MaxLength="30" ToolTip="Last Name"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvLastNameBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPILastName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revLastNameBPI" runat="server" ValidationGroup="reggroupf3"
                                                        ControlToValidate="BPILastName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Dashes, Apostrophes are allowed."
                                                        ValidationExpression="^([a-zA-Z \'\-]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litPDOBBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PDOBBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="BPIDateOfBirth" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" NeedsValidation="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litBirthStateOrCountryBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.BirthStateOrCountryBPI" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIBirthStateOrCountry"
                                     CssClass="text" runat="server" MaxLength="30" ToolTip="Birth State or Country"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvBirthStateOrCountryBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPIBirthStateOrCountry" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revBirthStateOrCountryBPI" runat="server" ValidationGroup="reggroupf3"
                                                        ControlToValidate="BPIBirthStateOrCountry" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Commas, Dashes, Periods or Hash are allowed."
                                                        ValidationExpression="^([a-zA-Z0-9 \#\,\-\.]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSSNBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SSNBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPISSN"
                                     onkeyup="FormatSSN(this);" CssClass="fright tbox text" runat="server" MaxLength="11"
                                     ToolTip="Social Security Number"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSSNBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPISSN" Display="dynamic" SetFocusOnError="true"
                                                    ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSSNBPI" runat="server" ValidationGroup="reggroupf3"
                                                        ControlToValidate="BPISSN" Display="dynamic" SetFocusOnError="true"
                                                        ErrorMessage="Invalid Social Security Number" ValidationExpression="^([0-9]{3}[-][0-9]{2}[-][0-9]{4}|xxx-xx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOfHispanicBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OfHispanicBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="BPIOfHispanic" runat="server" CssClass="rbl-form radio" ToolTip="Of Hispanic origin or descent?" onclick="CheckOfHispanicBPI()">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvOfHispanicBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPIOfHispanic" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" id="IfOfHispanictrBPI">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litIfOfHispanicBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.IfOfHispanicBPI" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIIfOfHispanic" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc."></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revIfOfHispanicBPI" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPIIfOfHispanic"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litRaceBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.RaceBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="BPIRace" runat="server" CssClass="rbl-form" DataTextField="Race" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="BPIRace_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_Race.GetRace" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="revRaceBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPIRace" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOccupationBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OccupationBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIOccupation" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Occupation (Within Last Year)"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvOccupationBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPIOccupation" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revOccupationBPI" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPIOccupation"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litTypeOfBusinessBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.TypeOfBusinessBPI" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPITypeOfBusiness" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Type of Business or Industry"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revTypeOfBusinessBPI" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPITypeOfBusiness"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litWorkPhoneBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.WorkPhoneBPI" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIWorkPhone"
                                     onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                                     ToolTip="Work Phone"></asp:TextBox><div class="clear">
                    </div>
                        <asp:RegularExpressionValidator ID="revWorkPhoneBPI" runat="server" ValidationGroup="reggroupf3"
                                                        ControlToValidate="BPIWorkPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Work Phone"
                                                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litHighestGradeBPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HighestGradeBPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="BPIHighestGrade" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="Highest Grade of School Completed"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvHighestGradeBPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="BPIHighestGrade" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revHighestGradeBPI" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="BPIHighestGrade"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
        </div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="liParentalIdentification" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ParentalIdentification" DisplayColon="false" /></h3>
            <hr />
        </div>
        <div class="formSectn">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litOfHispanicPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.OfHispanicPI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="PICEOfHispanic" runat="server" CssClass="rbl-form radio" ToolTip="Of Hispanic origin or descent?" onclick="CheckOfHispanicPI()">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvOfHispanicPI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="PICEOfHispanic" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row" id="IfOfHispanictrPI">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litIfOfHispanicPI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.IfOfHispanicPI" DisplayColon="false" />:</div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PICEIfOfHispanic" runat="server" CssClass="text" MaxLength="30"
                                     ToolTip="If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc."></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revIfOfHispanicPI" ValidationExpression="^([a-zA-Z0-9 \,\#\.\-]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="PICEIfOfHispanic"
                                                        ValidationGroup="reggroupf3" ErrorMessage="Only Letters, Numbers, Dashes, Periods, commas or Hashes are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litRacePI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.RacePI" DisplayColon="false" />:<font color="red">*</font></div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="PICERace" runat="server" CssClass="rbl-form" DataTextField="Race" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="PICERace_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_PR_Race.GetRace" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="revRacePI" runat="server" ValidationGroup="reggroupf3"
                                                    ControlToValidate="PICERace" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
        </div>
        <div class="formSectn">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12"></div>
                    <div class="col-md-6 col-sm-6 col-xs-12 box-button" align="right">
                        <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="FormButton" ValidationGroup="reggroupf3" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="FormButton" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="FormButton" /></div>
                </div>
        </div>
    </div>
</asp:Panel>

<script type="text/javascript">
    HideOfHispanic();
    CheckOfHispanicBPI();
    CheckOfHispanicPI();
    function CheckOfHispanicBPI() {
        var RB1 = document.getElementById("<%=BPIOfHispanic.ClientID%>");
        var radio = RB1.getElementsByTagName("input");
        if (radio[0].checked) {
            document.getElementById("IfOfHispanictrBPI").style.display = 'table-row';
        }
        else if (radio[1].checked) {
            document.getElementById("IfOfHispanictrBPI").style.display = 'none';
            document.getElementById("<%=BPIIfOfHispanic.ClientID%>").value = '';
        }
        else
            document.getElementById("<%=BPIIfOfHispanic.ClientID%>").value = '';
    }
    function CheckOfHispanicPI() {
        var RB1 = document.getElementById("<%=PICEOfHispanic.ClientID%>");
        var radio = RB1.getElementsByTagName("input");
        if (radio[0].checked) {
            document.getElementById("IfOfHispanictrPI").style.display = 'table-row';
        }
        else if (radio[1].checked) {
            document.getElementById("IfOfHispanictrPI").style.display = 'none';
            document.getElementById("<%=PICEIfOfHispanic.ClientID%>").value = '';
        }
        else
            document.getElementById("<%=PICEIfOfHispanic.ClientID%>").value = '';
    }
    function HideOfHispanic() {
        document.getElementById("IfOfHispanictrBPI").style.display = 'none';
        document.getElementById("IfOfHispanictrPI").style.display = 'none';
    }
</script>
