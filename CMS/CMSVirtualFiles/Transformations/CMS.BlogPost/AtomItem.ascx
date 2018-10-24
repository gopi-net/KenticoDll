<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><entry>
  <title><%# EvalCDATA("BlogPostTitle") %></title>
  <link href="<%# GetAbsoluteUrl(GetDocumentUrlForFeed()) %>"/>
  <id>urn:uuid:<%# Eval("NodeGUID") %></id>
  <published><%# GetAtomDateTime(Eval("BlogPostDate")) %></published>
  <updated><%# GetAtomDateTime(Eval("DocumentModifiedWhen")) %></updated>
  <author>
    <name><%# Eval("NodeOwnerFullName") %></name>
  </author>
  <summary type="html"><%# EvalCDATA("BlogPostSummary") %></summary>
</entry>