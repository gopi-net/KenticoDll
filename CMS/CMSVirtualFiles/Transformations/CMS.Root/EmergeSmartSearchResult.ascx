<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div style="margin-bottom: 30px;">
        <%-- Search result title --%>
        <div>
            <a style="font-weight: bold" href='<%# SearchResultUrl(true) %>'>
                <%#SearchHighlight(CMS.Helpers.HTMLHelper.HTMLEncode(CMS.Base.Web.UI.ControlsHelper.RemoveDynamicControls(DataHelper.GetNotEmpty(Eval("Title"), "/"))), "<span style='font-weight:bold;'>", "</span>")%>
            </a>
        </div>
        <%-- Search result summary --%>
        <div style="margin-top: 5px; width: 590px;"> <%# GetSearchValue("LastName")%>
            <%#GetSearchValue("DocumentPageDescription")%></div>
        
    </div>