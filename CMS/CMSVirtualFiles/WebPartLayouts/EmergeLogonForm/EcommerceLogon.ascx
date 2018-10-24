<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSWebParts_Membership_Logon_LogonForm"
        CodeFile="~/CMSWebParts/Membership/Logon/logonform.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server" CssClass="LogonPageBackground">
  <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default.aspx">
    <LayoutTemplate>
      <asp:Panel runat="server" ID="pnlLogin" DefaultButton="LoginButton">
        <table class="logon">
          <tr>
            <td class="label"><cms:LocalizedLabel ID="lblUserName" runat="server" AssociatedControlID="UserName" EnableViewState="false" /></td>
            <td class="input"><cms:CMSTextBox ID="UserName" runat="server" MaxLength="100" CssClass="LogonTextBox" /><cms:CMSRequiredFieldValidator ID="rfvUserNameRequired" runat="server" ControlToValidate="UserName" EnableViewState="false">*</cms:CMSRequiredFieldValidator></td>
          </tr>
          <tr>
            <td class="label"><cms:LocalizedLabel ID="lblPassword" runat="server" AssociatedControlID="Password" EnableViewState="false" /></td>
            <td class="input"><cms:CMSTextBox ID="Password" runat="server" TextMode="Password" MaxLength="110" CssClass="LogonTextBox" /></td>
          </tr>
          <tr>
            <td></td>
            <td class="remember"><cms:LocalizedCheckBox ID="chkRememberMe" runat="server" /></td>
          </tr>
          <tr>
            <td colspan="2"><cms:LocalizedLabel ID="FailureText" runat="server" EnableViewState="False" CssClass="ErrorLabel" /></td>
          </tr>
          <tr>
            <td></td>
            <td><cms:LocalizedButton ID="LoginButton" runat="server" CommandName="Login" EnableViewState="false" /></td>
          </tr>
        </table>
       </asp:Panel>
    </LayoutTemplate>
  </asp:Login>
  <div class="forgottenPwd">
  <asp:LinkButton ID="lnkPasswdRetrieval" runat="server" EnableViewState="false" />
  <asp:Panel ID="pnlPasswdRetrieval" runat="server" CssClass="LoginPanelPasswordRetrieval" DefaultButton="btnPasswdRetrieval">
    <asp:Label ID="lblPasswdRetrieval" runat="server" EnableViewState="false" AssociatedControlID="txtPasswordRetrieval" />
    <cms:CMSTextBox ID="txtPasswordRetrieval" runat="server" /><br /><br />
    <cms:CMSButton ID="btnPasswdRetrieval" runat="server" EnableViewState="false" /><br />
    <cms:CMSRequiredFieldValidator ID="rqValue" runat="server" ControlToValidate="txtPasswordRetrieval" EnableViewState="false" /><br />
    <span class="ErrorLabel"><asp:Label ID="lblResult" runat="server" Visible="false" EnableViewState="false" /></span>
  </asp:Panel>
   </div>
</asp:Panel>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<asp:HiddenField runat="server" ID="hdnPasswDisplayed" />