<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><entry>
  <title><%# EvalCDATA("ArticleTitle") %></title>
  <link href="<%# GetAbsoluteUrl(GetDocumentUrlForFeed()) %>"/>
  <id>urn:uuid:<%# Eval("NodeGUID") %></id>
  <published><%# GetAtomDateTime(Eval("DocumentCreatedWhen")) %></published>
  <updated><%# GetAtomDateTime(Eval("DocumentModifiedWhen")) %></updated>
  <author>
    <name><%# Eval("NodeOwnerFullName") %></name>
  </author>
  <summary type="html"><%# EvalCDATA("ArticleText") %></summary>
</entry>