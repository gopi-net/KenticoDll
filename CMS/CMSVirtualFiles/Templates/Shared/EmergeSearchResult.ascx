<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<cms:CMSContent runat="server" id="cntBanner" PagePlaceholderID="pageplaceholderBanner">
  <cms:CMSWebPartZone ZoneID="zoneBanner" runat="server" />
</cms:CMSContent>
<cms:CMSContent runat="server" id="cntMainContent" PagePlaceholderID="pageplaceholderMainContent">
     <section class="contentInner findPhysician">
       <section id="DefaultSearch">
        <cms:CMSWebPartZone ZoneID="zoneDefaultSearchResult" runat="server" />
         </section>
       <section id="CustomTableSearch">
        <cms:CMSWebPartZone ZoneID="zoneCustomTableSearchResult" runat="server" />
         </section>
    </section>
</cms:CMSContent>