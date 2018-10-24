<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Cart.ascx.cs" Inherits="CMSWebParts_CMS_GiftShop_Cart" %>


<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>
<asp:Panel ID="gsCartPanel" runat="server">
    <div class="selectCard shopCart clearfix">

        <cms:LocalizedLabel ID="lblNumberOfItems" runat="server" EnableViewState="true"
            DisplayColon="false" />
        <div class="table-responsive">
            <table class="table">
                <cms:CMSRepeater ID="cartRepeater" runat="server" EnableViewState="true" >
                    <HeaderTemplate>

                        <cms:LocalizedLiteral ID="CartHeaderLiteral" runat="server"></cms:LocalizedLiteral>


                        
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                    </ItemTemplate>


                </cms:CMSRepeater>
                </tbody>
            </table>
        </div>

        <div class="grandtotal clearfix">
            <dl>
                <dt>
                    <cms:LocalizedLiteral ID="litTotal" runat="server" EnableViewState="true" ResourceString="Emerge.GS.GiftShopCart.Label.Total"
                        DisplayColon="true" /></dt>
                <dd>$&nbsp;<cms:LocalizedLiteral ID="Total" runat="server" EnableViewState="true" />
                </dd>

                <dt>
                    <cms:LocalizedLiteral ID="litTaxPercentage" runat="server" EnableViewState="true"
                        DisplayColon="true" /></dt>
                <dd>$&nbsp;<cms:LocalizedLiteral ID="TotalTaxAmount" runat="server" EnableViewState="true" /></dd>

                <dt>
                    <cms:LocalizedLiteral ID="lblGrandTotal" runat="server" EnableViewState="true" ResourceString="Emerge.GS.GiftShopCart.Label.GrandTotal"
                        DisplayColon="true" /></dt>
                <dd>$&nbsp;<cms:LocalizedLiteral ID="GrandTotal" runat="server" EnableViewState="true" /></dd>
            </dl>
        </div>
    </div>

    <div class="btnWrapper">
         <cms:LocalizedButton ID="cmdChekOut" runat="server" CausesValidation="false" ResourceString="Emerge.GS.Button.Caption.CheckOut"
            />
        <cms:LocalizedButton ID="cmdContinue" runat="server" CausesValidation="false" ResourceString="Emerge.GS.Button.Caption.Continue"
            />
        <%--<asp:Button ID="cmdChekOut" Text="Check Out" CausesValidation="false" runat="server" />--%>
        <%--<asp:Button ID="cmdContinue" Text="Continue Shopping" CausesValidation="false" runat="server" />--%>

    </div>
</asp:Panel>
<script type="text/javascript">
    jQuery(document).ready(function ($) {

        if ($('.FormErrorMessage').is(':visible'))
            scrollGo('.FormErrorMessage');


    });
</script>
