<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_EmergeFormControls_USphone" CodeFile="~/CMSFormControls/EmergeFormControls/USphone.ascx.cs" %>

<cms:CMSTextBox runat="server" ID="txt1st" MaxLength="3" Width="50" CssClass="moveNextfirst validateNumber" />
<cms:CMSTextBox runat="server" ID="txt2nd" MaxLength="3" Width="50" CssClass="moveNextMiddle validateNumber" />
<cms:CMSTextBox runat="server" ID="txt3rd" MaxLength="4" Width="70" CssClass="moveNextLast validateNumber" />
<cms:LocalizedLabel ID="localizedExtensionLabel" AssociatedControlID="txtExt" Visible="false" EnableViewState="false" runat="server" ResourceString="Emerge.USPhone.ExtensionNo" />
<cms:CMSTextBox runat="server" Visible="false" ID="txtExt" MaxLength="4" Width="70" CssClass="moveNextExt validateNumber" />
<cms:LocalizedLabel ID="lbl2nd" AssociatedControlID="txt2nd" EnableViewState="false" runat="server" ResourceString="USPhone.2nd" />
<cms:LocalizedLabel ID="lbl3rd" AssociatedControlID="txt3rd" EnableViewState="false" ResourceString="USPhone.3rd" runat="server" />
<style>
    label {
        margin-top:8px;
        color: #0F6194;
        font-family: "Segoe UI Semibold",Helvetica,Verdana,Arial,sans-serif;
        font-weight: 600;
        font-size:14px
    }
</style>
<script>
    jQuery(function ($) {
        $('.validateNumber').keyup(function () {
            if (isNaN(this.value)) {
                this.value = this.value.substring(0, this.value.length - 1);
                return;
            }
        });
        $('.validateNumber').keyup(function () {
            if (this.value.length == $(this).attr('maxlength')) {
                $(this).next(':input').focus();
            }

            if (this.value.length == 0 && $(this).attr('class') != "moveNextfirst") {
                $(this).prev(':input').focus();

            }
        });

    });
</script>
