﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><%@ Register Src="~/CMSModules/Ecommerce/Controls/ProductOptions/ShoppingCartItemSelector.ascx" TagName="CartItemSelector" TagPrefix="uc1" %>
<h1 style="margin-top:15px"><%# Eval("CellName", true) %></h1>
<table cellspacing="0" cellpadding="0" class="productDetail">
<tr>
<td style="text-align:center; vertical-align:top;">
<%#IfEmpty(Eval("SKUImagePath"), "Image", "<a href='" + ResolveUrl((Eval("SKUImagePath")).ToString()) + "' target='_blank'>Image</a>")%>
</td>
<td>
<table class="productDetailInfo TextContent" cellspacing="0" cellpadding="0">
<tr>
<td class="caption">Manufacturer:</td>
<td><%# HTMLEncode(EcommerceFunctions.GetManufacturer(Eval("SKUManufacturerID"),"ManufacturerDisplayName").ToString()) %></td>
</tr>
<tr>
<td class="caption">Availability (days):</td>
<td><%# IfEmpty(Eval("SKUAvailableInDays"), "-", Eval("SKUAvailableInDays")) %></td>
</tr>
<tr>
<td class="caption">In stock:</td>
<td><%# IfEmpty(Eval("SKUAvailableItems"), "no", "yes") %></td>
</tr>
<tr>
<td class="caption"><h3>Parameters:</h3></td>
<td>&nbsp;</td>
</tr>
<tr class="alt">
<td>Display type:</td>
<td><%# Eval("CellDisplayType",true) %></td>
</tr>
<tr>
<td>Display width:</td>
<td><%# Eval("CellDisplayWidth") %></td>
</tr>
<tr class="alt">
<td>Display height:</td>
<td><%# Eval("CellDisplayHeight") %></td>
</tr>
<tr>
<td>Display resolution:</td>
<td><%# Eval("CellDisplayResolution") %></td>
</tr>
<tr class="alt">
<td>Bluetooth:</td>
<td><%# IfCompare(Eval("CellBluetooth"), false, "yes", "no") %></td>
</tr>
<tr>
<td>IrDA:</td>
<td><%# IfCompare(Eval("CellIrDA"), false, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>GPRS:</td>
<td><%# IfCompare(Eval("CellGPRS"), false, "yes", "no") %></td>
</tr>
<tr>
<td>EDGE:</td>
<td><%# IfCompare(Eval("CellEDGE"), false, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>HSCSD:</td>
<td><%# IfCompare(Eval("CellHSCSD"), false, "yes", "no") %></td>
</tr>
<tr>
<td>3G:</td>
<td><%# IfCompare(Eval("Cell3G"), false, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>Wi-Fi:</td>
<td><%# IfCompare(Eval("CellWiFi"), false, "yes", "no") %></td>
</tr>
<tr>
<td>Java:</td>
<td><%# IfCompare(Eval("CellJava"), false, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>Built-in camera:</td>
<td><%# IfCompare(Eval("CellCamera"), false, "yes", "no") %></td>
</tr>
<tr>
<td>MP3 player:</td>
<td><%# IfCompare(Eval("CellMP3"), false, "yes", "no") %></td>
</tr>
</table>
</td>
</tr>
<tr><td colspan="2">
<div class="productDetailLinks">
<table width="100%">
  <tr>
    <td align="left">
      <strong>Our price: <span class="ProductPrice"><%# GetSKUFormattedPrice(true, false) %></span></strong>
    </td>
    <td>
      <uc1:CartItemSelector id="cartItemSelector" runat="server" SKUID='<%# ValidationHelper.GetInteger(Eval("SKUID"), 0) %>
            ' SKUEnabled='
            <%# ValidationHelper.GetBoolean(Eval("SKUEnabled"), false) %> ' AddToCartImageButton="addtocart.gif" AddToCartLinkText="Add to shopping cart" ShowProductOptions="true" ShowUnitsTextBox="true"/>
    </td>
    <td>
       <%# EcommerceFunctions.GetAddToWishListLink(Eval("NodeSKUID")) %>    
    </td>
  </tr>
</table>
</div>
</td></tr>
<tr><td colspan="2">
  <h3>Description:</h3>
  <div class="TextContent">
    <%# Eval("SKUDescription") %>
  </div>
</td></tr>
</table>
