<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><strong><a href="<%# ResolveUrl(GetUrl( Eval("NodeAliasPath"), Eval("DocumentUrlPath"))) %>">
<%# Eval("NewsTitle",true) %></a></strong>&nbsp;
(<%# System.Convert.ToString(Convert.ToDateTime(Eval("NewsReleaseDate"))) %>)<br />
<br />
<em>
<%# Eval("NewsSummary") %>
</em>