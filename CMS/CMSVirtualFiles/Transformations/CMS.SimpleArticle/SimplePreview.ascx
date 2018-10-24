<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="Article">
<div class="Header">
	<a href="<%# GetDocumentUrl() %>" ><%# Eval("ArticleTitle",true) %></a>
</div>
<div class="Body">
	<div>
	<div class="TextContent"><%# Eval("ArticleText") %></div>
	</div>
</div>
</div>
<br />