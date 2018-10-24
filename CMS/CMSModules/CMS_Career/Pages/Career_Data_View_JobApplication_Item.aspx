<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Career_Pages_Career_Data_View_JobApplication_Item" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="Career_Data_View_JobApplication_Item.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeViewItem.ascx" TagName="CustomTableViewItem"
    TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <ul style="list-style: none;">
        <li onclick="showDiv('personalIformation', this)">
            <a href="javascript:showDiv('personalIformation')">Personal Information</a>
        </li>
        <li onclick="showDiv('jobInterest')">
            <a href="javascript:showDiv('jobInterest')">Job Interest</a>
        </li>
        <li onclick="showDiv('educationTraining')">
            <a href="javascript:showDiv('educationTraining')">Education & Training</a>
        </li>
        <li onclick="showDiv('employmentHistory')">
            <a href="javascript:showDiv('employmentHistory')">Employment History</a>
        </li>
        <li onclick="showDiv('references')">
            <a href="javascript:showDiv('references')">References & Referrals</a>
        </li>
        <li onclick="showDiv('affidavit')">
            <a href="javascript:showDiv('affidavit')">Affidavit & Authorization</a>
        </li>
    </ul>
    <br />
    <br />
    <br />
    <br />

    <div id="personalIformation" class="data">
        <cms:CMSRepeater ID="personalIformationView" runat="server" />
    </div>
    <div id="jobInterest" class="data">
        <cms:CMSRepeater ID="jobInterestView" runat="server" />
    </div>
    <div id="educationTraining" class="data">
        <cms:CMSRepeater ID="educationTrainingView" runat="server" />
        <br />
        <center><b>Professional Licensure and Certification Information</b></center>
        <br />
        <cms:CMSRepeater ID="professionalLicenseView" runat="server" />
    </div>
    <div id="employmentHistory" class="data">
        <cms:CMSRepeater ID="employmentHistoryView" runat="server" />
        <center><b>Military Record in the United States Armed Forces</b></center>
        <br />
        <cms:CMSRepeater ID="militaryRecordView" runat="server" />
    </div>
    <div id="references" class="data">
        <cms:CMSRepeater ID="referencesView" runat="server" />
        <center><b>Referrals</b></center>
        <br />
        <cms:CMSRepeater ID="referralView" runat="server" />
    </div>
    <div id="affidavit" class="data">
        <cms:CMSRepeater ID="affidavitView" runat="server" />
    </div>
    <style type="text/css">

        ul li a {
                text-decoration: none !important;
                border: 1px solid #CCCCCC -moz-use-text-color;
                font-weight:bold;
            }

        ul li {
            float: left;
            padding-left: 10px;
            background-color: #F5F5F5;
            border: 1px solid #CCCCCC;
            padding: 5px;
            margin: 10px;
        }


        ul {
            list-style: none;
        }
        .active li {
            background-color:#0F6194 
            
        }
        .active a {
            color: #F5F5F5;
        }
    </style>
    <script>
	
        $(document).ready(function () {
		
            showDiv('personalIformation');
            return fs_m_pt($(this));
        });
		
        function showDiv(divId) {
            
            $('.data').hide();
            $('#' + divId).show();
        }
	
    </script>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" ResourceString="general.close"
            OnClientClick="return CloseDialog();" EnableViewState="false" />
    </div>
</asp:Content>
