<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CheerCardList.ascx.cs" Inherits="CMSWebParts_CMS_CheerCard_CheerCardList" %>



<asp:Panel ID="panCheerCardList" runat="server" DefaultButton="NextButton">
   

    <label class="noCard">
        <asp:RadioButton ID="rbNoCard" runat="server" rel="NoImage" CssClass="cheerCardRadio" GroupName="CardGroupName" Checked='<%# IsNoImageSelected %>'
        />
        <span>No card design</span>
    </label>
    <div class="selectCard">

        <cms:LocalizedHidden ID="hdnSelectedImageGuid" runat="server" />
   
        <cms:CMSRepeater ID="repCheerCardCategories" runat="server" >
        </cms:CMSRepeater>

    </div>
    <div class="btnWrapper">
     
        <asp:LinkButton ID="NextButton" runat="server" >
            <cms:LocalizedLiteral ID="NextButtonLit" runat="server" ResourceString="Emerge.CC.CheerCardList.NextButton.Text"  >
            </cms:LocalizedLiteral>

        </asp:LinkButton>
      
        
    </div>
</asp:Panel>
<div class="message_box">
<cms:MessagesPlaceHolder  ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true"  runat="server" />
</div>