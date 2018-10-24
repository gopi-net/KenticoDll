<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div class="topBlock">
	<cms:CMSWebPartZone ZoneID="zoneTop" runat="server" />
</div>
<div class="mainBlock">
	<cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
</div>