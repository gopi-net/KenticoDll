<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
    <guid isPermaLink="false">
        <%# ValidationHelper.GetString(Eval("CommentID"),string.Empty).PadLeft(10,'0') %></guid>
    <title>
        <![CDATA[<%# TextHelper.LimitLength(HttpUtility.HtmlDecode(ValidationHelper.GetString(EvalCDATA("CommentText",false),string.Empty)),100) %>]]></title>
    <description>
        <![CDATA[
        <strong>
            <%# EvalCDATA("CommentUserName",false) %></strong>
        <br /><%# EvalCDATA("CommentText",false) %>]]></description>
        <pubDate>
            <%# GetRSSDateTime(Eval("CommentDate")) %></pubDate>
        <link>
            <![CDATA[<%# GetAbsoluteUrl(GetBlogCommentUrlForFeed(EvalInteger("CommentPostDocumentID"))) %>]]></link>
    </item>