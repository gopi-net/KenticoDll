<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Maintenance_Controls_EmergeDataList"
    CodeFile="EmergeDataList.ascx.cs" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/EmergeUnigrid.ascx" TagName="UniGrid" TagPrefix="Emerge" %>
<cms:MessagesPlaceHolder ID="plcMess" runat="server" />
<Emerge:UniGrid  runat="server" ID="EmergeGridControl" IsLiveSite="false"></Emerge:UniGrid>
<asp:Literal ID="ltlScript" runat="server" />





