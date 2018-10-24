<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><h1><%# Eval("CellName", true) %></h1>
<table cellspacing="0" cellpadding="0" class="productDetail">
<tr>
<td style="text-align:center; vertical-align:top;">
<a href="<%# Eval("SKUImagePath", true) %>" target="_blank">
Image
</a>
</td>
<td>
<table class="productDetailInfo" cellspacing="0" cellpadding="0">
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
<td><%# IfCompare(Eval("CellBluetooth"), true, "yes", "no") %></td>
</tr>
<tr>
<td>IrDA:</td>
<td><%# IfCompare(Eval("CellIrDA"), true, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>GPRS:</td>
<td><%# IfCompare(Eval("CellGPRS"), true, "yes", "no") %></td>
</tr>
<tr>
<td>EDGE:</td>
<td><%# IfCompare(Eval("CellEDGE"), true, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>HSCSD:</td>
<td><%# IfCompare(Eval("CellHSCSD"), true, "yes", "no") %></td>
</tr>
<tr>
<td>3G:</td>
<td><%# IfCompare(Eval("Cell3G"), true, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>Wi-Fi:</td>
<td><%# IfCompare(Eval("CellWiFi"), true, "yes", "no") %></td>
</tr>
<tr>
<td>Java:</td>
<td><%# IfCompare(Eval("CellJava"), true, "yes", "no") %></td>
</tr>
<tr class="alt">
<td>Built-in camera:</td>
<td><%# IfCompare(Eval("CellCamera"), true, "yes", "no") %></td>
</tr>
<tr>
<td>MP3 player:</td>
<td><%# IfCompare(Eval("CellDisplayMP3"), true, "yes", "no") %></td>
</tr>
</table>
</td>
</tr>
</table>