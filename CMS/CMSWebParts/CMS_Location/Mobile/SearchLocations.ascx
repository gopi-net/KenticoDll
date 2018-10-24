<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchLocations.ascx.cs" Inherits="CMSWebParts_CMS_Location_Mobile_SearchLocations" %>
<%@ Register Assembly="Bluespire.Emerge.Web" TagPrefix="EmergeControls"
    Namespace="Bluespire.Emerge.Web.Controls" %>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>






<asp:Panel ID="SearchPanel" runat="server" DefaultButton="btnSearch">

    <div class="container locationForm">

        <div class="formWrapper clearfix">

            <h2>Search: </h2>
            <label>
                <cms:LocalizedLiteral ID="AddressLit" runat="server" ResourceString="Emerge.LOC.AddressLabel"></cms:LocalizedLiteral>
                <cms:CMSTextBox runat="server" ID="Address" MaxLength="100"></cms:CMSTextBox>
            </label>
            <label>
                <cms:LocalizedLiteral ID="CityLit" runat="server" ResourceString="Emerge.LOC.CityLabel"></cms:LocalizedLiteral>
                <cms:CMSTextBox runat="server" ID="City" MaxLength="100"></cms:CMSTextBox>
            </label>
            <label>
                <cms:LocalizedLiteral ID="StateLit" runat="server" ResourceString="Emerge.LOC.StateLabel"></cms:LocalizedLiteral>
                <cms:LocalizedDropDownList runat="server" ID="StateID" DataTextField="StateCode" DataValueField="ItemId"></cms:LocalizedDropDownList>
                <cms:CMSQueryDataSource ID="StateID_DataSource" runat="server"
                    QueryName="customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetStates" />
            </label>
            <label>
                <cms:LocalizedLiteral ID="ZipLit" runat="server" ResourceString="Emerge.LOC.ZipLabel"></cms:LocalizedLiteral>
                <cms:CMSTextBox runat="server" ID="Zipcode" MaxLength="100"></cms:CMSTextBox>
            </label>
            <label>
                <cms:LocalizedLiteral ID="CategoryLit" runat="server" ResourceString="Emerge.LOC.CategoryLabel"></cms:LocalizedLiteral>
                <cms:LocalizedDropDownList runat="server" ID="CategoryID" DataTextField="CategoryName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                <cms:CMSQueryDataSource ID="CategoryID_DataSource" runat="server"
                    QueryName="customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetCategories" />
            </label>
        </div>

        <div class="btnWrapper">

            <cms:LocalizedLinkButton CssClass="btn btn-default" ID="btnSearch" runat="server" ResourceString="Emerge.LOC.Button.Search"></cms:LocalizedLinkButton>
            <cms:LocalizedLinkButton CssClass="btn btn-default" ID="btnClear" runat="server" ResourceString="Emerge.LOC.Button.Clear"></cms:LocalizedLinkButton>



        </div>

    </div>



    <div class="container resultWrapper">

        <div class=" clearfix" id="result">
            <div id="scrollbox3">
                <h2>Search Result: </h2>
                <dl>



                    <cms:CMSRepeater runat="server" ID="LocationRepeater">

                        <ItemTemplate>
                        </ItemTemplate>

                    </cms:CMSRepeater>

                </dl>
            </div>
        </div>
        <div class="btnWrapper">

            <cms:LocalizedLinkButton CssClass="btn btn-default" ID="btnBackToSearch" runat="server" ResourceString="Emerge.LOC.Button.BackToSearch"></cms:LocalizedLinkButton>
        </div>
    </div>


    <div class="container mapWrapper">
        <div class="mapImg clearfix map_canvas" id="map_canvas" >
        </div>

        <div class="btnWrapper">
            <cms:LocalizedLinkButton CssClass="btn btn-default" ID="btnBackToResult" OnClientClick="slideBox('.mapWrapper', '.resultWrapper','left');return false;" runat="server" ResourceString="Emerge.LOC.Button.BackToSearchResult"></cms:LocalizedLinkButton>

        </div>
    </div>


    <script type="text/javascript">

        jQuery(document).ready(function ($) {
      
            $('#scrollbox3').enscroll({
                showOnHover: true,
                verticalTrackClass: 'track3',
                verticalHandleClass: 'handle3'
            });


       

        });

    

        function slideBox (ele1, ele2, direction) {
            //e.preventDefault();
       
            //alert('slidebox called');
            direction = (typeof direction === "undefined") ? "right" : direction;
            if (direction == 'left') {
            
                jQuery(ele1).animate({ "left": "-110%" }, "slow");
                jQuery(ele2).animate({ "left": "0" }, "slow");
            } else {
                jQuery(ele1).animate({ "left": "0" }, "slow");
                jQuery(ele2).animate({ "left": "110%" }, "slow");
            }

        };

        var gmarkers = [];  
        mapinitialize();
        function mapinitialize() {
            codeAddress(0,0);
        }

        function codeAddress(lang,lat) 
        {
            gmarkers = []; 

            document.getElementById("map_canvas").innerHTML ="";

 
            var markerBounds = new google.maps.LatLngBounds();
            var locations = <%#GetJSONLocations()%>;
        if(locations.length>0)
        {
            var add = locations[0];
           
            lang=add[3];
            lat=add[4];

            var options = {
                center: new google.maps.LatLng(lat, lang),
                mapTypeControl: true,
                streetViewControl:true,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                zoom: 16
            };
            
            var map = new google.maps.Map(document.getElementById("map_canvas"), options);
            addMarkers(map,locations,markerBounds);

            if (locations.length == 1)
            {
                var listener = google.maps.event.addListener(map, "idle", function() { 
                    map.setZoom(16); 
                    google.maps.event.removeListener(listener); 
                });
            }
        }
    }

  
    function addMarkers(map,locations,markerBounds)
    {
       
        for (var i = 0; i < locations.length; i++)
        {	    
            var location = locations[i];
        
            var myLatLng = new google.maps.LatLng(location[2], location[3]);
            var marker = new google.maps.Marker({
                position: myLatLng,
                map: map,              
                title: location[4],
                zIndex: location[1],
                info:location[5],               
                icon:location[6] 
            });
         
            markerBounds.extend(myLatLng);
            map.fitBounds(markerBounds);
           
          
            var infowindow = new google.maps.InfoWindow();
          
            google.maps.event.addListener(marker, 'click', function()
            {      
                infowindow.setContent(this.info);
                infowindow.open(map, this);     
            });

            gmarkers[i] = new Array(2);

            gmarkers[i][0] = location[0];
            gmarkers[i][1] = marker;
         
		
        }
    
	 
    }
	 

    function ShowInfoWindow(ItemID) 
    {
        slideBox('.resultWrapper', '.mapWrapper', 'left');
        var index = 0;
        for (var i = 0; i < gmarkers.length; i++) 
        {
            if (gmarkers[i][0].toString() == ItemID.toString())
            {
                index = i;
                break;
            }
        }
    
        google.maps.event.trigger(gmarkers[index][1], "click");
    }
 
    function AnimateToResultPanel() {

        slideBox('.locationForm', '.resultWrapper', 'left');
    }
   
</script>

</asp:Panel>







<%--  <EmergeControls:EmergeTextBox runat="server" ID="test" MappedToCustomTableColumns="Address,City"></EmergeControls:EmergeTextBox>--%>



<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage"  ID="plcMess" BasicStyles="true" runat="server" />
</div>