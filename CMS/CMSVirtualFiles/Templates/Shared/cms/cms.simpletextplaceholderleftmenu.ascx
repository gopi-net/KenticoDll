﻿<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="textPlaceholderLeftMenu">
	<!-- Left zone -->
	<div class="zoneLeft" style="float: left; width: 180px;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />
	</div>
	<!-- Content -->
	<div class="zoneContent" style="float: right;">
		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>