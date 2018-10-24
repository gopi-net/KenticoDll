<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfirmationMessage.ascx.cs" Inherits="CMSWebParts_CMS_GiftShop_ConfirmationMessage" %>
<div class="message_box">
         <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
    </div>
<asp:Panel ID="panelConfirmationMessage" runat="server">
    
    <div class="selectCard shopCart clearfix">

        <cms:CMSRepeater ID="cartRepeater" runat="server" EnableViewState="true">
            <HeaderTemplate>
                <div>
                    <table>

                        <cms:LocalizedLiteral ID="CartHeaderLiteral" runat="server"></cms:LocalizedLiteral>

                       
                        <tbody>
            </HeaderTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </div>
      
            </FooterTemplate>

        </cms:CMSRepeater>

        <cms:CMSRepeater ID="emailCartRepeater" runat="server" Visible="false">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="4" width="100%" bordercolor="#4d7427" border="1" bgcolor="71ac39" >
                    <thead>
                        <tr>
                            <td ><span class="style6">Item Code</span></td>
                            <td class="itemName" ><span class="style6">Item Name</span></td>
                            <td class="qnty" style="text-align: right"><span class="style6">Qty</span></td>
                            <td class="price" style="text-align: right"><span class="style6">Price</span></td>
                            <td class="total" style="text-align: right"><span class="style6">Total</span></td>

                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
           
      
            </FooterTemplate>

        </cms:CMSRepeater>


        <cms:CMSRepeater ID="productsWithNegativeStockRepeater" runat="server" Visible="false">
            <HeaderTemplate>
                <table cellspacing="0" bordercolor="#E5E4E2" border="1" width="600">
                    <thead >
                        <tr>

                            <td style="text-align: center">Item Code</td>
                            <td style="text-align: center">Item Name</td>
                            <td style="text-align: right">Purchased Qty</td>
                            <td style="text-align: right">Item Stock</td>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate></ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </cms:CMSRepeater>

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
                    <cms:LocalizedLiteral ID="litGrandTotal" runat="server" EnableViewState="true" ResourceString="Emerge.GS.GiftShopCart.Label.GrandTotal"
                        DisplayColon="true"  /></dt>
                <dd>$&nbsp;<cms:LocalizedLiteral ID="GrandTotal" runat="server" EnableViewState="true" /></dd>
            </dl>
        </div>
    </div>
  

</asp:Panel>
