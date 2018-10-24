<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCartRegistration.ascx.cs" Inherits="CMSWebParts_CMS_EventsCalendar_EventCartRegistration" %>
<asp:Panel ID="CartRegistrationPanel" runat="server" DefaultButton="btnProceedToPayment">
    <div class="eventRegistration">

        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.FirstName") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="FirstName" MaxLength="50" runat="server"></asp:TextBox>


            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:RequiredFieldValidator ID="RequiredRecipientFirstName" runat="server" ControlToValidate="FirstName"
                    ErrorMessage="Please enter First Name." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RequiredFieldValidator>




            </div>
        </div>

        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.LastName") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="LastName" MaxLength="50" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:RequiredFieldValidator ID="RequiredLastName" runat="server" ControlToValidate="LastName" ErrorMessage="Please enter Last Name." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RequiredFieldValidator>




            </div>
        </div>


        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.Email") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Email" MaxLength="100" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:RequiredFieldValidator ID="RequiredEmail" ControlToValidate="Email" CssClass="ErrorMessage"
                    ValidationGroup="VG_EC_ECR" ErrorMessage="Please enter Email."
                    runat="server" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularEmail" runat="server" CssClass="ErrorMessage"
                    Display="Dynamic" ErrorMessage="Invalid email format." ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="VG_EC_ECR" SetFocusOnError="true"></asp:RegularExpressionValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.Phone") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Phone" MaxLength="15" runat="server"></asp:TextBox>



            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:RegularExpressionValidator ID="RegularPhone" runat="server" CssClass="ErrorMessage"
                    Display="Dynamic" ErrorMessage="Invalid Phone."
                    ControlToValidate="Phone" ValidationExpression="(^\(\d{3}\)\s{1}\d{3}-\d{4}$)|((___) ___-____)" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RegularExpressionValidator>

                <asp:RequiredFieldValidator ID="RequiredPhone" runat="server" ControlToValidate="Phone" ErrorMessage="Please enter Phone Number." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RequiredFieldValidator>

            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12">
                <label><%= GetString("Emerge.EC.Label.StreetAddress") %>:</label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="StreetAddress" MaxLength="100" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">

                <asp:RegularExpressionValidator ID="RegularStreetAddress" runat="server" ControlToValidate="StreetAddress"
                    ErrorMessage="Please enter char(a-z, 0-9, dash, period, comma, #) for Street."
                    SetFocusOnError="True" Display="Dynamic" ValidationExpression="^([a-zA-Z0-9-.,#\s]*)$" CssClass="ErrorMessage" ValidationGroup="VG_EC_ECR"></asp:RegularExpressionValidator>


            </div>
        </div>


        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.City") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="City" MaxLength="100" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">

                <asp:RequiredFieldValidator ID="RequiredCity" ControlToValidate="City" CssClass="ErrorMessage" ErrorMessage="Please enter City." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularCity" runat="server" ControlToValidate="City"
                    ErrorMessage="Please enter char(a-z, dash) for City." SetFocusOnError="True" CssClass="ErrorMessage"
                    Display="Dynamic" ValidationExpression="^([a-zA-Z-\s]*)$" ValidationGroup="VG_EC_ECR"></asp:RegularExpressionValidator>


            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.State") %>: <font color="red">*</font></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="State" MaxLength="100" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">

                <asp:RequiredFieldValidator ID="RequiredState" ControlToValidate="State" CssClass="ErrorMessage" ErrorMessage="Please enter State." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_EC_ECR"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularState" runat="server" ControlToValidate="State"
                    ErrorMessage="Please enter char(a-z, dash) for State." SetFocusOnError="True" CssClass="ErrorMessage"
                    Display="Dynamic" ValidationExpression="^([a-zA-Z-\s]*)$" ValidationGroup="VG_EC_ECR"></asp:RegularExpressionValidator>


            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"><%= GetString("Emerge.EC.Label.Zip") %>:</div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Zip" MaxLength="5" runat="server"></asp:TextBox>

            </div>

        </div>
        <div class="row">
            <div class="col-md-3 col-sm-3 col-xs-12"></div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:RegularExpressionValidator ID="ZipValidator" CssClass="ErrorMessage" ValidationExpression="(^\d{5}$)|(_____)" runat="server"
                    ControlToValidate="Zip" ErrorMessage="Please enter a 5 digit zip code" Display="Dynamic" ValidationGroup="VG_EC_ECR"
                    SetFocusOnError="true"></asp:RegularExpressionValidator>
            </div>

        </div>


        <div class="btnWrapper">

            <cms:LocalizedButton ID="btnProceedToPayment" ResourceString="Emerge.EC.Button.Submit" runat="server" CausesValidation="true" ValidationGroup="VG_EC_ECR" />
            <cms:LocalizedButton ID="btnClear" ResourceString="Emerge.EC.Button.ClearForm" runat="server" CausesValidation="false" />
            <cms:LocalizedButton ID="btnBackToCart" ResourceString="Emerge.EC.Button.BackToCart" runat="server" CausesValidation="false" />

        </div>
    </div>
    <script>
        jQuery("input[id*=Zip]").mask("99999");
        jQuery("input[id*=Phone]").mask("(999) 999-9999");
        jQuery("input[id*='RegistrationFormFields']").change(function () { showHideFields(); });
        function showHideFields() {
            jQuery('[id^="tr"]').hide();
            var registrationFormFields = jQuery("input[id*='RegistrationFormFields'][type=hidden]").val();
            var selectedFields = registrationFormFields.split('|');
            jQuery.each(selectedFields, function (index, value) {
                jQuery('#tr' + value).show();
            });
        }
    </script>
</asp:Panel>

<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>

