<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="MostActiveThread">
  <div style="float:left;" class="Thread">
    <a href="<%# ForumFunctions.GetPostURL(Eval("PostIDPath"), Eval("PostForumID")) %>">
      <%# Eval("PostSubject", true) %>
    </a>
  </div>
  <div style="float:right;" class="PostTime"><%# Eval("PostThreadPosts") %></div>
  <div style="clear:both;"></div>
</div>