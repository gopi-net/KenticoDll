<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="empHead clearfix">
  <h4>Employer : <%# Eval("CompanyName") %></h4>
  <div class="icon-action flRight">
    <cms:LocalizedLinkButton ID='Edit' Cssclass="edit" runat='server' CommandArgument='<%# Eval("ItemId") %>' CommandName='edit' ToolTip="Edit"></cms:LocalizedLinkButton>
    <cms:LocalizedLinkButton ID='Delete' Cssclass="close" runat='server' CommandArgument='<%# Eval("ItemId") %>' CommandName='delete' ToolTip="Delete"></cms:LocalizedLinkButton>                              
    <a href='javascript:showHideHistoryDetail(<%#Eval("ItemId") %>);' id="<%# "showDetailButton"+Eval("ItemId") %>" class="open" title="View"></a>
  </div>
</div>
<div class="employerInfo" id="<%# "detail"+Eval("ItemId") %>" style="display:none">
	<table>
		<tbody>
			<tr>
				<td><label>Employment<br>
                Date - From:</label></td>
                <td><%# Eval("EmploymentFrom","{0:MMMM dd, yyyy}") %></td>
            </tr>
            <tr>
				<td><label>Employment<br>
				Date - To:</label></td>
				<td><%# Eval("EmploymentTo","{0:MMMM dd, yyyy}") %></td>
			</tr>
			<tr>
				<td><label>Company's <br>
				Name: </label></td>
				<td><%# Eval("CompanyName") %></td>
			</tr>
			<tr>
				<td><label>Company's <br>
				Address: </label></td>
				<td><%# Eval("ComapnyAddress") %></td>
			</tr>
			<tr>
				<td><label>Position Held:</label></td>
				<td><%# Eval("PositionHeld") %></td>
			</tr>
			<tr>
				<td><label>Supervisor's<br>
				Name:</label></td>
				<td><%# Eval("SupervisorFirstName") %> <%# Eval("SupervisorLastName") %></td>
			</tr>
			<tr>
				<td><label>Department's<br>
				Name:</label></td>
				<td><%# Eval("DepartmentName") %></td>
			</tr>
			<tr>
				<td><label>Supervisor's<br>
				Title:</label></td>
				<td><%# Eval("SupervisorTitle") %></td>
			</tr>
			<tr>
				<td><label>Phone Number:</label></td>
				<td><%# Eval("PhoneNumber") %></td>
			</tr>
        </tbody>
  </table>
  <table class="halfContent">
    <tbody>
      <tr>
        <td><label>May we contact for reference?</label></td>
        <td><%# Convert.ToString(Eval("MayContactForReference")).Replace("|","")  %></td>
      </tr>
    </tbody>
  </table>
  <table>
    <tbody>
      <tr>
        <td><label>Job<br>
          Responsibilities:</label></td>
        <td><%# Eval("JobResponsibilities") %></td>
      </tr>
      <tr>
        <td><label>Last Salary:</label></td>
        <td><%# Eval("LastSalary")%></td>
      </tr>
    </tbody>
  </table>
  <table class="halfContent">
    <tbody>
      <tr>
        <td><label>Last Salary (Check One):</label></td>
        <td><%# Convert.ToString(Eval("LastSalaryType")).Replace("|","")  %></td>
      </tr>
    </tbody>
  </table>
  <table>
    <tbody>
      <tr>
        <td><label>Other<br>
          Compensation:</label></td>
        <td><%# Eval("OtherCompensation") %></td>
      </tr>
      <tr>
        <td><label>Other Name<br>
          employed under:</label></td>
        <td><%# Eval("OtherNameEmployedUnder") %></td>
      </tr>
      <tr>
        <td><label>Reason For<br>
          Leaving:</label></td>
        <td><%# Eval("ReasonForLeaving") %></td>
      </tr>
    </tbody>
  </table>
</div>
