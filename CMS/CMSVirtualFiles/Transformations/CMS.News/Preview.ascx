<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="newsItem">
<p><strong><a href="<%# GetDocumentUrl() %>"><%# Eval("NewsTitle",true) %></a></strong></p>
<p><%# StripTags(Eval("NewsSummary")) %></p>
</div>