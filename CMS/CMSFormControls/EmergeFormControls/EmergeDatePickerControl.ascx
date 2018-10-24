<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmergeDatePickerControl.ascx.cs"
    Inherits="EmergeDatePickerControl" %>
<link href="/CMSPages/GetResource.ashx?stylesheetfile=/CMSAdminControls/ModalCalendar/Themes/LiveSite/jquery-ui-1.8.16.custom.css" type="text/css" rel="stylesheet" />
<cms:DateTimePicker ID="dtPicker" runat="server" />
<div style="color:red"><asp:Literal ID="ltStatus" runat="server"></asp:Literal></div>
