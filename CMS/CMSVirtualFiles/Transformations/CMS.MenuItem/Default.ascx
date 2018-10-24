<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Document name:</td>
    <td><%# Eval("MenuItemName") %></td>
  </tr>
  <tr>
    <td>Teaser image:</td>
    <td><%# Eval("MenuItemTeaserImage") %></td>
  </tr>
  <tr>
    <td>Menu group:</td>
    <td><%# Eval("MenuItemGroup") %></td>
  </tr>
</table>
