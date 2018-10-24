<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<div class="zone-banner">
  <cms:CMSWebPartZone ZoneID="zoneBanner" runat="server" />
</div>
<div class="container zone-page-content">
  <article class="col-md-8 ">
    <cms:CMSWebPartZone ZoneID="zonePageContent" runat="server" />
  </article>
  <aside class="col-md-4 zone-right">
    <cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />
  </aside>
</div>