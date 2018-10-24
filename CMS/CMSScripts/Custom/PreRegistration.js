function FormatPhone(t) {
    var patt = /(\d{3}).*(\d{3}).*(\d{4})/;
    var donepatt = /^(\d{3})-(\d{2})-(\d{4})$/;
    var str = t.value;
    var result;
    if (!str.match(donepatt)) {
        result = str.match(patt);
        if (result != null) {
            t.value = t.value.replace(/[^\d]/gi, '');
            str = result[1] + '-' + result[2] + '-' + result[3];
            t.value = str;
        }
        else {
            if (t.value.match(/[^\d]/gi))
                t.value = t.value.replace(/[^\d]/gi, '');
        }
    }
}

function FormatSSN(t) {
    var patt = /(\d{3}).*(\d{2}).*(\d{4})/;
    var donepatt = /^(\d{3})-(\d{1})-(\d{4})$/;
    var str = t.value;
    var result;
    if (!str.match(donepatt)) {
        result = str.match(patt);
        if (result != null) {
            t.value = t.value.replace(/[^\d]/gi, '');
            str = result[1] + '-' + result[2] + '-' + result[3];
            t.value = str;
        }
        else {
            if (t.value.match(/[^\d]/gi))
                t.value = t.value.replace(/[^\d]/gi, '');
        }
    }
}
function getBritishColumbiaZip(arguments) {
    var patt = new RegExp("^([a-zA-Z0-9]{3})$");
    if (arguments.Value != '') {
        var res = patt.test(arguments.Value);
        if (res == true)
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
}
function getOtherZip(arguments) {
    var patt = new RegExp("^([a-zA-Z0-9]{5,7})$");
    if (arguments.Value != '') {
        var res = patt.test(arguments.Value);
        if (res == true)
            arguments.IsValid = true;
        else
            arguments.IsValid = false;
    }
}
function clearRadioButtonList(elementRef) {
    var inputElementArray = elementRef.getElementsByTagName('input');
    for (var i = 0; i < inputElementArray.length; i++) {
        var inputElement = inputElementArray[i];

        inputElement.checked = false;
    }
    return false;
}
function addRadioButtonHandlers(controlID) {
    jQuery('.' + controlID).on('change', function () {
        setState(controlID)
    });
}

function setState(controlID) {
    var controlValue = jQuery('input[ID*="' + controlID + '"]:checked').val();
    if (controlValue) {
        var isChecked = controlValue.toLowerCase() == "yes" ? true : false;
        if (isChecked) {
            showSection(controlID);
        }
        else {
            hideSection(controlID);
        }
    }
}
function showSection(controlID) {
    jQuery('#sec' + controlID + '').show();
    enableValidator(controlID + 'Validator');
}

function hideSection(controlID) {
    jQuery('#sec' + controlID + '').hide();
    disableValidator(controlID + 'Validator');
}

function enableValidator(validatorClass) {
    var validators = document.getElementsByClassName(validatorClass);
    for (var i = 0; i < validators.length; i++) {
        ValidatorEnable(validators[i], true);
    }
}

function disableValidator(validatorClass) {
    var validators = document.getElementsByClassName(validatorClass);
    for (var i = 0; i < validators.length; i++) {
        ValidatorEnable(validators[i], false);
    }
}

function CMSDatepicker() {
    this._defaults = {
        defaultDate: null,
        yearRange: 'c-20:c+10'
    }
}