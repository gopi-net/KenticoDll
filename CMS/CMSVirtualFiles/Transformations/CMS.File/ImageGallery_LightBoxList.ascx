﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><%#IfEmpty(Eval("FileAttachment"), "no image", "<a href='" + GetFileUrl("FileAttachment") + "' class='ImgLightBox'><img alt='" + Eval("FileDescription") + "' src='" + GetFileUrl("FileAttachment") + "?maxsidesize=800' /></a>")%>


