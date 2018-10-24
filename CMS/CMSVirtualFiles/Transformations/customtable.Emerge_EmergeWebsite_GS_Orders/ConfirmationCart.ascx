<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>
<td>
            <asp:Image ID="Image2" ImageUrl='<%# "~/getmedia/" + GetValueByPropertyKeyForCartProducts("ProductImage", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") )) + "/file.aspx?Height=57&Width=50" %>' runat="server" />

        </td>
        <td>
          <asp:Label ID="ProductID" runat="server" Text=<%# Eval("ProductID") %> ></asp:Label> 
            

        </td>
        <td>
            <%# GetValueByPropertyKeyForCartProducts("ProductName", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>

        </td>
        <td>
       <asp:Label ID="PurchasedQty" runat="server" Text='<%# GetValueByPropertyKeyForCartProducts("PurchasedQty", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>'></asp:Label>  
        
        </td>
        <td>$
       <asp:Label ID="UnitPrice" runat="server" Text='<%# GetValueByPropertyKeyForCartProducts("UnitPrice", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>'></asp:Label>
        </td>

        <td>
          <asp:Label ID="Price" runat="server" Text='<%#   Math.Round(  Convert.ToDouble( GetValueByPropertyKeyForCartProducts("PurchasedQty", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") )) ) * Convert.ToDouble( GetValueByPropertyKeyForCartProducts("UnitPrice", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))),2)  %>'></asp:Label>
            

        </td>
       
</tr>