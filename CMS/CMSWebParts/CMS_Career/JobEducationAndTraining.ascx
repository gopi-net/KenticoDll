<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobEducationAndTraining.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobEducationAndTraining" %>
<%@ Register Src="~/CMSFormControls/EmergeFormControls/EmergeDateTimeControl.ascx" TagPrefix="em" TagName="EmergeDateTimeControl" %>
<section class="contentInner careerWrap">
    <asp:Panel ID="pnlMain" runat="server" DefaultButton="Save">

        <div class="clearfix">
            <h1>
                <cms:LocalizedLiteral ID="litApplication" ResourceString="Emerge.CR.Application" runat="server" /></h1>
            <div class="pageNos pull-right">
                <i>
                    <cms:LocalizedLiteral ID="litPage" ResourceString="Emerge.CR.Page3" runat="server" /></i>
            </div>
        </div>

        <asp:Panel ID="pnlEducationTraining" runat="server">

            <div class="personalInfo eduTraining">
                <h3>
                    <cms:LocalizedLiteral ID="litEducationTraining" ResourceString="Emerge.CR.EducationTraining" runat="server" />:</h3>
                <hr>
                <div class="table-responsive">
                    <table  class="table">
                        <thead>
                        <tr>
                            <th></th>
                            <th>
                                <cms:LocalizedLiteral ID="litHighSchool" ResourceString="Emerge.CR.EducationHighSchool" runat="server" /></th>
                            <th>
                                <cms:LocalizedLiteral ID="litCollege" ResourceString="Emerge.CR.EducationCollege" runat="server" /></th>
                            <th>
                                <cms:LocalizedLiteral ID="litOther" ResourceString="Emerge.CR.EducationOther" runat="server" /></th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr>
                            <td>
                                <cms:LocalizedLiteral ID="litEducationTrainingName" ResourceString="Emerge.CR.EducationTrainingName" runat="server" /></td>
                            <td class="name">
                                <asp:TextBox ID="HighSchoolName" MaxLength="100" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvHighSchoolName" ValidationGroup="CREducationTraining" runat="server" SetFocusOnError="true" Display="Dynamic" CssClass="ErrorMessage" ControlToValidate="HighSchoolName" ErrorMessage="Required" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="CollegeName" MaxLength="100" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvCollegeName" ValidationGroup="CREducationTraining" Display="Dynamic" SetFocusOnError="true" CssClass="ErrorMessage" runat="server" ControlToValidate="CollegeName" ErrorMessage="Required" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="OtherName" MaxLength="100" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLiteral ID="litEducationTrainingCityState" ResourceString="Emerge.CR.EducationTrainingCityState" runat="server" /></td>
                            <td class="name">
                                <asp:TextBox ID="HighSchoolCity" MaxLength="30" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvHighSchoolCity" ValidationGroup="CREducationTraining" runat="server" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ControlToValidate="HighSchoolCity" ErrorMessage="Required" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="CollegeCity" MaxLength="30" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvCollegeCity" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="CollegeCity" ErrorMessage="Required" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="OtherCity" MaxLength="30" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLiteral ID="litEducationTrainingLastYearCompleted" ResourceString="Emerge.CR.EducationTrainingLastYearCompleted" runat="server" /></td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="HighSchoolLastYearCompleted" CssClass="rbl2" runat="server">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="GED" Value="GED"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="CollegeLastYearCompleted" CssClass="rbl2" runat="server">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="GED" Value="GED"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="OtherLastYearCompleted" CssClass="rbl2" runat="server">
                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="GED" Value="GED"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLiteral ID="litEducationTrainingDidYouGraduate" ResourceString="Emerge.CR.EducationTrainingDidYouGraduate" runat="server" /></td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="HighSchoolWasGraduated" CssClass="rbl" runat="server">
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvHighSchoolWasGraduated" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="HighSchoolWasGraduated" ErrorMessage="Required" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" />
                            </td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="CollegeWasGraduated" CssClass="rbl" runat="server">
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="rfvCollegeWasGraduated" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="CollegeWasGraduated" ErrorMessage="Required" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" />
                            </td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="OtherWasGraduated" CssClass="rbl" runat="server">
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cms:LocalizedLiteral ID="litEducationTrainingDegreeObtained" ResourceString="Emerge.CR.EducationTrainingDegreeObtained" runat="server" /></td>
                            <td class="name">
                                <asp:TextBox ID="HighSchoolDegreeObtained" MaxLength="30" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvHighSchoolDegreeObtained" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="HighSchoolDegreeObtained" ErrorMessage="Required" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="CollegeDegreeObtained" MaxLength="30" runat="server" />
                                <asp:RequiredFieldValidator ID="rfvCollegeDegreeObtained" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="CollegeDegreeObtained" ErrorMessage="Required" Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" />
                            </td>
                            <td class="name">
                                <asp:TextBox ID="OtherDegreeObtained" MaxLength="30" runat="server" /></td>
                        </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlProfessionalLicensure" runat="server">
            <div class="personalInfo eduTraining">
                <h3>
                    <cms:LocalizedLiteral ID="litProfessionalInfo" ResourceString="Emerge.CR.ProfessionalInfo" runat="server" />:</h3>
                <hr>
                <div class="halfContent">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoHaveLicenseInState" ResourceString="Emerge.CR.ProfessionalInfoHaveLicenseInState" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="HaveLicenseInState" runat="server" CssClass="HaveLicenseInState rbl">
                                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    
                </div>
                <div id="secHaveLicenseInState">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><b>(1)</b></div>
                        <div class="col-md-6 col-sm-6 col-xs-12"></div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoProfessionalLicense1" ResourceString="Emerge.CR.ProfessionalInfoProfessionalLicense" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ProfessionalLicense1" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProfessionalLicense1" CssClass="HaveLicenseInStateValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="ProfessionalLicense1" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoLicenseNumber1" ResourceString="Emerge.CR.ProfessionalInfoLicenseNumber" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="LicenseNumber1" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLicenseNumber1" CssClass="HaveLicenseInStateValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="LicenseNumber1" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="ProfessionalInfoStateOfIssuance1" ResourceString="Emerge.CR.ProfessionalInfoStateOfIssuance" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="StateOfIssuance1" DataTextField="State" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="StateOfIssuance1_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_States.GetStates" />
                            <asp:RequiredFieldValidator ID="rfvStateOfIssuance1" CssClass="HaveLicenseInStateValidator ErrorMessage" InitialValue="-1" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="StateOfIssuance1" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoDateIssued1" ResourceString="Emerge.CR.ProfessionalInfoDateIssued" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="DateIssued1" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="ProfessionalInfoExpirationDate1" ResourceString="Emerge.CR.ProfessionalInfoExpirationDate" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ExpirationDate1" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="ProfessionalInfoIssuedBy1" ResourceString="Emerge.CR.ProfessionalInfoIssuedBy" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="IssuedBy1" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIssuedBy1" CssClass="HaveLicenseInStateValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="IssuedBy1" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <hr />
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><b>(2)</b></div>
                        <div class="col-md-6 col-sm-6 col-xs-12"></div>
                        </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoProfessionalLicense2" ResourceString="Emerge.CR.ProfessionalInfoProfessionalLicense" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="ProfessionalLicense2" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoLicenseNumber2" ResourceString="Emerge.CR.ProfessionalInfoLicenseNumber" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="LicenseNumber2" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoStateOfIssuance2" ResourceString="Emerge.CR.ProfessionalInfoStateOfIssuance" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="StateOfIssuance2" DataTextField="State" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="StateOfIssuance2_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_States.GetStates" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoDateIssued2" ResourceString="Emerge.CR.ProfessionalInfoDateIssued" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="DateIssued2" runat="server" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoExpirationDate2" ResourceString="Emerge.CR.ProfessionalInfoExpirationDate" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ExpirationDate2" runat="server" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoIssuedBy2" ResourceString="Emerge.CR.ProfessionalInfoIssuedBy" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="IssuedBy2" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="halfContent">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoHasLicenseEverRevokedSuspendedTerminated" ResourceString="Emerge.CR.ProfessionalInfoHasLicenseEverRevokedSuspendedTerminated" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="HasLicenseEverRevokedSuspendedTerminated" runat="server" CssClass="HasLicenseEverRevokedSuspendedTerminated rbl">
                                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    
                </div>
                <div id="secHasLicenseEverRevokedSuspendedTerminated">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoExplaination" ResourceString="Emerge.CR.ProfessionalInfoExplaination" runat="server" />
                            <span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="Explaination" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExplaination" CssClass="HasLicenseEverRevokedSuspendedTerminatedValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="Explaination" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoDateOfAction" ResourceString="Emerge.CR.ProfessionalInfoDateOfAction" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="DateOfAction" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoReactivationDate" ResourceString="Emerge.CR.ProfessionalInfoReactivationDate" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ReactivationDate" runat="server" IsRequired="true" DisplayNow="false" EditTime="false" />
                        </div>
                    </div>
                    
                </div>
                <div class="halfContent">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoHaveContactedBoardForConversion" ResourceString="Emerge.CR.ProfessionalInfoHaveContactedBoardForConversion" runat="server" />
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="HaveContactedBoardForConversion" runat="server" CssClass="HaveContactedBoardForConversion rbl">
                                <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    
                </div>
                <div id="secHaveContactedBoardForConversion">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoExpectedConversionDate" ResourceString="Emerge.CR.ProfessionalInfoExpectedConversionDate" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="ExpectedConversionDate" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoOtherCountriesOrStateOfCertification" ResourceString="Emerge.CR.ProfessionalInfoOtherCountriesOrStateOfCertification" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OtherCountriesOrStateOfCertification" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOtherCountriesOrStateOfCertification" CssClass="HaveContactedBoardForConversionValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="OtherCountriesOrStateOfCertification" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoOutOfStateProfessionalLicenseNumber" ResourceString="Emerge.CR.ProfessionalInfoOutOfStateProfessionalLicenseNumber" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OutOfStateProfessionalLicenseNumber" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOutOfStateProfessionalLicenseNumber" CssClass="HaveContactedBoardForConversionValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="OutOfStateProfessionalLicenseNumber" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoOutOfStateEffectiveDate" ResourceString="Emerge.CR.ProfessionalInfoOutOfStateEffectiveDate" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="OutOfStateEffectiveDate" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoOutOfStateExpirationDate" ResourceString="Emerge.CR.ProfessionalInfoOutOfStateExpirationDate" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <em:EmergeDateTimeControl ID="OutOfStateExpirationDate" runat="server" IsRequired="true" EditTime="false" DisplayNow="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoOutOfStateIssuedBy" ResourceString="Emerge.CR.ProfessionalInfoOutOfStateIssuedBy" runat="server" /><span style="color: red">*</span>:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="OutOfStateIssuedBy" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvOutOfStateIssuedBy" CssClass="HaveContactedBoardForConversionValidator ErrorMessage" ValidationGroup="CREducationTraining" runat="server" ControlToValidate="OutOfStateIssuedBy" ErrorMessage="Required" Display="Dynamic" SetFocusOnError="true" />
                        </div>
                    </div>

                    
                </div>
                <div class="courses">
                    
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLiteral ID="litProfessionalInfoCoursesWithCertification" ResourceString="Emerge.CR.ProfessionalInfoCoursesWithCertification" runat="server" />:</div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:TextBox ID="CoursesWithCertification" TextMode="MultiLine" runat="server" />
                        </div>
                    </div>
                    
                </div>
            </div>
        </asp:Panel>
        <div class="clearfix">
           <div class="btnWrapper clearfix pull-left">
            <asp:LinkButton ID="Save" Text="Save" ValidationGroup="CREducationTraining" runat="server" />
            <asp:LinkButton ID="Clear" Text="Clear" runat="server" />
            </div>
           <div class="btnWrapper clearfix btn-prev pull-left">
            <asp:LinkButton ID="Next" Text="Save & Next <span class='icon-rightArrowGrey'>" ValidationGroup="CREducationTraining" runat="server" CssClass="pull-right" />
            <asp:LinkButton ID="Previous" Text="<span class='icon-leftArrowGrey'></span>Previous" runat="server" CssClass="pull-right" />

        </div>
         </div>


        <script>
            jQuery(document).ready(function () {
                InitializeCareer();
                setState('HaveLicenseInState');
                setState('HasLicenseEverRevokedSuspendedTerminated');
                setState('HaveContactedBoardForConversion');
                addRadioButtonHandlers('HaveLicenseInState');
                addRadioButtonHandlers('HasLicenseEverRevokedSuspendedTerminated');
                addRadioButtonHandlers('HaveContactedBoardForConversion');
                removeSectionBorders('secHaveLicenseInState');
                removeSectionBorders('secHasLicenseEverRevokedSuspendedTerminated');
                removeSectionBorders('secHaveContactedBoardForConversion');
            });
        </script>
    </asp:Panel>

    <div class="message_box">
        <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="FormConfirmationMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
</section>
