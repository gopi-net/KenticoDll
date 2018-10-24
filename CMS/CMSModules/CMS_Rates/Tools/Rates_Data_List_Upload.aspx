<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Rates_Tools_Rates_Data_List_Upload" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Rates - Data List"
    Theme="Default" CodeFile="Rates_Data_List_Upload.aspx.cs" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="CustomTableSearch" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableSearch ID="customTableSearch" runat="server" />
        <asp:Panel ID="pnlUpload" runat="server">
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblFile" runat="server" ResourceString="Emerge.RT.ImportData.Label.SelectFile" />
                    </td>
                    <td>
                        <asp:FileUpload ID="fuFile" runat="server" CssClass="btn"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblSheetName" runat="server" ResourceString="Emerge.RT.ImportData.Label.SheetName" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtSheetName" CssClass="form-control" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ImportFile" CssClass="btn btn-primary" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <cms:CustomTableDataList ID="customTableDataList" runat="server" IsLiveSite="false" />
    </asp:PlaceHolder>
    
</asp:Content>
