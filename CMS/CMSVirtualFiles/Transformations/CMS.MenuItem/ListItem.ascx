<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>  <div style="font-weight:bold; padding-top: 3px; padding-bottom: 3px;">
    <span style="color:black"><%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("MenuItemName"))) %>
    </span>
  </div>

