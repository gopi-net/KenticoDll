<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("NodeGUID") %></guid>
  <title><%# EvalCDATA("ArticleTitle") %></title>
  <description><%# EvalCDATA("ArticleText") %></description>
  <pubDate><%# GetRSSDateTime(Eval("DocumentCreatedWhen")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetDocumentUrlForFeed()) %>]]></link>
</item>