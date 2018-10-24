<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
<tr>
<td><strong>Item text:</strong></td>
<td><%# Eval("ItemText") %> <a href='?itemid=<%# Eval("ItemID") %>'>detail</a></td>
</tr>
</table>
