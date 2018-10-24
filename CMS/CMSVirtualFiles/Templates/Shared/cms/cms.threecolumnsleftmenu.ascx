<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="threeColsLeftMenu">
	<!-- Left menu zone -->
	<div class="zoneLeftMenu" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneLeftMenu" runat="server" />
	</div>
	<!-- Left zone -->
	<div class="zoneLeft" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />
	</div>
	<!-- Center zone -->
	<div class="zoneCenter" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneCenter" runat="server" />
	</div>
	<!-- Right zone -->
	<div class="zoneRight" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>