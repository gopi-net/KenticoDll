<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<header class="navbar">
    <section class="container">
      <div class="navbar-header text-center clearfix">
       <cms:CMSWebPartZone ZoneID="zoneHeader" runat="server" />
      </div>
    </section>
</header>
<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
<footer>
    <section class="container">
      <div class="pull-left">
       <cms:CMSWebPartZone ZoneID="zoneFooter" runat="server" />
      </div>
    </section>
</footer>