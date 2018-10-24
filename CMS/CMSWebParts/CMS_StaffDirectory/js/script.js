var staffLocations;
var locations=[];
var locationCounter='A';
function AddToMap(address)
{
    
    locations.push(['Place '+locationCounter,address,locationCounter]);
        locationCounter=nextChar(locationCounter);
    }

function nextChar(c) {
    return String.fromCharCode(c.charCodeAt(0) + 1);
}
function RenderStaffLocationMap()
{ 
    var marker, i;

    if (locations.length > 0) {
        //var locations = [
        //    ['Place A', '2430 N 1060 E, North Logan, UT', 'A'],
        //    ['Place B', '495 W Jackson Street, Knoxville, IA', 'B'],
        //    ['Place C', '1900 N Flagler Drive, West Palm Beach, FL', 'C']
        //];

        var map = new google.maps.Map(document.getElementById('map'), {

            zoom: 12,
            center: new google.maps.LatLng(-33.92, 151.25),

            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        for (i = 0; i < locations.length; i++) {

            marker = createMarker(locations[i]);
        }
        function createMarker(location) {

            var image = new google.maps.MarkerImage('http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + location[2] + '|FF0000|000000',
                  new google.maps.Size(21, 34),
                  new google.maps.Point(0, 0),
                  new google.maps.Point(10, 34)
              );

            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({
                'address': location[1],

            }, function (results, status) {

                if (status == google.maps.GeocoderStatus.OK) {
                    map.setCenter(results[0].geometry.location);
                    marker = new google.maps.Marker({
                        position: results[0].geometry.location,
                        map: map,
                        icon: image

                    });
                }
            });
        }
    }
}