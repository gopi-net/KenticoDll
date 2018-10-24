<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div>
<a target="_blank" href="<%# GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID")) %>">
<img style="border: none;" src="<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true, GetAttachmentIcon(Eval("AttachmentExtension")), GetAbsoluteUrl(GetAttachmentUrl(Eval("AttachmentName"), Eval("NodeAliasPath")), Eval<int>("AttachmentSiteID"))) %>?maxsidesize=150" alt="<%# Eval("AttachmentName", true) %>" />
</a>
<%# IfCompare(ImageHelper.IsImage((string)Eval("AttachmentExtension")), true, "<br />" + ResHelper.GetString("attach.openfile"), "") %>
<br />
<%# Eval("AttachmentName",true) %>
<br />
</div>