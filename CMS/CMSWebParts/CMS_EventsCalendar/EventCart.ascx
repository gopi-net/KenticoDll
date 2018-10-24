<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCart.ascx.cs" Inherits="CMSWebParts_CMS_EventsCalendar_EventCart" %>

<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>
<asp:Panel ID="panelCart" runat="server">
    <div class="clearfix">
                        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
                    </div>
                    <hr>
<div class="sessionCartWrap">
    <div class="sessionCart">
        <table>
                    <cms:LocalizedLiteral ID="EventCartHeaderLiteral" runat="server"></cms:LocalizedLiteral>
        <cms:CMSRepeater runat="server" ID="EventCartRepeater"></cms:CMSRepeater>
        </table>
                    <div class="total clearfix">
                        <dl>
                            <dt>
                                <cms:LocalizedLiteral ID="lblTotalLiteral" ResourceString="Emerge.EC.EventCart.Total" runat="server"></cms:LocalizedLiteral>
                            </dt>
                            <dd>
                                <cms:LocalizedLiteral ID="TotalLiteral" runat="server"></cms:LocalizedLiteral>
                            </dd>
                        </dl>
                    </div>


        <div class="btnWrapper">
            <cms:LocalizedButton ID="cmdBrowseEvents" ResourceString="Emerge.EC.Label.ContinueBrowsing" CausesValidation="false" runat="server" />
            <cms:LocalizedButton ID="cmdProceedToRegistration" ResourceString="Emerge.EC.Label.ProceedRegistration" CausesValidation="true" runat="server" />

        </div>
    </div>
</div>
</asp:Panel>