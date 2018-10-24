<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="blogsHome">
<h4>
<a href="<%# ForumFunctions.GetPostURL("/Groups/Czech_Republic_fans/Forums",Eval("PostIDPath"), Eval("PostForumID")) %>"><%# Eval("PostSubject",true) %></a>
</h4>
<div>
<%#  StripTags(LimitLength(RemoveDynamicControls(RemoveDiscussionMacros(Eval("PostText"))), 300, "...")) %>
</div>
<div class="date">Posted on <strong><%# Eval("PostTime") %></strong></div>
</div>