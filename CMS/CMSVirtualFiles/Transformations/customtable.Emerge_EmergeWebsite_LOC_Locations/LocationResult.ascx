<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><dt><a href="javascript:ShowInfoWindow(<%# Eval("ItemID") %>)"><cms:LocalizedLiteral ID="LocationName" runat="server" Text='<%# Eval("LocationName") %>'></cms:LocalizedLiteral>
                                </a></dt>
                                <dd><cms:LocalizedLiteral ID="LocationAddress" runat="server" Text='<%# Eval("Address") %>'></cms:LocalizedLiteral><br />
                                     <cms:LocalizedLiteral ID="City" runat="server" Text='<%# Eval("City") %>'></cms:LocalizedLiteral>,
                        <cms:LocalizedLiteral ID="StateCode" runat="server" Text='<%# Eval("StateCode") %>'></cms:LocalizedLiteral>
                        <cms:LocalizedLiteral ID="Zipcode" runat="server" Text='<%# Eval("Zipcode") %>'></cms:LocalizedLiteral>
                                </dd>