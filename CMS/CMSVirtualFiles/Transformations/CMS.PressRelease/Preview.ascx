﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h2><a href="<%# GetDocumentUrl() %>"><%# GetDate("PressReleaseDate") %> - <%# Eval("PressReleaseTitle",true) %></a></h2>
<p><em><%# Eval("PressReleaseSummary") %></em></p>