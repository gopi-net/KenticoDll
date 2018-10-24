<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div class="topHome">
  <div class="padding">
    <cms:CMSWebPartZone runat="server" ZoneID="zT" />
  </div>
</div>
<!-- Container -->
<div class="home">
  <div class="inner">
    <cms:CMSWebPartZone runat="server" ZoneID="zA" />
    <div class="left">
      <div class="padding">
        <cms:CMSWebPartZone runat="server" ZoneID="zL" />
      </div>
    </div>
    <div class="right">
      <div class="padding">
        <cms:CMSWebPartZone runat="server" ZoneID="zR" />
      </div>
    </div>
    <div class="responsiveClear"></div>
    <div class="center">
      <div class="padding">
        <cms:CMSWebPartZone runat="server" ZoneID="zM" />
      </div>
    </div>
  </div>
</div>