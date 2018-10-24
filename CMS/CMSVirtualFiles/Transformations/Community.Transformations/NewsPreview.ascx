<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="newsItem">
<%#IfEmpty(Eval("NewsTeaser"), "", "<div class='teaser'><img src='" + GetFileUrl("NewsTeaser") + "?maxsidesize=66' alt='" + Eval("NewsTitle", true) + " ' /></div>")%>
<h3><a href="<%# GetDocumentUrl() %>"><%# Eval("NewsTitle",true) %></a> | <%# GetDateTime("NewsReleaseDate", "d") %></h3>
<p><%# Eval("NewsSummary") %></p>
<div class="clear">&nbsp;</div>
</div>