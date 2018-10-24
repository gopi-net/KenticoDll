<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Maintenance_Tools_Maintenance_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Maintenance Tables List"
    CodeFile="Maintenance_List.aspx.cs" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/EmergeUnigrid.ascx" TagName="UniGrid" TagPrefix="Emerge" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
     <Emerge:UniGrid  runat="server" ID="EmergeGridControl" GridName="Maintenance_List.xml"
         AfterActionRedirectTo="Maintenance_Data_List.aspx" IsLiveSite="false"></Emerge:UniGrid>
</asp:Content>
