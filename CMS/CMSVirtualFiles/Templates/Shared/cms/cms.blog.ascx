<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<!-- Container -->
<div class="blogDetail">
	<!-- Top zone -->
	<div class="zoneTop">
		<cms:CMSWebPartZone ZoneID="zoneTop" runat="server" />      
	</div>
	<!-- Left zone -->
	<div class="zoneLeft" style="float: left;">
		<cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />      
	</div>
	<!-- Right zone -->
	<div class="zoneRight" style="float: right;">
		<cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />      
	</div>
</div>