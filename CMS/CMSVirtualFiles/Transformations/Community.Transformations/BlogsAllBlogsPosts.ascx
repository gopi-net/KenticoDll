<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="BlogPTitle"><a href="<%# GetDocumentUrl() %>">
  <%# Eval("BlogPostTitle",true) %></a>
</div>
<div class="BlogPBody">
  <%# IfEmpty(Eval("BlogPostTeaser"),"","<div class=\"teaser\"><img src=\""+GetFileUrl("BlogPostTeaser")+"?maxsidesize=66\" alt=\""+Eval("BlogPostTitle",true)+" \" /></div>") %>
<%# Eval("BlogPostSummary") %>
</div>
<br class="clear" />
<div class="BlogPDateWhole">
    Posted to <strong><cms:CMSBreadCrumbs ID="ucBreadcrumbs" runat="server" StartingPath="/Blogs" Path='<%# Eval("NodeAliasPath") %>' ClassNames="cms.blog" ShowCurrentItemAsLink="true" /></strong> by 
<strong><%# BlogFunctions.GetUserFullName(Eval("NodeOwner")) %></strong> on <span class="BlogPDate"><%# Eval("BlogPostDate") %></span> | with <a href="<%# GetDocumentUrl() %>#comments"><%# BlogFunctions.GetBlogCommentsCount(Eval("DocumentID"), Eval("NodeAliasPath")) %> comments</a></div>
<br/>
<br/>