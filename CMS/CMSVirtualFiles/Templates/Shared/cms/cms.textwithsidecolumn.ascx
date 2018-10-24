<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="textCol">
	<!-- Content -->
	<div class="zoneContent" style="float: left;">
  		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
	<!-- Right zone -->
	<div class="zoneRight" style="float: left; width: 180px;">
		<cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>