<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><li class=' <%# Convert.ToString(Eval("DocumentMenuClass"))%>'>
  <a href="<%# Convert.ToString(Eval("NodeAliasPath"))%>">
    <%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("DocumentMenuCaption"))!=string.Empty?Convert.ToString(Eval("DocumentMenuCaption")):Convert.ToString(Eval("DocumentName"))) %>
  </a>
</li>