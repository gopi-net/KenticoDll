<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("EventName", true) %></h1>
<table width="100%">
<tr valign="top">
<td width="150">
Event location:
</td>
<td>
<%# Eval("EventLocation") %>
</td>
</tr>
<tr valign="top">
<td>
Event date and time:
</td>
<td>
<%# Eval("EventDate") %>
</td>
</tr>
<tr valign="top">
<td colspan="2">
<%# Eval("EventSummary") %>
</td>
</tr>
<tr valign="top">
<td colspan="2">
<%# Eval("EventDetails") %>
</td>
</tr>
</table>