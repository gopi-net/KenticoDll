<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="textHeader">
	<!-- Header zone -->
	<div class="zoneHeader">
		<cms:CMSWebPartZone ZoneID="zoneHeader" runat="server" />
	</div>
	<!-- Content -->
	<div class="zoneContent">
		<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
	</div>
</div>