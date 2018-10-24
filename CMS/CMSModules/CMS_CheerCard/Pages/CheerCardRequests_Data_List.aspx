<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CMS_CheerCard_Pages_CheerCardRequests_Data_List" EnableEventValidation="false"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Cheer Card - Data List"
    Theme="Default" CodeFile="CheerCardRequests_Data_List.aspx.cs" %>

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
              
        <cms:LocalizedButton ID="btnDelivered" runat="server" ResourceString="Emerge.CC.Delivered"
            EnableViewState="false" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Content>

