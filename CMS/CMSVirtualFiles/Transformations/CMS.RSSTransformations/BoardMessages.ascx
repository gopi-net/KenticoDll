<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("MessageGUID") %></guid>
  <title><![CDATA[<%# TextHelper.LimitLength(ValidationHelper.GetString(EvalCDATA("MessageText",false),string.Empty),100) %>]]></title>
  <description><![CDATA[<strong><%# EvalCDATA("MessageUserName",false) %></strong><br /><%# EvalCDATA("MessageText",false) %>]]></description>
  <pubDate><%# GetRSSDateTime(Eval("MessageInserted")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetMessageBoardUrlForFeed(EvalInteger("BoardDocumentID")), EvalInteger("BoardSiteID")) %>]]></link>
</item>