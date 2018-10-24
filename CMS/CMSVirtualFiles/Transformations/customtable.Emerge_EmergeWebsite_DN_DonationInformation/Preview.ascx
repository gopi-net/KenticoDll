<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Corporation Name:</td>
    <td><%# Eval("CorporationName") %></td>
  </tr>
  <tr>
    <td>Name of the Donor(s):</td>
    <td><%# Eval("DonorName") %></td>
  </tr>
  <tr>
    <td>Address1:</td>
    <td><%# Eval("Address1") %></td>
  </tr>
  <tr>
    <td>Address2:</td>
    <td><%# Eval("Address2") %></td>
  </tr>
  <tr>
    <td>City:</td>
    <td><%# Eval("City") %></td>
  </tr>
  <tr>
    <td>State:</td>
    <td><%# Eval("State") %></td>
  </tr>
  <tr>
    <td>Zip:</td>
    <td><%# Eval("Zip") %></td>
  </tr>
  <tr>
    <td>Phone:</td>
    <td><%# Eval("Phone") %></td>
  </tr>
  <tr>
    <td>Extension:</td>
    <td><%# Eval("Extension") %></td>
  </tr>
  <tr>
    <td>Email:</td>
    <td><%# Eval("Email") %></td>
  </tr>
  <tr>
    <td>Type of Donation:</td>
    <td><%# Eval("DonationType") %></td>
  </tr>
  <tr>
    <td>Amount of Donation:</td>
    <td><%# Eval("DonationAmount") %></td>
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
