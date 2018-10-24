<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><table class="table table-hover EducationTraining">

    <tr>
        <td><strong>Type</strong></td>
        <td><strong>Name</strong></td>
        <td><strong>City</strong></td>
        <td><strong>Check Last Year Completed?</strong></td>
        <td><strong>Did you Graduate?</strong></td>
        <td><strong>Degree Obtained & GPA</strong></td>
    </tr>
    <tr>
        <td><strong>High School</strong></td>
        <td class="wrap-normal"><%# Eval("HighSchoolName") %></td>
        <td class="wrap-normal"><%# Eval("HighSchoolCity") %></td>
        <td class="wrap-normal"><%# Eval("HighSchoolLastYearCompleted") %></td>
        <td class="wrap-normal"><%# Eval("HighSchoolWasGraduated") %></td>
        <td class="wrap-normal"><%# Eval("HighSchoolDegreeObtained") %></td>
    </tr>
    <tr>
        <td><strong>College</strong></td>
        <td class="wrap-normal"><%# Eval("CollegeName") %></td>
        <td class="wrap-normal"><%# Eval("CollegeCity") %></td>
        <td class="wrap-normal"><%# Eval("CollegeLastYearCompleted") %></td>
        <td class="wrap-normal"><%# Eval("CollegeWasGraduated") %></td>
        <td class="wrap-normal"><%# Eval("CollegeDegreeObtained") %></td>
    </tr>

    <tr>
        <td><strong>Other</strong></td>
        <td class="wrap-normal"><%# Eval("OtherName") %></td>
        <td class="wrap-normal"><%# Eval("OtherCity") %></td>
        <td class="wrap-normal"><%# Eval("OtherLastYearCompleted") %></td>
        <td class="wrap-normal"><%# Eval("OtherWasGraduated") %></td>
        <td class="wrap-normal"><%# Eval("OtherDegreeObtained") %></td>
    </tr>

</table>
