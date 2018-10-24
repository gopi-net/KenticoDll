<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="EvntHolder">
<a href="<%# Convert.ToString(Eval("ActivateLink")) == "1" ? Convert.ToString(Eval("EventType")) == "VOLUNTEER" ? "~/EventsCalendar/VolunteerEventDetails/" + Convert.ToString(Eval("ItemID2")) : "~/EventsCalendar/EventsDetails/" + Convert.ToString(Eval("ItemID2")) : "#"%>" class="eventItem">
<%# Eval("ScheduleTitle") %><span class="times"> <%# Eval("StartTime") %> - <%# Eval("EndTime") %> </span></a>

<div class="tipContent"><ul class="clearfix">
<li><p class="title"><a href="<%# Convert.ToString(Eval("ActivateLink")) == "1" ? Convert.ToString(Eval("EventType")) == "VOLUNTEER" ? "~/EventsCalendar/VolunteerEventDetails/" + Convert.ToString(Eval("ItemID2")) : "~/EventsCalendar/EventsDetails/" + Convert.ToString(Eval("ItemID2")) : "#"%>"><%# Eval("ScheduleTitle") %></a></p>
<small><%# Eval("OccurenceDate","{0:dddd, MMMM dd, yyyy}")%>, <%# Eval("StartTime") %> - <%# Eval("EndTime") %></small>
</li>
</ul></div>

</div>