<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("SKUGUID") %></guid>
  <title><%# EvalCDATA("SKUName") %></title>
  <description><%# EvalCDATA("SKUDescription") %></description>
  <pubDate><%# GetRSSDateTime(Eval("SKUCreated")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetProductUrlForFeed(Eval("SKUGUID"),Eval("SKUName"),Eval("SiteName")),EvalText("SiteName")) %>]]></link>
</item>