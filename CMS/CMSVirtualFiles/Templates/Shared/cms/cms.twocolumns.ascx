﻿<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="twoCols">
	<!-- Left zone -->
	<div class="zoneLeft" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />
	</div>
	<!-- Right zone -->
	<div class="zoneRight" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>