﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("CellName", true) %></h1>
<div class="productDetail">
<div class="productImage">
<%# IfEmpty(Eval("SKUImagePath"),
"Image",
  "<a href=\"" + ResolveUrl((Eval("SKUImagePath")).ToString()) + "\" target=\"_blank\">Image</a>") %>
</div>
<div class="productDescription">
<h3>Product parameters</h3>
<div class="parameters"><%# Eval("CellDisplayType",true) %> <%# Eval("CellDisplayResolution") %>, <%# Eval("CellDisplayWidth") %>cm x <%# Eval("CellDisplayHeight") %>cm<%# IfCompare(Eval("CellBluetooth"), false, ", Bluetooth", "") %><%# IfCompare(Eval("CellIrDA"), false, ", IrDA", "") %><%# IfCompare(Eval("CellGPRS"), false, ", GPRS", "") %><%# IfCompare(Eval("CellEDGE"), false, ", EDGE", "") %><%# IfCompare(Eval("CellHSCSD"), false, ", HSCSD", "") %><%# IfCompare(Eval("Cell3G"), false, ", 3G", "") %><%# IfCompare(Eval("CellWiFi"), false, ", WiFi", "") %><%# IfCompare(Eval("CellJava"), false, ", Java", "") %><%# IfCompare(Eval("CellCamera"), false, ", Camera", "") %><%# IfCompare(Eval("CellMP3"), false, ", MP3 player", "") %></div>
<table cellspacing="0" cellpadding="0" border="0" class="parameterTable">
<tr><td class="caption">Manufacturer:</td>
<td class="parameter"><%# IfEmpty(Eval("SKUManufacturerID"), "-", HTMLEncode(EcommerceFunctions.GetManufacturer(Eval("SKUManufacturerID"),"ManufacturerDisplayName").ToString())) %></td>
</tr><tr>
<td class="caption">Availability (days):</td>
<td class="parameter"><%# IfEmpty(Eval("SKUAvailableInDays"), "-", Eval("SKUAvailableInDays")) %></td>
</tr><tr>
<td class="caption">In stock:</td>
<td class="parameter"><%# IfEmpty(Eval("SKUAvailableItems"), "no", "yes") %></td>
</tr></table>
<div class="ourPrice">Our price: <span class="ProductPrice"><%# GetSKUFormattedPrice(true, false) %></span></div>
</div>
<div class="clear"></div>
<h3>Product description</h3>
  <div class="TextContent productDetailDescription">
Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus pulvinar, orci id accumsan feugiat, purus purus molestie risus, ut volutpat urna quam quis mi. Suspendisse at nisi quis urna sodales vehicula id vitae velit. In elit augue, adipiscing ac commodo ac, tristique sed risus. Aenean eleifend, orci quis egestas condimentum, ante lacus facilisis risus, ac tristique purus erat non nisi.
    <%# Eval("SKUDescription") %>
  </div>
</div>
