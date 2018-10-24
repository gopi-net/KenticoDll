<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCartRegistrationConfirmation.ascx.cs" Inherits="CMSWebParts_CMS_EventsCalendar_EventCartRegistrationConfirmation" %>
<div class="message_box">
    <div class="clearfix">
        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
    </div>
    <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>
<asp:Panel ID="panelConfirmationMessage" runat="server">
    <div class="sessionCartWrap">
        <div class="sessionCart">
            <cms:CMSRepeater runat="server" ID="EventCartRepeater">
                <HeaderTemplate>
                    <table>
                        <cms:LocalizedLiteral ID="EventCartHeaderLiteral" runat="server"></cms:LocalizedLiteral>
                </HeaderTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    </table>                    
                </FooterTemplate>
            </cms:CMSRepeater>
            <div class="total clearfix">
                <dl>
                    <dt>
                        <cms:LocalizedLiteral ID="lblTotalLiteral" ResourceString="Emerge.EC.EventCart.Total" runat="server"></cms:LocalizedLiteral></dt>
                    <dd>
                        <cms:LocalizedLiteral ID="TotalLiteral" runat="server"></cms:LocalizedLiteral></dd>
                </dl>
                <dl>
                    <dt></dt>
                    <dd>
                        <cms:LocalizedLiteral ID="calLink" runat="server"></cms:LocalizedLiteral>
                    </dd>
                </dl>
            </div>
            <cms:CMSRepeater runat="server" ID="ExcludedEventsRepeater">
                <HeaderTemplate>
                    <table>
                        <cms:LocalizedLiteral ID="EventCartHeaderLiteral" runat="server"></cms:LocalizedLiteral>
                </HeaderTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    </table>                    
                </FooterTemplate>
            </cms:CMSRepeater>
            <cms:CMSRepeater runat="server" ID="EventCartEmailRepeater" EnableViewState="true" Visible="false">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td><span class="style6">Event Name</span></td>
                            <td><span class="style6">Location</span></td>
                            <td><span class="style6">Date(s)</span></td>
                            <td>Discount Code</td>
                            <td>Total Cost</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </cms:CMSRepeater>
        </div>
    </div>
    <div class="btnWrapper">
        <cms:LocalizedButton ID="BackButton" runat="server" ResourceString="Emerge.EC.Button.BacktoCalendarHome" />
    </div>
</asp:Panel>
