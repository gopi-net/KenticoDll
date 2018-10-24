<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><td class="hasEvent tipPoint">
  
    
           <asp:Literal ID="ltWeekDay" runat="server" Text='<%# Eval("OccurenceDate", "{0:ddd - d}") %>'></asp:Literal>
          <!-- Hidden Field ID "hdnWeekDate" is used in code, changing it may break the page/webpart functionality  -->
          <asp:HiddenField ID="hdnWeekDate" runat="server" Value='<%# Eval("OccurenceDate") %>' />
       <br/>      <br/>
          <!-- Literal ID "ltNoEventLiteral" is used in code, changing it may break the page/webpart functionality -->
          <asp:Literal ID="ltNoEventLiteral" runat="server" />
          <div class="event">
            <!-- CMSRepeater ID "repeaterWeekViewChild" is used in code, changing it may break the page/webpart functionality -->
            <cms:CMSRepeater ID="repeaterWeekViewChild" runat="server" TransformationName="customtable.Emerge_EmergeWebsite_EC_Events.CalendarWeekViewChild">
            </cms:CMSRepeater>
            <!-- Literal ID "literalCallout" is used in code, changing it may break the page/webpart functionality -->
            <asp:Literal ID="literalCallout" runat="server" />
          </div>
        
 
</td>