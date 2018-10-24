<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="gallery">
<h3><%# Eval("DocumentName",true) %></h3>
<a href="<%# GetDocumentUrl() %>"><img class="teaser" src="<%# GetFileUrl("MenuItemTeaserImage") %>?height=150&amp;width=200" alt="<%# Eval("DocumentName",true) %>" /></a>
</div>