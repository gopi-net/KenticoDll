<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_License_Tools_License_Data_List" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="License - Data List"
    Theme="Default" CodeFile="License_Data_List.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>
