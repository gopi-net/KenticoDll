<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
<tr>
<td>ItemCreatedBy:</td>
<td><%# Eval("ItemCreatedBy") %></td>
</tr>
<tr>
<td>ItemCreatedWhen:</td>
<td><%# Eval("ItemCreatedWhen") %></td>
</tr>
<tr>
<td>ItemModifiedBy:</td>
<td><%# Eval("ItemModifiedBy") %></td>
</tr>
<tr>
<td>ItemModifiedWhen:</td>
<td><%# Eval("ItemModifiedWhen") %></td>
</tr>
<tr>
<td>ItemOrder:</td>
<td><%# Eval("ItemOrder") %></td>
</tr>
<tr>
<td>Item text:</td>
<td><%# Eval("ItemText") %></td>
</tr>
</table>