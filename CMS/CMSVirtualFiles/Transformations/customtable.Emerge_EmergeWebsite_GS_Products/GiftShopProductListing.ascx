<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><cms:LocalizedHidden ID="hdnProductID" runat="server" Value='<%# Eval("ProductID") %>' />
        <cms:LocalizedHidden ID="hdnCategoryID" runat="server" Value='<%# Eval("CategoryID") %>' />
  <li>
        <a href='<%# String.Format("~/Gift-Shop-Product-Listing/Product-Detail?ProductID={0}", Eval("ProductID")) %>'>

            <asp:Image ID="Image1" ImageUrl='<%# "~/getmedia/" + GetValueByPropertyKeyForProducts("ProductImage", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") )) + "/file.aspx?Height=141&Width=130" %>' runat="server" />   
            <span>
                <%# GetValueByPropertyKeyForProducts("ProductName", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>
            </span>    
        </a>
</li>
