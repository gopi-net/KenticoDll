<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Top info -->
<div class="zoneTopInfo">
  <div class="inner">
    <div class="left"><cms:CMSWebPartZone ZoneID="zTI" runat="server" /></div>
    <div class="clear"></div>
  </div>
</div>
<div class="zoneTopWrap">
  <div class="inner">
    <!-- Logo -->
    <div class="zoneLogo">
      <cms:CMSWebPartZone ZoneID="zL" runat="server" />
    </div>
    <div class="clear"></div>
    <!-- Menu -->
    <div class="zoneMenu">
      <cms:CMSWebPartZone ZoneID="zM" runat="server" />
      <div class="clear"></div>
    </div>
  </div>
</div>
<div class="zoneMenuWrap">
  <div class="inner">
    <!-- Breadcrumbs -->
    <div class="zoneBreadcrumbs">
      <cms:CMSWebPartZone ZoneID="zB" runat="server" />
    </div>
    <div class="clear"></div>
  </div>
</div>
<!-- Content -->
<div class="zoneMainContent">
  <cms:CMSWebPartZone ZoneID="zC" runat="server" />
  <div class="clear"></div>
</div>
<!-- Footer zone -->
<div class="zoneFooter">
  <div class="inner">
    <cms:CMSWebPartZone ZoneID="zF" runat="server" />
    <div class="clear"></div>
  </div>
</div>