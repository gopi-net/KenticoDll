<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistrationConfirmation.ascx.cs"
    Inherits="CMSWebParts_CMS_EventsCalendar_RegistrationConfirmation" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="ConfirmationPanel" runat="server">

    <div class="clearfix">
        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
    </div>
    <hr>

    <div class="vntHead">

        <span>
            <asp:Literal ID="ConfirmationMessage" runat="server"></asp:Literal></span>
    </div>
    <hr>
    <div class="eventHead">
        <h2>
            <asp:Literal ID="EventLongTitle" runat="server"></asp:Literal></h2>
        <span>
            <asp:Literal ID="EventShortTitle" runat="server"></asp:Literal></span>
    </div>

    <div class="eventDetails">
        <div class="vntPrint" id="evtsessionDiv">
            <ul>
                <cms:CMSRepeater ID="SessionsRepeater" runat="server" />

            </ul>
        </div>
        <div class="venueDetails">
            <ul class="clearfix">
                <li><i class="date"></i>
                    <asp:Literal ID="OccurenceDate" runat="server"></asp:Literal></li>
                <li><i class="clock"></i>
                    <asp:Literal ID="StartTime" runat="server"></asp:Literal>
                    –
                    <asp:Literal ID="EndTime" runat="server"></asp:Literal></li>
                <li id="mapli"><i class="location"></i>
                    <span id="LocationSpan">
                        <asp:Literal ID="Location" runat="server"></asp:Literal></span>
                    <a href="#" id="LocationMap" target="_blank"><%= GetString("Emerge.EC.MapsDirections") %></a>
                </li>
            </ul>
            <asp:Literal ID="ltAddToCalendar" runat="server"></asp:Literal>
            <asp:LinkButton ID="lnkAddToOutlook" runat="server"></asp:LinkButton>
        </div>

    </div>
    <div class="btnWrapper">
        <cms:LocalizedButton ID="BackButton" runat="server" ResourceString="Emerge.EC.Button.BacktoCalendarHome" />
    </div>
    <asp:HiddenField ID="SessionsCount" ClientIDMode="Static" runat="server" Value="0" />
    <script type="text/javascript">

        jQuery(document).ready(function ($) {

            var sessionsCount = $("#SessionsCount").val();

            if (sessionsCount == "0")
                $("#evtsessionDiv").attr("style", "display:none");
            var location = $("#LocationSpan").html().trim();
            if (location == '')
                jQuery("#mapli").attr("style", "display:none");
            else
                $("#LocationMap").attr("href", "http://maps.google.com/maps?q=" + location);
        });

    </script>
</asp:Panel>
