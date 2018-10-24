var webpartFor;


jQuery(document).ready(function ($) {
   
    $(".cheerCardRadio input:radio").click(function () {
        $(".cheerCardRadio input:radio ").prop("checked", false);
        $(this).prop("checked", true);
        var selectedValue = $(this).parents("span.cheerCardRadio").attr("rel");

        $("input[id$='hdnSelectedImageGuid']").val(selectedValue);
        
        
    });

    if ($(location).attr('href').toLowerCase().indexOf("?image=") != -1) {
        var relid = GetQueryStringParams("image")
        $("span[rel=" + relid + "] input:radio ").prop("checked", true);
        $("input[id$='hdnSelectedImageGuid']").val(relid);
    }
    if (webpartFor == "Mobile")
        SetDivStyle();
  
    if ($('.FormErrorMessage').is(':visible'))
        scrollGo('.FormErrorMessage');
});


function scrollGo(element) {
    var x = jQuery(element).offset().top - 100; // 100 provides buffer in viewport
    jQuery('html,body').animate({ scrollTop: x }, 500);
}
// set focus event on element


function GetQueryStringParams(sParam) {
    var sPageURL = jQuery(location).attr('search').substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0].toLowerCase() == sParam.toLowerCase()) {
            return sParameterName[1];
        }
    }
}

function SetDivStyle() {
   
    jQuery("input:radio[name$='CardGroupName']:checked").parent().parent().parent().parent().parent().find("a.accordion-toggle").removeClass("collapsed");
    jQuery("input:radio[name$='CardGroupName']:checked").parent().parent().parent().parent().addClass("in");
    if (jQuery(".panel-collapse.collapse.in").html() == null && jQuery(".panel-collapse.collapse").html() != null) {
        
        jQuery(".panel-collapse.collapse:first").addClass("in")
        jQuery("a.accordion-toggle.clearfix").addClass("collapsed");
        jQuery("a.accordion-toggle.clearfix.collapsed:first").removeClass("collapsed");
       
    }
}

function ShowHidePatientRow() {
    if (jQuery("input[id*=radioHospital]").prop('checked')) {
        jQuery("#RowPatientEmail").hide();
        ValidatorEnable(jQuery('[id$=RequiredPatientEmail]')[0], false);
        ValidatorEnable(jQuery('[id$=RegularPatientEmail]')[0], false);
        jQuery("input[id*='hdnIsMailToPatient']").prop('value', "false");
        MakeRoomNumberMandatory();
    }
    else {
        jQuery("#RowPatientEmail").show();
        ValidatorEnable(jQuery('[id$=RequiredPatientEmail]')[0], true);
        ValidatorEnable(jQuery('[id$=RegularPatientEmail]')[0], true);
        jQuery("input[id*='hdnIsMailToPatient']").prop('value', "true");

        jQuery('[id$=RequiredPatientEmail]').css('visibility', 'visible');
        jQuery('[id$=RegularPatientEmail]').css('visibility', 'visible');

        if (jQuery('.formWrapper SPAN.ErrorMessage').css('display') == 'none') {
            jQuery('[id$=RequiredPatientEmail]')[0].isvalid = true;
            jQuery('[id$=RequiredPatientEmail]').css('display', 'none');
            
            jQuery('[id$=RegularPatientEmail]')[0].isvalid = true;
            jQuery('[id$=RegularPatientEmail]').css('display', 'none');
        
            
        }

        MakeRoomNumberNonMandatory();
    }

    setTimeout(function () {
        jQuery(".formWrapper input[type=text]:first").focus();
    }, 15);

}

function MakeRoomNumberMandatory() {
   
    var listWithoutDisplayNone = jQuery('.formWrapper SPAN.ErrorMessage').filter(function () {
        if (jQuery(this).css('display') == 'inline')
            return jQuery(this);
    });


    if (jQuery("#spnRoomNumberAst") != null) {
        jQuery("#spnRoomNumberAst").css('display','inline') ;
        ValidatorEnable(jQuery('[id$=RequiredRoomNumber]')[0], true);
        jQuery('[id$=RequiredRoomNumber]').css('visibility', 'visible');

        if (listWithoutDisplayNone.length == 0) {
            jQuery('[id$=RequiredRoomNumber]')[0].isvalid = true;
            jQuery('[id$=RequiredRoomNumber]').css('display', 'none');
            
        }

        
    }
}

function MakeRoomNumberNonMandatory() {
    if (jQuery("#spnRoomNumberAst") != null) {
     
        jQuery("#spnRoomNumberAst").css('display', 'none');
        ValidatorEnable(jQuery('[id$=RequiredRoomNumber]')[0], false);
    }
}

function SetMaxLimit(control, size) {
    var maxlength = new Number(size); // Change number to your max length.
    if (control.value.length > maxlength) {
        control.value = control.value.substring(0, maxlength);
    }
}

function SetCheerCardMaskFields() {
    jQuery("input[id*=Phone]").mask("999-999-9999");
    jQuery("input[id*=Extension]").mask("9999");
}