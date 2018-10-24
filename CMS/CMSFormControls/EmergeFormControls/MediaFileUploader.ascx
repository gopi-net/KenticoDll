<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MediaFileUploader.ascx.cs" Inherits="CMSFormControls_EmergeFormControls_MediaFileUploader" %>
<div style="margin-bottom: 5px">
  
    <div>
        <asp:FileUpload ID="FileUpload" runat="server" CssClass="btn" />
        <asp:Button ID="CmdPostFile" runat="server" Text="Upload File" CssClass="btn" style="margin-top: 7px" />
        <asp:Button ID="RemoveFileButton" runat="server" Text="Remove File" CssClass="btn" style="margin-top: 5px" Visible="false" />

        <asp:HiddenField ID="hdnFilePath" runat="server" />
    </div>
       <div style="margin-top: 7px">
      <cms:MessagesPlaceHolder ID="plcMess" runat="server" />
           </div>
    <div style="margin-top: 5px">

        <asp:Literal ID="previewPlaceHolder" runat="server"></asp:Literal>


    </div>
</div>