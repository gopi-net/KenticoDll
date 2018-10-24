<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="NewsTitle">
  <a href="<%# GetDocumentUrl() %>"><%# Eval("NewsTitle",true) %></a>
</div>
<div class="NewsSummary">
  <%# IfEmpty(Eval("NewsTeaser"), "", GetImage("NewsTeaser")) %>
  <div class="Date">
  <%# GetDateTime("NewsReleaseDate", "d") %>
  </div>
  <%# Eval("NewsSummary") %>
  <div class="Clearer"></div>
</div>