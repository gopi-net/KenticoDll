<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("NewsTitle",true) %></h1>
<%# GetDateTime("NewsReleaseDate", "d") %><br/>
<%# IfEmpty(Eval("NewsTeaser"), "", GetImage("NewsTeaser")) %>
<%# IfEmpty(Eval("NewsSummary"), "", Eval("NewsSummary") + "<br />") %>
<%# Eval("NewsText") %>