<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>

        <td style="text-align:center">
          
          <%# Eval("ProductID") %>
          
            

        </td>
        <td style="text-align:center">
            <%# GetValueByPropertyKeyForCartProducts("ProductName", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>

        </td>
        <td style="text-align:right">
        <%# GetValueByPropertyKeyForCartProducts("PurchasedQty", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>  
       
        
        </td>
        <td style="text-align:right">$ <%# GetValueByPropertyKeyForCartProducts("Stock", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>
       
        </td>

       
       
</tr>