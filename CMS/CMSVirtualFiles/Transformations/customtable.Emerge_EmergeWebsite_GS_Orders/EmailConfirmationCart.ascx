<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>

        <td style="text-align:center" bgcolor="#9ED071">
         <span class="style7"> 
          <%# Eval("ProductID") %>
          </span>
            

        </td>
        <td style="text-align:center" bgcolor="#9ED071">
          <span class="style7">
            <%# GetValueByPropertyKeyForCartProducts("ProductName", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>
        </span>
        </td>
        <td style="text-align:right" bgcolor="#9ED071">
          <span class="style7">
        <%# GetValueByPropertyKeyForCartProducts("PurchasedQty", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>  
       </span>
        
        </td>
  
        <td style="text-align:right" bgcolor="#9ED071">
          <span class="style7">
          $ <%# GetValueByPropertyKeyForCartProducts("UnitPrice", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))  %>
         </span>
        </td>

        <td style="text-align:right" bgcolor="#9ED071">
          <span class="style7">
          $ <%#   Math.Round(  Convert.ToDouble( GetValueByPropertyKeyForCartProducts("PurchasedQty", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") )) ) * Convert.ToDouble( GetValueByPropertyKeyForCartProducts("UnitPrice", Convert.ToInt32( Eval("ProductID") ) , Convert.ToInt32( Eval("CategoryID") ))),2)  %>
          </span>
            

        </td>
       
</tr>