<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><p><strong><%# GetDate("NewsReleaseDate") %></strong></p>
<div class="NewsReleaseTitle">
<a href="<%# GetDocumentUrl() %>"><%# StripTags(Eval("NewsSummary")) %></a>
</div>
