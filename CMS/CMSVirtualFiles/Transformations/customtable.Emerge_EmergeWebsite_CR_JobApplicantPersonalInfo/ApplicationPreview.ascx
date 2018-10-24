<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover">
  <tr>
    <td><strong>Name:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantFirstName") %> <%# Eval("ApplicantMiddleName") %> <%# Eval("ApplicantLastName") %></td>
  </tr>
  <tr>
    <td><strong>Address:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantAddress") %> <br /><%# Eval("ApplicantCity") %><br /><%# Eval("State") %>
      <br /><%# Eval("ApplicantZip") %></td>
  </tr>
  <tr>
    <td><strong>Tel Home #:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantPhoneHome") %></td>
  </tr>
  <tr>
    <td><strong>Tel Cell/Beeper/Other#:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantPhoneOther") %></td>
  </tr>
  <tr>
    <td><strong>Email Address:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmail") %></td>
  </tr>
  <tr>
    <td><strong>If you are under 18, and it is required,<br /> can you furnish a work permit?:</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("ApplicantCanFurnishWorkPermit")).Replace("|","") %></td>
  </tr>
  <tr>
    <td><strong>Are you either a U.S. citizen or an alien<br /> who is legally eligible for employment in this country?:</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("ApplicantElegibleForEmploymentInCountry")).Replace("|","") %></td>
  </tr>
  <tr>
    <td><strong>In case of emergency notify:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmergengyContactName") %></td>
  </tr>
  <tr>
    <td><strong>Relationship:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmergengyContactRelationship") %></td>
  </tr>
  <tr>
    <td><strong>Tel # (Home):</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmergengyContactPhoneHome") %></td>
  </tr>
  <tr>
    <td><strong>Tel # (Cell):</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmergengyContactPhoneCell") %></td>
  </tr>
  <tr>
    <td><strong>Tel # (Work):</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantEmergengyContactPhoneWork") %></td>
  </tr>
</table>
