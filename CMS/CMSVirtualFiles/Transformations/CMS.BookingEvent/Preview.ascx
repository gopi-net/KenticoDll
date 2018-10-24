<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="Event">
<div class="Header">
  <a href="<%# GetDocumentUrl() %>"><%# Eval("EventName", true) %></a>
</div>
<div class="Body TextContent">
  <p><%# Eval("EventSummary") %></p>
  <strong>Location: </strong><%# Eval("EventLocation") %><br />
  <strong>Date: </strong><%# GetEventDateString(Eval("EventDate"),Eval("EventEndDate"),Eval<bool>("EventAllDay")) %>
</div>
</div>
<br />