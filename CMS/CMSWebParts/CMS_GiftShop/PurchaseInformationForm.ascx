<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseInformationForm.ascx.cs" Inherits="CMSWebParts_CMS_GiftShop_PurchaseInformationForm" %>
<asp:Panel ID="panelPurchaseInformationForm" DefaultButton="btnProceedToPayment" runat="server">


    <div class="formWrap">

            <div class="row title">
                <div class="col-md-3 col-sm-6 col-xs-12">Delivery Information<hr>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblGiftFor" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.GiftFor"
                                        DisplayColon="true" AssociatedControlID="GiftID" />

                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">


                    <cms:CMSQueryDataSource ID="GiftID_Datasource" runat="server"
                                            QueryName="customtable.Emerge_{0}_GS_GiftsFor.GetGiftsFor" />
                    <cms:LocalizedDropDownList ID="GiftID" DataTextField="GiftFor" DataValueField="ItemID" runat="server"></cms:LocalizedDropDownList>


                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredGiftID" runat="server" ControlToValidate="GiftID" ErrorMessage="Please Select Gift For." CssClass="ErrorMessage" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator></div>
            </div>


            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblRecipientFName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.RecipientFName"
                                        DisplayColon="true" AssociatedControlID="RecipientFName" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="RecipientFName" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredRecipientFName" runat="server" ControlToValidate="RecipientFName" ErrorMessage="Please Enter Patient/Recipient's First Name." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularRecipientFName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.\''-'\s]{1,30}$" runat="server" ControlToValidate="RecipientFName" ErrorMessage="Invalid First Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>


                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblRecipientLName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.RecipientLName"
                                        DisplayColon="true" AssociatedControlID="RecipientLName" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="RecipientLName" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredRecipientLName" runat="server" ControlToValidate="RecipientLName" ErrorMessage="Please Enter Patient/Recipient's Last Name." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularRecipientLName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.\''-'\s]{1,30}$" runat="server" ControlToValidate="RecipientLName" ErrorMessage="Invalid Last Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblHospitalLocation" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.HospitalLocation"
                                        DisplayColon="true" AssociatedControlID="HospitalLocationID" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSQueryDataSource ID="HospitalLocationID_Datasource" runat="server"
                                            QueryName="customtable.Emerge_{0}_GS_HospitalLocations.GetHospitalLocations" />
                    <cms:LocalizedDropDownList ID="HospitalLocationID" DataTextField="HospitalName" DataValueField="ItemID" runat="server"></cms:LocalizedDropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12"></div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblRoomNumber" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.RoomNumber"
                                        DisplayColon="true" AssociatedControlID="RecipientRoomNumber" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="RecipientRoomNumber" MaxLength="10"></cms:CMSTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularRecipientRoomNumber" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Room Number."
                                                    ControlToValidate="RecipientRoomNumber" ValidationExpression="^[a-zA-Z\-0-9]*$" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>


                </div>

            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">


                    <cms:LocalizedLabel ID="lblRecipientPhone" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.RecipientPhoneNumber"
                                        DisplayColon="true" AssociatedControlID="RecipientPhoneNumber" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="RecipientPhoneNumber" CssClass="PhoneNumber" MaxLength="12"></cms:CMSTextBox>
                    <%--  <ajaxToolkit:MaskedEditExtender ID="maskedRecipientPhoneNumber" ClearMaskOnLostFocus="false"     runat="server" Mask="999-999-9999" TargetControlID="RecipientPhoneNumber"></ajaxToolkit:MaskedEditExtender>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularRecipientPhoneNumber" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Phone."
                                                    ControlToValidate="RecipientPhoneNumber" ValidationExpression="(^\d{3}-\d{3}-\d{4}$)" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredRecipientPhoneNumber" runat="server" ControlToValidate="RecipientPhoneNumber" ErrorMessage="Please Enter Patient/Recipient's Phone Number." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator></div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblRecipientEmail" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.RecipientsEmailaddress"
                                        DisplayColon="true" AssociatedControlID="RecipientsEmailaddress" />
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="RecipientsEmailaddress" MaxLength="100"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <%-- <asp:RequiredFieldValidator ID="RequiredRecipientsEmailaddress" ControlToValidate="RecipientsEmailaddress" ErrorMessage="Please Enter Recipient's Email." runat="server" Display="Dynamic" ViewStateMode="Enabled" CssClass="ErrorMessage" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularRecipientsEmailaddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ViewStateMode="Enabled" runat="server" ControlToValidate="RecipientsEmailaddress" ErrorMessage="Invalid Recipient's Email." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>



                </div>
            </div>





            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblMessage" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.PersonalizedMessage"
                                        DisplayColon="true" AssociatedControlID="PersonalizedMessage" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <cms:CMSTextBox runat="server" ID="PersonalizedMessage" MaxLength="500" onKeyUp="SetMaxLimit(this,500)" onChange="SetMaxLimit(this,500)" TextMode="MultiLine" Rows="3" Columns="24">
                    </cms:CMSTextBox>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblTheme" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.ThemeID"
                                        DisplayColon="true" AssociatedControlID="ThemeID" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSQueryDataSource ID="ThemeID_Datasource" runat="server"
                                            QueryName="customtable.Emerge_{0}_GS_GiftShopThemes.GetThemes" />
                    <cms:LocalizedDropDownList ID="ThemeID" DataTextField="Theme" DataValueField="ItemID" runat="server"></cms:LocalizedDropDownList></div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblSpecialRequest" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SpecialRequest"
                                        DisplayColon="true" AssociatedControlID="SpecialRequest" />
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <cms:CMSTextBox runat="server" ID="SpecialRequest" MaxLength="500" onKeyUp="SetMaxLimit(this,500)" onChange="SetMaxLimit(this,500)" TextMode="MultiLine" Rows="3" Columns="24">
                    </cms:CMSTextBox>

                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
            </div>
            <div class="row title">
                <div class="col-md-6 col-sm-6 col-xs-12">Sender's Information<hr>
                </div>

            </div>


            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblSenderFName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SenderFName"
                                        DisplayColon="true" AssociatedControlID="SenderFName" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderFName" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredSenderFName" ControlToValidate="SenderFName" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender's First Name." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularSenderFName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.''-'\s]{1,30}$" runat="server" ControlToValidate="SenderFName" ErrorMessage="Invalid Sender's First Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>



                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblSenderLName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SenderLName"
                                        DisplayColon="true" AssociatedControlID="SenderLName" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderLName" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredSenderLName" ControlToValidate="SenderLName" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender's Last Name." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularSenderLName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.\''-'\s]{1,30}$" runat="server" ControlToValidate="SenderLName" ErrorMessage="Invalid Sender's Last Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>



                </div>
            </div>



            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblHomeAddress" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.HomeAddress"
                                        DisplayColon="true" AssociatedControlID="SenderHomeAddressStreet" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderHomeAddressStreet" placeholder="* Street" MaxLength="50"></cms:CMSTextBox>

                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressStreetIE" InitialValue="* Street" ControlToValidate="SenderHomeAddressStreet" CssClass="ErrorMessage" ErrorMessage="Please Enter Street." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressStreet" ControlToValidate="SenderHomeAddressStreet" CssClass="ErrorMessage" ErrorMessage="Please Enter Street." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularSenderHomeAddressStreet" runat="server" ControlToValidate="SenderHomeAddressStreet"
                                                    ErrorMessage="Please enter char(a-z, 0-9, dash, period, comma, #) for Street."
                                                    SetFocusOnError="True" Display="Dynamic" ValidationExpression="^([a-zA-Z0-9-.,#\s]*|\* Street)$" CssClass="ErrorMessage" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>


                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderHomeAddressCity" placeholder="* City" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressCityIE" InitialValue="* City" ControlToValidate="SenderHomeAddressCity" CssClass="ErrorMessage" ErrorMessage="Please Enter City." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressCity" ControlToValidate="SenderHomeAddressCity" CssClass="ErrorMessage" ErrorMessage="Please Enter City." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularSenderHomeAddressCity" runat="server" ControlToValidate="SenderHomeAddressCity"
                                                    ErrorMessage="Please enter char(a-z, dash) for City." SetFocusOnError="True" CssClass="ErrorMessage"
                                                    Display="Dynamic" ValidationExpression="^([a-zA-Z-\s]*|\* City)$" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>


                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSQueryDataSource ID="SenderHomeAddressStateID_Datasource" runat="server"
                                            QueryName="customtable.Emerge_{0}_GS_States.GetStates" />
                    <cms:LocalizedDropDownList ID="SenderHomeAddressStateID" DataValueField="ItemID" DataTextField="StateCode" runat="server"></cms:LocalizedDropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressStateID" runat="server" ControlToValidate="SenderHomeAddressStateID" ErrorMessage="Please Select State." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator></div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">


                    <cms:CMSTextBox runat="server" ID="SenderHomeAddressZipCode" CssClass="Zip" placeholder="* Zip" MaxLength="5"></cms:CMSTextBox>

                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularSenderHomeAddressZipCode" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Zip."
                                                    ControlToValidate="SenderHomeAddressZipCode" ValidationExpression="^(\d{5}|\* Zip)$" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressZipCode" runat="server" ControlToValidate="SenderHomeAddressZipCode" ErrorMessage="Please Enter Zip." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>

                    <asp:RequiredFieldValidator ID="RequiredSenderHomeAddressZipCodeIE" InitialValue="* Zip" ControlToValidate="SenderHomeAddressZipCode" CssClass="ErrorMessage" ErrorMessage="Please Enter Zip." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>


                </div>

            </div>


            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:LocalizedCheckBox ID="chkBillingAddress" CssClass="checkBoxForSameAddress" runat="server" CausesValidation="false" Text="Billing address is same as Home address" />
                </div>
            </div>
            <div class="row" id="billingAddressStreetRow">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblBillingAddress" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.BillingAddress"
                                        DisplayColon="true" AssociatedControlID="BillingAddressStreet" />
                    <font color="red">*</font>

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="BillingAddressStreet" placeholder="* Street" MaxLength="50"></cms:CMSTextBox>

                </div>
            </div>
            <div class="row" id="billingAddressStreetVal">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredBillingAddressStreet" ControlToValidate="BillingAddressStreet" CssClass="ErrorMessage" ErrorMessage="Please Enter Street." runat="server" Display="Dynamic" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredBillingAddressStreetIE" InitialValue="* Street" ControlToValidate="BillingAddressStreet" CssClass="ErrorMessage" ErrorMessage="Please Enter Street." runat="server" Display="Dynamic" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularBillingAddressStreet" runat="server" ControlToValidate="BillingAddressStreet"
                                                    ErrorMessage="Please enter char(a-z, 0-9, dash, period, comma, #) for Street." CssClass="ErrorMessage"
                                                    SetFocusOnError="True" Display="Dynamic" ValidationExpression="^([a-zA-Z0-9-.,#\s]*|\* Street)$" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                </div>
            </div>

            <div class="row"  id="billingAddressCityRow">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="BillingAddressCity" placeholder="* City" MaxLength="30"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row" id="billingAddressCityVal">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <asp:RequiredFieldValidator ID="RequiredBillingAddressCityIE" InitialValue="* City" ControlToValidate="BillingAddressCity" CssClass="ErrorMessage" ErrorMessage="Please Enter City." runat="server" Display="Dynamic" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredBillingAddressCity" ControlToValidate="BillingAddressCity" CssClass="ErrorMessage" ErrorMessage="Please Enter City." runat="server" Display="Dynamic" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularBillingAddressCity" runat="server" ControlToValidate="BillingAddressCity"
                                                    ErrorMessage="Please enter char(a-z, dash) for City." CssClass="ErrorMessage"
                                                    Display="Dynamic" ValidationExpression="^([a-zA-Z-\s]*|\* City)$" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>


                </div>
            </div>
            <div class="row"  id="billingAddressStateRow">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSQueryDataSource ID="BillingAddressStateID_Datasource" runat="server"
                                            QueryName="customtable.Emerge_{0}_GS_States.GetStates" />
                    <cms:LocalizedDropDownList ID="BillingAddressStateID" DataValueField="ItemID" DataTextField="StateCode" runat="server"></cms:LocalizedDropDownList>
                </div>
            </div>

            <div class="row"  id="billingAddressStateVal">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredBillingAddressStateID" runat="server" ControlToValidate="BillingAddressStateID" ErrorMessage="Please Select State." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator></div>
            </div>

            <div class="row"  id="billingAddressZipRow">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">


                    <cms:CMSTextBox runat="server" ID="BillingAddressZipCode" CssClass="Zip" placeholder="* Zip" MaxLength="5"></cms:CMSTextBox>

                </div>
            </div>

            <div class="row"  id="billingAddressZipVal">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularBillingAddressZipCode" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Zip."
                                                    ControlToValidate="BillingAddressZipCode" ValidationExpression="^(\d{5}|\* Zip)$" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredBillingAddressZipCode" runat="server" ControlToValidate="BillingAddressZipCode" ErrorMessage="Please Enter Zip." Display="Dynamic" CssClass="ErrorMessage" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>

                    <asp:RequiredFieldValidator ID="RequiredBillingAddressZipCodeIE" InitialValue="* Zip" ControlToValidate="BillingAddressZipCode" CssClass="ErrorMessage" ErrorMessage="Please Enter Zip." runat="server" Display="Dynamic" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>


                </div>

            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblSenderHomePhoneNumber" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SenderPhoneNumber"
                                        DisplayColon="true" AssociatedControlID="SenderHomePhoneNumber" />
                    <font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderHomePhoneNumber" CssClass="PhoneNumber" MaxLength="12"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularSenderHomePhoneNumber" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Phone."
                                                    ControlToValidate="SenderHomePhoneNumber" ValidationExpression="(^\d{3}-\d{3}-\d{4}$)" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                    <asp:RequiredFieldValidator ID="RequiredSenderHomePhoneNumber" runat="server" ControlToValidate="SenderHomePhoneNumber" ErrorMessage="Please Enter Phone Number." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator></div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <cms:LocalizedLabel ID="lblSenderMobileNumber" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SenderMobileNumber"
                                        DisplayColon="true" AssociatedControlID="SenderMobileNumber" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderMobileNumber" CssClass="PhoneNumber" MaxLength="12"></cms:CMSTextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RegularExpressionValidator ID="RegularSenderMobileNumber" runat="server" CssClass="ErrorMessage"
                                                    Display="Dynamic" ErrorMessage="Invalid Mobile."
                                                    ControlToValidate="SenderMobileNumber" ValidationExpression="(^\d{3}-\d{3}-\d{4}$)" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>

                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblSenderEmailAddress" runat="server" EnableViewState="false" ResourceString="Emerge.GS.PurchaseInformation.Label.Message.SenderEmailAddress"
                                        DisplayColon="true" AssociatedControlID="SenderEmailAddress" />
                    <font color="red">*</font>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <cms:CMSTextBox runat="server" ID="SenderEmailAddress" MaxLength="100"></cms:CMSTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12"></div>
                <div class="col-md-6 col-sm-6 col-xs-12">
                    <asp:RequiredFieldValidator ID="RequiredSenderEmailAddress" ControlToValidate="SenderEmailAddress" ErrorMessage="Please Enter Email." runat="server" Display="Dynamic" ViewStateMode="Enabled" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularSenderEmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ViewStateMode="Enabled" runat="server" ControlToValidate="SenderEmailAddress" ErrorMessage="Invalid Email." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PIF"></asp:RegularExpressionValidator>



                </div>
            </div>


        <div class="clearfix">
        <div class="btnWrapper clearfix pull-left">
            <cms:LocalizedButton ID="btnProceedToPayment" runat="server" CausesValidation="true" ValidationGroup="VG_GS_PIF" ResourceString="Emerge.GS.Button.Caption.ProceedToPayment" />
            <cms:LocalizedButton ID="btnClear" runat="server" CausesValidation="false" ResourceString="Emerge.GS.Button.Caption.ClearForm" />
        </div>
         <div class="btnWrapper clearfix pull-right btn-prev">
            <cms:LocalizedLinkButton ID="btnBackToCart" ResourceString="Emerge.CC.CheerCardList.BackToCheerCardListingButton.Text" runat="server" CausesValidation="false">
            </cms:LocalizedLinkButton>

            <%-- <a href="#"><span class="icon-leftArrowGrey"></span>Back</a>--%>
        </div>

        </div>
    </div>

</asp:Panel>

<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>

<script type="text/javascript">
    jQuery(document).ready(function ($) {


        bindHandlerToSameBillingAddressCheckbox();

        if (jQuery(".checkBoxForSameAddress :checkbox").is(':checked')) {
            copyHomeAddressToBillingAddress();
            hideBillingAddress();
        }
        else {
            showBillingAddress();
        }
    });
    jQuery('input[type=text], textarea').placeholder();
    setMaskPurchaseInformationForm();
</script>
