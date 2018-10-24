<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><tr>
                    <td>
                        <cms:LocalizedHidden runat="server" ID="hdnOccuranceID" Value='<%# Eval("OccurenceID") %>' />
                        <cms:LocalizedHidden runat="server" ID="hdnScheduleID" Value='<%# Eval( "ScheduleID") %>' />
                        <cms:LocalizedHidden runat="server" ID="hdnCost" Value='<%# Eval( "CostForPublic") %>' />
                        <cms:LocalizedLiteral runat="server" ID="EventNameLiteral" Text='<%# Eval("EventName") %>'>

                        </cms:LocalizedLiteral>

                    </td>
                    <td>
                        <cms:LocalizedLiteral runat="server" ID="LocationLiteral" EnableViewState="true" Text='<%# Eval("Location") %>'>

                        </cms:LocalizedLiteral>
                    </td>
                    <td>
                        <cms:LocalizedLiteral runat="server" ID="DateTimeLiteral" EnableViewState="true" Text='<%# Convert.ToDateTime(Eval("OccurenceDate")).ToString("MM/dd/yyyy") + "<br/>" + Eval("SelectedSessionsDetails") %>'>

                        </cms:LocalizedLiteral>
                    </td>
                    <td>
                        <cms:LocalizedLiteral runat="server" ID="CostLiteral" EnableViewState="true" Text='<%# Eval("CostForPublic") %>'>

                        </cms:LocalizedLiteral>
                      
                    </td>
                    <td>
                        <asp:TextBox ID="DiscountCouponTextbox" EnableViewState="true" Text='<%# Eval("DiscountCode") %>' runat="server"></asp:TextBox>
                        <cms:LocalizedLinkButton ID="cmdUpdate" runat="server" CommandArgument='<%# Eval("ScheduleID")   %>' CommandName="DiscountCouponChanged"
                            ResourceString="Emerge.EC.Cart.DiscountCoupen.Text"></cms:LocalizedLinkButton><br />
                      <cms:LocalizedLiteral runat="server" ID="DiscountCodeNA" EnableViewState="true" Visible="false" Text='<%# Eval("CostForPublic") %>'>

                        </cms:LocalizedLiteral>
                    </td>
                    <td>
                        <cms:LocalizedLiteral EnableViewState="true" runat="server" ID="AmountLiteral" Text='<%# Eval("Amount") %>'>

                        </cms:LocalizedLiteral>
                     
                    </td>

                    <td>

                        <cms:LocalizedLinkButton ID="cmdRemove" CssClass="removeArrow" runat="server" CommandArgument='<%# Eval("OccurenceID")%>' CommandName="RemoveEventOccuranceFromCart" Text=""></cms:LocalizedLinkButton>

                    </td>

                </tr>