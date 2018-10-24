<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Donation_Tools_Donation_Data_SelectFields" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Title="Donation - Select Fields" CodeFile="Donation_Data_SelectFields.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="PageContent">
        <asp:PlaceHolder ID="plcContent" runat="server">
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContentModal">
                <cms:CMSCheckBoxList ID="chkListFields" runat="server" />
            </asp:Panel>
        </asp:PlaceHolder>
    </div>

    <script type="text/javascript">

        // Closes modal dialog and refresh parent window
        function CloseAndRefresh() {
            if (wopener != null) {
                wopener.location.replace(wopener.location);
            }
            CloseDialog();
        }

        // Closes modal dialog
        function Close() {
            CloseDialog();
        }

        // Selects/Unselects all checkboxes
        function ChangeFields(select) {
            var items = document.forms[0].elements;
            for (i = 0; i < items.length; ++i) {
                if (items[i].type == 'checkbox') {
                    items[i].checked = select;
                }
            }
        }
                        
    </script>
</asp:Content>
