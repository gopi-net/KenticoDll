﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="NewsLatest">
  <div class="NewsTitle">
    <%# Eval("Title") %>
  </div>
  <hr size="1" />
  <div class="NewsSummary">
    <%# Eval("Description") %>
  </div>
  <a href="<%# Eval("Link") %>" class="LinkMore">more</a>
</div>
