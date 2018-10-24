<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover">
    <tr>
        <td><strong>Date Available for Work:</strong></td>
        <td class="wrap-normal"><%# Eval("AvailabilityDate","{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr>
        <td><strong>Type of employment desired:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("EmploymentType"))==string.Empty?"All":Eval("EmploymentType") %></td>
    </tr>
    <tr>
        <td><strong>Willing to work weekends?:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("WillingToWorkOnWeekends")).Replace("|","") %></td>
    </tr>
    <tr>
        <td><strong>ShiftPreference:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("ShiftPreference"))==string.Empty?"All":Eval("ShiftPreference") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("UnitPreference1"))!=string.Empty?"table-row":"none" %>">
        <td><strong>1st Unit Preference:</strong></td>
        <td class="wrap-normal"><%# Eval("UnitPreference1") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("UnitPreference2"))!=string.Empty?"table-row":"none" %>">
        <td><strong>2nd Unit Preference:</strong></td>
        <td class="wrap-normal"><%# Eval("UnitPreference2") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("UnitPreference3"))!=string.Empty?"table-row":"none" %>">
        <td><strong>3rd Unit Preference:</strong></td>
        <td class="wrap-normal"><%# Eval("UnitPreference3") %></td>
    </tr>
    <tr>
        <td><strong>Have you ever been employed here before?:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("WasHiredEarlier")).Replace("|","") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("WasHiredEarlier")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Position:</strong></td>
        <td class="wrap-normal"><%# Eval("PrevEmploymentPosition") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("WasHiredEarlier")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Department:</strong></td>
        <td class="wrap-normal"><%# Eval("PrevEmploymentDepartment") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("WasHiredEarlier")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>From:</strong></td>
        <td class="wrap-normal"><%# Eval("PrevEmploymentFrom", "{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("WasHiredEarlier")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>To:</strong></td>
        <td class="wrap-normal"><%# Eval("PrevEmploymentTo", "{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("WasHiredEarlier")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Reason for leaving:</strong></td>
        <td class="wrap-normal"><%# Eval("PrevEmploymentReasonForLeaving") %></td>
    </tr>
    <tr>
        <td><strong>Do you have any relatives that are presently
            <br />
            employed at any facility of this hospital?:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("IsRelativeEmployedInHospital")).Replace("|","") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("IsRelativeEmployedInHospital")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Relative Name:</strong></td>
        <td class="wrap-normal"><%# Eval("RelativeTitle") %> <%# Eval("RelativeFirstName") %> <%# Eval("RelativeLastName") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("IsRelativeEmployedInHospital")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Relationship:</strong></td>
        <td class="wrap-normal"><%# Eval("RelativeRelationship") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("IsRelativeEmployedInHospital")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Department:</strong></td>
        <td class="wrap-normal"><%# Eval("RelativeDepartment") %></td>
    </tr>
    <tr>
        <td><strong>Have you ever pleaded 'guilty' or 'no contest' to,<br />
            or been convicted of a crime or other offense?:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("HasOffense")).Replace("|","") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("HasOffense")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Date:</strong></td>
        <td class="wrap-normal"><%# Eval("OffenseDate", "{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("HasOffense")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Details:</strong></td>
        <td class="wrap-normal"><%# Eval("OffenseDetails") %></td>
    </tr>
    <tr>
        <td><strong>Driver's License Number:</strong></td>
        <td class="wrap-normal"><%# Eval("DriversLicenseNumber") %></td>
    </tr>
    <tr>
        <td><strong>State:</strong></td>
        <td class="wrap-normal"><%# Eval("State") %></td>
    </tr>
    <tr>
        <td><strong>Date of expiration of the license should be mentioned.:</strong></td>
        <td class="wrap-normal"><%# Eval("DriversLicenseValidity", "{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr>
        <td><strong>Any traffic Violations?:</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("HasAnyTrafficViolations")).Replace("|","") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("HasAnyTrafficViolations")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Traffic Violation Date:</strong></td>
        <td class="wrap-normal"><%# Eval("TrafficViolationDate", "{0:dddd, MMMM dd, yyyy}") %></td>
    </tr>
    <tr style="display: <%# Convert.ToString(Eval("HasAnyTrafficViolations")).ToLower()=="yes"?"table-row":"none" %>">
        <td><strong>Traffic Violation Explanation:</strong></td>
        <td class="wrap-normal"><%# Eval("TrafficViolationExplanation") %></td>
    </tr>
    <tr>
        <td><strong>With or without reasonable accommodation,<br />
            do you have the ability to perform the essential<br />
            functions of the job for which you are applying?<br />
            (Please ask for job description, if necessary):</strong></td>
        <td class="wrap-normal"><%# Convert.ToString(Eval("IsImmediateAccomodationRequired")).Replace("|","") %></td>
    </tr>
</table>
