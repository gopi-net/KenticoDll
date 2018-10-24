<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History_Data_List.aspx.cs" Inherits="CMSModules_CMS_HistoryTracker_Pages_History_Data_List" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="History - Data List" Theme="Default" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="CustomTableSearch" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">


    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableSearch ID="customTableSearch" runat="server" />

        <table>
            <tr>
                <td>

                    <asp:Panel ID="panDateRangePicker" runat="server"></asp:Panel>
                </td>
                <td>
                   
                    <cms:LocalizedButton ID="btnDelete" runat="server"  ResourceString="Emerge.HT.DeleteHistoryRecords"
                        EnableViewState="false"  Width="180px" OnClientClick="return confirm('Are you sure you want to delete History Details?');" />




                </td>
            </tr>
        </table>

        <cms:CustomTableDataList ID="customTableDataList" runat="server" IsLiveSite="false" />


    </asp:PlaceHolder>

</asp:Content>
