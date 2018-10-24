

function setDonorTypeValue() {
    if (jQuery("#LevelsList tr td").length == 0) {
        jQuery("#LevelsTR").attr("style", "display:none");
    }
    else {
        jQuery("#LevelsTR").attr("style", "display:");
    }
    var value;
    if (jQuery("#Individual").is(':checked')) {
        value = "Individual";
        jQuery("#CorporationTR").attr("style", "display:none");
    }
    else {
        value = "Corporation";
        jQuery("#CorporationTR").attr("style", "display:");
    }
    jQuery("#DonorType").val(value);

    if (jQuery("#DonationType tr td input").length == 0) {
        jQuery("#DonationTypeSection").attr("style", "display:none")
    }
    else {
        jQuery("#DonationTypeSection").attr("style", "display:");
    }
}

function attachedHandlers() {
    jQuery("#ProcessButton").click(function () {
        return ProcessClick();
    });
    if (jQuery("#HonourType tr td input").length == 0) {
        jQuery("#HonourLabel").attr("style", "display:none")
    }
    jQuery("#ClearButton").click(function () {
        return clearFields();
    });

    jQuery("#HonourType tr td input").change(function () {
        ManageNotificationForm();
    });

    jQuery("#LevelsList tr td input").change(function () {
        setDonationValue(this);

    });

    jQuery("#Individual").change(function () {
        individualChange(this);
    });

    jQuery("#Corporation").change(function () {
        corporationChange(this);
    });

    jQuery("#NotificationCheckbox").change(function () {
        ManageNotificationForm();
    });
    jQuery("#Amount").change(function () {
        jQuery("#DonationAmount").val(jQuery("#Amount").val());
    });
    jQuery("#DonationType").change(function () {

    });
}

function clearFields() {
    $('form').find('input[type=text]').val('');
    $('form').find('input[type=radio]').attr("checked", false);
    $('form').find('input[type=checkbox]').attr("checked", false);
    return true;
}


function setDonationValue(obj)
{
    if (obj.value == "0") {
        jQuery("#DonationAmount").val("");
        jQuery("#Amount").val("");
        jQuery("#Amount").attr("disabled", false);
    }
    else {
        jQuery("#DonationAmount").val(obj.value);
        jQuery("#Amount").val(obj.value);
        jQuery("#Amount").attr("disabled", true);
    }
}


function individualChange(obj) {
    jQuery("#DonorType").val(obj.value);
    jQuery("#CorporationTR").attr("style", "display:none");
    
}

function corporationChange(obj) {
    jQuery("#DonorType").val(obj.value);
    jQuery("#CorporationTR").attr("style", "display:");
}

function ManageNotificationForm() {
    var isVisible = false;
    if (jQuery("#HonourType tr td input:checked").length > 0) {
        jQuery("#PersonNameTR").attr("style", "display:");
        jQuery("#NotificationChkTR").attr("style", "display:");
       
        if (jQuery("#NotificationCheckbox").is(':checked')) {
            isVisible = false;
        }
        else {
            isVisible = true;
        }
    }
    else {
        isVisible = false;
        setVisibility("PersonName", isVisible);
        setVisibility("NotificationChk", isVisible);
    }
    SetNotificationFormVisibility(isVisible);

}

function SetNotificationFormVisibility(isVisible) {
    setVisibility("NotificationName", isVisible);
    setVisibility("NotificationAddress1", isVisible);
    setVisibility("NotificationAddress2", isVisible);
    setVisibility("NotificationCity", isVisible);
    setVisibility("NotificationState", isVisible);
    setVisibility("NotificationZip", isVisible);
}

function setVisibility(id, visible) {
    if (visible) {
        jQuery("#" + id + "TR").attr("style", "display:");
    }
    else {
        jQuery("#" + id + "TR").attr("style", "display:none");
    }
}

function clearNotificationFields() {
    clearField("NotificationName");
    clearField("NotificationAddress1");
    clearField("NotificationCity");
    clearField("NotificationState");
    clearField("NotificationAddress2");
    clearNotificationZipField();
}

function clearField(id) {
    jQuery("#" + id).val("");
}
function clearNotificationZipField() {
    jQuery("#NotificationZip").val("");
}

var formhandler = function () {
    var submit, isSubmit = false;
    submit = function () {
        // flop and return false once by the use of operator order.
        return isSubmit != (isSubmit = true);
    };
    return {
        submit: submit
    };
}();

function ProcessClick() {
    if (ValidateForm()) {
        if(formhandler.submit())
            return true;
    }
    return false;
}

function ValidateForm() {
    if (typeof (Page_ClientValidate) == 'function') {
        var isPageValid = Page_ClientValidate();
    }
    var count = 0;
    if (jQuery("#HonourType tr td input:checked").length > 0) {

        if (!jQuery("#NotificationCheckbox").is(':checked')) {
            count = validateField("PersonName", count);
            count = validateField("NotificationName", count);
            count = validateField("NotificationAddress1", count);
            count = validateField("NotificationCity", count);
            count = validateField("NotificationState", count);
            count = checkNotificationZip(count);
        }
        else {
            count = validateField("PersonName", count);
            clearNotificationFields();
        }
    }
    else {
        clearNotificationFields();
        clearField("PersonName");
    }
    if (jQuery("#Corporation").is(':checked')) {
        count = validateField("CorporationName", count);
    }
    else {
        clearField("CorporationName");
    }
    count = validateCheckbox(count);
    if (count > 0) {
        return false;
    }
    return isPageValid;
}
function validateCheckbox(count) {
    if (jQuery("#ConfirmationChk").is(':checked')) {
        jQuery("#ConfirmationChkRequired").attr("style", "display:none");
    }
    else {
        jQuery("#ConfirmationChkRequired").attr("style", "display:");
        count++;
    }
    return count;
}

function validateField(id, count) {
    var value = jQuery("#" + id).val();
    if ( value == "" || value == undefined) {
        jQuery("#" + id + "Required").attr("style", "display:");
        count++;
    }
    else {
        jQuery("#" + id + "Required").attr("style", "display:none");
    }

    return count;
}


function checkNotificationZip(count) {
    
    if (jQuery("#NotificationZip").val() == "_____" || jQuery("#NotificationZip").val() == "") {
        jQuery("#NotificationZipRequired").attr("style", "display:");
        count++;
    }
    else {
        jQuery("#NotificationZipRequired").attr("style", "display:none");
    }

    return count;
}

