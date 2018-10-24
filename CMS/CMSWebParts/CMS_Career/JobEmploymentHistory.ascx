<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobEmploymentHistory.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobEmploymentHistory" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDateTimeControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>
<section class="contentInner careerWrap">
    <asp:Panel ID="pnlMain" runat="server">

        <div class="clearfix">
            <h1>
                <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right">
                <i>
                    <cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page4" runat="server" /></i>
            </div>
        </div>
        <asp:Panel ID="pnlEmploymentHistory" runat="server" DefaultButton="Add">
            <div class="personalInfo employmentHistory">

                <h3>
                    <cms:LocalizedLiteral ID="litEmploymentHistory" ResourceString="Emerge.CR.EmploymentHistory" runat="server" />:</h3>
                <hr>
                <cms:CMSRepeater ID="repEmplymentHistory" runat="server">
                </cms:CMSRepeater>
                <hr>
                <div class="employmentHistoryFields">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryEmploymentFrom" ResourceString="Emerge.CR.EmploymentHistoryEmploymentFrom" runat="server" /><span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl runat="server" ID="EmploymentFrom" EditTime="false" DisplayNow="false" IsRequired="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryEmploymentTo" ResourceString="Emerge.CR.EmploymentHistoryEmploymentTo" runat="server" /><span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="EmploymentTo" runat="server" EditTime="false" DisplayNow="false" IsRequired="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryCompanyName" ResourceString="Emerge.CR.EmploymentHistoryCompanyName" runat="server" /><span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="CompanyName" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvCompanyName" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="CompanyName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryComapnyAddress" ResourceString="Emerge.CR.EmploymentHistoryComapnyAddress" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ComapnyAddress" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvComapnyAddress" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="ComapnyAddress" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryPositionHeld" ResourceString="Emerge.CR.EmploymentHistoryPositionHeld" runat="server" /><span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="PositionHeld" MaxLength="50" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvPositionHeld" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="PositionHeld" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistorySupervisorName" ResourceString="Emerge.CR.EmploymentHistorySupervisorName" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12 name">
                            <asp:TextBox ID="SupervisorFirstName" MaxLength="50" runat="server" placeholder="First Name"  />
                            <asp:TextBox ID="SupervisorLastName" MaxLength="50" runat="server" placeholder="Last Name"  />
                            <asp:RequiredFieldValidator ID="rfvSupervisorFirstName" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" CssClass="ErrorMessage" runat="server" ControlToValidate="SupervisorFirstName" ErrorMessage="Required"/>
                            <asp:RequiredFieldValidator ID="rfvSupervisorLastName" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" CssClass="ErrorMessage" runat="server" ControlToValidate="SupervisorLastName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryDepartmentName" ResourceString="Emerge.CR.EmploymentHistoryDepartmentName" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="DepartmentName" MaxLength="50" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvDepartmentName" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="DepartmentName" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistorySupervisorTitle" ResourceString="Emerge.CR.EmploymentHistorySupervisorTitle" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="SupervisorTitle" MaxLength="80" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvSupervisorTitle" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="SupervisorTitle" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryPhoneNumber" ResourceString="Emerge.CR.EmploymentHistoryPhoneNumber" runat="server" /><span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12 name">
                            <asp:TextBox ID="PhoneNumberNum" MaxLength="14" CssClass="phoneMask" runat="server" />
                            <label class="extension">
                                <cms:LocalizedLiteral ID="litEmploymentHistoryPhoneNumberExt" ResourceString="Emerge.CR.Ext" runat="server" />:</label>
                            <asp:TextBox ID="PhoneNumberExt" runat="server" CssClass="extn" MaxLength="4" />
                            <asp:TextBox ID="PhoneNumber" runat="server" Visible="false" />
                            <asp:RequiredFieldValidator ID="rfvPhoneNumberNum" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="PhoneNumberNum" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryMayContactForReference" ResourceString="Emerge.CR.EmploymentHistoryMayContactForReference" runat="server" />:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList ID="MayContactForReference" runat="server" CssClass="rbl" RepeatDirection="Horizontal">
                                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryJobResponsibilities" ResourceString="Emerge.CR.EmploymentHistoryJobResponsibilities" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="JobResponsibilities" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvJobResponsibilities" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="JobResponsibilities" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryLastSalary" ResourceString="Emerge.CR.EmploymentHistoryLastSalary" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="LastSalary" MaxLength="20" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvLastSalary" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="LastSalary" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryLastSalaryType" ResourceString="Emerge.CR.EmploymentHistoryLastSalaryType" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList ID="LastSalaryType" runat="server" CssClass="rbl" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Annual" Value="Annual" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Per Hour" Value="PerHour"></asp:ListItem>
                                <asp:ListItem Text="Weekly" Value="Weekly"></asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvLastSalaryType" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="LastSalaryType" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryOtherCompensation" ResourceString="Emerge.CR.EmploymentHistoryOtherCompensation" runat="server" />:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OtherCompensation" MaxLength="80" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryOtherNameEmployedUnder" ResourceString="Emerge.CR.EmploymentHistoryOtherNameEmployedUnder" runat="server" />
                                :</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OtherNameEmployedUnder" MaxLength="80" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litEmploymentHistoryReasonForLeaving" ResourceString="Emerge.CR.EmploymentHistoryReasonForLeaving" runat="server" /><span style="color: red">*</span>:</label>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ReasonForLeaving" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvReasonForLeaving" ValidationGroup="CREmploymentHistory" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="ReasonForLeaving" ErrorMessage="Required" />
                        </div>
                    </div>
                    
                </div>
            </div>
            <div class="btnWrapper">
                <asp:LinkButton ID="Add" Text="Add" CssClass="employmentHistoryFields" ValidationGroup="CREmploymentHistory" runat="server" />
            </div>
        </asp:Panel>
        <div class="message_box">
            <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcHistory" BasicStyles="true" runat="server" />
        </div>
        <asp:Panel ID="pnlMilitaryRecord" runat="server" DefaultButton="Save">
            <div class="personalInfo employmentHistory">
                <h3>
                    <cms:LocalizedLiteral ID="litMilitaryRecord" ResourceString="Emerge.CR.MilitaryRecord" runat="server" />:</h3>
                <hr>
                <div class="halfContent">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordIsWarVateran" ResourceString="Emerge.CR.MilitaryRecordIsWarVateran" runat="server" />:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList ID="IsWarVateran" CssClass="IsWarVateran rbl" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Yes" Value="Yes" />
                                <asp:ListItem Text="No" Value="No" Selected="True" />
                            </asp:RadioButtonList></div>
                    </div>
                    
                </div>
                <div id="secIsWarVateran">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordListConflict" ResourceString="Emerge.CR.MilitaryRecordListConflict" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ListConflict" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvListConflict" ValidationGroup="CRMilitaryRecord" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="ListConflict" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordServiceFrom" ResourceString="Emerge.CR.MilitaryRecordServiceFrom" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ServiceFrom" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordServiceTo" ResourceString="Emerge.CR.MilitaryRecordServiceTo" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ServiceTo" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordServiceBranch" ResourceString="Emerge.CR.MilitaryRecordServiceBranch" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ServiceBranch" MaxLength="100" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvServiceBranch" ValidationGroup="CRMilitaryRecord" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="ServiceBranch" ErrorMessage="Required" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <label>
                                <cms:LocalizedLiteral ID="litMilitaryRecordMilitaryAssignment" ResourceString="Emerge.CR.MilitaryRecordMilitaryAssignment" runat="server" />
                                <span style="color: red">*</span>:</label></div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="MilitaryAssignment" TextMode="MultiLine" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvMilitaryAssignment" ValidationGroup="CRMilitaryRecord" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" runat="server" ControlToValidate="MilitaryAssignment" ErrorMessage="Required" />
                        </div>
                    </div>
                    
                </div>
            </div>
        </asp:Panel>
        <div class="clearfix">
        <div class="btnWrapper clearfix pull-left">
            <asp:LinkButton ID="Save" Text="Save" ValidationGroup="CRMilitaryRecord" runat="server" />
            <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
            </div>
            <div class="btnWrapper clearfix btn-prev pull-right">
            <asp:LinkButton ID="Next" Text="Next<span class='icon-rightArrowGrey'></span>" runat="server" CssClass="pull-right" />
            <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" runat="server" CssClass="pull-right" />
        </div>

        </div>



        <script type="text/javascript">
            jQuery(document).ready(function () {
                InitializeCareer();
                jQuery('input[type=text], textarea').placeholder();
                setState('IsWarVateran');
                addRadioButtonHandlers('IsWarVateran');
            });
            function showHideHistoryDetail(detailId) {
                jQuery('#detail' + detailId).slideToggle("slow", function () {
                    jQuery('#showDetailButton' + detailId).toggleClass('openWrap');
                });
            }

        </script>
    </asp:Panel>
    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>
