﻿<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Top info -->
<div class="zoneTopInfo">
	<cms:CMSWebPartZone ZoneID="zoneTopInfo" runat="server" />
</div>
<!-- Content container -->
<div class="mainDivLeftMenu">
	<!-- Logo -->
	<div class="zoneLogo">
		<cms:CMSWebPartZone ZoneID="zoneLogo" runat="server" />
	</div>
	<!-- Top zone -->
	<div class="zoneTop">
		<cms:CMSWebPartZone ZoneID="zoneTop" runat="server" />
	</div>
	<div style="clear:both;line-height: 1px; height: 1px;" ></div>
	<!-- Menu -->
	<div class="zoneMenu" style="float:left;">
		<cms:CMSWebPartZone ZoneID="zoneMenu" runat="server" />
	</div>
	<!-- Content -->
	<div class="zoneMainContent" style="float:left;">
		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
	<div style="clear:both;line-height: 1px; height: 1px;" ></div>
	<!-- Bottom zone -->
	<div class="zoneBottom">
		<cms:CMSWebPartZone ZoneID="zoneBottom" runat="server" />
	</div>
	<!-- Footer zone -->
	<div class="zoneFooter">
		<cms:CMSWebPartZone ZoneID="zoneFooter" runat="server" />
	</div>
</div>