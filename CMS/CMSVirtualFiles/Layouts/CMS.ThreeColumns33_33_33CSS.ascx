<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div>
  <cms:CMSWebPartZone ZoneID="zoneA" runat="server" />      
</div>
<div>
  <div style="width: 33%; float: left;">
    <cms:CMSWebPartZone ZoneID="zoneB" runat="server" />      
  </div>
  <div style="width: 34%; float: left;">
    <div class="Content"><cms:CMSWebPartZone ZoneID="zoneC" runat="server" /></div>      
  </div>
  <div style="width: 33%; float: right;">
    <cms:CMSWebPartZone ZoneID="zoneD" runat="server" />      
  </div>
</div>
<div style="clear: both">
  <cms:CMSWebPartZone ZoneID="zoneE" runat="server" />
</div>