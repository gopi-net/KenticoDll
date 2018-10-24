<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><a target="_blank" href="<%# GetFileUrl("FileAttachment") %>">
<%# IfImage("FileAttachment", GetImage("FileAttachment", 400, 400, 400, Eval("FileDescription")), "") %>
<br /><%# Eval("FileName",true) %></a><br />