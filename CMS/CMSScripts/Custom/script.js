jQuery(window).load(function() {
    jQuery('.flexslider').flexslider({
        animation: "fade"
    });
    //   code edited on 15/01/2014
    jQuery('#scrollbox3').enscroll({
        showOnHover: true,
        verticalTrackClass: 'track3',
        verticalHandleClass: 'handle3'
    });
    //   code edited on 15/01/2014
});

jQuery(document).ready(function(){
    jQuery('.featuredImg a').click(function(e){
        e.preventDefault();
        var featuredDetail = jQuery(e.currentTarget).attr('data-target');
        jQuery('.featuredImg li').removeClass('active');
        jQuery(e.currentTarget).parent('li').addClass('active');
        jQuery('.featuredDetails').hide();
        jQuery(featuredDetail).show();
    });
});


function SetMaxLimit(control, size) {
    var maxlength = new Number(size); // Change number to your max length.
    if (control.value.length > maxlength) {
        control.value = control.value.substring(0, maxlength);
    }
}




jQuery(document).ready(function () {
    var filename = jQuery(location).attr('href').substr(jQuery(location).attr('href').lastIndexOf('/') + 1);
    filename = filename.toLowerCase().replace(".aspx", "");
    if (filename.indexOf("?") >= 0) {
        filename = filename.substr(filename.indexOf("?") + 1);
    }
    if (filename == "" || filename == "default" || filename == "home")
    {
        jQuery(".banner").addClass("sliderWrapper").removeClass("banner");
        jQuery("#BreadcrumbDiv").hide();
        jQuery(".boxWrapper").addClass("homePage");
    }
    
    jQuery("#spnCopyrightYear").html(new Date().getFullYear());
});

//  js for pagination issue
//code edited on 02/12/2013
jQuery(document).ready(function () {
    var html1 = '<span class="SelectedNext">&gt;</span>'
    var html2 = '<span class="SelectedNext">&lt;</span>'
    jQuery(".PagerNumberArea .CustomStyle1").each(function () {

        if (jQuery(this).children('a').length <= 0) {
            if (jQuery(this).is(".CustomStyle4")) {
                jQuery(this).html(html2)
            }
            if (jQuery(this).is(".CustomStyle3")) {
                jQuery(this).html(html1)
            }
        }
    })


});

//code edited on 02/12/2013