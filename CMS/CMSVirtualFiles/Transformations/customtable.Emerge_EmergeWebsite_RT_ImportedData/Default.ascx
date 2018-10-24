<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
  <tr>
    <td><%# Eval("LoanType") %></td>
    <td><%# Eval("LoanTerm") %></td>
    <td><%# Eval("InterestRate") %></td>
    <td><%# Eval("DiscountPoints") %></td>
    <td><%# Eval("APR") %></td>
    <td><%# Eval("MonthlyPayments") %></td>
  </tr>

