<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="panel panel-default">
                    <div class="panel panel-heading">
                          <h3 class="panel-title"><a runat="server" id="headerAnchor" href='<%# "#" +Eval("itemID").ToString()%>'   data-parent="#accordion" data-toggle="collapse" class="accordion-toggle clearfix collapsed">
                          <%# Eval("CategoryName") %><i class="icon-arrow pull-right"></i></a></h3>
                    </div>
                    <cms:LocalizedHidden ID="hdnCategoryID" runat="server" Value='<%# Eval("itemID") %>' />
                    <cms:LocalizedHidden ID="hdnIsCheerCardSelected" runat="server" Value="0" />
  
                    <div class="panel-collapse collapse" id='<%# Eval("itemID") %>' style="height: 0px;">
                        <div class="panel-body">
                           <cms:CMSRepeater ID="repCheerCard" runat="server" >
                            <ItemTemplate>
                                <label>
                                    <cms:LocalizedRadioButton ID="rbtn" CssClass="cheerCardRadio" rel='<%# Eval("ImageGUID") %>' runat="server" GroupName="CardGroupName" />
                                    <span><%# Eval("Title") == null ? Eval("Title") :  ( Eval("Title").ToString().Length > 35 ? Eval("Title").ToString().Substring(0,33) + ".." : Eval("Title").ToString() )   %>
                                        <asp:Image ID="imgCheerCard" runat="server" Width="106" Height="72" ToolTip='<%# Eval("Title") %>'   AlternateText='<%# Eval("Title") %>'
                                            ImageUrl='<%# "~/getmedia/" + Eval("ImageGUID") + "/file.aspx" %>' />
                                        <cms:LocalizedHidden ID="hdnfileGUID" runat="server" Value='<%# Eval("ImageGUID") %>' />
                                    </span>
                                </label>

                            </ItemTemplate>
                        </cms:CMSRepeater>
                        </div>
                    </div>

                </div>
