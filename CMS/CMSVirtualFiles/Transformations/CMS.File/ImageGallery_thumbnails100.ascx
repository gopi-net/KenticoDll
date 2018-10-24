<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><a href="?imagepath=<%# System.Web.HttpUtility.UrlEncode(DataBinder.Eval(Container, "DataItem.NodeAliasPath").ToString()) %>">

<%#IfEmpty(Eval("FileAttachment"), "no image", "<img alt='" + Eval("FileName", true) + "' src='" + GetFileUrl("FileAttachment") + "?maxsidesize=100' border='0' />")%>

</a>