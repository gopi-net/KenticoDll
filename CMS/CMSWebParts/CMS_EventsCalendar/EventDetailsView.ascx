<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventDetailsView.ascx.cs"
    Inherits="CMSWebParts_CMS_EventsCalendar_EventDetailsView" %>

<asp:Panel ID="EventsDetailsPanel" runat="server">

    <div class="clearfix">
        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
    </div>
    <hr>
    <div class="eventHead">
        <h2>
            <asp:Literal ID="EventLongTitle" runat="server"></asp:Literal></h2>
        <span>
            <asp:Literal ID="EventShortTitle" runat="server"></asp:Literal></span>
    </div>
    <hr>
    <div class="eventDetails">
        <div class="venueDetails">
            <ul class="clearfix">
                <li><i class="date"></i>
                    <asp:Literal ID="OccurenceDate" runat="server"></asp:Literal></li>
                <li><i class="clock"></i>
                    <asp:Literal ID="StartTime" runat="server"></asp:Literal>
                    –
                        <asp:Literal ID="EndTime" runat="server"></asp:Literal></li>

                <li id="mapli"><i class="location"></i>
                    <span id="locationDiv">
                        <asp:Literal ID="EventLocation" runat="server"></asp:Literal></span>
                    <a href="#" id="mapsDirection" target="_blank"><%= GetString("Emerge.EC.MapsDirections") %></a>
                </li>
            </ul>
        </div>
        <div class="contentWrap">
            <p>
                <asp:Literal ID="EventDescription" runat="server"></asp:Literal>
            </p>
            <div class="attachments" id="attachmentDiv">
                <strong><%= GetString("Emerge.EC.Attachments") %></strong>
                <ul>
                    <li><i class="download"></i><a href="#" id="AttachmentURL" target="_blank">
                        <asp:Literal ID="AttachmentText" runat="server"></asp:Literal></a> </li>
                </ul>
                <div id="attGUIDDiv" style="display: none">
                    <asp:Literal ID="AttachmentGUID" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="contact" id="CostDiv" runat="server">
                <address>
                    <strong><%= GetString("Emerge.EC.Cost") %></strong>
                    $<asp:Literal ID="CostForPublic" runat="server"></asp:Literal>
                    <br>
                    <br>
                </address>
            </div>
            <div class="contact" id="ContactDiv">
                <address>
                    <strong><%= GetString("Emerge.EC.Contacts") %></strong>
                    <span id="ContactName">
                        <asp:Literal ID="EventContactName" runat="server"></asp:Literal></span><span id="ContactNameBR"><br />
                        </span>
                    <span id="ContactPhone">
                        <asp:Literal ID="EventContactPhone" runat="server"></asp:Literal></span><span id="ContactPhoneBR"><br />
                        </span>

                    <a href="#" id="contactEmail">
                        <asp:Literal ID="EventContactEmail" runat="server"></asp:Literal></a>
                </address>
            </div>
            <div class="webLink" id="WeblinkDiv">
                <strong><%= GetString("Emerge.EC.WebsiteLink") %></strong>
                <a href="#" target="_blank" id="websitelink">
                    <asp:Literal ID="WebsiteLink" runat="server"></asp:Literal></a>
            </div>
        </div>

        <div class="eventSession" id="eventSessionDiv" runat="server" visible="false">
            <br />
            <br />
            <div class="sessionList">
                <h3><%= GetString("Emerge.EC.UpcomingSessions") %>:</h3>
                <div>
                    <cms:CMSRepeater ID="SessionListRepeater" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </cms:CMSRepeater>
                </div>
            </div>
        </div>
        <div class="message_box">
        </div>
        <section class="btnWrapper">
            <cms:LocalizedButton ID="RegisterButton" runat="server" ResourceString="Emerge.EC.Label.Register" Visible="false" />
            <cms:LocalizedButton ID="AddToCartButton" runat="server" ResourceString="Emerge.EC.AddtoCart" />
            <cms:LocalizedButton ID="ViewCartButton" runat="server" ResourceString="Emerge.EC.ViewCart" />
            <cms:LocalizedButton ID="VolunteerRegistration" runat="server" ResourceString="Emerge.EC.ViewCart" Visible="false" />
            <cms:LocalizedButton ID="backToAllEvents" runat="server" ResourceString="Emerge.EC.BackEvents" />
        </section>
        <asp:HiddenField ID="OccurenceIDField" runat="server" EnableViewState="true" Value="0" />
        <script>
            function scrollGo(element) {
                var x = jQuery(element).offset().top - 100; // 100 provides buffer in viewport
                jQuery('html,body').animate({ scrollTop: x }, 500);
            }
            jQuery(document).ready(function ($) {
                if ($('.FormErrorMessage').is(':visible'))
                    scrollGo('.FormErrorMessage');
                var attachmentGUID = $("#attGUIDDiv").html().trim();
                $("#AttachmentURL").attr("href", "~/getmedia/" + attachmentGUID + "/file.aspx");
                if (attachmentGUID == "") {
                    $("#attachmentDiv").attr("style", "display:none");
                }
                var websitelink = $("#websitelink").html().trim();
                if (websitelink.substring(0, 4) != "http")
                    $("#websitelink").attr("href", "http://" + websitelink);
                else
                    $("#websitelink").attr("href", websitelink);
                if (websitelink == "") {
                    $("#WeblinkDiv").attr("style", "display:none");
                }
                var location = $("#locationDiv").html().trim();
                if (location == '')
                    jQuery("#mapli").attr("style", "display:none");
                else
                    $("#mapsDirection").attr("href", "http://maps.google.com/maps?q=" + location);
                var contactName = $("#ContactName").html().trim();
                var contactPhone = $("#ContactPhone").html().trim();
                var contactEmail = $("#contactEmail").html().trim();
                if (contactName == "")
                    $("#ContactNameBR").html("");
                if (contactPhone == "")
                    $("#ContactPhoneBR").html("");
                if (contactEmail == "" && contactName == "" && contactPhone == "")
                    $("#ContactDiv").attr("style", "display:none");

                $("#contactEmail").attr("href", "mailto:" + contactEmail);
            });
        </script>
</asp:Panel>
<asp:Literal ID="ltAddToCalendar" runat="server" ></asp:Literal>		
<asp:LinkButton ID="lnkAddToOutlook" runat="server" OnClick="lnkAddToOutlook_Click" Visible="false"></asp:LinkButton>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />


