<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div class="topHome">
  <div class="padding">
    <cms:CMSWebPartZone runat="server" ZoneID="zT" />
  </div>
</div>
<!-- Container -->
<div class="twoColumnsLeft innerContent">
  <div class="inner">
    <div class="left">
      <div class="padding">
        <cms:CMSWebPartZone runat="server" ZoneID="zL" />
      </div>
    </div>
    <div class="center">
      <div class="padding">
        <cms:CMSWebPartZone runat="server" ZoneID="zM" />
      </div>
    </div>
  </div>
</div>