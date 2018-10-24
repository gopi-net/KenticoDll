<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="blogsHome">
<h4>
<a href="<%# GetDocumentUrl() %>"><%# Eval("BlogPostTitle",true) %></a>
</h4>
<div>
<%# StripTags(Eval("BlogPostSummary")) %>
</div>
<div class="date">Posted on <strong><%# Eval("BlogPostDate") %></strong></div>
</div>