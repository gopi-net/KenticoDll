<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Caption Text:</td>
    <td><%# Eval("CaptionToDisplay") %></td>
  </tr>
  <tr>
    <td>Caption Font Name:</td>
    <td><%# Eval("CaptionFontName") %></td>
  </tr>
  <tr>
    <td>Caption Font Size:</td>
    <td><%# Eval("CaptionFontSize") %></td>
  </tr>
  <tr>
    <td>Caption Font Style:</td>
    <td><%# Eval("CaptionFontStyle") %></td>
  </tr>
  <tr>
    <td>Caption Brush Color - Red:</td>
    <td><%# Eval("CaptionBrushColor_R") %></td>
  </tr>
  <tr>
    <td>Caption Brush Color - Green:</td>
    <td><%# Eval("CaptionBrushColor_G") %></td>
  </tr>
  <tr>
    <td>Caption Brush Color_Blue:</td>
    <td><%# Eval("CaptionBrushColor_B") %></td>
  </tr>
  <tr>
    <td>Caption Coordinate - X:</td>
    <td><%# Eval("CaptionCoordinate_X") %></td>
  </tr>
  <tr>
    <td>Caption Coordinate - Y:</td>
    <td><%# Eval("CaptionCoordinate_Y") %></td>
  </tr>
  <tr>
    <td>Caption Block Height (In case of Rectangle):</td>
    <td><%# Eval("CaptionBlock_Height") %></td>
  </tr>
  <tr>
    <td>Caption Block Width (In case of Rectangle):</td>
    <td><%# Eval("CaptionBlock_Width") %></td>
  </tr>
  <tr>
    <td>Value Format To Display (Column Names):</td>
    <td><%# Eval("ValueFormatToDisplay") %></td>
  </tr>
  <tr>
    <td>Value Fon tName:</td>
    <td><%# Eval("ValueFontName") %></td>
  </tr>
  <tr>
    <td>Value Font Size:</td>
    <td><%# Eval("ValueFontSize") %></td>
  </tr>
  <tr>
    <td>ValueFontStyle:</td>
    <td><%# Eval("ValueFontStyle") %></td>
  </tr>
  <tr>
    <td>Value Brush Color - Red:</td>
    <td><%# Eval("ValueBrushColor_R") %></td>
  </tr>
  <tr>
    <td>Value Brush Color - Green:</td>
    <td><%# Eval("ValueBrushColor_G") %></td>
  </tr>
  <tr>
    <td>ValueBrushColor_Blue:</td>
    <td><%# Eval("ValueBrushColor_B") %></td>
  </tr>
  <tr>
    <td>Value Coordinate - X:</td>
    <td><%# Eval("ValueCoordinate_X") %></td>
  </tr>
  <tr>
    <td>Value Coordinate - Y:</td>
    <td><%# Eval("ValueCoordinate_Y") %></td>
  </tr>
  <tr>
    <td>Value Block Height (in case of Rectangle):</td>
    <td><%# Eval("ValueBlock_Height") %></td>
  </tr>
  <tr>
    <td>Value Block Width (in case of Rectangle):</td>
    <td><%# Eval("ValueBlock_Width") %></td>
  </tr>
  <tr>
    <td>Created by:</td>
    <td><%# Eval("ItemCreatedBy") %></td>
  </tr>
  <tr>
    <td>Created when:</td>
    <td><%# Eval("ItemCreatedWhen") %></td>
  </tr>
  <tr>
    <td>Modified by:</td>
    <td><%# Eval("ItemModifiedBy") %></td>
  </tr>
  <tr>
    <td>Modified when:</td>
    <td><%# Eval("ItemModifiedWhen") %></td>
  </tr>
  <tr>
    <td>Order:</td>
    <td><%# Eval("ItemOrder") %></td>
  </tr>
  <tr>
    <td>GUID:</td>
    <td><%# Eval("ItemGUID") %></td>
  </tr>
</table>
<cc1:CMSEditModeButtonEditDelete runat="server" id="btnEditDeleteAutoInsert" Path='<%# Eval("NodeAliasPath") %>' AddedAutomatically="True" EnableByParent="True"   />