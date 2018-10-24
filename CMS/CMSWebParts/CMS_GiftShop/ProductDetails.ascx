<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.ascx.cs" Inherits="CMSWebParts_CMS_GiftShop_ProductDetails" %>




<asp:Panel ID="panelProductDetails" runat="server">
    <script>




        function CallFacybox() {
            jQuery(".ErrorMessage").css("display", "none");
            jQuery('#ShowPopup').modal({keyboard:true});


        }
    </script>
    <div class="giftDetailWrap clearfix">

        <div class="imageWrap">
            <asp:Image runat="server" ID="ProductImage" />
            <%--<img alt="Potted Plant" src="images/gsd_pottedPlant.jpg">--%>
        </div>

        <div class="giftDetails">
            <dl>

                <dt>
                    <cms:LocalizedLabel ID="lblProductCategory" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.ProductCategory"
                        DisplayColon="true" AssociatedControlID="CategoryID" />
                </dt>
                <dd>
                    <cms:LocalizedLabel ID="CategoryID" runat="server" EnableViewState="true" /></dd>

                <dt>
                    <cms:LocalizedLabel ID="lblProductName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.ProductName"
                        DisplayColon="true" AssociatedControlID="ProductName" />
                </dt>
                <dd>
                    <cms:LocalizedLabel ID="ProductName" runat="server" EnableViewState="true" /></dd>
                <dt>
                    <cms:LocalizedLabel ID="lblProductDescription" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.ProductDescription"
                        DisplayColon="true" AssociatedControlID="ProductDescription" />
                </dt>
                <dd>
                    <cms:LocalizedLabel ID="ProductDescription" runat="server" EnableViewState="true" /></dd>
                <dt>
                    <cms:LocalizedLabel ID="lblUnitPrice" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.UnitPrice"
                        DisplayColon="true" AssociatedControlID="UnitPrice" /></dt>
                <dd>$
                <cms:LocalizedLabel ID="UnitPrice" runat="server" EnableViewState="true" /></dd>
                <dt>
                    <cms:LocalizedLabel ID="lblPurchasedQuantity" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.PurchasedQuantity"
                        DisplayColon="false" AssociatedControlID="PurchasedQty" />
                </dt>
                <dd>
                    <cms:CMSTextBox runat="server" ID="PurchasedQty" MaxLength="4"></cms:CMSTextBox>
                    <asp:RequiredFieldValidator ID="RequiredPurchasedQty" ControlToValidate="PurchasedQty" CssClass="ErrorMessage" ErrorMessage="Please enter Quantity." runat="server" Display="Dynamic" ValidationGroup="GS_PD_VG" SetFocusOnError="true"></asp:RequiredFieldValidator>

                    <ajaxToolkit:FilteredTextBoxExtender TargetControlID="PurchasedQty" FilterType="Numbers" runat="server" ID="PurchasedQuantityFE"></ajaxToolkit:FilteredTextBoxExtender>
                </dd>
                <dt>
                    <cms:LocalizedLabel ID="lblAvailability" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.Availability"
                        DisplayColon="true" AssociatedControlID="Stock" /></dt>
                <dd>
                    <cms:LocalizedLabel ID="Stock" runat="server" EnableViewState="true" />
                </dd>
            </dl>
        </div>


    </div>


   
    
    <div id="ShowPopup" class="modal fade" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <label type="button" class="close pull-right" style="cursor:pointer" data-dismiss="modal" aria-hidden="true">&times;</label>
                    
                    <h4 class="modal-title">Please Enter Details:</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel runat="server" DefaultButton="btnProductInfo" ID="panSendProductInformation">

                        <div class="giftDetails">
                            <dl>

                                <dt>

                                    <cms:LocalizedLabel ID="lblSenderName" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.SenderName"
                                        DisplayColon="false" AssociatedControlID="SenderName" />
                                    <font color="red">*</font>

                                </dt>
                                <dd>
                                    <cms:CMSTextBox runat="server" ID="SenderName" MaxLength="80"></cms:CMSTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredSenderName" ControlToValidate="SenderName" CssClass="ErrorMessage" ErrorMessage="Please Enter Sender's Name." runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PRINFO"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularSenderName" CssClass="ErrorMessage" ValidationExpression="^[a-zA-Z-.\''-'\s]{1,30}$" runat="server" ControlToValidate="SenderName" ErrorMessage="Invalid Sender's Name." Display="Dynamic" SetFocusOnError="true" ValidationGroup="VG_GS_PRINFO"></asp:RegularExpressionValidator>


                                </dd>
                                <dt>
                                    <cms:LocalizedLabel ID="lblSenderEmail" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductDetails.Label.SenderEmail"
                                        DisplayColon="false" AssociatedControlID="SenderEmail" />
                                    <font color="red">*</font>


                                </dt>
                                <dd>
                                    <cms:CMSTextBox runat="server" ID="SenderEmail" MaxLength="100"></cms:CMSTextBox>
                                    <asp:RegularExpressionValidator ID="RegularSenderEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ViewStateMode="Enabled" runat="server" ControlToValidate="SenderEmail" ErrorMessage="Invalid Email." Display="Dynamic" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PRINFO"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredSenderEmail" ControlToValidate="SenderEmail" ErrorMessage="Please Enter Email." runat="server" Display="Dynamic" ViewStateMode="Enabled" CssClass="ErrorMessage" SetFocusOnError="true" ValidationGroup="VG_GS_PRINFO"></asp:RequiredFieldValidator>
                                </dd>
                            </dl>
                        </div>
                    </asp:Panel>
                </div>

                <div class="modal-footer">
                     <cms:LocalizedButton ID="btnProductInfo" CssClass="btn btn-primary" runat="server" CausesValidation="true" ValidationGroup="VG_GS_PRINFO" ResourceString="Emerge.GS.Button.Caption.ProductInfo"
            />
                    <%--<asp:Button ID="btnProductInfo" class="btn btn-primary" runat="server" Text="Get Product Information" CausesValidation="true" ValidationGroup="VG_GS_PRINFO" />--%>
                </div>

            </div>
        </div>

    </div>

    <div class="btnWrapper">

        <cms:LocalizedButton ID="btnAddToCart" runat="server"  CausesValidation="true" ValidationGroup="GS_PD_VG" ResourceString="Emerge.GS.Button.Caption.AddToCart" />

        <cms:LocalizedButton ID="btnSendInformation" OnClientClick="CallFacybox();return false;" runat="server" CausesValidation="false"  ResourceString="Emerge.GS.Button.Caption.SendInformation" />

        <cms:LocalizedButton ID="btnContinueShopping" runat="server" CausesValidation="false"  ResourceString="Emerge.GS.Button.Caption.Continue" />
        


    </div>

</asp:Panel>
<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>

<script type="text/javascript">
    jQuery(document).ready(function ($) {

        if ($('.FormErrorMessage').is(':visible'))
            scrollGo('.FormErrorMessage');


    });
</script>
