<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="TextContent">
<div class="NewsPreviewTitle"><a href="<%# GetDocumentUrl() %>"><%# Eval("NewsTitle",true) %></a></div>
<div class="NewsPreviewDate"><%# GetDateTime("NewsReleaseDate", "d") %></div>
<div class="NewsPreviewSummary TextContent"><%# Eval("NewsSummary") %></div>
</div>