<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><CMS_News>
  <NewsID><%# EvalCDATA("NewsID") %></NewsID>
  <NewsTitle><%# EvalCDATA("NewsTitle") %></NewsTitle>
  <NewsReleaseDate><%# EvalCDATA("NewsReleaseDate") %></NewsReleaseDate>
  <NewsSummary><%# EvalCDATA("NewsSummary") %></NewsSummary>
  <NewsText><%# EvalCDATA("NewsText") %></NewsText>
</CMS_News>

