<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><script runat="server"> 
string GetGroupName() {  
    if (CMS.Community.CommunityContext.CurrentGroup != null) {
        return CMS.Community.CommunityContext.CurrentGroup.GroupName;
    }
    return "";
} 
</script>

<div class="GroupMediaItem">
<table>
  <tr>
  <td class="GroupMediaLibraryPhoto">

<a href="~/Groups/<%# GetGroupName() %>/Library-<%# Eval("LibraryName") %>.aspx">
<%# IfEmpty(Eval("LibraryTeaserPath"), "<img border=\"0\" src=\"~/App_Themes/CommunitySite/Images/DefaultMediaTeaser.gif\" alt=\"Default\" />", "<img src=\"" + Eval("LibraryTeaserPath") + "?width=180\" alt=\""+ Eval("name") +"\" border=\"0\" />") %></a>

    </td>
    <td class="GroupMediaLibraryDescription">
<a href="~/Groups/<%# GetGroupName() %>/Library-<%# Eval("LibraryName") %>.aspx"><%# LimitLength(HTMLEncode(ResHelper.LocalizeString(Eval<string>("LibraryDisplayName"))), 20, "...") %>
</a>

<div class="GroupMediaLibraryDescriptionText">
<%# LimitLength(ResHelper.LocalizeString(Eval<string>("LibraryDescription")), 40, "...") %>
</div>
    </td>
  </tr>
</table>
<div class="GroupMediaItemBottom"></div>
</div>