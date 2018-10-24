<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmergeDateTimeControl.ascx.cs"
    Inherits="CMSFormControls_EmergeFormControls_EmergeDateTimeControl" %>
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/CMSAdminControls/ModalCalendar/Themes/LiveSite/jquery-ui-1.8.16.custom.css" type="text/css" rel="stylesheet" />
<cms:DateTimePicker ID="dtPicker" runat="server" />
<div style="color:red"><asp:Literal ID="ltStatus" runat="server"></asp:Literal></div>
