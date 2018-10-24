<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="TopContributors">
  <div style="float:left;" class="UserName">
    <%#IfEmpty(GetUserProfileURL(Eval("UserName")), "", "<a href='" + HTMLEncode(GetUserProfileURL(Eval("UserName"))) + "'>")%>
      <%# HTMLEncode(GetNotEmpty("UserNickname;UserName")) %>
    <%# IfEmpty(GetUserProfileURL(Eval("UserName")), "", "</a>") %>
  </div>
  <div style="float:right;" class="PostCount"><%# ValidationHelper.GetInteger(Eval("UserForumPosts"), 0) %></div>
  <div style="clear:both;"></div>
</div>