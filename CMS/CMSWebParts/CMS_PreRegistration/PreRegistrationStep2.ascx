<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreRegistrationStep2.ascx.cs" Inherits="CMSWebParts_CMS_PreRegistration_PreRegistrationStep2" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDatePickerControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="pnlStep2" runat="server" DefaultButton="btnNext">
    <div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHNextOfKin" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HNextOfKin" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litNearestRelativeNameCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.NearestRelativeNameCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRName" runat="server" CssClass="text" MaxLength="60"
                        ToolTip="Name of Nearest Relative"></asp:TextBox>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvtxtNearestRelativeNameCI" Display="dynamic" ControlToValidate="NRName"
                        SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf2" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revtxtNearestRelativeNameCI" ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$"
                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="NRName"
                        ValidationGroup="reggroupf2" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litRelationToPatientCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.RelationToPatientCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedRadioButtonList ID="NRRelationToPatient" runat="server" CssClass="rbl-form" DataTextField="RelationToPatientNR" DataValueField="ItemID" RepeatDirection="Vertical">
                    </cms:LocalizedRadioButtonList>
                    <cms:CMSQueryDataSource ID="NRRelationToPatient_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_RelationToPatientOfNR.GetRelationToPatientOfNR" />
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="revRelationToPatientCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRRelationToPatient" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litMailingAddressCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.MailingAddressCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revMailingAddressCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litCityCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.CityCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRCity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revCityCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRCity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litStateCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.StateCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedDropDownList runat="server" ID="NRState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByNRState();" />
                    <cms:CMSQueryDataSource ID="NRState_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litZipCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ZipCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:CustomValidator ID="cvNRZip" runat="server"
                        ControlToValidate="NRZip" Display="dynamic" SetFocusOnError="true"
                        ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByNRState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPhoneCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PhoneCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                        ToolTip="Phone"></asp:TextBox><div class="clear">
                        </div>
                    <asp:RequiredFieldValidator ID="rfvPhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Phone"
                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litAlternatePhoneCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.AlternatePhoneCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NRAlternatePhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                        ToolTip="Phone"></asp:TextBox><div class="clear">
                        </div>
                    <asp:RegularExpressionValidator ID="revAlternatePhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="NRAlternatePhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Alternate Phone"
                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litEmergencyContactNameCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.EmergencyContactNameCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECName" runat="server" CssClass="text" MaxLength="60"
                        ToolTip="Name of Emergency Contact"></asp:TextBox>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvEmergencyContactNameCI" Display="dynamic" ControlToValidate="ECName"
                        SetFocusOnError="true" runat="server" ErrorMessage="Required" ValidationGroup="reggroupf2" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmergencyContactNameCI" ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$"
                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="ECName"
                        ValidationGroup="reggroupf2" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECRelationToPatientCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECRelationToPatientCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedRadioButtonList ID="ECRelationToPatient" runat="server" CssClass="rbl-form" DataTextField="RelationToPatientEC" DataValueField="ItemID" RepeatDirection="Vertical">
                    </cms:LocalizedRadioButtonList>
                    <cms:CMSQueryDataSource ID="ECRelationToPatient_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_RelationToPatientOfEC.GetRelationToPatientEC" />
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvECRelationToPatientCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECRelationToPatient" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECMailingAddressCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECMailingAddressCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revECMailingAddressCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECCityCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECCityCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECCity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revECCityCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECCity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECStateCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECStateCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedDropDownList runat="server" ID="ECState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByECState();" />
                    <cms:CMSQueryDataSource ID="ECState_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECZipCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECZipCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:CustomValidator ID="cvECZip" runat="server"
                        ControlToValidate="ECZip" Display="dynamic" SetFocusOnError="true"
                        ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByECState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECPhoneCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECPhoneCI" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                        ToolTip="Phone"></asp:TextBox><div class="clear">
                        </div>
                    <asp:RequiredFieldValidator ID="rfvECPhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revECPhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Phone"
                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litECAlternatePhoneCI" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ECAlternatePhoneCI" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="ECAlternatePhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                        ToolTip="Phone"></asp:TextBox><div class="clear">
                        </div>
                    <asp:RegularExpressionValidator ID="revECAlternatePhoneCI" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="ECAlternatePhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Alternate Phone"
                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHPrimaryInsurance" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HPrimaryInsurance" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPICompanyName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PICompanyName" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PICompanyName" CssClass="text" runat="server" MaxLength="60" ToolTip="Primary Insurance Company Name"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPICompanyName" runat="server" ValidationGroup="reggroupf2"
                        Display="Dynamic" ControlToValidate="PICompanyName" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPICompanyName" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PICompanyName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIPolicyNumber" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIPolicyNumber" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PIPolicyNumber" CssClass="text" runat="server" MaxLength="30" ToolTip="Policy Number"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPIPolicyNumber" runat="server" ValidationGroup="reggroupf2"
                        Display="Dynamic" ControlToValidate="PIPolicyNumber" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPIPolicyNumber" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PIPolicyNumber" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIGroupNumber" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIGroupNumber" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PIGroupNumber" CssClass="text" runat="server" MaxLength="30" ToolTip="Group Number"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPIGroupNumber" runat="server" ValidationGroup="reggroupf2"
                        Display="Dynamic" ControlToValidate="PIGroupNumber" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPIGroupNumber" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PIGroupNumber" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPISubscriberName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PISubscriberName" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PISubscriberName" CssClass="text" runat="server" MaxLength="60" ToolTip="Subscriber Name"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPISubscriberName" runat="server" ValidationGroup="reggroupf2"
                        Display="Dynamic" ControlToValidate="PISubscriberName" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPISubscriberName" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PISubscriberName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPISSN" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PISSN" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PISocialSecurityNumber"
                        onkeyup="FormatSSN(this);" CssClass="fright tbox text" runat="server" MaxLength="11"
                        ToolTip="Social Security Number"></asp:TextBox>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPISSN" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PISocialSecurityNumber" Display="dynamic" SetFocusOnError="true"
                        ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revPISSN" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PISocialSecurityNumber" Display="dynamic" SetFocusOnError="true"
                        ErrorMessage="Invalid Social Security Number" ValidationExpression="^([0-9]{3}[-][0-9]{2}[-][0-9]{4}|xxx-xx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIMailingAddress" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIMailingAddress" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PIAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revPIMailingAddress" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PIAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPICity" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PICity" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PICity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revPICity" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PICity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIState" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIState" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedDropDownList runat="server" ID="PIState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationByPIState();" />
                    <cms:CMSQueryDataSource ID="PIState_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIZip" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIZip" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PIZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:CustomValidator ID="cvPIZip" runat="server"
                        ControlToValidate="PIZip" Display="dynamic" SetFocusOnError="true"
                        ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipByPIState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                    </asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIDOB" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIDOB" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <em:EmergeDateTimeControl ID="PIDateOfBirth" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" NeedsValidation="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIRelationToPolicyHolder" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIRelationToPolicyHolder" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedRadioButtonList ID="PIRelationToPolicyHolder" runat="server" CssClass="rbl-form" DataTextField="RelationToPolicyHolder" DataValueField="ItemID" RepeatDirection="Vertical">
                    </cms:LocalizedRadioButtonList>
                    <cms:CMSQueryDataSource ID="PIRelationToPolicyHolder_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolder" />
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPIRelationToPolicyHolder" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PIRelationToPolicyHolder" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIPhone" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIPhone" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PISubscriberPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                        ToolTip="Subscriber Phone"></asp:TextBox><div class="clear">
                        </div>
                    <asp:RegularExpressionValidator ID="revPIPhone" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PISubscriberPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Subscriber Phone"
                        ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litPIEmployer" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.PIEmployer" DisplayColon="false" />:
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PIEmployer" CssClass="text" runat="server" MaxLength="30" ToolTip="Employer"></asp:TextBox><div
                        class="clear">
                    </div>
                    <asp:RegularExpressionValidator ID="revPIEmployer" runat="server" ValidationGroup="reggroupf2"
                        ControlToValidate="PIEmployer" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                        ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                </div>
            </div>
        </div>
        <div class="mainTitle">
            <h3>
                <cms:LocalizedLabel ID="litHSecondaryInsurance" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HSecondaryInsurance" DisplayColon="false" />:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="litDoYouHaveSecInsPolicy" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.DoYouHaveSecInsPolicy" DisplayColon="false" />:<font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButtonList ID="DoYouHaveSecInsPolicy" runat="server" CssClass="rbl-form DoYouHaveSecInsPolicy rbl radio" ToolTip="Do you have a secondary insurance policy?" onchange="CheckDoYouHaveSecInsPolicy();">
                        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div class="clear">
                    </div>
                    <asp:RequiredFieldValidator ID="rfvDoYouHaveSecInsPolicy" runat="server" ValidationGroup="reggroupf1"
                        ControlToValidate="DoYouHaveSecInsPolicy" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="formSectn">
            <div id="secDoYouHaveSecInsPolicy">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSICompanyName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SICompanyName" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SICompanyName" CssClass="text" runat="server" MaxLength="60" ToolTip="Primary Insurance Company Name"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSICompanyName" runat="server" ValidationGroup="reggroupf2"
                            Display="Dynamic" ControlToValidate="SICompanyName" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSICompanyName" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SICompanyName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIPolicyNumber" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIPolicyNumber" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SIPolicyNumber" CssClass="text" runat="server" MaxLength="30" ToolTip="Policy Number"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSIPolicyNumber" runat="server" ValidationGroup="reggroupf2"
                            Display="Dynamic" ControlToValidate="SIPolicyNumber" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSIPolicyNumber" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SIPolicyNumber" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIGroupNumber" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIGroupNumber" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SIGroupNumber" CssClass="text" runat="server" MaxLength="30" ToolTip="Group Number"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSIGroupNumber" runat="server" ValidationGroup="reggroupf2"
                            Display="Dynamic" ControlToValidate="SIGroupNumber" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSIGroupNumber" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SIGroupNumber" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSISubscriberName" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SISubscriberName" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SISubscriberName" CssClass="text" runat="server" MaxLength="60" ToolTip="Subscriber Name"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSISubscriberName" runat="server" ValidationGroup="reggroupf2"
                            Display="Dynamic" ControlToValidate="SISubscriberName" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSISubscriberName" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SISubscriberName" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSISSN" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SISSN" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SISocialSecurityNumber"
                            onkeyup="FormatSSN(this);" CssClass="fright tbox text" runat="server" MaxLength="11"
                            ToolTip="Social Security Number"></asp:TextBox>
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSISSN" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SISocialSecurityNumber" Display="dynamic" SetFocusOnError="true"
                            ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revSISSN" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SISocialSecurityNumber" Display="dynamic" SetFocusOnError="true"
                            ErrorMessage="Invalid Social Security Number" ValidationExpression="^([0-9]{3}[-][0-9]{2}[-][0-9]{4}|xxx-xx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIMailingAddress" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIMailingAddress" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SIAddress" CssClass="text" runat="server" MaxLength="50" ToolTip="Address"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revSIMailingAddress" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SIAddress" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSICity" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SICity" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SICity" CssClass="text" runat="server" MaxLength="30" ToolTip="City"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revSICity" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SICity" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIState" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIState" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="SIState" DataTextField="StateName" DataValueField="ItemId" onchange="UpdateZipValidationBySIState();" />
                        <cms:CMSQueryDataSource ID="SIState_DataSource" runat="server"
                            QueryName="customtable.Emerge_{0}_PR_States.GetStates" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIZip" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIZip" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SIZip" CssClass="text" runat="server" MaxLength="7" ToolTip="Zip"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:CustomValidator ID="cvSIZip" runat="server"
                            ControlToValidate="SIZip" Display="dynamic" SetFocusOnError="true"
                            ErrorMessage="Invalid Zip" ClientValidationFunction="ValidateZipBySIState" ValidationGroup="reggroupf1" CssClass="ErrorMessage">
                        </asp:CustomValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIDOB" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIDOB" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="SIDateOfBirth" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" NeedsValidation="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIRelationToPolicyHolder" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIRelationToPolicyHolder" DisplayColon="false" />:<font color="red">*</font>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedRadioButtonList ID="SIRelationToPolicyHolder" runat="server" CssClass="rbl-form" DataTextField="RelationToPolicyHolder" DataValueField="ItemID" RepeatDirection="Vertical">
                        </cms:LocalizedRadioButtonList>
                        <cms:CMSQueryDataSource ID="SIRelationToPolicyHolder_DataSource" runat="server"
                            QueryName="customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolder" />
                        <div class="clear">
                        </div>
                        <asp:RequiredFieldValidator ID="rfvSIRelationToPolicyHolder" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SIRelationToPolicyHolder" Display="dynamic" SetFocusOnError="true" ErrorMessage="Required" CssClass="ErrorMessage"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIPhone" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIPhone" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SISubscriberPhone" onkeyup="FormatPhone(this);" CssClass="text" runat="server" MaxLength="12"
                            ToolTip="Subscriber Phone"></asp:TextBox><div class="clear">
                            </div>
                        <asp:RegularExpressionValidator ID="revSIPhone" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SISubscriberPhone" Display="dynamic" SetFocusOnError="true" ErrorMessage="Invalid Subscriber Phone"
                            ValidationExpression="^([0-9]{3}[-][0-9]{3}[-][0-9]{4}|xxx-xxx-xxxx)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="litSIEmployer" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.SIEmployer" DisplayColon="false" />:
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <asp:TextBox ID="SIEmployer" CssClass="text" runat="server" MaxLength="30" ToolTip="Employer"></asp:TextBox><div
                            class="clear">
                        </div>
                        <asp:RegularExpressionValidator ID="revSIEmployer" runat="server" ValidationGroup="reggroupf2"
                            ControlToValidate="SIEmployer" Display="dynamic" SetFocusOnError="true" ErrorMessage="Only Letters, Numbers, Dashes, Periods, Comma, or Hash are allowed."
                            ValidationExpression="^([a-zA-Z0-9 \-\.\,\#]+)$" CssClass="ErrorMessage"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="formSectn">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-9 col-sm-6 col-xs-12 box-button" align="right">
                    <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="FormButton" ValidationGroup="reggroupf2" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="FormButton" />
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="FormButton" />
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<script type="text/javascript">
    CheckDoYouHaveSecInsPolicy();
    function CheckDoYouHaveSecInsPolicy() {
        var RB1 = document.getElementById("<%=DoYouHaveSecInsPolicy.ClientID%>");
        var radio = RB1.getElementsByTagName("input");
        if (radio[0].checked) {
            document.getElementById("secDoYouHaveSecInsPolicy").style.display = 'block';
            document.getElementById('<%= rfvSICompanyName.ClientID %>').enabled = true;
		    document.getElementById('<%= revSICompanyName.ClientID %>').enabled = true;
            document.getElementById('<%= rfvSIPolicyNumber.ClientID %>').enabled = true;
            document.getElementById('<%= revSIPolicyNumber.ClientID %>').enabled = true;
            document.getElementById('<%= rfvSIGroupNumber.ClientID %>').enabled = true;
            document.getElementById('<%= revSIGroupNumber.ClientID %>').enabled = true;
            document.getElementById('<%= rfvSISubscriberName.ClientID %>').enabled = true;
            document.getElementById('<%= revSISubscriberName.ClientID %>').enabled = true;
            document.getElementById('<%= rfvSISSN.ClientID %>').enabled = true;
            document.getElementById('<%= revSISSN.ClientID %>').enabled = true;
            document.getElementById('<%= rfvSIRelationToPolicyHolder.ClientID %>').enabled = true;
            document.getElementById('<%= SIDateOfBirth.ClientID %>').enabled = true;

        }
        else if (radio[1].checked) {
            document.getElementById("secDoYouHaveSecInsPolicy").style.display = 'none';
            document.getElementById('<%= rfvSICompanyName.ClientID %>').enabled = false;
            document.getElementById('<%= revSICompanyName.ClientID %>').enabled = false;
            document.getElementById('<%= rfvSIPolicyNumber.ClientID %>').enabled = false;
            document.getElementById('<%= revSIPolicyNumber.ClientID %>').enabled = false;
            document.getElementById('<%= rfvSIGroupNumber.ClientID %>').enabled = false;
            document.getElementById('<%= revSIGroupNumber.ClientID %>').enabled = false;
            document.getElementById('<%= rfvSISubscriberName.ClientID %>').enabled = false;
            document.getElementById('<%= revSISubscriberName.ClientID %>').enabled = false;
            document.getElementById('<%= rfvSISSN.ClientID %>').enabled = false;
            document.getElementById('<%= revSISSN.ClientID %>').enabled = false;
            document.getElementById('<%= rfvSIRelationToPolicyHolder.ClientID %>').enabled = false;
            document.getElementById('<%= SICompanyName.ClientID %>').value = '';
            document.getElementById('<%= SIPolicyNumber.ClientID %>').value = '';
            document.getElementById('<%= SIGroupNumber.ClientID %>').value = '';
            document.getElementById('<%= SISubscriberName.ClientID %>').value = '';
            document.getElementById('<%= SISocialSecurityNumber.ClientID %>').value = '';
            document.getElementById('<%= SIAddress.ClientID %>').value = '';
            document.getElementById('<%= SICity.ClientID %>').value = '';
            document.getElementById('<%= SIZip.ClientID %>').value = '';
            document.getElementById('<%= SISubscriberPhone.ClientID %>').value = '';
            document.getElementById('<%= SIEmployer.ClientID %>').value = '';
            document.getElementById('<%= SIState.ClientID %>').selectedIndex = 0;
            document.getElementById('<%= SIRelationToPolicyHolder.ClientID %>').selectedIndex = -1;
            var elementRef = document.getElementById('<%= SIRelationToPolicyHolder.ClientID %>');
            clearRadioButtonList(elementRef);

            $("input[id^='<%= SIDateOfBirth.ClientID %>']").val('');
            document.getElementById('<%= SIDateOfBirth.ClientID %>').initialized = true;
            document.getElementById('<%= SIDateOfBirth.ClientID %>').disabled = true;

        }
}
function SetDoYouHaveSecInsPolicy() {
    var rblDoYouHaveSecInsPolicy = document.getElementById("<%=DoYouHaveSecInsPolicy.ClientID%>");
     var radio = rblDoYouHaveSecInsPolicy.getElementsByTagName("input");
     radio[0].checked = true;
 }


 window.onload = SetDoYouHaveSecInsPolicy;
</script>
<script type="text/javascript">
    var stateBC = "British Columbia";
    function ValidateZipByNRState(source, arguments) {
        if (jQuery(document.getElementById('<%= NRState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipByECState(source, arguments) {
        if (jQuery(document.getElementById('<%= ECState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipByPIState(source, arguments) {
        if (jQuery(document.getElementById('<%= PIState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function ValidateZipBySIState(source, arguments) {
        if (jQuery(document.getElementById('<%= SIState.ClientID %>')).children(':selected').text() == stateBC) {
            getBritishColumbiaZip(arguments);
        }
        else {
            getOtherZip(arguments);
        }
    }
    function UpdateZipValidationByNRState() {
        ValidatorValidate(document.getElementById('<%= cvNRZip.ClientID %>'));
    }
    function UpdateZipValidationByECState() {
        ValidatorValidate(document.getElementById('<%= cvECZip.ClientID %>'));
    }
    function UpdateZipValidationByPIState() {
        ValidatorValidate(document.getElementById('<%= cvPIZip.ClientID %>'));
    }
    function UpdateZipValidationBySIState() {
        ValidatorValidate(document.getElementById('<%= cvSIZip.ClientID %>'));
    }
</script>
