<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CMS_EmergePortalEngine_UI_WebParts_WebPartProperties_properties_frameset"
    EnableEventValidation="false" ValidateRequest="false" CodeFile="WebPartProperties_properties_frameset.aspx.cs" %>

<!DOCTYPE html>
<html>
<head runat="server" enableviewstate="false">
    <title>Untitled Page</title>
    <style type="text/css">
        body
        {
            margin: 0px;
            height: 100%;
            width: 100%;
            overflow: hidden;
        }
    </style>
</head>
<frameset border="0" rows="*,60" runat="server" id="rowsFrameset">
    <frame name="webpartpropertiescontent" frameborder="0" noresize="noresize" scrolling="auto"
        runat="server" id="frameContent" />
    <frame name="webpartpropertiesbuttons" frameborder="0" noresize="noresize" scrolling="no"
        runat="server" id="frameButtons" />
    <cms:NoFramesLiteral ID="ltlNoFrames" runat="server" />
</frameset>
</html>
