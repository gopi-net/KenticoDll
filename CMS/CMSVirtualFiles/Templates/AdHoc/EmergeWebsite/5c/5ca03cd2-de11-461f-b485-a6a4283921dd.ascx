<%@ Control Language="C#" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.PortalEngine.Web.UI" Assembly="CMS.PortalEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine" Assembly="CMS.DocumentEngine" %>
<cms:CMSContent runat="server" id="cntBanner" PagePlaceholderID="pageplaceholderBanner">
  <section class="innerWrapper">
  <cms:CMSWebPartZone ZoneID="zoneBannerLogon" runat="server" />
  </section>  
</cms:CMSContent>
<cms:CMSContent runat="server" id="cntMainContent" PagePlaceholderID="pageplaceholderMainContent">
  <section class="contentInner vntCalendarWrap">
  <cms:CMSWebPartZone ZoneID="zoneVntLogon" runat="server" />
  </section>
</cms:CMSContent>