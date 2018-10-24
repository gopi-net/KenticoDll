<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpcomingEventsBox.ascx.cs" Inherits="CMSWebParts_CMS_EventsCalendar_UpcomingEventsBox" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="UpcomingEventsPanel" runat="server">

<div class="box-events">
	<h3>
		Upcoming Events</h3>
	<div class="box-inner">
        <cms:CMSRepeater ID="EventsRepeater" runat="server">
                        <HeaderTemplate>
                            <ul class="list-events">
                        </HeaderTemplate>
                        <ItemTemplate>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </cms:CMSRepeater>
		<a id="ViewAllEventsLink" runat="server"><em>View All Events &gt;</em> </a></div>
</div>
    </asp:Panel>

