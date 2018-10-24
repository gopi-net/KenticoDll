<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><center><b>From <%# Eval("EmploymentFrom","{0:dddd, MMMM dd, yyyy}") %> To <%# Eval("EmploymentTo","{0:dddd, MMMM dd, yyyy}") %></b></center>
<br />
<table class="table table-hover">
  <tr>
    <td><strong>Company's Name:</strong></td>
    <td class="wrap-normal"><%# Eval("CompanyName") %></td>
  </tr>
  <tr>
    <td><strong>Company's Address:</strong></td>
    <td class="wrap-normal"><%# Eval("ComapnyAddress") %></td>
  </tr>
  <tr>
    <td><strong>Position Held:</strong></td>
    <td class="wrap-normal"><%# Eval("PositionHeld") %></td>
  </tr>
  <tr>
    <td><strong>Supervisor's Name:</strong></td>
    <td class="wrap-normal"><%# Eval("SupervisorFirstName") %> <%# Eval("SupervisorLastName") %></td>
  </tr>
  <tr>
    <td><strong>Department's Name:</strong></td>
    <td class="wrap-normal"><%# Eval("DepartmentName") %></td>
  </tr>
  <tr>
    <td><strong>Supervisor's Title:</strong></td>
    <td class="wrap-normal"><%# Eval("SupervisorTitle") %></td>
  </tr>
  <tr>
    <td><strong>Phone Number:</strong></td>
    <td class="wrap-normal"><%# Eval("PhoneNumber") %></td>
  </tr>
  <tr>
    <td><strong>May we contact for reference?:</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("MayContactForReference")).Replace("|","") %></td>
  </tr>
  <tr>
    <td><strong>Job Responsibilities:</strong></td>
    <td class="wrap-normal"><%# Eval("JobResponsibilities") %></td>
  </tr>
  <tr>
    <td><strong>Last Salary:</strong></td>
    <td class="wrap-normal"><%# Eval("LastSalary") %></td>
  </tr>
  <tr>
    <td><strong>Last Salary(Check One):</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("LastSalaryType")).Replace("|","") %></td>
  </tr>
  <tr>
    <td><strong>Other Compensation:</strong></td>
    <td class="wrap-normal"><%# Eval("OtherCompensation") %></td>
  </tr>
  <tr>
    <td><strong>Other Name employed under:</strong></td>
    <td class="wrap-normal"><%# Eval("OtherNameEmployedUnder") %></td>
  </tr>
  <tr>
    <td><strong>Reason For Leaving:</strong></td>
    <td class="wrap-normal"><%# Eval("ReasonForLeaving") %></td>
  </tr>
</table>
<br />
