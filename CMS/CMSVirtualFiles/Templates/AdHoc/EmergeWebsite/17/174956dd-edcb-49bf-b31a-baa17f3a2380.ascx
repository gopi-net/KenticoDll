<%@ Control Language="C#" ClassName="Simple" Inherits="CMS.PortalEngine.Web.UI.CMSAbstractLayout" %> 
<header class="zone-header">
  <div class="container">
    <cms:CMSWebPartZone ZoneID="zoneHeaderTopLeft" runat="server" />
      <div class="pull-right hidden-xs">
            <cms:CMSWebPartZone ZoneID="zoneHeaderTopRight" runat="server" />
      </div>
  </div>
  
  <nav class="navbar navbar-default main-nav">
<div class="container">
          
            <!-- Brand and toggle get grouped for better mobile display -->
          <div class="clearfix">
          <div class="pull-right visible-xs">
            <cms:CMSWebPartZone ZoneID="zoneHeaderTopRight" runat="server" />
          </div>
            <cms:CMSWebPartZone ZoneID="zoneToggleNavbar" runat="server" />
             </div>       
            <div class="zone-header-menu">
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                  <cms:CMSWebPartZone ZoneID="zoneHeaderMenu" runat="server" />
                </div>
            </div>
          
          </div>
    </nav> 
          
</header>
<section class="zone-content">
  <cms:CMSWebPartZone ZoneID="zoneContent" runat="server" />
</section>
<footer class="zone-footer">
  <div class="container">
    <div class="footer-left">
      <cms:CMSWebPartZone ZoneID="zoneFooterLeft" runat="server" />
    </div>
    <div class="footer-right">
      <cms:CMSWebPartZone ZoneID="zoneFooterRight" runat="server" />
    </div>
  </div>
</footer>
<script> 
  $( document ).ready(function() {
  jQuery('#scrollbox3').enscroll({
     showOnHover: true,
     verticalTrackClass: 'track3',
     verticalHandleClass: 'handle3'
 });
  $( 'ul.nav.nav-tabs  a' ).click( function ( e ) {
        e.preventDefault();
        $( this ).tab( 'show' );
    } );
  ;
    //Search Box Mobile animate
    if ($(window).width() > 768)  {
      //$('.box-search').click(function () {
         // $('.searchBox').animate({ height: 'toggle' }, "slow")
         //   $(this).animate({width:0},1000);
            //   $(this).preventDefault();
            // $(this).find('input')[0].focus();
      //});
     }
    //Search Box Mobile animate
  });
  $(window).load(function() {
 // executes when complete page is fully loaded, including all frames, objects and images
 //     alert("window is loaded");
  //    fakewaffle.responsiveTabs( [ 'xs', 'sm' ] );
     
});
</script>