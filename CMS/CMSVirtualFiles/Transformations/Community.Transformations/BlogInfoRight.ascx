<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="blogRightList">
<%# IfEmpty(Eval("BlogTeaser"),"","<div class=\"blogTeaser\"><img src=\""+GetFileUrl("BlogTeaser")+"?width=60\" /></div>") %>
<p><%# Eval("BlogSideColumnText") %></p>
<div class="clear">&nbsp;</div>
</div>