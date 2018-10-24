<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("NodeGUID") %></guid>
  <title><%# EvalCDATA("BlogPostTitle") %></title>
  <description><%# EvalCDATA("BlogPostSummary") %></description>
  <pubDate><%# GetRSSDateTime(Eval("BlogPostDate")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetDocumentUrlForFeed()) %>]]></link>     	
</item>