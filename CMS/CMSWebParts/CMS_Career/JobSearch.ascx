<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearch.ascx.cs" Inherits="CMSWebParts_CMS_Career_JobSearch" %>

<section class="contentInner careerWrap">
    <asp:Panel ID="pnlJobSearch" runat="server" DefaultButton="Submit">

        <div class="clearfix">
            <h1>Careers</h1>
        </div>
        <div class="searchWrap">
            <cms:CMSEditableRegion ID="regContent" RegionTitle="Header Content" RegionType="HtmlEditor" runat="server" />
            <table>
                <tbody>
                     <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><cms:LocalizedLiteral ID="litJobTitle" ResourceString="Emerge.CR.JobTitleLabel" runat="server"></cms:LocalizedLiteral>
                            </div>
                        <div class="col-md-5 col-sm-6 col-xs-12">
                            <asp:TextBox ID="JobTitle" MaxLength="30" runat="server" /></div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><cms:LocalizedLiteral ID="litDepartment" ResourceString="Emerge.CR.DepartmentLabel" runat="server"></cms:LocalizedLiteral>                          
                            </div>
                        <div class="col-md-5 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="Department" DataTextField="Department" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="Department_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_Department.GetDepartment" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><cms:LocalizedLiteral ID="litLocation" ResourceString="Emerge.CR.LocationLabel" runat="server"></cms:LocalizedLiteral>   </div>
                        <div class="col-md-5 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="Location" DataTextField="Location" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="Location_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_Location.GetLocation" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12"><cms:LocalizedLiteral ID="litEmploymentType" ResourceString="Emerge.CR.EmploymentTypeLabel" runat="server"></cms:LocalizedLiteral>
                            </div>
                        <div class="col-md-5 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="EmploymentType" DataTextField="EmploymentType" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="EmploymentType_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_JobEmploymentType.GetEmploymentType" />
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-3 col-sm-6 col-xs-12"><cms:LocalizedLiteral ID="litShift" ResourceString="Emerge.CR.ShiftLabel" runat="server"></cms:LocalizedLiteral>
                            </div>
                        <div class="col-md-5 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="JobShift" DataTextField="Shift" DataValueField="ItemId" />
                            <cms:CMSQueryDataSource ID="JobShift_DataSource" runat="server" QueryName="customtable.Emerge_{0}_CR_Shift.GetShifts" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 col-sm-6 col-xs-12 btnWrapper">
                            <asp:Button ID="Submit" Text="Search" runat="server" />
                            <asp:Button ID="Clear" Text="Clear" runat="server" />
                    </div>
                </tbody>
            </table>
        </div>
        <div class="searchResult">
            <hr>
           <h3> <cms:LocalizedLiteral ID="lblSearchResult" ResourceString="Emerge.CR.SearchResultLabel" runat="server" Visible="false"></cms:LocalizedLiteral></h3>
            <cms:LocalizedLiteral ID="litNoRecordsFound" ResourceString="Emerge.CR.NoRecordsFound" runat="server" Visible="false"></cms:LocalizedLiteral>
            
            <ul>
                <cms:CMSRepeater ID="repJobs" runat="server" />
            </ul>
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