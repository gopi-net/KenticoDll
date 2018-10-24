<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><li>
  <p class="title">
    <a href="<%# Convert.ToString(Eval("ActivateLink")) == "1" ? "~/EventsCalendar/EventsDetails/" + Convert.ToString(Eval("ItemID2")) : "#"%>">
      <%# Eval("LongTitle") %>
    </a>
  </p>
  <small>
       <%# Eval("OccurenceDate","{0:dddd, MMMM dd, yyyy}")%> 
    <br /> 
    <%# Eval("StartTime") %> - <%# Eval("EndTime") %>
  </small>
</li>
<hr />