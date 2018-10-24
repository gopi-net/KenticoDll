<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Loan Type:</td>
    <td><%# Eval("LoanType") %></td>
  </tr>
  <tr>
    <td>Loan Term:</td>
    <td><%# Eval("LoanTerm") %></td>
  </tr>
  <tr>
    <td>Interest Rate:</td>
    <td><%# Eval("InterestRate") %></td>
  </tr>
  <tr>
    <td>Discount Points:</td>
    <td><%# Eval("DiscountPoints") %></td>
  </tr>
  <tr>
    <td>APR:</td>
    <td><%# Eval("APR") %></td>
  </tr>
  <tr>
    <td>Monthly Payments:</td>
    <td><%# Eval("MonthlyPayments") %></td>
  </tr>
  <tr>
    <td>Created by:</td>
    <td><%# Eval("ItemCreatedBy") %></td>
  </tr>
  <tr>
    <td>Created when:</td>
    <td><%# Eval("ItemCreatedWhen") %></td>
  </tr>
  <tr>
    <td>Modified by:</td>
    <td><%# Eval("ItemModifiedBy") %></td>
  </tr>
  <tr>
    <td>Modified when:</td>
    <td><%# Eval("ItemModifiedWhen") %></td>
  </tr>
  <tr>
    <td>Order:</td>
    <td><%# Eval("ItemOrder") %></td>
  </tr>
  <tr>
    <td>GUID:</td>
    <td><%# Eval("ItemGUID") %></td>
  </tr>
</table>
