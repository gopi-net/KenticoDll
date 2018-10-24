<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhysicianDetailsMob.ascx.cs" Inherits="CMSWebParts_CMS_StaffDirectory_Mobile_PhysicianDetailsMob" %>
<asp:Panel ID="ResultPanel" runat="server">
    <script src="~/CMSWebParts/CMS_StaffDirectory/js/script.js" type="text/javascript"></script>

    <cms:CMSRepeater ID="repResults" DelayedLoading="true" runat="server" OnItemDataBound="repResults_ItemDataBound"
        EnablePaging="true">
    </cms:CMSRepeater>

    <asp:Literal runat="server" ID="ltScript"></asp:Literal>
    <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>

    <script>
        RenderStaffLocationMap();
    </script>
</asp:Panel>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />

