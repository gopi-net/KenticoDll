<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResult.ascx.cs" Inherits="CMSWebParts_CMS_StaffDirectory_SearchResult" %>
<asp:Panel ID="ResultPanel" runat="server">
    <div class="searchResult">
        <div class=" clearfix">
            <cms:LocalizedLabel CssClass="flLeft" Visible="false" runat="server" ID="lblPageResult"></cms:LocalizedLabel>
            <cms:LocalizedDropDownList CssClass="flRight" runat="server" ID="PageSize" DataTextField="PageSizeText" AutoPostBack="true" DataValueField="PageSizeValue"></cms:LocalizedDropDownList>
            <cms:CMSQueryDataSource ID="PageSize_DataSource" runat="server"
                QueryName="customtable.Emerge_{0}_SD_PageSize.GetPageSize" />
        </div>
        <hr>
        <cms:CMSRepeater ID="repResults" DelayedLoading="true" runat="server" 
            EnablePaging="true" PagerControl-ControlCssClass="flRight" PagerControl-SelectedPrevClass="CustomStyle1 CustomStyle4"
            PagerControl-UnselectedPrevClass="CustomStyle1 CustomStyle4" PagerControl-SelectedNextClass="CustomStyle1 CustomStyle3"
            PagerControl-UnselectedNextClass="CustomStyle1 CustomStyle3"
            PagerControl-FirstText="<span class='CustomStyle1'><span class='SelectedNext'>|&lt;</span></span>"
            PagerControl-LastText="<span class='CustomStyle1'><span class='SelectedNext'>&gt;|</span></span>">
        </cms:CMSRepeater>
    </div>
    <section class=" paginationWrap clearfix">
        <section class="btnWrapper flLeft">
            <asp:Button runat="server" Text="New Search" ID="btnSearch" />
        </section>

    </section>

</asp:Panel>
<div class="message_box">
    <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
</div>

