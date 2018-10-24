<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_License_Tools_License_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="License Tables List"
    CodeFile="License_List.aspx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/EmergeUnigrid.ascx" TagName="UniGrid" TagPrefix="Emerge" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <Emerge:UniGrid ShowObjectMenu="false"  runat="server" ID="EmergeGridControl" OrderBy="ClassDisplayName" GridName="~/CMSModules/CMS_License/Tools/License_List.xml"   IsLiveSite="false" ></Emerge:UniGrid>
    
  <%-- <cms:UniGrid runat="server" ID="uniGrid" GridName="Maintenance_List.xml" OrderBy="ClassDisplayName"
        IsLiveSite="false" ShowObjectMenu="false" />--%>
</asp:Content>
