<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventRegistration.ascx.cs"
    Inherits="CMSWebParts_CMS_EventsCalendar_EventRegistration" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="RegistrationPanel" runat="server" DefaultButton="RegisterButton">
<script src="~/CMSScripts/Custom/Mask.js"></script>
    <asp:Literal ID="InformationMessage" runat="server"></asp:Literal>

    <div class="clearfix">
        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
    </div>


    <div class="eventRegistration">
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12">
                <label><%= GetString("Emerge.EC.Label.FirstName") %>: <font color="red">*</font></label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="FirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="FirstNameRequired" ControlToValidate="FirstName" CssClass="ErrorMessage" ValidationGroup="ER_VG"
                    runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please enter First Name"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12">
                <label><%= GetString("Emerge.EC.Label.LastName") %>: <font color="red">*</font></label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="LastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="LastNameRequired" ControlToValidate="LastName" CssClass="ErrorMessage"
                    ValidationGroup="ER_VG" ErrorMessage="Please enter Last Name."
                    runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="row" id="trComments">
            <div class="col-md-3 col-sm-3 col-xs-12">
                <label>
                    <%= GetString("Emerge.EC.Label.Comments") %>:
                </label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </div>
        </div>
    

    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.Email") %>: <font color="red">*</font></label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="Email" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="EmailRequired" ControlToValidate="Email" CssClass="ErrorMessage"
                ValidationGroup="ER_VG" ErrorMessage="Please enter Email."
                runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="EmailRegular" runat="server" CssClass="ErrorMessage"
                Display="Dynamic" ErrorMessage="Invalid email format." ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="ER_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.Phone") %>: <font color="red">*</font></label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            
			 <asp:TextBox ID="Phone" runat="server" ClientIDMode="Static" placeholder="(xxx) xxx-xxxx"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularPhone" runat="server" CssClass="ErrorMessage"
                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please enter phone (xxx) xxx-xxxx format" ControlToValidate="Phone" ValidationExpression="(^\(\d{3}\) \d{3}-\d{4}$)"
                    ValidationGroup="ER_VG"></asp:RegularExpressionValidator>
           
            <asp:RequiredFieldValidator ID="PhoneRequired" Display="Dynamic" ControlToValidate="Phone" runat="server" CssClass="ErrorMessage"
                ErrorMessage="Please enter Phone." ValidationGroup="ER_VG" InitialValue="(___) ___-____"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.StreetAddress") %>:</label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="StreetAddress" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.City") %>: <font color="red">*</font></label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="City" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="CityRequired" ControlToValidate="City" CssClass="ErrorMessage"
                ValidationGroup="ER_VG" ErrorMessage="Please enter City."
                runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="revCity" ValidationExpression="^([a-zA-Z]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="City"
                                                        ValidationGroup="ER_VG" ErrorMessage="Only letters are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.State") %>: <font color="red">*</font></label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="State" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="StateRequired" ControlToValidate="State" CssClass="ErrorMessage"
                ValidationGroup="ER_VG" ErrorMessage="Please enter State."
                runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="revState" ValidationExpression="^([a-zA-Z]+)$"
                                                        SetFocusOnError="true" Display="Dynamic" runat="server" ControlToValidate="State"
                                                        ValidationGroup="ER_VG" ErrorMessage="Only letters are allowed." CssClass="ErrorMessage"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.Zip") %>:</label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="Zip" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="ZipValidator" CssClass="ErrorMessage" ValidationExpression="(^\d{5}$)|(_____)" runat="server"
                ControlToValidate="Zip" ErrorMessage="Please enter a 5 digit zip code" Display="Dynamic" ValidationGroup="ER_VG"
                SetFocusOnError="true"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div id="SessionTR" runat="server" class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.SessionSelection") %>: <font color="red">*</font></label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:ListBox ID="SelectedSessions" runat="server" Height="150" Width="290" SelectionMode="Multiple" /><asp:Literal ID="SessionsMessage" runat="server"></asp:Literal>
            <asp:RequiredFieldValidator ID="SelectedSessionsRequired" ControlToValidate="SelectedSessions" CssClass="ErrorMessage"
                ValidationGroup="ER_VG" ErrorMessage="Please select atleast one session."
                runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div id="AmountTR" runat="server" class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.Cost") %>:</label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <label>$<asp:Literal ID="Amount" runat="server"></asp:Literal></label>
        </div>
    </div>

    <div id="DiscountCodeTR" runat="server" class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.DiscountCode") %>:</label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <asp:TextBox ID="DiscountCode" runat="server" AutoPostBack="true"></asp:TextBox><br />
            <asp:Literal ID="DiscountCodeMessage" runat="server"></asp:Literal>
        </div>
    </div>
    <div id="DiscountedCostTR" runat="server" class="row">
        <div class="col-md-3 col-sm-3 col-xs-12">
            <label><%= GetString("Emerge.EC.Label.DiscountedCost") %>:</label>
        </div>
        <div class="col-md-6 col-sm-6 col-xs-12">
            <label>$<asp:Literal ID="DiscountedCost" runat="server"></asp:Literal></label>
        </div>
    </div>
	</div>
    <table>
        <tbody>
        </tbody>
    </table>

    <div class="btnWrapper">
        <cms:LocalizedButton ID="RegisterButton" runat="server" ResourceString="Emerge.EC.Button.Confirm" ValidationGroup="ER_VG" CausesValidation="true" />
        <cms:LocalizedButton ID="BackButton" runat="server" ResourceString="Emerge.EC.Button.BackToEvents" />
    </div>




    <asp:HiddenField ID="OccurenceID" Value="0" runat="server" />
    <asp:HiddenField ID="ScheduleID" Value="0" runat="server" />
    <asp:HiddenField ID="RegistrationFormFields" runat="server" />
</asp:Panel>

<script type="text/javascript">

  jQuery(document).ready(function ($) {
debugger;
         $("input[id*=Phone]").mask("(999) 999-9999");
        $("input[id*=Zip]").mask("99999");
        $("input[id*='RegistrationFormFields']").change(function () { showHideFields(); });

    });
  
	
	
    function showHideFields() {

        jQuery('[id^="tr"]').hide();
        var registrationFormFields = jQuery("input[id*='RegistrationFormFields'][type=hidden]").val();

        var selectedFields = registrationFormFields.split('|');

        jQuery.each(selectedFields, function (index, value) {
            jQuery('#tr' + value).show();
        });
    }
</script>
<asp:Literal ID="bindChangeEvent" runat="server" />