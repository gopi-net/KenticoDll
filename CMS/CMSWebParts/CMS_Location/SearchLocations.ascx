<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchLocations.ascx.cs" Inherits="CMSWebParts_CMS_Location_SearchLocations" %>
<%@ Register Assembly="Bluespire.Emerge.Web" TagPrefix="EmergeControls"
    Namespace="Bluespire.Emerge.Web.Controls" %>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
<asp:Panel ID="SearchPanel" runat="server" DefaultButton="btnSearch">

    <div class="mainContent clearfix row">

        <div class="col-md-3 col-sm-4 col-xs-12 leftContent">
            <div class="search">
                <h3>search:</h3>
                <div>
                    <div class="row">

                    </div>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <label>
                                        <cms:LocalizedLiteral ID="AddressLit" runat="server" ResourceString="Emerge.LOC.AddressLabel"></cms:LocalizedLiteral>
                                    </label>

                                    <cms:CMSTextBox runat="server" ID="Address"  MaxLength="100"></cms:CMSTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <cms:LocalizedLiteral ID="CityLit" runat="server" ResourceString="Emerge.LOC.CityLabel"></cms:LocalizedLiteral>
                                    </label>


                                    <cms:CMSTextBox runat="server" ID="City"  MaxLength="100"></cms:CMSTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <cms:LocalizedLiteral ID="StateLit" runat="server" ResourceString="Emerge.LOC.StateLabel"></cms:LocalizedLiteral>
                                    </label>

                                    <cms:LocalizedDropDownList runat="server" ID="StateID" DataTextField="StateCode" DataValueField="ItemId"></cms:LocalizedDropDownList>
                                    <cms:CMSQueryDataSource ID="StateID_DataSource" runat="server"
                                        QueryName="customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetStates" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <cms:LocalizedLiteral ID="ZipLit" runat="server" ResourceString="Emerge.LOC.ZipLabel"></cms:LocalizedLiteral>
                                    </label>


                                    <cms:CMSTextBox runat="server" ID="Zipcode"  MaxLength="5"></cms:CMSTextBox>
									 <asp:RegularExpressionValidator Display="Dynamic" ID="revZip" runat="server" CssClass="ErrorMessage" ControlToValidate="Zipcode" ValidationExpression="[0-9]*" ErrorMessage="Invalid zip code" SetFocusOnError="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        <cms:LocalizedLiteral ID="CategoryLit" runat="server" ResourceString="Emerge.LOC.CategoryLabel"></cms:LocalizedLiteral>
                                    </label>

                                    <cms:LocalizedDropDownList runat="server" ID="CategoryID" DataTextField="CategoryName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                                    <cms:CMSQueryDataSource ID="CategoryID_DataSource" runat="server"
                                        QueryName="customtable.Emerge_{0}_LOC_Locations.Query_LOC_GetCategories" />
                                </td>
                            </tr>
                            <tr>
                                <td class="btnWrapper">
                                    <cms:LocalizedLinkButton ID="btnSearch" runat="server" ResourceString="Emerge.LOC.Button.Search"></cms:LocalizedLinkButton>
                                    <cms:LocalizedLinkButton ID="btnClear" runat="server" ResourceString="Emerge.LOC.Button.Clear"></cms:LocalizedLinkButton>
                                   
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="result">
                <h3>Results:</h3>
                <div id="scrollbox3">
                    <dl>
                        <cms:CMSRepeater runat="server" ID="LocationRepeater">

                            <ItemTemplate>
                            </ItemTemplate>

                        </cms:CMSRepeater>

                    </dl>
                </div>
            </div>
        </div>
        <div class="col-md-9 col-sm-8 col-xs-12 rightContent">
            <div id="map_canvas"  class="map_canvas_class" >
            </div>
        </div>
    </div>

    


    <%--  <EmergeControls:EmergeTextBox runat="server" ID="test" MappedToCustomTableColumns="Address,City"></EmergeControls:EmergeTextBox>--%>

<script type="text/javascript">
  
    jQuery("#map_canvas").attr("style", "height:800px");
    jQuery("#map_canvas").attr("style", "width:100%");
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
    //script for map responsive
    var width = $(window).width();
    if (width <= 768) {
        jQuery("#map_canvas").attr("style", "height:400px");
        jQuery("#map_canvas").attr("style", "width:100%");
     //   alert('ipad')
    } else {
        jQuery("#map_canvas").attr("style", "height:400px");
        jQuery("#map_canvas").attr("style", "width:100%");
    }
   
        
   
</script>
</asp:Panel>

<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>