<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover">
   <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("NewspaperName")?"table-row":"none" %>">
    <td><strong>Newspaper:</strong></td>
    <td class="wrap-normal"><%# Eval("NewspaperName") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("SchoolName")?"table-row":"none" %>">
    <td><strong>School:</strong></td>
    <td class="wrap-normal"><%# Eval("SchoolName") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("OpenHouse")?"table-row":"none" %>">
    <td><strong>Open House:</strong></td>
    <td class="wrap-normal"><%# Eval("OpenHouse") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("JobFair")?"table-row":"none" %>">
    <td><strong>Job Fair:</strong></td>
    <td class="wrap-normal"><%# Eval("JobFair") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("InternetPosting")?"table-row":"none" %>">
    <td><strong>Internet Posting:</strong></td>
    <td class="wrap-normal"><%# Eval("InternetPosting") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("EmploymentAgency")?"table-row":"none" %>">
    <td><strong>Employment Agency:</strong></td>
    <td class="wrap-normal"><%# Eval("EmploymentAgency") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("ProfessionalJournal")?"table-row":"none" %>">
    <td><strong>Professional Journal:</strong></td>
    <td class="wrap-normal"><%# Eval("ProfessionalJournal") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("Other")?"table-row":"none" %>">
    <td><strong>Other:</strong></td>
    <td class="wrap-normal"><%# Eval("Other") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("Referral")).Contains("EmployeeReferral")?"table-row":"none" %>">
    <td><strong>Employee Referral (Provide Name/ Dept.):</strong></td>
    <td class="wrap-normal"><%# Eval("EmployeeReferral") %></td>
  </tr>
</table>
