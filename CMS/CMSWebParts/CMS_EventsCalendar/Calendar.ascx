<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Calendar.ascx.cs" Inherits="CMSWebParts_CMS_EventsCalendar_Calendar" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel runat="server" ID="CalendarPanel">
    <section class="calendarWrap">

        <div class="clearfix">
            <div class="grid-list flRight">
                <ul class="clearfix">
                    <li><a href="javascript:showDay()"><i class="day"></i>
                        <cms:LocalizedLiteral ID="lblDay" runat="server"
                            EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.Day" /></a></li>
                    <li><a href="javascript:showWeek()"><i class="week"></i>
                        <cms:LocalizedLiteral ID="lblWeek" runat="server"
                            EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.Week" /></a></li>
                    <li><a href="javascript:showGrid()"><i class="grid"></i>
                        <cms:LocalizedLiteral ID="lblGrid" runat="server"
                            EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.Grid" /></a></li>
                    <li><a href="javascript:showList()"><i class="list"></i>
                        <cms:LocalizedLiteral ID="lblList" runat="server"
                            EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.List" /></a></li>
                </ul>
            </div>
        </div>
        <asp:HiddenField ID="hdnView" ClientIDMode="Static" runat="server" Value="grid" />
        <hr>
        <div class="category-select">
            <cms:LocalizedLabel ID="lblCategory" runat="server"
                EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.Category"
                DisplayColon="true" AssociatedControlID="Category" />
            <cms:LocalizedDropDownList runat="server" AutoPostBack="true" ID="Category" DataTextField="CategoryName" DataValueField="ItemId"></cms:LocalizedDropDownList>
            <cms:CMSQueryDataSource ID="Category_DataSource" runat="server"
                QueryName="customtable.Emerge_{0}_EC_Categories.GetCategory" />


            <cms:LocalizedLabel ID="lblSubCategory" runat="server"
                EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.SubCategory"
                DisplayColon="true" AssociatedControlID="SubCategory" />
            <cms:LocalizedDropDownList runat="server" AutoPostBack="true" ID="SubCategory" DataTextField="SubCategoryName" DataValueField="ItemId"></cms:LocalizedDropDownList>
            <cms:CMSQueryDataSource ID="SubCategory_DataSource" runat="server"
                QueryName="customtable.Emerge_{0}_EC_SubCategories.GetSubCategory" />
        </div>
        <hr>
        <section class="btnWrapper">
            <asp:Button ID="ViewCartButton" runat="server" Text="View Cart" />
        </section>
        <section class="calendarGrid clearfix">
            <cms:CMSCalendar ID="calEvents" runat="server" Width="100%"
                ShowNextPrevMonth="true" ShowTitle="true"
                BorderStyle="None"
                TitleStyle-Width="200px"
                TitleFormat="MonthYear" TitleStyle-CssClass="month-select" TitleStyle-Wrap="true" TitleStyle-BackColor="Transparent"
                TitleStyle-BorderColor="Transparent"
                DayHeaderStyle-CssClass="dayHeader"
                ShowGridLines="true" SelectionMode="None"
                DayNameFormat="Short"
                EnableViewState="false"
                UseAccessibleHeader="false"
                NextPrevFormat="CustomText" NextPrevStyle-CssClass="btnWrapper"
                NextMonthText='<span class="icon-rightArrowGrey">&nbsp;</span>' PrevMonthText='<span class="icon-leftArrowGrey">&nbsp;</span>'>
            </cms:CMSCalendar>
            <div style="display: none">
                <cms:CMSRepeater ID="repeater" runat="server"></cms:CMSRepeater>
            </div>
            <ul class="eventList listView">

                <cms:CMSRepeater ID="repeaterListView" runat="server"></cms:CMSRepeater>

                <cms:LocalizedLabel ID="lblNoEvents" Visible="false" runat="server"
                    EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.NoEventsFound" />
            </ul>
            <ul class="eventList dayView">
                <cms:CMSRepeater ID="repeaterDayView" runat="server"></cms:CMSRepeater>
                <cms:LocalizedLabel ID="lblNoEventsDay" Visible="false" runat="server"
                    EnableViewState="false" ResourceString="Emerge.EC.Calendar.Label.NoEventsFound" />
            </ul>
            <div class="calendarGrid weekView">
                <table cellpadding="0" cellspacing="0" class='weekViewTable'>
                    <tr>
                        <cms:CMSRepeater ID="repeaterWeekViewParent" runat="server">
                        </cms:CMSRepeater>
                    </tr>
                </table>

            </div>
        </section>

    </section>
    <script>
        var view = jQuery('#hdnView').val();
        switch (view) {
            case 'grid':
                showGrid();
                break;
            case 'list':
                showList();
                break;
            case 'week':
                showWeek();
                break;
            case 'day':
                showDay();
                break;
            default:
                showGrid();
                break;
        }

        function showGrid() {
            jQuery('.calendarGrid table tr').show();
            jQuery('.eventList').hide();
            jQuery('.weekView').hide();
            jQuery('#hdnView').val('grid');

        }
        function showList() {
            jQuery('.calendarGrid table tr').hide();
            jQuery('.dayView').hide();
            jQuery('.weekView').hide();
            jQuery('.calendarGrid table tr:first-child').show();
            jQuery('.listView').show();
            jQuery('#hdnView').val('list');
        }
        function showWeek() {
            jQuery('.calendarGrid table tr').hide();
            jQuery('.dayView').hide();
            jQuery('.listView').hide();
            jQuery('.weekView').show();
            jQuery('.weekViewTable').show();
            jQuery('.weekViewTable tr').show();
            jQuery('.weekViewTable tr td').show();
            jQuery('#hdnView').val('week');
        }
        function showDay() {
            jQuery('.calendarGrid table tr').hide();
            jQuery('.weekView').hide();
            jQuery('.listView').hide();
            jQuery('.dayView').show();
            jQuery('#hdnView').val('day');
        }
    </script>   
</asp:Panel>
