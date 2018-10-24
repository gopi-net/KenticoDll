<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><a href="<%# Convert.ToString(Eval("ActivateLink"))== "1" ? "~/EventsCalendar/EventsDetails/" + Convert.ToString(Eval("ItemID2")): "#"%>" class="eventItem">
  <%# Eval("ScheduleTitle") %> 
  <span class="times">
    <%# Eval("StartTime") %> - <%# Eval("EndTime") %> 
  </span>
</a>