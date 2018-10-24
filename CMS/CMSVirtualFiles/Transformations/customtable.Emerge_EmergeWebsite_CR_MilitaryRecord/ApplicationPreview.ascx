<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover">
  <tr>
    <td><strong>Are you a War Veteran ?:</strong></td>
    <td class="wrap-normal"><%# Convert.ToString(Eval("IsWarVateran")).Replace("|","") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("IsWarVateran")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Please list conflict:</strong></td>
    <td class="wrap-normal"><%# Eval("ListConflict") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("IsWarVateran")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Date of Service From:</strong></td>
    <td class="wrap-normal"><%# Eval("ServiceFrom","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("IsWarVateran")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Date of Service To:</strong></td>
    <td class="wrap-normal"><%# Eval("ServiceTo","{0:dddd, MMMM dd, yyyy}") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("IsWarVateran")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Branch of Service:</strong></td>
    <td class="wrap-normal"><%# Eval("ServiceBranch") %></td>
  </tr>
  <tr style="display:<%# Convert.ToString(Eval("IsWarVateran")).ToLower()=="yes"?"table-row":"none" %>">
    <td><strong>Military Assignment:</strong></td>
    <td class="wrap-normal"><%# Eval("MilitaryAssignment") %></td>
  </tr>
</table>
