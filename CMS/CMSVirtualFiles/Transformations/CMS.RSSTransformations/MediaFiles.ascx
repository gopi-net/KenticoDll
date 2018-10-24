<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><%@ Register Src="~/CMSModules/MediaLibrary/Controls/LiveControls/MediaFilePreview.ascx" TagName="MediaFilePreview" TagPrefix="cc1" %>
<item>
  <guid isPermaLink="false"><%# Eval("FileGUID") %></guid>
  <title><%# EvalCDATA("FileTitle") %></title>
  <description><![CDATA[<cc1:MediaFilePreview ID="filePreview" runat="server" maxsidesize="200" width="200" height="200" DisplayActiveContent="false" /><br /><%# EvalCDATA("FileDescription",false) %>]]></description>
  <pubDate><%# GetRSSDateTime(Eval("FileCreatedWhen")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetMediaFileUrlForFeed(Eval("FileGUID"), Eval("FileName")), EvalInteger("FileSiteID")) %>]]></link>
</item>