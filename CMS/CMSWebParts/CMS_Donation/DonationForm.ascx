<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DonationForm.ascx.cs"
    Inherits="CMSWebParts_CMS_Donation_DonationForm" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="DonationFormPanel" runat="server" DefaultButton="ProcessButton">

    <h1>Online Donation</h1>

    <div class="personalInfo">
        <h3>Personal Information</h3>
        <hr>
        <div class="halfContent">

            <div class="row">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    Is this donation being made by an individual
                    or a corporation?
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RadioButton ID="Individual" ClientIDMode="Static" runat="server" Text="Individual" Checked="true" GroupName="DonationGroup" />
                    <asp:RadioButton ID="Corporation" ClientIDMode="Static" runat="server" Text="Corporation" GroupName="DonationGroup" />
                    <asp:TextBox ID="DonorType" ClientIDMode="Static" runat="server" Style="display: none" Text="Individual" />
                </div>
            </div>
        </div>

        <div class="row" id="CorporationTR">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Corporation Name:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="CorporationName" ClientIDMode="Static" runat="server" MaxLength="200"></asp:TextBox>
                <span id="CorporationNameRequired" class="ErrorMessage" style="display: none">Please enter Corporation name.</span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Name of Donor(s):</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="DonorName" runat="server" MaxLength="70"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DonorNameRequired" ControlToValidate="DonorName" CssClass="ErrorMessage"
                    ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter name of donor."
                    runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Address 1:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Address1" runat="server" MaxLength="150"></asp:TextBox>
                <asp:RequiredFieldValidator ID="Address1Required" ControlToValidate="Address1" CssClass="ErrorMessage"
                    ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter Address."
                    runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Address 2:</label>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Address2" runat="server" MaxLength="150"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>City:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="City" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CityRequired" ControlToValidate="City" CssClass="ErrorMessage"
                    ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter City."
                    runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>State:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="State" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="StateRequired" ControlToValidate="State" CssClass="ErrorMessage"
                    ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter State."
                    runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Zip:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Zip" runat="server" ClientIDMode="Static"></asp:TextBox>



                <asp:RegularExpressionValidator ID="ZipValidator" CssClass="ErrorMessage" ValidationExpression="(^\d{5}$)" runat="server"
                    ControlToValidate="Zip" SetFocusOnError="true" ErrorMessage="Please enter a 5 digit zip code" Display="Dynamic" ValidationGroup="DN_VG"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="ZipRequired" Display="Dynamic" ControlToValidate="Zip" runat="server" CssClass="ErrorMessage"
                    SetFocusOnError="true" ErrorMessage="Please enter Zip." ValidationGroup="DN_VG"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row PI-PhoneNo">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Phone Number:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Phone" runat="server"></asp:TextBox>


                <label>Ext No:</label>
                <asp:TextBox ID="Extension" CssClass="extn" runat="server"></asp:TextBox>

                <asp:RegularExpressionValidator ID="RegularPhone" runat="server" CssClass="ErrorMessage"
                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="" ControlToValidate="Phone" ValidationExpression="(^\(\d{3}\) \d{3}-\d{4}$)"
                    ValidationGroup="DN_VG"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="PhoneRequired" Display="Dynamic" ControlToValidate="Phone" runat="server" CssClass="ErrorMessage"
                    SetFocusOnError="true" ErrorMessage="Please enter Phone." ValidationGroup="DN_VG"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4 col-sm-6 col-xs-12">
                <label>Email Address:</label><font color="red">*</font>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="Email" runat="server" MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="EmailRequired" ControlToValidate="Email" CssClass="ErrorMessage"
                    ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter Email."
                    runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="EmailRegular" runat="server" CssClass="ErrorMessage"
                    Display="Dynamic" SetFocusOnError="true" ErrorMessage="Invalid email format." ControlToValidate="Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="DN_VG"></asp:RegularExpressionValidator>
            </div>
        </div>


        <div id="DonationTypeSection">
            <h3>Type of Donation</h3>
            <hr>
            <div class="Type-Donation">

                <div class="row">
                    <div class="col-md-4 col-sm-6 col-xs-12">
                        Would like to:
                    </div>
                    <div class="col-md-8 col-sm-6 col-xs-12">
                        <asp:RadioButtonList ID="DonationType" ClientIDMode="Static" CausesValidation="false" AutoPostBack="true"
                            RepeatDirection="Horizontal" RepeatColumns="1" DataTextField="TypeName" EnableViewState="true" ValidationGroup="DN_VG" DataValueField="ItemID" runat="server">
                        </asp:RadioButtonList>
                        <cms:CMSQueryDataSource ID="DonationType_DataSource" runat="server" QueryName="customtable.Emerge_{0}_DN_DonationInformation.GetDonationTypes" />
                        <asp:TextBox ID="TypeOfDonationForEmail" ClientIDMode="Static" runat="server" Style="display: none" />
                    </div>
                </div>


            </div>
            <h3>Levels of donation</h3>
            <hr>

            <table>
                <tbody>
                    <tr>
                        <td valign="top">Amount of your choice<font color="red">*</font>
                        </td>
                        <td class="amount-choice"><span class="dollar-sign">$ </span>
                            <asp:TextBox ID="Amount" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="DonationAmountRequired" ClientIDMode="Static" ControlToValidate="Amount" CssClass="ErrorMessage"
                                ValidationGroup="DN_VG" SetFocusOnError="true" ErrorMessage="Please enter amount."
                                runat="server" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="DonationAmountRegular" runat="server" CssClass="ErrorMessage"
                                Display="Dynamic" SetFocusOnError="true" ErrorMessage="Please enter the valid amount." ControlToValidate="Amount"
                                ValidationExpression="^\d+(.\d+){0,1}$" ValidationGroup="DN_VG"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="DonationAmount" runat="server" Style="display: none" ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="LevelsTR">
                        <td>Other levels:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="LevelsList" ClientIDMode="Static" DataTextField="AmountInfo" RepeatDirection="Horizontal" RepeatColumns="3" DataValueField="Amount" runat="server"></asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr id="ConfirmationTR">
                        <td colspan="2">
                            <cms:LocalizedCheckBox ID="ConfirmationChk" runat="server" ResourceString="Emerge.DonationForm.DonationTypeCheckbox" ClientIDMode="Static"></cms:LocalizedCheckBox>
                            <br />
                            <span id="ConfirmationChkRequired" class="ErrorMessage" style="display: none">Please select the checkbox to confirm.</span>
                        </td>
                    </tr>
                </tbody>
            </table>

            <h3 id="HonourLabel" runat="server">This donation is</h3>
            <hr>
            <asp:RadioButtonList ID="HonourType" ClientIDMode="Static" DataTextField="Title" EnableViewState="true" DataValueField="ItemID" RepeatDirection="Horizontal" RepeatColumns="4" runat="server"></asp:RadioButtonList>
            <cms:CMSQueryDataSource ID="HonourType_DataSource" runat="server" QueryName="customtable.Emerge_{0}_DN_DonationInformation.GetHonourTypes" />




            <div class="row" id="PersonNameTR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>Provide the name:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="PersonName" runat="server" ClientIDMode="Static" MaxLength="200"></asp:TextBox>
                    <span id="PersonNameRequired" class="ErrorMessage" style="display: none">Please enter Person name.</span>
                </div>
            </div>
            <div class="row" id="NotificationNameTR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>Notification Name:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationName" runat="server" ClientIDMode="Static" MaxLength="150"></asp:TextBox>
                    <span id="NotificationNameRequired" class="ErrorMessage" style="display: none">Please enter Notification name.</span>
                </div>
            </div>
            <div class="row" id="NotificationAddress1TR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>Address1:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationAddress1" runat="server" ClientIDMode="Static" MaxLength="150"></asp:TextBox>
                    <span id="NotificationAddress1Required" class="ErrorMessage" style="display: none">Please enter Address</span>
                </div>
            </div>
            <div class="row" id="NotificationAddress2TR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>Address2:</label>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationAddress2" runat="server" ClientIDMode="Static" MaxLength="150"></asp:TextBox>
                </div>
            </div>
            <div class="row" id="NotificationCityTR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>City:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationCity" runat="server" ClientIDMode="Static" MaxLength="100"></asp:TextBox>
                    <span id="NotificationCityRequired" class="ErrorMessage" style="display: none">Please enter City.</span>
                </div>
            </div>
            <div class="row" id="NotificationStateTR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>State:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationState" runat="server" ClientIDMode="Static" MaxLength="100"></asp:TextBox>
                    <span id="NotificationStateRequired" class="ErrorMessage" style="display: none">Please enter State.</span>

                </div>
            </div>
            <div class="row" id="NotificationZipTR">
                <div class="col-md-4 col-sm-6 col-xs-12">
                    <label>Zip Code:</label><font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:TextBox ID="NotificationZip" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <span id="NotificationZipRequired" class="ErrorMessage" style="display: none">Please enter zip.</span>


                </div>
            </div>

            <div class="chkbox" id="NotificationChkTR">
                <label>
                    <cms:LocalizedCheckBox ID="NotificationCheckbox" runat="server" ResourceString="Emerge.DonationForm.NotificationCheckbox" ClientIDMode="Static"></cms:LocalizedCheckBox>
                </label>
            </div>
            <div class="btnWrapper">
                <asp:HiddenField ID="IsClicked" ClientIDMode="Static" runat="server" Value="0" />
                <asp:LinkButton ID="ProcessButton" ClientIDMode="Static" Text="Submit" runat="server" CausesValidation="true" ValidationGroup="DN_VG" />
                <asp:LinkButton ID="ClearButton" ClientIDMode="Static" Text="Clear" runat="server" CausesValidation="false" />
            </div>
        </div>
    </div>
</asp:Panel>


<script type="text/javascript">
    jQuery(document).ready(function ($) {

        jQuery("input[id=NotificationZip]").mask("99999");
        jQuery("input[id*=Extension]").mask("9999");
        jQuery("input[id=Zip]").mask("99999");
        jQuery("input[id*=Phone]").mask("(999) 999-9999");
        setDonorTypeValue();
        ManageNotificationForm();
        attachedHandlers();

    });


    function resetDotNetScrollPosition() {
        var scrollX = document.getElementById('__SCROLLPOSITIONX');
        var scrollY = document.getElementById('__SCROLLPOSITIONY');

        if (scrollX != null && scrollY != null) {
            scrollX.value = 0;
            scrollY.value = 0;
        }
    }


</script>

