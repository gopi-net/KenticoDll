<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheerCardForm.ascx.cs" Inherits="CMSWebParts_CMS_CheerCard_Mobile_CheerCardForm" %>



<asp:MultiView ID="cheercardMV" runat="server">

    <asp:View ID="CheerCardFormView" runat="server">


        <h2>Send Card to: </h2>

        <label>
            <cms:LocalizedRadioButton ID="radioHospital" onclick="ShowHidePatientRow();" Checked="true" EnableViewState="true" runat="server" GroupName="CardSendTo" />
            <cms:LocalizedLiteral ID="litRadioHospital" runat="server" ResourceString="Emerge.CC.CheerCardForm.Radio.Hospital" />
        </label>
        <label>
            <cms:LocalizedRadioButton onclick="ShowHidePatientRow();" ID="radioEmailAddress" runat="server" GroupName="CardSendTo" />
            <cms:LocalizedLiteral ID="litRadioEmailAddress" runat="server" ResourceString="Emerge.CC.CheerCardForm.Radio.EmailAddress" />
        </label>
        <asp:Panel ID="formPanelWithButtons" DefaultButton="cmdPreview" runat="server">
            <section class="formWrapper">
                <asp:Panel ID="panelCheerCardForm" runat="server">
                    <cms:LocalizedLabel ID="ErrorLabel" runat="server" EnableViewState="false" />
                    <cms:LocalizedHidden ID="hdnIsMailToPatient" runat="server" />

                    <label>

                        <cms:LocalizedLiteral ID="litSenderName" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.SenderName" />

                        <cms:CMSTextBox runat="server" ID="SenderName" MaxLength="50"></cms:CMSTextBox>
                        <asp:RequiredFieldValidator ID="RequiredSenderName" ControlToValidate="SenderName" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularSenderName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.''-'\s]{1,50}$" runat="server" ControlToValidate="SenderName" ErrorMessage="Invalid Sender Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
                    </label>

                    <label>
                        <cms:LocalizedLiteral ID="litSenderEmail" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.SenderEmail" />



                        <cms:CMSTextBox runat="server" ID="SenderEmail" MaxLength="80"></cms:CMSTextBox>
                        <asp:RequiredFieldValidator ID="RequiredSenderEmail" ControlToValidate="SenderEmail" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender Email." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularSenderEmail" CssClass="ErrorMessage" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" ControlToValidate="SenderEmail" ErrorMessage="Invalid Sender Email." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>

                    </label>
                    <label>

                        <cms:LocalizedLiteral ID="litPhone" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.Phone" />

                        <cms:CMSTextBox runat="server" ID="Phone" MaxLength="12"></cms:CMSTextBox>
                        <asp:RegularExpressionValidator ID="RegularPhone" runat="server" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="Invalid Phone."
                            ControlToValidate="Phone" ValidationExpression="(^\d{3}-\d{3}-\d{4}$)|(___-___-____)" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litExtension" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.Extension" />


                        <cms:CMSTextBox runat="server" ID="Extension" MaxLength="12" CssClass="extension_width">

                        </cms:CMSTextBox>

                        <asp:RegularExpressionValidator ID="RegularExtension" runat="server" CssClass="ErrorMessage"
                            Display="Dynamic" ErrorMessage="Invalid Ext."
                            ControlToValidate="Extension" ValidationExpression="(^\d{1,4}$)|(____)" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>


                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litHospital" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.HospitalName" />




                        <cms:CMSTextBox runat="server" ID="Hospital" MaxLength="60">

                        </cms:CMSTextBox>

                        <asp:RequiredFieldValidator ID="RequiredHospital" ControlToValidate="Hospital" CssClass="ErrorMessage" ErrorMessage="Please Enter Hospital Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litPatientFirstName" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.PatientFirstName" />



                        <cms:CMSTextBox runat="server" ID="PatientFirstName" MaxLength="30">

                        </cms:CMSTextBox>
                        <asp:RegularExpressionValidator ID="RegularPatientFirstName" ValidationExpression="^[a-zA-Z-.''-'\s]{1,30}$" CssClass="ErrorMessage" runat="server" ControlToValidate="PatientFirstName" ErrorMessage="Invalid Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredPatientFirstName" ControlToValidate="PatientFirstName" CssClass="ErrorMessage" ErrorMessage="Please Enter Patient First Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litPatientLastName" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.PatientLastName" />

                        <cms:CMSTextBox runat="server" ID="PatientLastName" MaxLength="30">

                        </cms:CMSTextBox>
                        <asp:RegularExpressionValidator ID="RegularPatientLastName" ValidationExpression="^[a-zA-Z-.''-'\s]{1,30}$" runat="server" CssClass="ErrorMessage" ControlToValidate="PatientLastName" ErrorMessage="Invalid Name." Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredPatientLastName" ControlToValidate="PatientLastName" CssClass="ErrorMessage" ErrorMessage="Please Enter Patient Last Name." runat="server" Display="Dynamic" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </label>
                    <label id="RowPatientEmail">
                        <cms:LocalizedLiteral ID="litPatientEmail" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.PatientEmail" />



                        <cms:CMSTextBox runat="server" ID="PatientEmail" MaxLength="80">

                        </cms:CMSTextBox>

                        <asp:RequiredFieldValidator ID="RequiredPatientEmail" ControlToValidate="PatientEmail" ErrorMessage="Please Enter Patient Email." runat="server" Display="Dynamic" ViewStateMode="Enabled" CssClass="ErrorMessage" ValidationGroup="CCF_VG"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularPatientEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ViewStateMode="Enabled" runat="server" ControlToValidate="PatientEmail" ErrorMessage="Invalid Patient Email." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CCF_VG" SetFocusOnError="true"></asp:RegularExpressionValidator>


                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litRoomNumber" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.RoomNumber" />

                        <span id="spnRoomNumberAst"><font color="red">*</font></span>
                        <cms:CMSTextBox runat="server" ID="RoomNumber" MaxLength="10">

                        </cms:CMSTextBox>
                        <asp:RegularExpressionValidator ID="RegularRoomNumber" ValidationExpression="^[a-zA-Z0-9]+$" runat="server" ControlToValidate="RoomNumber" ErrorMessage="Invalid Room Number." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="CCF_VG"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredRoomNumber" ControlToValidate="RoomNumber" CssClass="ErrorMessage" ErrorMessage="Please Enter Room Number." runat="server" Display="Dynamic" ValidationGroup="CCF_VG"></asp:RequiredFieldValidator>
                    </label>
                    <label>
                        <cms:LocalizedLiteral ID="litMessage" runat="server" ResourceString="Emerge.CC.CheerCardForm.Literal.Message" />

                        <cms:CMSTextBox runat="server" ID="Message" MaxLength="300" Columns="24" onKeyUp="SetMaxLimit(this,300)" onChange="SetMaxLimit(this,300)"> 
                        </cms:CMSTextBox>
                    </label>


                </asp:Panel>
            </section>
            <section class="btnWrapper">

                <asp:LinkButton ID="cmdBackToCheerCardListing" class="btn btn-default" runat="server" CausesValidation="false">

                    <cms:LocalizedLiteral ID="BackToCheerCardListingButtonLit" runat="server" ResourceString="Emerge.CC.Mobile.CheerCardList.BackToCheerCardListingButton.Text">
                    </cms:LocalizedLiteral>

                </asp:LinkButton>

                <cms:LocalizedLinkButton runat="server" class="btn btn-default" ID="cmdClear" ResourceString="Emerge.CC.Mobile.CheerCardList.ClearButton.Text">
                </cms:LocalizedLinkButton>

                <asp:LinkButton ID="cmdPreview" class="btn btn-primary" ValidationGroup="CCF_VG" runat="server" CausesValidation="true">

                    <cms:LocalizedLiteral ID="Preview" runat="server" ResourceString="Emerge.CC.Mobile.CheerCardList.PreviewButton.Text">
                    </cms:LocalizedLiteral>

                </asp:LinkButton>



            </section>
        </asp:Panel>
    </asp:View>
    <asp:View ID="CheerCardPreview" runat="server">

        <h2>Preview your card</h2>
        <asp:Panel ID="panCheerCardPreview" runat="server">

            <cms:LocalizedLiteral ID="previewCheerCard" runat="server"></cms:LocalizedLiteral>


            <%-- <div class="clearfix">
                <asp:Image ID="previewImage" runat="server" />
            </div>--%>
        </asp:Panel>

        <section class="btnWrapper">
            <asp:LinkButton ID="cmdBackToForm" class="btn btn-default" runat="server" CausesValidation="false">
                <cms:LocalizedLiteral ID="BackToFormLit" runat="server" ResourceString="Emerge.CC.Mobile.CheerCardList.BackToForm.Text">
                </cms:LocalizedLiteral>

            </asp:LinkButton>

            <asp:LinkButton ID="cmdSend" class="btn btn-primary" runat="server" CausesValidation="true" ValidationGroup="CCF_VG">
                <cms:LocalizedLiteral ID="SendLit" runat="server" ResourceString="Emerge.CC.Mobile.CheerCardList.Send.Text">
                </cms:LocalizedLiteral>

            </asp:LinkButton>


        </section>
    </asp:View>
</asp:MultiView>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />

<script type="text/javascript">

    SetCheerCardMaskFields();
    jQuery(document).ready(function ($) {
        ShowHidePatientRow();
    });

</script>
