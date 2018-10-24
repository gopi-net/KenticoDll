<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="PreRegistration_Data_View_PreRegistration_Item" Theme="Default"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="PreRegistration_Data_View_PreRegistration_Item.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeViewItem.ascx" TagName="CustomTableViewItem"
    TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <%--<ul style="list-style: none;">
        <li onclick="showDiv('patientInformation', this)">
            <a href="javascript:showDiv('patientInformation')">
                <cms:LocalizedLabel ID="litHPatientInformation" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HPatientInformation" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('jobInterest')">
            <a href="javascript:showDiv('responsibleParty')">
                <cms:LocalizedLabel ID="litHResponsibleParty" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HResponsibleParty" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('Employer')">
            <a href="javascript:showDiv('Employer')">
                <cms:LocalizedLabel ID="litHEmployer" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HEmployer" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('nextOfKin')">
            <a href="javascript:showDiv('nextOfKin')">
                <cms:LocalizedLabel ID="litHNextOfKin" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HNextOfKin" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('primaryInsurance')">
            <a href="javascript:showDiv('primaryInsurance')">
                <cms:LocalizedLabel ID="litHPrimaryInsurance" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HPrimaryInsurance" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('secondaryInsurance')">
            <a href="javascript:showDiv('secondaryInsurance')">
                <cms:LocalizedLabel ID="litHSecondaryInsurance" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HSecondaryInsurance" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('visitInformation')">
            <a href="javascript:showDiv('visitInformation')">
                <cms:LocalizedLabel ID="litHVisitInformation" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HVisitInformation" DisplayColon="false" />
            </a>
        </li>
        <li onclick="showDiv('babysParentInfo')">
            <a href="javascript:showDiv('babysParentInfo')">
                <cms:LocalizedLabel ID="litHBabysParentInfo" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.HBabysParentInfo" DisplayColon="false" /></a>
        </li>
        <li onclick="showDiv('parentalIdentification')">
            <a href="javascript:showDiv('parentalIdentification')">
                <cms:LocalizedLabel ID="liParentalIdentification" runat="server" EnableViewState="false" ResourceString="Emerge.PR.Label.ParentalIdentification" DisplayColon="false" /></a>
        </li>
    </ul>
    <br />
    <br />
    <br />
    <br />

    <div id="personalIformation" class="data">
        <cms:CMSRepeater ID="patientInformationView" runat="server" />
    </div>
    <div id="responsibleParty" class="data">
        <cms:CMSRepeater ID="responsiblePartyView" runat="server" />
    </div>
    <div id="employer" class="data">
        <cms:CMSRepeater ID="employerView" runat="server" />
    </div>
    <div id="nextOfKin" class="data">
        <cms:CMSRepeater ID="nextOfKinView" runat="server" />       
    </div>
    <div id="primaryInsurance" class="data">
        <cms:CMSRepeater ID="primaryInsuranceView" runat="server" />        
    </div>
    <div id="secondaryInsurance" class="data">
        <cms:CMSRepeater ID="secondaryInsuranceView" runat="server" />
    </div>
    <div id="visitInformation" class="data">
        <cms:CMSRepeater ID="visitInformationView" runat="server" />
    </div>
    <div id="babysParentInfo" class="data">
        <cms:CMSRepeater ID="babysParentInfoView" runat="server" />
    </div>
    <div id="parentalIdentification" class="data">
        <cms:CMSRepeater ID="parentalIdentificationView" runat="server" />
    </div>   
    <style type="text/css">
        ul li a {
            text-decoration: none !important;
            border: 1px solid #CCCCCC -moz-use-text-color;
            font-weight: bold;
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
            background-color: #0F6194;
        }

        .active a {
            color: #F5F5F5;
        }
    </style>
    <script>
        jQuery(document).ready(function () {
            showDiv('patientInformation');
            return fs_m_pt($j(this));
        });
        function showDiv(divId) {

            jQuery('.data').hide();
            jQuery('#' + divId).show();
        }
    </script>--%>
    <style type="text/css">
        .tbl-pre-reg tr td {border:1px solid;border-collapse:collapse;padding:10px;
        }
        .SecondaryInsDiv {margin-top:10px;
        }
        h3 {margin-bottom:10px!important;margin-top:10px!important;
        }
        hr {margin-top:0px!important;
        }
    </style>
     <cms:CMSRepeater ID="PreRegistrationInfo" runat="server">
        </cms:CMSRepeater>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" ResourceString="general.close"
            OnClientClick="return CloseDialog();" EnableViewState="false" />
    </div>
</asp:Content>
