<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
<tr>
<td>Product name:</td>
<td><%# Eval("SKUName", true) %></td>
</tr>
<tr>
<td>Price:</td>
<td><%# GetSKUFormattedPrice(true, false) %></td>
</tr>
<tr>
<td>Description:</td>
<td><%# Eval("SKUDescription") %></td>
</tr>
<tr>
<td>Photo:</td>
<td>Image</td>
</tr>
</table>
