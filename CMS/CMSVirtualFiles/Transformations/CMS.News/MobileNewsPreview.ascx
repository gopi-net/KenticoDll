<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="NewsPreviewTitle"><a href="~/mobile/news/<%# Eval("NewsID") %>.aspx"><%# Eval("NewsTitle",true) %></a></div>
<div class="NewsPreviewDate"><%# GetDateTime("NewsReleaseDate", "d") %></div>
<div class="NewsPreviewSummary"><%# Eval("NewsSummary") %></div>