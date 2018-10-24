<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<div class="zone-banner">
  <cms:CMSWebPartZone ZoneID="zoneBanner" runat="server" />
</div>
<div class="container zone-page-content ">
  <cms:CMSWebPartZone ZoneID="zoneBreadCrumb" runat="server" />
  <div class="page-content-inner page-9x3">
    <article class="col-md-12 page-content">
      <cms:CMSWebPartZone ZoneID="zonePageContent" runat="server" />
    </article>
   
  </div>
</div>