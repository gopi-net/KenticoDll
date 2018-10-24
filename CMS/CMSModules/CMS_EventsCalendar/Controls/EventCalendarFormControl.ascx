<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCalendarFormControl.ascx.cs"
    Inherits="CMSModules_CMS_EventsCalendar_Controls_EventCalendarFormControl" %>
<cms:CustomTableForm ID="customTableForm" runat="server" MarkRequiredFields="true"
    OnOnAfterSave="customTableForm_OnAfterSave" OnOnBeforeSave="customTableForm_OnBeforeSave" OnOnAfterDataLoad="customTableForm_OnAfterDataLoad" />
