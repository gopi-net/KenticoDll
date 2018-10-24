<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table>
  <tr>
    <td>Primary Table Name:</td>
    <td><%# Eval("PrimaryTableName") %></td>
  </tr>
  <tr>
    <td>Primary Key Column Name:</td>
    <td><%# Eval("PrimaryPkColumnName") %></td>
  </tr>
  <tr>
    <td>Primary Display Column Names:</td>
    <td><%# Eval("PrimaryDisplayColumnNames") %></td>
  </tr>
  <tr>
    <td>Foreign Table Name:</td>
    <td><%# Eval("ForeignTableName") %></td>
  </tr>
  <tr>
    <td>Foreign Table Column Name:</td>
    <td><%# Eval("ForeignTableColumnName") %></td>
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
