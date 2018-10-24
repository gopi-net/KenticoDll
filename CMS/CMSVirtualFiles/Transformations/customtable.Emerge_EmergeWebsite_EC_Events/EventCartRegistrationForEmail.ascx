<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>
                       <td bgcolor="#9ED071"><span class="style7">
           <%# Eval("EventName") %>
                   </span>
                        </td>
                        <td bgcolor="#9ED071"><span class="style7">
                          <%# Eval("Location") %>
                            </span>
                        </td>
                        <td bgcolor="#9ED071"><span class="style7">
                          <%# Convert.ToDateTime(Eval("OccurenceDate")).ToString("MM/dd/yyyy") + "<br/> " + Eval("SelectedSessionsDetails") %>
                          </span>
                            
                        </td>
                        
                        <td bgcolor="#9ED071"><span class="style7">
                          
                          <%# Eval("DiscountCode") %>
                          </span>
                        </td>
                        <td bgcolor="#9ED071"><span class="style7">
            <%# Eval("Amount") %>
</span>

                        </td>
                        

                   

                    </tr>