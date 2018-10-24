<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="newsItemDetail">
<h1><%# Eval("NewsTitle",true) %></h1>
<div class="NewsSummary">
  <%# IfEmpty(Eval("NewsTeaser"), "", GetImage("NewsTeaser")) %>
  <div class="NewsContent">
    <div class="Date"><%# GetDateTime("NewsReleaseDate", "d") %></div>
    <div class="TextContent"><%# Eval("NewsSummary") %></div>
  </div>
  <div class="Clearer">&nbsp;</div>
</div>
<div class="NewsBody">
  <div class="TextContent"><%# Eval("NewsText") %></div>
</div>
</div>