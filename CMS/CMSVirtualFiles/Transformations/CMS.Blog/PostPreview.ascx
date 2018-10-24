<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="BlogPTitle"><a href="<%# GetDocumentUrl() %>">
  <%# Eval("BlogPostTitle", true) %></a>
</div>
<div class="BlogPBody TextContent">
  <%# Eval("BlogPostBody") %>
</div>
<div class="BlogPDateWhole">
    Posted: <span class="BlogPDate"><%# Eval("BlogPostDate") %></span> by 
<strong><%# BlogFunctions.GetUserFullName(Eval("DocumentCreatedByUserID")) %></strong> | with <a href="<%# GetDocumentUrl() %>#comments"><%# BlogFunctions.GetBlogCommentsCount(Eval("DocumentID"), Eval("NodeAliasPath")) %> comments</a></div>
<br/>
<br/>