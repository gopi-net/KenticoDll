<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="BlogPTitle"><strong><a href="<%# GetDocumentUrl() %>">
  <%# Eval("BlogName", true) %></a></strong>
</div>
<div class="BlogPBody TextContent">
  <%# Eval("BlogDescription") %>
</div>
<br />
