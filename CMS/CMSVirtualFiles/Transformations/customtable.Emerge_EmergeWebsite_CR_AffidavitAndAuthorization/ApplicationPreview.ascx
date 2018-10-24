<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover">
  <tr>
    <td><strong>Affidavit and Authorization Details:</strong></td>
    <td class="wrap-normal"><%# Eval("AuthorizationDetails") %></td>
  </tr>
  <tr>
    <td><strong>Applicant's Signature:</strong></td>
    <td class="wrap-normal"><%# Eval("ApplicantSignature") %></td>
  </tr>
  <tr>
    <td><strong>Date:</strong></td>
    <td class="wrap-normal"><%# Eval("Date","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
</table>
