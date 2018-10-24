<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChooseTimeSlotView.ascx.cs"
    Inherits="CMSWebParts_CMS_EventsCalendar_ChooseTimeSlotView" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="TimeslotPanel" runat="server">
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

    <div class="vntOpportunities">
        <div class="clearfix">
            <span class="pull-left"><strong><%= GetString("Emerge.EC.Label.SelectaTimeSlot") %></strong></span>
            <span class="pull-right">
                <asp:Literal ID="OccurenceDate" runat="server"></asp:Literal></span>
        </div>
        <div class="timeSlot">
            <table>
                <cms:CMSRepeater ID="SessionsRepeater" runat="server" />
            </table>
        </div>
        <div class="btnWrapper">
            <cms:LocalizedButton  ID="RegisterButton" runat="server" ResourceString="Emerge.EC.Button.Register" Visible="false" />
        </div>
    </div>
    <asp:Label ID="MessageInfo" CssClass="ErrorMessage" runat="server"></asp:Label>

    <asp:HiddenField ID="OccurenceIDField" runat="server" Value="0" />
    <asp:HiddenField ID="SelectedSession" ClientIDMode="Static" runat="server" Value="" />

    <script type="text/javascript">
        jQuery(function () {
            jQuery('span[dataid]').each(function () {
                var span = jQuery(this);
                var value = span.attr('dataid');
                span.find('input').attr('dataid', value);
            });
        })

        function checkThis(object) {
            var sessionID = jQuery(object).parent().attr("dataid");

            if (object.checked) {
                var selectedSession = jQuery("#SelectedSession").val();
                selectedSession += sessionID + "|";
                jQuery("#SelectedSession").val(selectedSession);
            }
            else {
                var selectedSession = jQuery("#SelectedSession").val();
                selectedSession = selectedSession.replace(sessionID + "|", "");
                jQuery("#SelectedSession").val(selectedSession);

            }

        }
    </script>
</asp:Panel>

