<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_CMS_EmergePortalEngine_FormControls_WebPartLayouts_WebPartLayoutSelector"
    CodeFile="WebPartLayoutSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<cms:UniSelector runat="server" ID="uniselect" SelectionMode="SingleDropDownList"
    UseUniSelectorAutocomplete="false" ObjectType="cms.webpartlayout" ResourcePrefix="webpartlayoutselect"
    AllowEmpty="false" OnOnSelectionChanged="uniselect_OnSelectionChanged" ReturnColumnName="WebPartLayoutCodeName" />
