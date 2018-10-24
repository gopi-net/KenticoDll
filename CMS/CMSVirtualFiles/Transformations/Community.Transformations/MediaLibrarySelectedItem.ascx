<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><%@ Register Src="~/CMSModules/MediaLibrary/Controls/LiveControls/MediaFilePreview.ascx" TagName="MediaFilePreview" TagPrefix="cc1" %>

<div class="MediaGalleryPagger">
<a href="<%# URLHelper.RemoveParameterFromUrl(CMS.Helpers.RequestContext.CurrentURL, "fileid") %>">Back</a>
</div>


<div class="MediaGalleryDetail">
	<cc1:MediaFilePreview ID="filePreview" runat="server" Width="530" Height="360" maxsidesize="530" />
</div>

<div class="MediaGalleryDetailBottom">
<h2 style="float: left"><%# HTMLEncode(GetNotEmpty("FileTitle;FileName")) %></h2>
<a href="<%# HTMLHelper.HTMLEncode(MediaLibraryFunctions.GetMediaFileUrl(Eval("FileLibraryID") ,Eval("FilePath"), Eval("FileGUID"), Eval("FileName"), GetDataControlValue<bool>("UseSecureLinks"), true)) %>" target="_blank">
<img src="~/App_Themes/CommunitySite/Images/button_media_download.gif" alt="Download" />
</a>
</div>
<div class="clear"></div>
<p>
<strong>Size:</strong>&nbsp;<%# DataHelper.GetSizeString(ValidationHelper.GetLong(Eval("FileSize"), 0)) %><br/>
<strong>Description:</strong>&nbsp;<%# CMS.Helpers.ResHelper.GetString(Convert.ToString(Eval("FileDescription", true))) %><br/>
<strong>Uploaded:</strong>&nbsp;<%# ((DateTime)Eval("FileCreatedWhen")).ToString("M/d/yyyy") %><br />
<strong>Type:</strong>&nbsp;<%# Eval("FileExtension", true) %>
</p>
