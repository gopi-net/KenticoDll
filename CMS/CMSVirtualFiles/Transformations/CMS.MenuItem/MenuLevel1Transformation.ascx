<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><li class="dropdown">
  <a href="<%# Convert.ToString(Eval("NodeAliasPath"))%>" class="dropdown-toggle" id="dropdownMenu1" data-toggle="dropdown">
    <%# HTMLHelper.HTMLEncode(Convert.ToString(Eval("DocumentMenuCaption"))!=string.Empty?Convert.ToString(Eval("DocumentMenuCaption")):Convert.ToString(Eval("DocumentName"))) %>
  </a>
    <cms:CmsRepeater SelectOnlyPublished="true" MaxRelativeLevel="1" Path='<%# Convert.ToString(Eval("NodeAliasPath"))+"/%"%>' DelayedLoading="true" NestedControlsID="menuContentPages" id="innerMenu" runat="server" TransformationName="CMS.MenuItem.MenuLevel2Transformation" WhereCondition="DocumentMenuItemHideInNavigation=0">
    <HeaderTemplate>
      <div class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
        <div class="row">
    </HeaderTemplate>
    <FooterTemplate>
        </div>
      </div>
    </FooterTemplate>
  </cms:CmsRepeater>
</li>