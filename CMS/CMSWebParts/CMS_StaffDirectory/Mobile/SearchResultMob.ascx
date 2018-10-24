<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResultMob.ascx.cs" Inherits="CMSWebParts_CMS_StaffDirectory_Mobile_SearchResultMob" %>
<asp:Panel ID="ResultPanel" runat="server">
    <h2>Search Results</h2>
    <span>
        <cms:LocalizedLabel CssClass="flLeft" Visible="false" runat="server" ID="lblPageResult"></cms:LocalizedLabel></span>
    <%--<span >5 of 26 results</span>--%>
    <div class="formWrapper">
        <cms:LocalizedDropDownList CssClass="flRight" runat="server" ID="PageSize" DataTextField="PageSizeText" AutoPostBack="true" DataValueField="PageSizeValue"></cms:LocalizedDropDownList>
        <cms:CMSQueryDataSource ID="PageSize_DataSource" runat="server"
            QueryName="customtable.Emerge_{0}_SD_PageSize.GetPageSize" />

        <hr>
    </div>
    <div class="searchWrap">
        <cms:CMSRepeater ID="repResults" DelayedLoading="true" runat="server"
            EnablePaging="true" PagerControl-ControlCssClass="flRight">
        </cms:CMSRepeater>

    </div>
    <section class="pagination">
        <cms:UniPager ID="UniPager1" DirectPageControlID="repResults" runat="server" CssClass="pagination">
        </cms:UniPager>
    </section>

    <section class="btnWrapper">
        <asp:Button runat="server" Text="New Search" ID="btnSearch" CssClass="btn btn-default" />
    </section>



</asp:Panel>

<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />

