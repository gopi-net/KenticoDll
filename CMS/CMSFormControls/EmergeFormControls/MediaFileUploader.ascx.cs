using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.MediaLibrary;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.CommonService;
using CMS.Helpers;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;

public partial class CMSFormControls_EmergeFormControls_MediaFileUploader : EmergeBaseFormEngineUserControl
{

    #region properties
    public override Object Value
    {
        get
        {
            return hdnFilePath.Value;
        }
        set
        {

            previewPlaceHolder.Text = GetUploadedFileDetails(System.Convert.ToString(value));
            hdnFilePath.Value = System.Convert.ToString(value);
            if (UseRemoveButton)
            {
                if (String.IsNullOrEmpty(ValidationHelper.GetString(value, string.Empty)))
                    RemoveFileButton.Visible = false;
                else
                    RemoveFileButton.Visible = true;
            }
        }
    }


    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    /// <summary>
    /// Property used to access the Media Library Folder name.
    /// </summary>
    /// 
    public string MediaFolderName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MediaFolderName"), string.Empty);
        }
        set
        {
            SetValue("MediaFolderName", value);
        }
    }

    
    public bool UseRemoveButton
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("UseRemoveButton"), false);
        }
        set
        {
            SetValue("UseRemoveButton", value);
        }
    }



    public string Extensions
    {

        get
        {
            return ValidationHelper.GetString(GetValue("Extensions"), string.Empty);
        }
        set
        {
            SetValue("Extensions", value);
        }
    }
    
    public string ExtentionErrorMessage
    {

        get
        {
            return ValidationHelper.GetString(GetValue("ExtentionErrorMessage"), string.Empty);
        }
        set
        {
            SetValue("ExtentionErrorMessage", value);
        }
    }

    /// <summary>
    /// Property used to access or set image Width.
    /// </summary>
    /// 
    public string ImageWidth
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ImageWidth"), "0");
        }
        set
        {
            SetValue("ImageWidth", value);
        }
    }

    /// <summary>
    /// Property used to access or set image Height.
    /// </summary>
    /// 
    public string ImageHeight
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ImageHeight"), "0");
        }
        set
        {
            SetValue("ImageHeight", value);
        }
    }

    /// <summary>
    /// Property used to access or set image resize message. This message will be shown if fixed height and width are provided.
    /// </summary>
    /// 
    public string ImageResizeMessage
    {

        get
        {
            return ValidationHelper.GetString(GetValue("ImageResizeMessage"), string.Empty);
        }
        set
        {
            SetValue("ImageResizeMessage", value);
        }
    }

    #endregion properties

    #region methods

    protected void Page_Load(object sender, EventArgs e)
    {
        CmdPostFile.Click += CmdPostFile_Click;
        RemoveFileButton.Click += RemoveFileButton_Click;

        if (!ImageHeight.Equals("0") || !ImageWidth.Equals("0"))
            ShowInformation(ImageResizeMessage);
        if (UseRemoveButton)
        {
            if (String.IsNullOrEmpty(ValidationHelper.GetString(this.Value, string.Empty)))
                RemoveFileButton.Visible = false;
            else
                RemoveFileButton.Visible = true;
        }

    }

    void RemoveFileButton_Click(object sender, EventArgs e)
    {
        Guid fileGUID = ValidationHelper.GetGuid(this.hdnFilePath.Value, Guid.Empty);
        MediaFileInfo mediaFile = MediaFileInfoProvider.GetMediaFileInfo(fileGUID, EmergeCMSContext.CurrentSiteName);
        mediaFile.Delete();
        int itemID = QueryHelper.GetInteger("itemid", 0);
        if (itemID > 0)
        {
            int customTableID = QueryHelper.GetInteger("customtableid", 0);
            string fieldName = this.FieldInfo.Name;
            IDictionary<string, object> tableData = new Dictionary<string, object>();
            tableData.Add(new KeyValuePair<string,object>(fieldName, string.Empty));
            CustomTableDataHelper.SaveCustomTableItem(customTableID, ref itemID, tableData);
        }
        this.hdnFilePath.Value = string.Empty;
        this.previewPlaceHolder.Text = string.Empty;
        RemoveFileButton.Visible = false;
    }

    void CmdPostFile_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
            try
            {
                if (!AddFile())
                {
                    // check extension error message property is blank if yes show default error message
                    if (string.IsNullOrEmpty(ExtentionErrorMessage))
                        ShowError(string.Format(ResHelper.GetString("Emerge.MediaFileUploader.ErrorMessage.DefaultExtensionError"), Extensions));
                    else
                        ShowError(ResHelper.GetString(ExtentionErrorMessage));
                }
            }
            catch (PermissionException )
            {

                ShowError(ResHelper.GetString("Emerge.MediaFileUploader.ErrorMessage.PermissionException"));
            }
           
        }
        else
        {
            // check extension error message property is blank if yes show default error message
            if (string.IsNullOrEmpty(ExtentionErrorMessage))
                ShowError(ResHelper.GetString("Emerge.MediaFileUploader.ErrorMessage.NoFileSelected"));
            else
                ShowError(ResHelper.GetString(ExtentionErrorMessage));
        }
    }

    /// <summary>
    /// Function to add a file to specified media folder.
    /// </summary>
    private bool AddFile()
    {
        bool result = ValidateExtensions();
        if (result)
        {
            CreateMediaLibrary();

            CreateMediaFolder();

            result = CreateMediaFile();

            if (result && UseRemoveButton)
            {
                if (String.IsNullOrEmpty(ValidationHelper.GetString(this.hdnFilePath.Value, string.Empty)))
                    RemoveFileButton.Visible = false;
                else
                    RemoveFileButton.Visible = true;
            }
        }
        
        return result;
    }

    /// <summary>
    /// Validate Uploaded file extention e.g. check .jpg, .png etc
    /// </summary>
    /// <returns></returns>
    private bool ValidateExtensions()
    {
        bool result = false;
        if (!string.IsNullOrEmpty(Extensions))
        {
            string[] extns = Extensions.ToLower().Split(',');
            string extension = System.IO.Path.GetExtension(FileUpload.FileName);
            return extns.Contains(extension.ToLower());
        }
        return result;
    }

    /// <summary>
    /// Created Media File.
    /// </summary>
    /// <returns>true if media file created.</returns>
    private bool CreateMediaFile()
    {
        string filePath = string.Empty;


        MediaLibraryInfo library = MediaLibraryInfoProvider.GetMediaLibraryInfo(Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString(), EmergeCMSContext.CurrentSiteName);
        if (library != null)
        {
            

            MediaFileInfo mediaFile = new MediaFileInfo(FileUpload.PostedFile, library.LibraryID, MediaFolderName, Convert.ToInt32(ImageWidth), Convert.ToInt32(ImageHeight),0);
            
            if (mediaFile != null)
            {

               

                MediaFileInfoProvider.SetMediaFileInfo(mediaFile);
                

                string fileURL = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(mediaFile.FileGUID, mediaFile.FileName);
                MediaLibraryInfo libraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(mediaFile.FileLibraryID);
                String urlFile = MediaFileURLProvider.GetMediaFileUrl(mediaFile, EmergeCMSContext.CurrentSiteName, libraryInfo.LibraryFolder);

                previewPlaceHolder.Text = GetPreviewHtml(mediaFile);

                hdnFilePath.Value = mediaFile.FileGUID.ToString();

                return true;
            }
        }

        return false;

    }

    /// <summary>
    /// Function to create Media Library (if not exists)
    /// </summary>
    private bool CreateMediaLibrary()
    {
        // Create new media library object

        MediaLibraryInfo updateLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString(), EmergeCMSContext.CurrentSiteName);

        if (updateLibrary == null)
        {
            MediaLibraryInfo newLibrary = new MediaLibraryInfo();

            // Set the properties
            newLibrary.LibraryDisplayName = Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString();
            newLibrary.LibraryName = Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString();
            newLibrary.LibraryDescription = Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString();
            newLibrary.LibraryFolder = Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString();
            newLibrary.LibrarySiteID = EmergeCMSContext.CurrentSiteID;
            newLibrary.LibraryGUID = Guid.NewGuid();
            newLibrary.LibraryLastModified = DateTime.Now;
            newLibrary.Access = SecurityAccessEnum.AuthenticatedUsers;
            // Create the media library
            MediaLibraryInfoProvider.SetMediaLibraryInfo(newLibrary);
        }
        return true;
    }

    /// <summary>
    /// Function to create Media Folder (if not exists)
    /// </summary>
    private void CreateMediaFolder()
    {
        // Get media library
        MediaLibraryInfo library = MediaLibraryInfoProvider.GetMediaLibraryInfo(Constants.MEDIA_LIBRARY_PREFIX + EmergeCMSContext.CurrentSiteName.ToString(), EmergeCMSContext.CurrentSiteName);
        if (library != null)
        {
            // Create new media folder object
            MediaLibraryInfoProvider.CreateMediaLibraryFolder(EmergeCMSContext.CurrentSiteName.ToString(), library.LibraryID, MediaFolderName, false);
        }

    }

    /// <summary>
    /// Function to get File Details by FileGuid to display file for Preview
    /// </summary>
    private string GetUploadedFileDetails(string FileGUID)
    {
        if (FileGUID.Trim().Equals(string.Empty)) return string.Empty;
        string displayText = string.Empty;
        int ItemID = QueryHelper.GetInteger("itemId", 0);

        string where = " ItemID = " + ItemID.ToString();

        Guid mFileGuid = ValidationHelper.GetGuid(FileGUID, Guid.Empty);
        MediaFileInfo mediaFile = MediaFileInfoProvider.GetMediaFileInfo(mFileGuid, EmergeCMSContext.CurrentSiteName);

        displayText = GetPreviewHtml(mediaFile);

        return displayText;
    }

    /// <summary>
    /// Function to return html for preview.(Depends on type of file)
    /// </summary>
    private string GetPreviewHtml(MediaFileInfo mediaFile)
    {
        if (mediaFile == null) return string.Empty;
        string previewHtml = string.Empty;
        string fileURL = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(mediaFile.FileGUID, mediaFile.FileName);

        if (mediaFile.FileMimeType.ToLower().Contains("image"))
        {
            string queryParam = string.Empty;

            if (ImageHeight == "0" && ImageWidth == "0" )
            {
                queryParam = "?Height=" + Constants.MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY.ToString();
                queryParam += "&Width=" + Convert.ToInt16(mediaFile.FileImageWidth * Constants.MAX_MEDIA_IMAGE_HEIGHT_FOR_DISPLAY / mediaFile.FileImageHeight).ToString();

            }
            //else if (mediaFile.FileImageWidth > Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY)
            //{

            //    queryParam = "?Width=" + Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY.ToString();
            //    queryParam += "&Height=" + Convert.ToInt16(mediaFile.FileImageHeight * Constants.MAX_MEDIA_IMAGE_WIDTH_FOR_DISPLAY / mediaFile.FileImageWidth).ToString();
            //}

            fileURL += queryParam;
            previewHtml = "<img src='" + fileURL + "' alt='" + mediaFile.FileName + "' />";
        }
        else
        {
            previewHtml = "<a href='" + fileURL + "' target='_blank' >" + mediaFile.FileName + " </a>";
        }

        return previewHtml;
    }

    #endregion methods
}