


function InitializeCareer() {
    removeBorders('.rbl > tbody > tr > td');
    removeBorders('.rbl2 > tbody > tr > td');
    removeCalendarIconBorders();
    setRadioSpacing();
    setPhoneMask();
    bindReferralCheckBoxEvent();
    setSelectedReferals();
}

function setRadioSpacing() {
    jQuery('.rbl').attr('cellspacing', 10);

}
function removeBorders(rbl) {
    jQuery(rbl).css({ "border-width": "0px" });
}

function setPhoneMask() {
    jQuery(".phoneMask").mask("(999) 999-9999");
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
    if (controlID == 'HaveContactedBoardForConversion') {
        jQuery('#secHaveContactedBoardForConversion tbody tr td span input:first').focus();
    }
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
function removeSectionBorders(sectionId) {
    removeBorders('#' + sectionId + ' > tbody > tr > td');
    removeBorders('#' + sectionId);
}
function removeCalendarIconBorders() {
    removeBorders('.CalendarIcon');
}
function bindReferralCheckBoxEvent() {
    jQuery('.ReferralCheckBoxList').click(function () {
        setSelectedReferals();
    });
}
function setSelectedReferals()
{
    hideAllCheckBoxDependentTextbox();
    var checkedItems = [];
    jQuery('.ReferralCheckBoxList input:checked').each(function () {
        checkedItems.push(this.value);
    });
    for (i = 0 ; i < checkedItems.length; i++) 
        showCheckBoxdependentTextbox(checkedItems[i]);
}
function hideAllCheckBoxDependentTextbox()
{
    jQuery('.checkBoxDependentTextbox').hide();
    disableValidator('referralValidator');
}
function showCheckBoxdependentTextbox(item) {
    jQuery('#tr' + item + '').show();
    enableValidator('rfv' + item + '');
}
