<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>
  <td><label><asp:CheckBox ID="chk_1" runat="server" dataID='<%# Eval("ItemID") %>' onclick="checkThis(this);"  /></label>
  </td>
    <td><%# Eval("SessionTitle") %></td>
    <td><%#Eval("StartTime")%> - <%#Eval("EndTime")%></td>
<td><%# Eval("Registrations") %> </td>
</tr>