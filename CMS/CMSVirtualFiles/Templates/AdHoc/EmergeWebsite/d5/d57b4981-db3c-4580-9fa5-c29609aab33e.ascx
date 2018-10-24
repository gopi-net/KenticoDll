<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<cms:CMSWebPartZone ZoneID="zoneHeader" runat="server" />
<section class="cheerCardsWrapper">
    <!--Included home-->
    <section class="contentWrapper">
    <section class="container cheerCardsForm">
      <cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
     </section>
    </section>
 </section>