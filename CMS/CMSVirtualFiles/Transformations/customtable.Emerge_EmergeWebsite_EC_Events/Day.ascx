﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<li><a href="<%# Convert.ToString(Eval("ActivateLink")) == "1" ? Convert.ToString(Eval("EventType")) == "VOLUNTEER" ? "~/EventsCalendar/VolunteerEventDetails/" + Convert.ToString(Eval("ItemID2")) : "~/EventsCalendar/EventsDetails/" + Convert.ToString(Eval("ItemID2")) : "#"%>"><%# Eval("ScheduleTitle") %></a><br />
                            <small><%# Eval("OccurenceDate","{0:dddd, MMMM dd, yyyy}")%>, <%# Eval("StartTime") %> - <%# Eval("EndTime") %></small>
                            </li>
