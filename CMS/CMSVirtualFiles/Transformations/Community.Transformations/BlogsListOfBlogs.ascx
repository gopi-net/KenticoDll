<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="blogMainList">
<%# IfEmpty(Eval("BlogTeaser"),"","<div class=\"blogListTeaser\"><img src=\""+GetFileUrl("BlogTeaser")+"?width=65\" /></div>") %>
<p><a href="<%# GetDocumentUrl() %>" title="<%# Eval("BlogName", true) %>"><%# Eval("BlogName", true) %></a></p>
<p><%# Eval("BlogDescription") %></p>
<div class="clear">&nbsp;</div>
</div>