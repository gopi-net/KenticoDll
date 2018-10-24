<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductListing.ascx.cs" Inherits="CMSWebParts_CMS_GiftShop_ProductListing" %>


<asp:Panel runat="server" ID="panProductListing">
    <cms:LocalizedHidden ID="hdnSelectedProductID" runat="server" />
    <div class="categoryWrap">
        <div class="category-select">
            <cms:LocalizedLabel ID="lblCategoryFilter" runat="server" EnableViewState="false" ResourceString="Emerge.GS.ProductListing.Label.CategoryFilter"
                DisplayColon="true" />
            <cms:LocalizedDropDownList EnableViewState="true"
                ViewStateMode="Enabled"
                runat="server"
                ID="ddlCategories" DataTextField="CategoryName" AutoPostBack="true" DataValueField="ItemID">
            </cms:LocalizedDropDownList>
            <cms:CMSQueryDataSource ID="ddlCategories_DataSource" runat="server"
                QueryName="customtable.Emerge_{0}_GS_Products.GetCategories" />
        </div>
        <div class="selectCard">

            <div class="clearfix">


                <cms:CMSRepeater ID="repGiftShopItems" runat="server" EnablePaging="false">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </cms:CMSRepeater>




            </div>


        </div>
        <section class="flRight">
            <cms:UniPager ID="UniPagerGiftShopItems" EnableViewState="true" PagerMode="PostBack" PageControl="repGiftShopItems" runat="server" HidePagerForSinglePage="true">

                <PageNumbersSeparatorTemplate>
                    &nbsp;-&nbsp;
                </PageNumbersSeparatorTemplate>
                <PageNumbersTemplate>
                    <a href="<%# Eval("PageURL") %>"><%# Eval("Page") %></a>
                </PageNumbersTemplate>
                <CurrentPageTemplate>
                    <span><%# Eval("Page") %></span>
                </CurrentPageTemplate>
                <PreviousPageTemplate>
                    <a href="<%# Eval("PreviousURL") %>">&lt;&nbsp;</a>
                </PreviousPageTemplate>
                <NextPageTemplate>
                    <a href="<%# Eval("NextURL") %>">&gt;</a>
                </NextPageTemplate>
                <FirstPageTemplate>
                    <a href="<%# Eval("FirstURL") %>">|&lt;  </a>
                </FirstPageTemplate>
                <LastPageTemplate>
                    <a href="<%# Eval("LastURL") %>">&gt;|</a>
                </LastPageTemplate>
                <PreviousGroupTemplate>
                    <a href="<%# Eval("PreviousGroupURL") %>">&nbsp;...&nbsp;</a>
                </PreviousGroupTemplate>
                <NextGroupTemplate>
                    <a href="<%# Eval("NextGroupURL") %>">&nbsp;...&nbsp;</a>
                </NextGroupTemplate>

            </cms:UniPager>
            <cms:LocalizedLiteral ID="pagerMessage" runat="server"></cms:LocalizedLiteral>
        </section>
       



        <div class="btnWrapper ">

            <cms:LocalizedButton ID="btnViewCart" runat="server" ResourceString="Emerge.GS.Button.Caption.ViewCart" />
            <cms:LocalizedButton ID="btnChekOut" runat="server" ResourceString="Emerge.GS.Button.Caption.CheckOut" />

        </div>



    </div>
</asp:Panel>
 <div class="message_box">
            <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ConfirmationBasicCssClass="FormErrorMessage" ID="plcMess" BasicStyles="true" runat="server" />
        </div>
<script type="text/javascript">
    jQuery(document).ready(function ($) {

        if ($('.FormErrorMessage').is(':visible'))
            scrollGo('.FormErrorMessage');


    });
</script>