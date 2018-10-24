<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>
                       <td>
                        <cms:LocalizedLiteral runat="server" ID="EventNameLiteral" Text='<%# Eval("EventName") %>'>

                        </cms:LocalizedLiteral>
  
                        </td>
                        
                        <td>
                            <cms:LocalizedLiteral runat="server" ID="DateTimeLiteral" EnableViewState="true" Text='<%# Convert.ToDateTime(Eval("OccurenceDate")).ToString("MM/dd/yyyy") + "<br/> " + Eval("StartTime") + " - " +  Eval("EndTime") %>'>

                            </cms:LocalizedLiteral>
                        </td>
                        
                       
                        
                        

                   

                    </tr>