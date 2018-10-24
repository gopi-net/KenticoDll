<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><dt>Job Code:</dt>
<dd><%# Eval("JobCode") %></dd>
<dt>Job Title:</dt>
<dd><%# Eval("JobTitle") %></dd>
<dt style='display:<%# Eval("DepartmentName")== string.Empty || Convert.IsDBNull(Eval("DepartmentName"))?"none":"block" %>'>Department:</dt>
<dd style='display:<%# Eval("DepartmentName")==string.Empty || Convert.IsDBNull(Eval("DepartmentName"))?"none":"block" %>'><%# Eval("DepartmentName") %></dd>
<dt style='display:<%# Eval("LocationName")==string.Empty || Convert.IsDBNull(Eval("LocationName"))?"none":"block" %>'>Location:</dt>
<dd style='display:<%# Eval("LocationName")==string.Empty || Convert.IsDBNull(Eval("LocationName"))?"none":"block" %>'><%# Eval("LocationName") %></dd>
<dt style='display:<%# Eval("JobShiftName")==string.Empty || Convert.IsDBNull(Eval("JobShiftName"))?"none":"block" %>'>Job Shift:</dt>
<dd style='display:<%# Eval("JobShiftName")==string.Empty || Convert.IsDBNull(Eval("JobShiftName"))?"none":"block" %>'><%# Convert.ToString(Eval("JobShiftName")).Replace("|",",") %></dd>
<dt style='display:<%# Eval("EmploymentTypeName")==string.Empty || Convert.IsDBNull(Eval("EmploymentTypeName"))?"none":"block" %>'>Employment Type:</dt>
<dd style='display:<%# Eval("EmploymentTypeName")==string.Empty || Convert.IsDBNull(Eval("EmploymentTypeName"))?"none":"block" %>'><%# Convert.ToString(Eval("EmploymentTypeName")).Replace("|",",") %></dd>
<dt style='display:<%# Eval("EssentialDuties")==string.Empty?"none":"block" %>'>Essential Duties:</dt>
<dd style='display:<%# Eval("EssentialDuties")==string.Empty?"none":"block" %>'><%# Eval("EssentialDuties") %></dd>
<dt style='display:<%# Eval("Qualifications")==string.Empty?"none":"block" %>'>Qualifications:</dt>
<dd style='display:<%# Eval("Qualifications")==string.Empty?"none":"block" %>'><%# Eval("Qualifications") %></dd>
<dt>Contact Person:</dt>
<dd><%# Eval("ContactFirstName") %> <%# Eval("ContactLastName") %></dd>
<dt>Email Address:</dt>
<dd><%# Eval("ContactEmailAddress") %></dd>
<dt style='display:<%# Eval("ContactPhone")==string.Empty?"none":"block" %>'>Phone:</dt>
<dd style='display:<%# Eval("ContactPhone")==string.Empty?"none":"block" %>'><%# Convert.ToString(Eval("ContactPhone")).Replace("Ext:","<b>Ext:</b>") %></dd>
<dt style='display:<%# Eval("ContactFax")==string.Empty?"none":"block" %>'>Fax:</dt>
<dd style='display:<%# Eval("ContactFax")==string.Empty?"none":"block" %>'><%# Eval("ContactFax") %></dd>