<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<header class="zone-header">
  <div class="container">
    <cms:CMSWebPartZone ZoneID="zoneHeaderTopLeft" runat="server" />
    <div class="pull-right">
      <cms:CMSWebPartZone ZoneID="zoneHeaderTopRight" runat="server" />
    </div>
  </div>
  <div class="container zone-header-menu">
    <cms:CMSWebPartZone ZoneID="zoneHeaderMenu" runat="server" />
  </div>
</header>
<section class="zone-content">
  <cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
</section>
<footer class="zone-footer">
  <div class="container">
    <div class="pull-left">
      <cms:CMSWebPartZone ZoneID="zoneFooterLeft" runat="server" />
    </div>
    <div class="pull-right">
      <cms:CMSWebPartZone ZoneID="zoneFooterRight" runat="server" />
    </div>
  </div>
</footer>
<script> jQuery('#scrollbox3').enscroll({
     showOnHover: true,
     verticalTrackClass: 'track3',
     verticalHandleClass: 'handle3'
 });</script>