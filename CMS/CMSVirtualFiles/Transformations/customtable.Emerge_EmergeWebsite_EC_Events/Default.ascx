<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Event Name:</td>
    <td><%# Eval("EventName") %></td>
  </tr>
  <tr>
    <td>Short Title:</td>
    <td><%# Eval("ShortTitle") %></td>
  </tr>
  <tr>
    <td>Long Title:</td>
    <td><%# Eval("LongTitle") %></td>
  </tr>
  <tr>
    <td>Teaser Text:</td>
    <td><%# Eval("TeaserText") %></td>
  </tr>
  <tr>
    <td>Event Description:</td>
    <td><%# Eval("EventDescription") %></td>
  </tr>
  <tr>
    <td>Event Location:</td>
    <td><%# Eval("EventLocation") %></td>
  </tr>
  <tr>
    <td>Attachment:</td>
    <td><%# Eval("Attachment") %></td>
  </tr>
  <tr>
    <td>Attachment Text:</td>
    <td><%# Eval("AttachmentText") %></td>
  </tr>
  <tr>
    <td>Website Link:</td>
    <td><%# Eval("WebsiteLink") %></td>
  </tr>
  <tr>
    <td>Contact Name:</td>
    <td><%# Eval("ContactName") %></td>
  </tr>
  <tr>
    <td>Contact Email:</td>
    <td><%# Eval("ContactEmail") %></td>
  </tr>
  <tr>
    <td>Contact Phone:</td>
    <td><%# Eval("ContactPhone") %></td>
  </tr>
  <tr>
    <td>Registration form needed?:</td>
    <td><%# Eval("IsRegistrationNeeded") %></td>
  </tr>
  <tr>
    <td>Registration Limit:</td>
    <td><%# Eval("RegistrationLimit") %></td>
  </tr>
  <tr>
    <td>Is this a paid event?:</td>
    <td><%# Eval("IsPaidEvent") %></td>
  </tr>
  <tr>
    <td>Cost for Public:</td>
    <td><%# Eval("Cost") %></td>
  </tr>
  <tr>
    <td>Discounted Cost:</td>
    <td><%# Eval("DiscountedCost") %></td>
  </tr>
  <tr>
    <td>Discount Code1:</td>
    <td><%# Eval("DiscountCode1") %></td>
  </tr>
  <tr>
    <td>Discount Code2:</td>
    <td><%# Eval("DiscountCode2") %></td>
  </tr>
  <tr>
    <td>Is this a series event?:</td>
    <td><%# Eval("IsSeriesEvent") %></td>
  </tr>
  <tr>
    <td>Event Status:</td>
    <td><%# Eval("EventStatus") %></td>
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
