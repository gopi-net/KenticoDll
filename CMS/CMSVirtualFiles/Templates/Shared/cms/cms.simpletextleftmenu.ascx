﻿<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="textLeftMenu">
	<!-- Left zone -->
	<div class="zoneLeft" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />
	</div>
	<!-- Content zone -->
	<div class="zoneContent" style="float: right;">
		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>