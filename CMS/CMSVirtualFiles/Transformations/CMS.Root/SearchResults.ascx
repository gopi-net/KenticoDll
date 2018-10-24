<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="SearchResult">
  <div class="ResultTitle">
    <a href="<%# GetDocumentUrl()%>"><%# IfEmpty(Eval("SearchResultName",true), "/", Eval("SearchResultName",true)) %></a>
  </div>
  <div class="ResultPath">
    Path: <%# Eval("DocumentNamePath",true) %><br />
  </div>
</div>
