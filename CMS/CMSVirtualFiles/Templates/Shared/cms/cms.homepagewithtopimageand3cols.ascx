<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<div class="homeTopImage3cols">
  <!-- Top image -->
  <div class="zoneTop">
    <cms:CMSWebPartZone ZoneID="zoneTop" ID="zoneTop" runat="server" />
  </div>
  <!-- Left column -->
  <div class="zoneLeft" style="float: left;">
    <cms:CMSWebPartZone ZoneID="zoneLeft" ID="zoneLeft" runat="server" />
  </div>
  <!-- Center column -->
  <div class="zoneCenter" style="float: left;">
    <cms:CMSWebPartZone ZoneID="zoneCenter" ID="zoneCenter" runat="server" />
  </div>
  <!-- Right column -->
  <div class="zoneRight" style="float: left;">
    <asp:Panel runat="server" id="pnlRight">
        <cms:CMSWebPartZone ZoneID="zoneRight" ID="zoneRight" runat="server" />
    </asp:Panel>
  </div>
  <div style="clear:both;line-height:0px;height:0px;" ></div>
</div>
                
<script runat="server">
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (zoneRight.WebParts.Count >= 2)
        {
            pnlRight.CssClass = "OverLimit";
        }
    }
</script>
                  