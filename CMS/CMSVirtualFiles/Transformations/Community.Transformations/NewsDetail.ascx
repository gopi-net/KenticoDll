<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="newsItemDetail">
<h1><%# GetDateTime("NewsReleaseDate", "d") %></h1>
<div class="NewsSummary">
  <div class="newsSummary">
<%#IfEmpty(Eval("NewsTeaser"), "", "<div class='teaser'><img src='" + GetFileUrl("NewsTeaser") + "?maxsidesize=66' alt='" + Eval("NewsTitle", true) + " ' /></div>")%>
    <%# Eval("NewsSummary") %>
  </div>
</div>
<div class="NewsBody">
  <%# Eval("NewsText") %>
</div>
</div>