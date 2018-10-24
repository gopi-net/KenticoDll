<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><li>
  <label>
    <cms:LocalizedCheckBox ID="chkSession" runat="server" Text='<%# Eval("Title") + " - " + Eval("StartTime") + " - " + Eval("EndTime") %>'
     />
    
   <cms:LocalizedHidden ID="hdnSessionID" runat="server" Value='<%# Eval("SessionID") %>' />
  </label>
  <cms:LocalizedLiteral ID="ltAddToCalendar" runat="server"  />
  <asp:LinkButton ID="lnkAddToOutlook" runat="server" ></asp:LinkButton>
</li>
