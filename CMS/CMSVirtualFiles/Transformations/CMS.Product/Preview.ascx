<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="ProductPreview">
<div class="ProductBox">
<div class="ProductTitle">
  <a href="<%# GetDocumentUrl() %>"><%# EvalText("SKUName", true) %></a>
</div>
<div class="ProductImage">
  <img src="<%# GetSKUImageUrl(100) %>" alt="<%# EvalText("SKUName", true) %>" />
</div>
<div class="ProductFooter">
  our price: <span class="ProductPrice"><%# GetSKUFormattedPrice() %></span><br />
  <a href="<%# GetDocumentUrl() %>" class="LinkMore">more</a>
</div>
</div>
</div>