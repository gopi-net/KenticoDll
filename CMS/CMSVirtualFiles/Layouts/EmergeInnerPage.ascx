<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<div class="content-wrapper">
    <div class="content-left hidden-xs hidden-sm">
      <cms:CMSWebPartZone ZoneID="zoneLeftContent" runat="server" />
    </div>
    <div class="content-main">
      <cms:CMSWebPartZone ZoneID="zoneMainHeader" runat="server" />
      <cms:CMSWebPartZone ZoneID="zoneMainContent" runat="server" />
    </div>
</div>