<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><br />
<table class="table table-hover">
  <tr>
    <td><strong>Reference Type:</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("ReferenceType")).Replace("|","") %></td>
  </tr>
  <tr>
    <td><strong>Name:</strong></td>
    <td class="wrap-normal"><%# Eval("Title") %> <%# Eval("FirstName") %> <%# Eval("LastName") %></td>
  </tr>
   <tr>
    <td><strong>Company Name:</strong></td>
    <td class="wrap-normal"><%# Eval("CompanyName") %></td>
  </tr>
  <tr>
    <td><strong>Address:</strong></td>
    <td class="wrap-normal"><%# Eval("Address") %> <br /><%# Eval("City") %><br /><%# Eval("State") %> <br /> <%# Eval("Zip") %></td>
  </tr>
  <tr>
    <td><strong>Business Phone:</strong></td>
    <td class="wrap-normal"><%# Eval("BusinessPhone") %></td>
  </tr>
  <tr>
    <td><strong>Cell:</strong></td>
    <td class="wrap-normal"><%# Eval("Cell") %></td>
  </tr>
  <tr>
    <td><strong>Home Phone:</strong></td>
    <td class="wrap-normal"><%# Eval("HomePhone") %></td>
  </tr>
</table>
<br />