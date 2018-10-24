function scrollGo(element) {
    var x = jQuery(element).offset().top - 100; // 100 provides buffer in viewport
    jQuery('html,body').animate({ scrollTop: x }, 500);
}


function setMaskPurchaseInformationForm() {
    jQuery("input[id*=RecipientPhoneNumber]").mask("999-999-9999");
   //jQuery("input[id*=SenderHomeAddressZipCode]").mask("99999");
    //jQuery("input[id*=BillingAddressZipCode]").mask("99999");
    jQuery("input[id*=SenderHomePhoneNumber]").mask("999-999-9999");
    jQuery("input[id*=SenderMobileNumber]").mask("999-999-9999");

}


function hideBillingAddress() {
    jQuery("#billingAddressStreetRow").hide();
    jQuery("#billingAddressStreetVal").hide();
    jQuery("#billingAddressCityRow").hide();
    jQuery("#billingAddressCityVal").hide();

    jQuery("#billingAddressStateRow").hide();
    jQuery("#billingAddressStreetVal").hide();
    jQuery("#billingAddressZipRow").hide();
    jQuery("#billingAddressZipVal").hide();

    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStreet]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStreetIE]')[0], false);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressStreet]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressCityIE]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressCity]')[0], false);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressCity]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStateID]')[0], false);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressZipCode]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressZipCode]')[0], false);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressZipCodeIE]')[0], false);
    
}

function showBillingAddress() {

    var listWithDisplayInline = jQuery('table tr td SPAN.ErrorMessage').filter(function () {
        if (jQuery(this).css('display') == 'inline')
            return jQuery(this);
    });

    


    jQuery("#billingAddressStreetRow").show();
    jQuery("#billingAddressStreetVal").show();
    jQuery("#billingAddressCityRow").show();
    jQuery("#billingAddressCityVal").show();

    jQuery("#billingAddressStateRow").show();
    jQuery("#billingAddressStreetVal").show();
    jQuery("#billingAddressZipRow").show();
    jQuery("#billingAddressZipVal").show();

    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStreet]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStreetIE]')[0], true);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressStreet]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressCityIE]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressCity]')[0], true);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressCity]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressStateID]')[0], true);
    ValidatorEnable(jQuery('[id$=RegularBillingAddressZipCode]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressZipCode]')[0], true);
    ValidatorEnable(jQuery('[id$=RequiredBillingAddressZipCodeIE]')[0], true);

    jQuery('[id$=RequiredBillingAddressStreet]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressStreetIE]').css('visibility', 'visible');
    jQuery('[id$=RegularBillingAddressStreet]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressCityIE]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressCity]').css('visibility', 'visible');
    jQuery('[id$=RegularBillingAddressCity]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressStateID]').css('visibility', 'visible');
    jQuery('[id$=RegularBillingAddressZipCode]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressZipCode]').css('visibility', 'visible');
    jQuery('[id$=RequiredBillingAddressZipCodeIE]').css('visibility', 'visible');


  
    if (listWithDisplayInline.length == 0) {
        setBillingValidatorAsValid()
        
    }

    jQuery('input[id$=BillingAddressStreet]').val('');
    jQuery('input[id$=BillingAddressCity]').val("");
    jQuery('input[id$=BillingAddressZipCode]').val("");
    jQuery('[id$=BillingAddressStateID]')[0].selectedIndex = 0;
   
}

function setBillingValidatorAsValid() {
    jQuery('[id$=RequiredBillingAddressStreet]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressStreet]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressStreetIE]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressStreetIE]').css('display', 'none');

    jQuery('[id$=RegularBillingAddressStreet]')[0].isvalid = true;
    jQuery('[id$=RegularBillingAddressStreet]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressCityIE]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressCityIE]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressCity]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressCity]').css('display', 'none');

    jQuery('[id$=RegularBillingAddressCity]')[0].isvalid = true;
    jQuery('[id$=RegularBillingAddressCity]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressStateID]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressStateID]').css('display', 'none');

    jQuery('[id$=RegularBillingAddressZipCode]')[0].isvalid = true;
    jQuery('[id$=RegularBillingAddressZipCode]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressZipCode]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressZipCode]').css('display', 'none');

    jQuery('[id$=RequiredBillingAddressZipCodeIE]')[0].isvalid = true;
    jQuery('[id$=RequiredBillingAddressZipCodeIE]').css('display', 'none');
}

function copyHomeAddressToBillingAddress() {
    
    jQuery('input[id$=BillingAddressStreet]').val( jQuery('input[id$=SenderHomeAddressStreet]').val() );
    jQuery('input[id$=BillingAddressCity]').val(jQuery('input[id$=SenderHomeAddressCity]').val());
    jQuery('input[id$=BillingAddressZipCode]').val(jQuery('input[id$=SenderHomeAddressZipCode]').val());
   
    
    //jQuery('input[id$=SenderHomeAddressStateID]').val('NY').change();

    jQuery('[id$=BillingAddressStateID]').val(jQuery('[id$=SenderHomeAddressStateID]').val());

}


function bindHandlerToSameBillingAddressCheckbox() {

    jQuery(".checkBoxForSameAddress :checkbox").click(function () {

        if (jQuery(this).is(':checked')) {
            copyHomeAddressToBillingAddress();
            hideBillingAddress();
        }
        else {
            showBillingAddress();
        }



    });

   

}

