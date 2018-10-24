<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Last Name:</td>
    <td><%# Eval("LastName") %></td>
  </tr>
  <tr>
    <td>First Name:</td>
    <td><%# Eval("FirstName") %></td>
  </tr>
  <tr>
    <td>Social Security Number:</td>
    <td><%# Eval("SocialSecurityNumber") %></td>
  </tr>
  <tr>
    <td>Address:</td>
    <td><%# Eval("Address") %></td>
  </tr>
  <tr>
    <td>Position Applied for:</td>
    <td><%# Eval("PositionAppliedFor") %></td>
  </tr>
  <tr>
    <td>Referral Source:</td>
    <td><%# Eval("ReferralSource") %></td>
  </tr>
  <tr>
    <td>Date Of Birth:</td>
    <td><%# Eval("DateOfBirth") %></td>
  </tr>
  <tr>
    <td>Sex:</td>
    <td><%# Eval("Sex") %></td>
  </tr>
  <tr>
    <td>Race/Ethnic Group:</td>
    <td><%# Eval("Race") %></td>
  </tr>
  <tr>
    <td>Education:</td>
    <td><%# Eval("Education") %></td>
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
