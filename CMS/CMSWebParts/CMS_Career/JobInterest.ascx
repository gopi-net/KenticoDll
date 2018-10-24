<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobInterest.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobInterest" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDateTimeControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlJobInterest" runat="server" DefaultButton="Submit">

        <div class="clearfix">
            <h1>
                <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right">
                <i>
                    <cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page2" runat="server" /></i>
            </div>
        </div>
        <div class="personalInfo jobInterest">
            <h3>
                <cms:LocalizedLiteral ID="litJobInterest" ResourceString="Emerge.CR.JobInterest" runat="server" />:</h3>
            <hr>
            <div>
                <tbody>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litJobInterestAvailabilityDate" ResourceString="Emerge.CR.JobInterestAvailabilityDate" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="AvailabilityDate" runat="server" DisplayNow="false" IsRequired="true" EditTime="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litJobInterestEmploymentType" ResourceString="Emerge.CR.JobInterestEmploymentType" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="EmploymentType" DataTextField="EmploymentType" DataValueField="ItemId" />
                        <cms:CMSQueryDataSource ID="EmploymentType_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_JobEmploymentType.GetEmploymentType" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litWillingToWorkOnWeekends" ResourceString="Emerge.CR.JobInterestWillingToWorkOnWeekends" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="WillingToWorkOnWeekends" RepeatDirection="Horizontal" CssClass="rbl" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" Selected="True" />
                            <asp:ListItem Text="No" Value="No" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litShiftPreference" ResourceString="Emerge.CR.JobInterestShiftPreference" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="ShiftPreference" DataTextField="Shift" DataValueField="ItemId" />
                        <cms:CMSQueryDataSource ID="ShiftPreference_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_Shift.GetShifts" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litUnitPreference1" ResourceString="Emerge.CR.JobInterestUnitPreference1" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="UnitPreference1" MaxLength="100" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litUnitPreference2" ResourceString="Emerge.CR.JobInterestUnitPreference2" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="UnitPreference2" MaxLength="100" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litUnitPreference3" ResourceString="Emerge.CR.JobInterestUnitPreference3" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="UnitPreference3" MaxLength="100" runat="server" />
                    </div>
                </div>
                </tbody>
            </div>
            <div class="halfContent">
                <tbody>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litWasHiredEarlier" ResourceString="Emerge.CR.WasHiredEarlier" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="WasHiredEarlier" CssClass="WasHiredEarlier rbl" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" Selected="True" />
                        </asp:RadioButtonList></div>
                </div>
                </tbody>
            </div>
            <div id="secWasHiredEarlier">
                <tbody>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litPrevEmploymentPosition" ResourceString="Emerge.CR.PrevEmploymentPosition" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PrevEmploymentPosition" MaxLength="100" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvPrevEmploymentPosition" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="PrevEmploymentPosition" ErrorMessage="Required" CssClass="WasHiredEarlierValidator ErrorMessage" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litDepartment" ResourceString="Emerge.CR.DepartmentLabel" runat="server" /><span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PrevEmploymentDepartment" MaxLength="100" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvPrevEmploymentDepartment" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="PrevEmploymentDepartment" ErrorMessage="Required" CssClass="WasHiredEarlierValidator ErrorMessage" /></div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litFrom" ResourceString="Emerge.CR.From" runat="server" />
                            <span style="color: red">*</span>:</label>

                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="PrevEmploymentFrom" DisplayNow="false" EditTime="false" IsRequired="true" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litTo" ResourceString="Emerge.CR.To" runat="server" /><span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="PrevEmploymentTo" DisplayNow="false" EditTime="false" IsRequired="true" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litPrevEmploymentReasonForLeaving" ResourceString="Emerge.CR.PrevEmploymentReasonForLeaving" runat="server" /><span style="color: red">*</span>:

                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="PrevEmploymentReasonForLeaving" MaxLength="200" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvPrevEmploymentReasonForLeaving" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="PrevEmploymentReasonForLeaving" ErrorMessage="Required" CssClass="WasHiredEarlierValidator ErrorMessage" />
                    </div>
                </div>
            </div>
            <div class="halfContent">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litIsRelativeEmployedInHospital" ResourceString="Emerge.CR.IsRelativeEmployedInHospital" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="IsRelativeEmployedInHospital" CssClass="IsRelativeEmployedInHospital rbl" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" Selected="True" />
                        </asp:RadioButtonList>
                </div>
                </div>
            </div>
            <div id="secIsRelativeEmployedInHospital">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litName" ResourceString="Emerge.CR.Name" runat="server" /><span style="color: red">*</span>:</label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12 name">
                        <asp:TextBox ID="RelativeFirstName" MaxLength="30" runat="server" placeholder="First Name"  />
                        <asp:TextBox ID="RelativeLastName" MaxLength="30" runat="server" placeholder="Last Name"  />
                        <asp:RequiredFieldValidator ID="rfvRelativeFirstName" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="RelativeFirstName" ErrorMessage="Required" CssClass="IsRelativeEmployedInHospitalValidator ErrorMessage"
                                                    />
                        <asp:RequiredFieldValidator ID="rfvRelativeLastName" Display="Dynamic" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="RelativeLastName" ErrorMessage="Required"  CssClass="IsRelativeEmployedInHospitalValidator ErrorMessage" SetFocusOnError="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litTitle" ResourceString="Emerge.CR.Title" runat="server" />
                            <span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="RelativeTitle" MaxLength="10" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvRelativeTitle" Display="Dynamic" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="RelativeTitle" ErrorMessage="Required" CssClass="IsRelativeEmployedInHospitalValidator ErrorMessage" SetFocusOnError="true" />
                </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litRelationship" ResourceString="Emerge.CR.Relationship" runat="server" /><span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="RelativeRelationship" MaxLength="30" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvRelativeRelationship" Display="Dynamic" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="RelativeRelationship" ErrorMessage="Required" CssClass="IsRelativeEmployedInHospitalValidator ErrorMessage" SetFocusOnError="true" />
                </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litRelativeDepartment" ResourceString="Emerge.CR.DepartmentLabel" runat="server" /><span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="RelativeDepartment" MaxLength="30" runat="server" />
                        <asp:RequiredFieldValidator ID="rfvRelativeDepartment" Display="Dynamic" ValidationGroup="CRJobInterest" runat="server" ControlToValidate="RelativeDepartment" ErrorMessage="Required" CssClass="IsRelativeEmployedInHospitalValidator ErrorMessage" SetFocusOnError="true" />
                    </div>
                </div>
            </div>
            <div class="halfContent">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litHasOffence" ResourceString="Emerge.CR.HasOffence" runat="server" />:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="HasOffense" RepeatDirection="Horizontal" CssClass="HasOffense rbl" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" Selected="True" />
                        </asp:RadioButtonList>
                </div>
                </div>
            </div>
            <div id="secHasOffense">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litDate" ResourceString="Emerge.CR.Date" runat="server" />
                            <span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="OffenseDate" runat="server" DisplayNow="false" IsRequired="true" EditTime="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litOffenceDetails" ResourceString="Emerge.CR.OffenceDetails" runat="server" />
                            <span style="color: red">*</span>:
                        </label>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="OffenseDetails" TextMode="MultiLine" runat="server" />
                    </div>
                </div>

            </div>
            <div class="clearfix">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litDriversLicenseNumber" ResourceString="Emerge.CR.DriversLicenseNumber" runat="server" />
                            :</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="DriversLicenseNumber" MaxLength="100" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litState" ResourceString="Emerge.CR.State" runat="server" />
                            :</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="State" DataTextField="State" DataValueField="ItemId" />
                        <cms:CMSQueryDataSource ID="State_DataSource" runat="server"
                                                QueryName="customtable.Emerge_{0}_CR_States.GetStates" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litExpirationDate" ResourceString="Emerge.CR.ExpirationDate" runat="server" />
                            :</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="DriversLicenseValidity" runat="server" DisplayNow="false" EditTime="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litHasAnyTrafficViolations" ResourceString="Emerge.CR.HasAnyTrafficViolations" runat="server" />
                            :</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="HasAnyTrafficViolations" CssClass="HasAnyTrafficViolations rbl" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" />
                            <asp:ListItem Text="No" Value="No" Selected="True" />
                        </asp:RadioButtonList>

                    </div>
                </div>
            </div>
            <div id="secHasAnyTrafficViolations">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litTrafficViolationDate" ResourceString="Emerge.CR.TrafficViolationDate" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <em:EmergeDateTimeControl ID="TrafficViolationDate" runat="server" IsRequired="true" DisplayNow="false" EditTime="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <label>
                            <cms:LocalizedLiteral ID="litTrafficViolationExplanation" ResourceString="Emerge.CR.TrafficViolationExplanation" runat="server" /><span style="color: red">*</span>:</label></div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:TextBox ID="TrafficViolationExplanation" MaxLength="100" runat="server" />
                    </div>
                </div>
            </div>
            <div class="halfContent">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLiteral ID="litIsImmediateAccomodationRequired" ResourceString="Emerge.CR.IsImmediateAccomodationRequired" runat="server" />
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="IsImmediateAccomodationRequired" CssClass="rbl" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes" Selected="True" />
                            <asp:ListItem Text="No" Value="No" />
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>

            <div class="clearfix">
            <div class="btnWrapper clearfix pull-left">
                <asp:LinkButton ID="Submit" Text="Save" ValidationGroup="CRJobInterest" runat="server" />
                <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
                </div>
            <div class="btnWrapper clearfix pull-right btn-prev">
                <asp:LinkButton ID="Next" Text="Save & Next<span class='icon-rightArrowGrey'></span>" CssClass="pull-right" ValidationGroup="CRJobInterest" runat="server" />
                <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" CssClass="pull-right" runat="server" />
            </div>

            </div>
        </div>


        <script type="text/javascript">
            jQuery(document).ready(function () {
                InitializeCareer();
                jQuery('input[type=text], textarea').placeholder();
                setState('WasHiredEarlier');
                setState('IsRelativeEmployedInHospital');
                setState('HasOffense');
                setState('HasAnyTrafficViolations');
                addRadioButtonHandlers('WasHiredEarlier');
                addRadioButtonHandlers('IsRelativeEmployedInHospital');
                addRadioButtonHandlers('HasOffense');
                addRadioButtonHandlers('HasAnyTrafficViolations');
            });
        </script>
    </asp:Panel>

    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>
