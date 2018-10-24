<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master"
    CodeFile="Dashboard.aspx.cs" Inherits="CMSModules_CMS_Location_Dashboard" Theme="Default" %>

<asp:Content ID="pnlContent" ContentPlaceHolderID="plcContent" runat="server">
    <div class="dashboard">
        <asp:PlaceHolder ID="plcDashboard" runat="server">
            <ul>
                <asp:Repeater ID="repOuter" runat="server">
                    <ItemTemplate>
                        <div class="dashboard-inner" style="width: 50%; float: left">
                            <li class="tile">
                                <div class="tile-btn-header">
                                    <%# UIHelper.GetAccessibleImageMarkup(Page, EvalText("ElementIconClass", "icon-app-default"), EvalText("ElementIconPath"), size: FontIconSizeEnum.Dashboard) %>
                                    <h2><%# EvalText("ElementDisplayName") %></h2>
                                </div>
                            </li>
                            <asp:HiddenField ID="hdnElementId" runat="server" Value='<%# EvalText("ElementID") %>' />
                            <asp:Repeater ID="repInner" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li class="tile">
                                        <asp:LinkButton CommandName="Open" ID="lnkItem" runat="server" class="tile-btn">
                                    <%# UIHelper.GetAccessibleImageMarkup(Page, EvalText("ElementIconClass", "icon-app-default"), EvalText("ElementIconPath"), size: FontIconSizeEnum.Dashboard) %>
                                    <h3><%# EvalText("ElementDisplayName") %></h3>
                                        </asp:LinkButton>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcEmpty" runat="server" Visible="false">
            <div class="empty">
                <div class="tile"></div>
                <div class="info">
                    <h2>
                        <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="cms.dashboard.empty.title"></cms:LocalizedLabel></h2>
                    <p>
                        <cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="cms.dashboard.empty.info"></cms:LocalizedLabel>
                    </p>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Content>
