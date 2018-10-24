<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="Carrier">
<div class="Header">
  <a href="<%# GetDocumentUrl() %>"><%# Eval("JobName",true) %></a>
</div>
<div class="Body TextContent">
  <p><%# Eval("JobSummary") %></p>
  <strong>Location:</strong> <%# Eval("JobLocation") %>
</div>
</div>
<br />