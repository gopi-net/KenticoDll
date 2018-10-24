<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Location Name:</td>
    <td><%# Eval("LocationName") %></td>
  </tr>
  <tr>
    <td>Category:</td>
    <td><%# Eval("Category") %></td>
  </tr>
  <tr>
    <td>Location Address:</td>
    <td><%# Eval("LocationAddress") %></td>
  </tr>
  <tr>
    <td>City:</td>
    <td><%# Eval("LocationCity") %></td>
  </tr>
  <tr>
    <td>State:</td>
    <td><%# Eval("State") %></td>
  </tr>
  <tr>
    <td>Zip Code:</td>
    <td><%# Eval("Zip") %></td>
  </tr>
  <tr>
    <td>Phone:</td>
    <td><%# Eval("Phone") %></td>
  </tr>
  <tr>
    <td>Fax:</td>
    <td><%# Eval("Fax") %></td>
  </tr>
  <tr>
    <td>Hours:</td>
    <td><%# Eval("Hours") %></td>
  </tr>
  <tr>
    <td>Feature Icon:</td>
    <td><%# Eval("FeatureIcon") %></td>
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
<cc1:CMSEditModeButtonEditDelete runat="server" id="btnEditDeleteAutoInsert" Path='<%# Eval("NodeAliasPath") %>' AddedAutomatically="True" EnableByParent="True"   />