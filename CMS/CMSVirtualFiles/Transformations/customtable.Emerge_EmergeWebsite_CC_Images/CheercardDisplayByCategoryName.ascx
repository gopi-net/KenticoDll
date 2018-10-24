<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div> 
<h3><%# Eval("CategoryName") %></h3>

                    <cms:LocalizedHidden ID="hdnCategoryID" runat="server" Value='<%# Eval("itemID") %>' />

                    <div class="clearfix">
                        <cms:CMSRepeater ID="repCheerCard" runat="server" >
                            <ItemTemplate>
                                <label>
                                    <cms:LocalizedRadioButton ID="rbtn" rel='<%# Eval("ImageGUID") %>' CssClass="cheerCardRadio"  runat="server" GroupName="CardGroupName"
                                  />
                                    <span><%# Eval("Title") %>
                                        <asp:Image ID="imgCheerCard" runat="server" Width="106" Height="72" AlternateText='<%# Eval("Title") %>'
                                            ImageUrl='<%# "~/getmedia/" + Eval("ImageGUID") + "/file.aspx" %>' />
                                        <cms:LocalizedHidden ID="hdnfileGUID" runat="server" Value='<%# Eval("ImageGUID") %>' />
                                    </span>
                                </label>

                            </ItemTemplate>
                        </cms:CMSRepeater>
                    </div>
</div>