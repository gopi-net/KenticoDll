<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
<tr>
<td>Name:</td>
<td><%# Eval("GalleryName",true) %></td>
</tr>
<tr>
<td>Description:</td>
<td><%# Eval("GalleryDescription") %></td>
</tr>
<tr>
<td>Teaser image:</td>
<td><%# Eval("GalleryTeaserImage") %></td>
</tr>
</table>
