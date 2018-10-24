<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("PostGUID") %></guid>
  <title><%# EvalCDATA("PostSubject") %></title>
  <description><![CDATA[<strong><%# EvalCDATA("PostUserName",false) %></strong><br /><%# RemoveDynamicControls(CMS.Helpers.DiscussionMacroHelper.ResolveDiscussionMacros(ValidationHelper.GetString(EvalCDATA("PostText",false),""))) %>]]></description>
  <pubDate><%# GetRSSDateTime(Eval("PostTime")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl(GetForumPostUrlForFeed(Eval("PostIDPath"),EvalInteger("PostForumID")), EvalInteger("ForumSiteID")) %>]]></link>
</item>