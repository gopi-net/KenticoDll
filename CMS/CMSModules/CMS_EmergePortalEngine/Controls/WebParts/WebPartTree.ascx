<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_CMS_EmergePortalEngine_Controls_WebParts_WebPartTree"
    CodeFile="WebPartTree.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Trees/UniTree.ascx" TagName="UniTree" TagPrefix="cms" %>
<cms:UniTree runat="server" ID="treeElem" ShortID="t" MultipleRoots="true" />
