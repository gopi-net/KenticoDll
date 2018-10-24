<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_StaffDirectory_Tools_StaffDirectory_Data_List" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Staff Directory - Data List"
    Theme="Default" CodeFile="StaffDirectory_Data_List.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="SearchCustomTable" TagPrefix="emg" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <div>
        <emg:SearchCustomTable ID="searchCustomTable" runat="server"  />
            </div>
        <br />
        <cms:CustomTableDataList id="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
</asp:Content>
