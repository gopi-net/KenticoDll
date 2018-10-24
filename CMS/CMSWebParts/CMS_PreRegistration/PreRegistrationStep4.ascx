<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreRegistrationStep4.ascx.cs" Inherits="CMSWebParts_CMS_PreRegistration_PreRegistrationStep4" %>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ConfirmationBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
<asp:Panel ID="pnlStep4" runat="server" DefaultButton="btnSubmit">
    <h3>Review:</h3>
    <asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label>
    <div>
        <cms:CMSRepeater ID="PreRegistrationInfo" runat="server">
        </cms:CMSRepeater>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td></td>
                    <td class="box-button" align="right">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="FormButton" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="FormButton" />
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="FormButton" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Panel>
