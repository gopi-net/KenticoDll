<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>key:</td>
    <td><%# Eval("Key") %></td>
  </tr>
  <tr>
    <td>Value:</td>
    <td><%# Eval("Value") %></td>
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