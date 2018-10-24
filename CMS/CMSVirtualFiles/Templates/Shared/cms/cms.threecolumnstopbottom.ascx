<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div style="width: 100%">
<table border="0" width="100%">
  <tr>
    <td colspan="3">
      <cms:CMSWebPartZone ZoneID="zoneTop" runat="server" />      
    </td>
  </tr>
  <tr valign="top">
    <td>
      <cms:CMSWebPartZone ZoneID="zoneLeft" runat="server" />      
    </td>
    <td>
      <cms:CMSWebPartZone ZoneID="zoneCenter" runat="server" />      
    </td>
    <td>
      <cms:CMSWebPartZone ZoneID="zoneRight" runat="server" />      
    </td>
  </tr>
  <tr>
    <td colspan="3">
      <cms:CMSWebPartZone ZoneID="zoneBottom" runat="server" />      
    </td>
  </tr>
</table>
</div>