<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("ArticleName",true) %></h1>
<p>
<%# IfEmpty(Eval("ArticleTeaserImage"), "", "<img alt=\"" + Eval("ArticleName",true) + "\" src=\"" + GetFileUrl("ArticleTeaserImage") + "?maxsidesize=100\" align=\"left\" hspace=\"5\" vspace=\"5\" />") %>
<%# Eval("ArticleText") %>
</p>
