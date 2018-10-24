<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="empHead clearfix">
   <h4><%# Convert.ToString(Eval("ReferenceType")).Replace("|","") %> - <%# Eval("FirstName") %> <%# Eval("LastName") %></h4>
   <div class="icon-action flRight">
     <cms:LocalizedLinkButton ID='Edit' runat='server' CssClass="edit" CommandArgument='<%# Eval("ItemId") %>' CommandName='edit' ToolTip="Edit"></cms:LocalizedLinkButton>
     <cms:LocalizedLinkButton ID='Delete' runat='server' CssClass="close" CommandArgument='<%# Eval("ItemId") %>' CommandName='delete' ToolTip="delete"></cms:LocalizedLinkButton>  
     <a href="javascript:showHideReferral(<%# Eval("ItemId") %>);" id="<%# "showDetailButton"+Eval("ItemId") %>" class="open" title="View"></a>
   </div>
</div>
<div class="employerInfo"  id="<%# "detail"+Eval("ItemId") %>" style="display:none">
  <table>
    <tbody>
      <tr>
        <td><label>Reference Type:</label></td>
        <td><%# Convert.ToString(Eval("ReferenceType")).Replace("|","") %></td>
      </tr>
      <tr>
        <td><label>Supervisor's<br>
          Name:</label></td>
        <td><%# Eval("FirstName") %> <%# Eval("LastName") %></td>
      </tr>
      <tr>
        <td><label>Company's<br>
          Name:</label></td>
        <td><%# Eval("CompanyName") %></td>
      </tr>
      <tr>
        <td><label>Address:</label></td>
        <td><%# Eval("Address") %></td>
      </tr>
      <tr>
        <td><label>City:</label></td>
        <td><%# Eval("City") %></td>
      </tr>
      <tr>
        <td><label>State:</label></td>
        <td><%# Eval("StateName") %></td>
      </tr>
      <tr>
        <td><label>Zip:</label></td>
        <td><%# Eval("Zip") %></td>
      </tr>
      <tr>
        <td><label>Business Phone:</label></td>
        <td><%# Eval("BusinessPhone") %></td>
      </tr>
      <tr>
        <td><label>Cell:</label></td>
        <td><%# Eval("Cell") %></td>
      </tr>
      <tr>
        <td><label>Home Phone:</label></td>
        <td><%# Eval("HomePhone") %></td>
      </tr>
    </tbody>
  </table>
</div>
