<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="NewsPTitle"><a href="<%# GetDocumentUrl() %>">
  <%# Eval("NewsTitle",true) %></a>&nbsp;|&nbsp;<%# GetDateTime("NewsReleaseDate", "MM/dd/yyyy HH:mm") %>
</div>
<div class="NewsPBody">
  <div class="TextContent"><%# Eval("NewsSummary") %></div>
</div>
