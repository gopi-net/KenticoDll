<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><item>
  <guid isPermaLink="false"><%# Eval("ItemGUID") %></guid>
  <title><%# EvalCDATA("ItemText") %></title>
  <description><%# EvalCDATA("ItemText") %></description>
  <pubDate><%# GetRSSDateTime(Eval("ItemCreatedWhen")) %></pubDate>
  <link><![CDATA[<%# GetAbsoluteUrl("~/Examples/Web-parts/Custom-tables/Custom-table-repeater.aspx?itemid=" + Eval("ItemID")) %>]]></link>
</item>

