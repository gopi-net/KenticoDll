<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><cms:usermenucontainer runat="server" ID="userMenuElem" MenuID="userContextMenu" Parameter='<%# Eval("UserID").ToString() %>' ContextMenuCssClass="UserContextMenu" >
<div class="member">
<div class="avatar">
<a href="<%# HTMLEncode(GetMemberProfileUrl(Eval("UserName"))) %>">
<%# GetUserAvatarImage(52, HTMLEncode(TrimSitePrefix(GetNotEmpty("UserNickname;UserName")))) %>
</a>
</div>
<div class="memberInfo">
<h3><a href="<%# HTMLEncode(GetMemberProfileUrl(Eval("UserName"))) %>" title="<%# HTMLEncode(TrimSitePrefix(GetNotEmpty("UserNickname;UserName"))) %>"><%# HTMLEncode(TrimSitePrefix(GetNotEmpty("UserNickname;UserName"))) %></a></h3>
<p>
Gender: <%# GetGender(Eval("UserGender")) %><br />
Age: <%# GetAge(Eval("UserDateOfBirth"), "N/A") %>
</p>
</div>
<div class="clear">&nbsp;</div>
</div>
</cms:usermenucontainer>