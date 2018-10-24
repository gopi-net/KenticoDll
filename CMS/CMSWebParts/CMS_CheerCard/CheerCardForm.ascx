<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheerCardForm.ascx.cs" Inherits="CMSWebParts_CMS_CheerCard_CheerCardForm" %>


<div class="box-cheerCard-Form">

    <asp:MultiView ID="cheercardMV" runat="server">

        <asp:View ID="CheerCardFormView" runat="server">


            <h3>Send Card to: </h3>
            <label>
                <cms:LocalizedRadioButton onclick="ShowHidePatientRow();" Checked="true" EnableViewState="true" ID="radioHospital" ResourceString="Emerge.CC.CheerCardForm.Radio.Hospital" runat="server" GroupName="CardSendTo" />

            </label>
            <label>
                <cms:LocalizedRadioButton onclick="ShowHidePatientRow();" ID="radioEmailAddress" ResourceString="Emerge.CC.CheerCardForm.Radio.EmailAddress" runat="server" GroupName="CardSendTo" />

            </label>
            <asp:Panel ID="formPanelWithButtons" runat="server" DefaultButton="cmdPreview">
                <div class="formWrapper">
                    <asp:Panel ID="panelCheerCardForm" runat="server">

                        <cms:LocalizedHidden ID="hdnIsMailToPatient" runat="server" />
                    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblSenderName" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.SenderName"
                                DisplayColon="true" AssociatedControlID="SenderName" />
            <font color="red">*</font>
        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">

            <cms:CMSTextBox runat="server" ID="SenderName" MaxLength="50"></cms:CMSTextBox>

            <asp:RequiredFieldValidator ID="RequiredSenderName" ControlToValidate="SenderName" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularSenderName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.''-'\s]{1,50}$" runat="server" ControlToValidate="SenderName" ErrorMessage="Invalid Sender Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>

        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblSenderEmail" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.SenderEmail"
                                DisplayColon="true" AssociatedControlID="SenderEmail" />
            <font color="red">*</font>
        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">
            <cms:CMSTextBox runat="server" ID="SenderEmail" MaxLength="80"></cms:CMSTextBox>
            <asp:RequiredFieldValidator ID="RequiredSenderEmail" ControlToValidate="SenderEmail" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender Email." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="RegularSenderEmail" CssClass="ErrorMessage" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ControlToValidate="SenderEmail" ErrorMessage="Invalid Sender Email." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>

        </div>
    </div>
    <div class="phone-number row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblPhone" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.Phone"
                                DisplayColon="true" AssociatedControlID="Phone" /></div>
        <div class="col-md-5 col-sm-6 col-xs-12">

            <cms:CMSTextBox runat="server" ID="Phone" MaxLength="12"></cms:CMSTextBox>

            <cms:LocalizedLabel ID="lblExtension" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.Extension"
                                DisplayColon="true" AssociatedControlID="Extension" />
            <cms:CMSTextBox runat="server" ID="Extension" MaxLength="12" CssClass="extension_width">

            </cms:CMSTextBox>

            <asp:RegularExpressionValidator ID="RegularPhone" runat="server" CssClass="ErrorMessage"
                                            Display="Dynamic" ErrorMessage="Invalid Phone."
                                            ControlToValidate="Phone" ValidationExpression="(^\d{3}-\d{3}-\d{4}$)|(___-___-____)" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>

            <asp:RegularExpressionValidator ID="RegularExtension" runat="server" CssClass="ErrorMessage"
                                            Display="Dynamic" ErrorMessage="Invalid Ext."
                                            ControlToValidate="Extension" ValidationExpression="(^\d{1,4}$)|(____)" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>



        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblHospital" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.HospitalName"
                                DisplayColon="true" AssociatedControlID="Hospital" />
            <font color="red">*</font>

        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">
            <cms:CMSTextBox runat="server" ID="Hospital" MaxLength="60">

            </cms:CMSTextBox>

            <asp:RequiredFieldValidator ID="RequiredHospital" ControlToValidate="Hospital" CssClass="ErrorMessage" ErrorMessage="Please Enter Hospital Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblPatientFirstName" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.PatientFirstName"
                                DisplayColon="true" AssociatedControlID="PatientFirstName" />
            <font color="red">*</font>


        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">

            <cms:CMSTextBox runat="server" ID="PatientFirstName" MaxLength="30">

            </cms:CMSTextBox>
            <asp:RegularExpressionValidator ID="RegularPatientFirstName" ValidationExpression="^[a-zA-Z-.''-'\s]{1,30}$" CssClass="ErrorMessage" runat="server" ControlToValidate="PatientFirstName" ErrorMessage="Invalid Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredPatientFirstName" ControlToValidate="PatientFirstName" CssClass="ErrorMessage" ErrorMessage="Please Enter Patient First Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>

        </div>
    </div>

    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblPatientLastName" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.PatientLastName"
                                DisplayColon="true" AssociatedControlID="PatientLastName" />
            <font color="red">*</font>

        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">

            <cms:CMSTextBox runat="server" ID="PatientLastName" MaxLength="30">

            </cms:CMSTextBox>
            <asp:RequiredFieldValidator ID="RequiredPatientLastName" ControlToValidate="PatientLastName" CssClass="ErrorMessage" ErrorMessage="Please Enter Patient Last Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularPatientLastName" ValidationExpression="^[a-zA-Z-.''-'\s]{1,30}$" runat="server" CssClass="ErrorMessage" ControlToValidate="PatientLastName" ErrorMessage="Invalid Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>


        </div>
    </div>
    <div id="RowPatientEmail" class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblPatientEmail" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.PatientEmail"
                                DisplayColon="true" AssociatedControlID="PatientEmail" />
            <font color="red">*</font>

        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">

            <cms:CMSTextBox runat="server" ID="PatientEmail" MaxLength="80">

            </cms:CMSTextBox>

            <asp:RequiredFieldValidator ID="RequiredPatientEmail" ControlToValidate="PatientEmail" ErrorMessage="Please Enter Patient Email." runat="server" Display="Dynamic" ViewStateMode="Enabled" CssClass="ErrorMessage" ValidationGroup="CCF_VG"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularPatientEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ViewStateMode="Enabled" runat="server" ControlToValidate="PatientEmail" ErrorMessage="Invalid Patient Email." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CCF_VG"></asp:RegularExpressionValidator>

        </div>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblRoomNumber" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.RoomNumber"
                                DisplayColon="true" AssociatedControlID="RoomNumber" />

            <span id="spnRoomNumberAst"><font color="red">*</font></span>

        </div>
        <div class="col-md-5 col-sm-6 col-xs-12">
            <cms:CMSTextBox runat="server" ID="RoomNumber" MaxLength="10">

            </cms:CMSTextBox>
            <asp:RegularExpressionValidator ID="RegularRoomNumber" ValidationExpression="^[a-zA-Z0-9]+$" runat="server" ControlToValidate="RoomNumber" ErrorMessage="Invalid Room Number." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredRoomNumber" ControlToValidate="RoomNumber" CssClass="ErrorMessage" ErrorMessage="Please Enter Room Number." runat="server" Display="Dynamic" ValidationGroup="CCF_VG"></asp:RequiredFieldValidator>

        </div>
    </div>



    <div class="row">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <cms:LocalizedLabel ID="lblMessage" runat="server" EnableViewState="false" ResourceString="Emerge.CC.CheerCardForm.Label.Message"
                                DisplayColon="true" AssociatedControlID="Message" /></div>
        <div class="col-md-5 col-sm-6 col-xs-12">
            <cms:CMSTextBox runat="server" ID="Message" MaxLength="300" onKeyUp="SetMaxLimit(this,300)" onChange="SetMaxLimit(this,300)" TextMode="MultiLine" Rows="3" Columns="24">
            </cms:CMSTextBox>

        </div>
    </div>


                    </asp:Panel>
                </div>
                <div class="btnWrapper">
                    <cms:LocalizedLinkButton ID="cmdBackToCheerCardListing" ResourceString="Emerge.CC.CheerCardList.BackToCheerCardListingButton.Text" runat="server" CausesValidation="false">
                    </cms:LocalizedLinkButton>

                    <cms:LocalizedLinkButton runat="server" ID="cmdClear" ResourceString="Emerge.CC.CheerCardList.ClearButton.Text">
                    </cms:LocalizedLinkButton>

                    <cms:LocalizedLinkButton CssClass="btn-green" runat="server" ID="cmdPreview" CausesValidation="true" ValidationGroup="CCF_VG" ResourceString="Emerge.CC.CheerCardList.Preview.Text">
                    </cms:LocalizedLinkButton>



                </div>
            </asp:Panel>
            <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />

        </asp:View>



        <asp:View ID="CheerCardPreview" runat="server">

            <h3>Preview your card</h3>

            <div class="previewCard">
                <div>
                    <cms:LocalizedLiteral ID="previewCheerCard" runat="server"></cms:LocalizedLiteral>
                </div>

            </div>
            <div class="btnWrapper">
                <cms:LocalizedLinkButton ID="cmdBackToCheerCardForm" ResourceString="Emerge.CC.CheerCardList.BackToCheerCardFormButton.Text" runat="server" CausesValidation="false">
                </cms:LocalizedLinkButton>
                <cms:LocalizedLinkButton CssClass="btn-green" runat="server" ID="cmdSend" CausesValidation="true" ValidationGroup="CCF_VG" ResourceString="Emerge.CC.CheerCardList.Send.Text">
                </cms:LocalizedLinkButton>

            </div>


        </asp:View>

    </asp:MultiView>





</div>












<script type="text/javascript">

    SetCheerCardMaskFields();


    jQuery(document).ready(function ($) {
        ShowHidePatientRow();
    });

</script>
