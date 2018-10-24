<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover professionalLicense">
  <tr>
    <td><strong>Do you hold a professional license or <br/>certification in the State of New Jersey?:</strong></td>
    <td class="wrap-normal" colspan ="2"><%# Eval("HaveLicenseInState") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Professional Licensure or Certification:</strong></td>
    <td class="wrap-normal"><%# Eval("ProfessionalLicense1")%></td>
    <td class="wrap-normal"><%# Eval("ProfessionalLicense2")%></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>License Number:</strong></td>
    <td class="wrap-normal"><%# Eval("LicenseNumber1") %></td>
    <td class="wrap-normal"><%# Eval("LicenseNumber2") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>State of Issuance:</strong></td>
    <td class="wrap-normal"><%# Eval("StateOfIssuance1") %></td>
    <td class="wrap-normal"><%# Eval("StateOfIssuance2") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Date Issued:</strong></td>
    <td class="wrap-normal"><%# Eval("DateIssued1","{0:dddd, MMMM dd, yyyy}") %></td>
    <td class="wrap-normal"><%# Eval("DateIssued2","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Expiration Date:</strong></td>
    <td class="wrap-normal"><%# Eval("ExpirationDate1","{0:dddd, MMMM dd, yyyy}") %></td>
    <td class="wrap-normal"><%# Eval("ExpirationDate2","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveLicenseInState")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Issued By:</strong></td>
    <td class="wrap-normal"><%# Eval("IssuedBy1") %></td>
    <td class="wrap-normal"><%# Eval("IssuedBy2") %></td>
  </tr>
  </table>
<br />
<table class="table table-hover">
  <tr>
    <td><strong>Has your licensure or certification with any federal,<br /> 
        state or local regulatory agency, <br />
        or any foreign country or its agency, <br />
        ever been revoked, suspended or terminated?:</strong></td>
    <td class="wrap-normal"><%# Eval("HasLicenseEverRevokedSuspendedTerminated") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HasLicenseEverRevokedSuspendedTerminated")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Explain & Lic/Cert Type & #:</strong></td>
    <td class="wrap-normal"><%# Eval("Explaination") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HasLicenseEverRevokedSuspendedTerminated")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Date of action:</strong></td>
    <td class="wrap-normal"><%# Eval("DateOfAction","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HasLicenseEverRevokedSuspendedTerminated")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Reactivation date:</strong></td>
    <td class="wrap-normal"><%# Eval("ReactivationDate","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr>
    <td><strong>Nurses with out-of-state licensure:<br />
         Have you contacted the New Jersey Board of Nursing <br />
        Examiners to convert your license?:</strong></td>
    <td class="wrap-normal"><%# Eval("HaveContactedBoardForConversion") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Expected Conversion Date:</strong></td>
    <td class="wrap-normal"><%# Eval("ExpectedConversionDate","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Other countries or State of Certification:</strong></td>
    <td class="wrap-normal"><%# Eval("OtherCountriesOrStateOfCertification") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Professional License Number:</strong></td>
    <td class="wrap-normal"><%# Eval("OutOfStateProfessionalLicenseNumber") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Effective Date:</strong></td>
    <td class="wrap-normal"><%# Eval("OutOfStateEffectiveDate","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Expiration Date:</strong></td>
    <td class="wrap-normal"><%# Eval("OutOfStateExpirationDate","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("HaveContactedBoardForConversion")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Issued By:</strong></td>
    <td class="wrap-normal"><%# Eval("OutOfStateIssuedBy") %></td>
  </tr>
  <tr>
    <td><strong>Courses with certification and/or<br /> continuing education credits:</strong></td>
    <td class="wrap-normal"><%# Eval("CoursesWithCertification") %></td>
  </tr>
</table>
