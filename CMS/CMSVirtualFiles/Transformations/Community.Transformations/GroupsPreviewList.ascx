<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><cms:groupmenucontainer runat="server" ID="groupMenuElem" MenuID="groupContextMenu" Parameter='<%# Eval("GroupID").ToString() %>' ContextMenuCssClass="UserContextMenu" >
<div class="groupSmall">
<div class="avatarSmall">
<a href="<%# GetGroupProfileUrl(Eval("GroupName",true)) %>" title="<%# Eval("GroupDisplayName",true) %>">
<%# GetGroupAvatarImage(30, Eval("GroupDisplayName",true)) %>
</a>
</div>
<div class="groupInfoSmall">
<h3><a href="<%# GetGroupProfileUrl(Eval("GroupName",true)) %>" title="<%# Eval("GroupDisplayName",true) %>"><%# Eval("GroupDisplayName",true) %></a></h3>
<%# LimitLength(Eval("GroupDescription",true), 127, "...") %>
</div>
</div>
</cms:groupmenucontainer>