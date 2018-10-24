<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VolunteerRegistrationConfirmation.ascx.cs" 
    Inherits="CMSWebParts_CMS_EventsCalendar_VolunteerRegistrationConfirmation" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="ConfirmationPanel" runat="server">
        
 
                    <div class="clearfix">
                        <h1 class="flLeft"><%= GetString("Emerge.EC.CalendarTitle") %></h1>
                    </div>
                    <hr>

                    <div class="vntHead">
                        
                        <span><asp:Literal ID="ThankYouMessage" runat="server"></asp:Literal></span>
                    </div>
                    <hr>
                    <div class="eventHead">
                        <h2><asp:Literal ID="EventLongTitle" runat="server"></asp:Literal></h2>
                        <span><asp:Literal ID="EventShortTitle" runat="server"></asp:Literal></span>
                    </div>

                    <div class="eventDetails">
                        <div class="vntPrint">
                         <ul>
                             <cms:CMSRepeater ID="SessionsRepeater" runat="server" />
                             
                         </ul>
                        </div>
                        <div class="venueDetails">
                            <ul class="clearfix">
                                <li><i class="date"></i><asp:Literal ID="OccurenceDate" runat="server"></asp:Literal></li>
                                <li> <i class="clock"></i><asp:Literal ID="StartTime" runat="server"></asp:Literal> – <asp:Literal ID="EndTime" runat="server"></asp:Literal></li>
                                <li><i class="location"></i><asp:Literal ID="Location" runat="server"></asp:Literal>
                              
                                </li>
                            </ul>
                        </div>

                    </div>
     <div class="btnWrapper">
            <cms:LocalizedButton ID="BackButton" runat="server" ResourceString="Emerge.EC.Button.BacktoCalendarHome" />
         </div>
    
    
    </asp:Panel>
