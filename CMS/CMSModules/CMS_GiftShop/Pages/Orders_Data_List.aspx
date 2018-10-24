<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Orders_Data_List.aspx.cs" Inherits="CMSModules_CMS_GiftShop_Pages_Orders_Data_List" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Gift Shop - Data List" Theme="Default" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/Controls/EmergeDataList.ascx" TagName="CustomTableDataList" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/CMS_EmergeCommon/EmergeAdminControls/CustomTableSearchControl.ascx" TagName="CustomTableSearch" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">

    <script type="text/javascript">

        jQuery(document).ready(function ($) {

            $(".checkbox_header_select_all :checkbox").click(function () {

                if ($(this).is(':checked')) {
                    $(".checkbox_record_select_single :checkbox").prop('checked', true);
                }
                else
                    $(".checkbox_record_select_single :checkbox").prop('checked', false);


            });

            //alert($(".buttonAlignToRight").html());
        });

    </script>
    <asp:PlaceHolder ID="plcContent" runat="server">
        <cms:CustomTableSearch ID="customTableSearch" runat="server" />
        <cms:CustomTableDataList ID="customTableDataList" runat="server" IsLiveSite="false" />

        <table class="buttonAlignToRight">
            <tr>
                <td>

                       <cms:LocalizedButton ID="btnPending" runat="server" ResourceString="Emerge.GC.Pending"
            EnableViewState="false" />
        <cms:LocalizedButton ID="btnCompleted" runat="server" ResourceString="Emerge.GC.Completed"
            EnableViewState="false" />


                
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>

</asp:Content>
