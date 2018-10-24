<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="headerTextLeftMenu">
	<!-- Content -->
	<div class="zoneHeader" style="float:right;">
		<cms:CMSWebPartZone ZoneID="zoneHeader" runat="server" />
	</div>
	<!-- Left zone -->
	<div class="zoneLeft" style="float:left;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />
	</div>
	<!-- Content -->
	<div class="zoneContent" style="float: right;">
		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
	<div style="clear: both;"></div>
</div>