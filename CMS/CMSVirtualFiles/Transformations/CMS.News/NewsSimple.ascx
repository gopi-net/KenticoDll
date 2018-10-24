<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="NewsTitle"><a href="<%# GetDocumentUrl() %>"><%# Eval("NewsTitle",true) %></a></div>
<div class="NewsDate"><%# GetDateTime("NewsReleaseDate", "d") %></div>

