<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="col-sm-4">
  <ul class="drpdwn-menu">
    <li class='dropdown-header <%# Convert.ToString(Eval("DocumentMenuClass"))%>'>
      <%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("DocumentMenuCaption"))!=string.Empty?Convert.ToString(Eval("DocumentMenuCaption")):Convert.ToString(Eval("DocumentName"))) %>
    </li>
    <cms:CMSRepeater TransformationName="CMS.MenuItem.MenuLevel3Transformation" Path='<%# Convert.ToString(Eval("NodeAliasPath"))+"/%"%>' ID="menuContentPages" runat="server" WhereCondition="Published=1 And DocumentMenuItemHideInNavigation=0">
    </cms:CMSRepeater>
  </ul>
</div>
  