<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<cms:CMSWebPartZone ZoneID="zoneTop" runat="server" />      
<div style="width: 100%">
<table border="0" width="100%">
  <tr valign="top">
    <td width="20%">
      <cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />      
    </td>
    <td width="80%">
      <cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />      
    </td>
  </tr>
</table>
</div>
<cms:CMSWebPartZone ZoneID="zoneBottom" runat="server" />      