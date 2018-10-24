<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("PressReleaseTitle",true) %></h1>
<p>
<strong>Release date: <%# GetDate("PressReleaseDate") %></strong>
</p>
<p>
<strong><%# Eval("PressReleaseSummary") %></strong>
</p>
<p><%# Eval("PressReleaseText") %></p>
<p><%# Eval("PressReleaseAbout") %></p>
<p><%# Eval("PressReleaseTrademarks") %></p>
