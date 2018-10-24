<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
<tr>
<td width="60" valign="top">
<%# IfEmpty(Eval("ArticleTeaserImage"), "", "<img alt=\"" + Eval("ArticleName",true) + "\" src=\"" + GetFileUrl("ArticleTeaserImage") + "?maxsidesize=50\" />") %></td>
<td>
<h2><a href="<%# GetDocumentUrl() %>"><%# Eval("ArticleName",true) %></a></h2>
<p><%# Eval("ArticleTeaserText") %></p>
</td>
</tr>
</table>