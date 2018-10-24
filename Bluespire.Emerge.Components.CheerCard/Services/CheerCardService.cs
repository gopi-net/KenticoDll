using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Common.CMS.MediaLibrary;
using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.CMSHelper;

namespace Bluespire.Emerge.Components.CheerCard.Services
{
    /// <summary>
    /// Cheer card Service.
    /// </summary>
    public class CheerCardService : ICheerCardsService
    {
        #region ICheerCardsService Members
        /// <summary>
        /// method to Get CheerCard Images
        /// </summary>
        /// <param name="WhereCondition">Where condition on the cheer card items.</param>
        /// <returns>dataset containing cheer card items.</returns>
        public DataSet GetCheerCardImagesByCriteria(string WhereCondition)
        {
            DataSet ds = GetCustomTableItemsByCriteria(string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_IMAGES, EmergeSiteInfoProvider.CurrentSiteName), WhereCondition);
            return ds;
        }
        /// <summary>
        /// method to Get CheerCard Requests 
        /// </summary>
        /// <param name="WhereCondition">Where condition on the cheer card items.</param>
        /// <exception cref="CheerCardItemNotFound"> Thrown in case of message configuration items not found.</exception>
        /// <returns>dataset containing cheer card items.</returns>
        private DataSet GetCheerCardRequestItemsByCriteria(string WhereCondition)
        {
            DataSet ds = GetCustomTableItemsByCriteria(string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS, EmergeSiteInfoProvider.CurrentSiteName), WhereCondition);
            if (ds == null || EmergeDataHelper.DataSourceIsEmpty(ds) || ds.Tables[0].Rows.Count == 0)
                throw new CheerCardItemNotFoundException(WhereCondition);
            return ds;
        }

        /// <summary>
        /// Method to Get Cheer Card Message configurations which will be used to write message on Preview card.
        /// </summary>
        /// <param name="WhereCondition">Where condition on the cheer card message configuration items.</param>
        /// <exception cref="CheerCardPreviewImageConfigItemsNotFound"> Thrown in case of message configuration items not found.</exception>
        /// <returns>Dataset containing Cheer Card Message configurations items.</returns>
        public DataSet GetCheerCardMessageConfigurationsByCriteria(string WhereCondition)
        {
            DataSet ds = GetCustomTableItemsByCriteria(string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_ATTACHMENT_IMAGE_CONFIGURATIONS, EmergeSiteInfoProvider.CurrentSiteName), WhereCondition);
            if (ds == null || EmergeDataHelper.DataSourceIsEmpty(ds) || ds.Tables[0].Rows.Count == 0)
                throw new CheerCardPreviewImageConfigItemsNotFound(WhereCondition);
            return ds;
        }
        /// <summary>
        /// method to Get CheerCard Request Items by ID
        /// </summary>
        /// <param name="ID">cheer card item id.</param>
        /// <exception cref="CheerCardItemNotFound"> Thrown in case of message configuration items not found.</exception>
        /// <returns>dataset containing cheer card item.</returns>
        public DataSet GetCheerCardRequestItemByID(int ID)
        {
            string WhereCondition = " ItemID = " + ID;
            DataSet ds = GetCheerCardRequestItemsByCriteria(WhereCondition);
            return ds;
        }
        /// <summary>
        /// method to Get CheerCard Categories by ID
        /// </summary>
        /// <param name="WhereCondition">Where condition on the cheer card Categories.</param>
        /// <returns>dataset containing cheer card categories.</returns>
        public DataSet GetCheerCardCategoriesByCriteria(string WhereCondition)
        {
            DataSet ds = GetCustomTableItemsByCriteria(string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_CATEGORIES, EmergeSiteInfoProvider.CurrentSiteName), WhereCondition);
            return ds;
        }
        /// <summary>
        /// method to Save Cheer Card Request.
        /// </summary>
        /// <param name="FormParameters">Dictionary containing all form fields.</param>
        /// <returns>ItemID of new item.</returns>
        public int SaveCheerCardRequest(Dictionary<string, object> FormParameters)
        {
            int ItemID = 0;
            CustomTableDataHelper.SaveCustomTableItem(String.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS, EmergeSiteInfoProvider.CurrentSiteName), ref ItemID, FormParameters);
            return ItemID;
        }

        /// <summary>
        /// method to Create and Save Preview Cheer card Image.
        /// </summary>
        /// <param name="FormParameters">Dictionary containing Key value pairs.</param>
        /// <param name="selectedCardGuid">GUID of Selected Cheer card Image.in case of No image selected then pass 'NoImage'</param>
        /// <param name="AttachmentImageConfigurationInfo">AttachmentImageConfigurationInfo.</param>
        /// <param name="EmptyStringPlaceHolder">Empty String placeholder.</param>
        /// <exception cref="CheerCardConfigurationItemNotFound"> Thrown in case of key not found in Cheer Card Configuration Custom Table.</exception>
        /// <exception cref="CheerCardPreviewImageConfigItemsNotFound"> Thrown in case of items nor found in Cheer Card Preview image Configuration Custom Table.</exception>
        /// <exception cref="CheerCardPreviewSaveExternalException"> Thrown in case of InteropServices.ExternalException while saving the image.</exception>
        /// <returns>preview image path.</returns>
        public string CreateAndSaveCheerCardAttachmentImage(Dictionary<string, object> FormParameters, string selectedCardGuid, CheerCardAttachementImageConfigurationInfo AttachmentImageConfigurationInfo, string EmptyStringPlaceHolder)
        {
            Image imgBG;
            Graphics g;
            bool isCheerCardImageSelected = true;
            if (selectedCardGuid.ToLower().Equals(CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT.ToLower()))
                isCheerCardImageSelected = false;
            imgBG = GetBackgroundImage(isCheerCardImageSelected, AttachmentImageConfigurationInfo);
            g = Graphics.FromImage(imgBG);
            if (isCheerCardImageSelected)
                WriteCheerCardImageOnBackgroundImage(selectedCardGuid, g, AttachmentImageConfigurationInfo);
            WriteMessageOnBackgroundImage(g, FormParameters, isCheerCardImageSelected, EmptyStringPlaceHolder);
            string path = SaveImage(imgBG);
            g.Dispose();
            imgBG.Dispose();
            return path;
        }

        /// <summary>
        /// method to save the created preview image.
        /// </summary>
        /// <param name="imgBG">Preview image</param>
        /// <returns>path of the saved preview image.</returns>
        private string SaveImage(Image imgBG)
        {
            const string imageExtention = ".bmp";
            string imageName = Guid.NewGuid().ToString() + imageExtention;
            string path = string.Empty;
            try
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(Constants.TEMP_IMAGES_FOLDER_NAME)))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(Constants.TEMP_IMAGES_FOLDER_NAME));
                imgBG.Save(HttpContext.Current.Server.MapPath(Constants.TEMP_IMAGES_FOLDER_NAME + "/" + imageName), ImageFormat.Bmp);
                imgBG.Dispose();
                path = Constants.TEMP_IMAGES_FOLDER_NAME + "/" + imageName;
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                EmergeLogWriter.WriteError("CheerCard Service - CreateAndSavePreviewImage", EventCode.EMERGE_ADD, ex.ToString());
                throw new CheerCardPreviewSaveExternalException();
            }
            return path;
        }
        /// <summary>
        /// returns background image.
        /// </summary>
        /// <param name="isCheerCardImageSelected">true if user has selected image for cheer card.</param>
        /// <returns></returns>
        private Image GetBackgroundImage(bool isCheerCardImageSelected, CheerCardAttachementImageConfigurationInfo AttachmentImageConfigurationInfo)
        {
            Image imgBG;
            if (isCheerCardImageSelected)
                imgBG = Image.FromFile(HttpContext.Current.Server.MapPath(AttachmentImageConfigurationInfo.BackgroundImagePathWithCCImage));
            else
                imgBG = Image.FromFile(HttpContext.Current.Server.MapPath(AttachmentImageConfigurationInfo.BackgroundImagePathWithNoCCImage));
            return imgBG;
        }

        /// <summary>
        /// method to Get Preview Card Html.
        /// </summary>
        /// <param name="FormParameters">Dictionary Item containing all Form fields along with their values .</param>
        /// <param name="Environment">enum which can be set to the environment to which webpart belogs to. (enviroment contains Mobile, Desktop..) .</param>
        /// <param name="selectedImageGuid">Selected cheer card Image guid.</param>
        /// <param name="EmptyStringPlaceHolder">EmptyStringPlaceHolder.</param>
        /// <param name="isCalledFromCmsDesk">True if Called from CMSDesk </param>
        /// <exception cref="CheerCardPreviewHtmlItemNotFound"> Thrown in case of key not found in Cheer Card Configuration Custom Table.</exception>
        /// <returns>Html of Preview Card.</returns>
        public string GetCheerCardPreviewHtml(Dictionary<string, object> FormParameters, Constants.Environments Environment, string selectedImageGuid, string EmptyStringPlaceHolder, bool isCalledFromCmsDesk)
        {
            const string PreviewHtmlKeyColumnName = "[Key]";
            const string PreviewHtmlValueColumnName = "Html";
            const string PreviewHtmlCheerCardImagePlaceHolder = "[DisplayImage]";
            string Key = string.Empty;
            Key = Environment.ToString();
            Key += selectedImageGuid.ToLower().Equals(CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT.ToLower()) ? CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT : string.Empty;
            string WhereCondition = PreviewHtmlKeyColumnName + Constants.WHERE_CONDITION_OPERATOR_EQUAL + Constants.WHERE_CONDITION_SINGLE_QUOTE + Key.Trim() + Constants.WHERE_CONDITION_SINGLE_QUOTE;
            DataSet ds = GetCustomTableItemsByCriteria(string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_PREVIEW_HTML_CONFIGURATIONS, EmergeSiteInfoProvider.CurrentSiteName), WhereCondition);
            if (ds == null || EmergeDataHelper.DataSourceIsEmpty(ds) || ds.Tables[0].Rows.Count == 0)
                throw new CheerCardPreviewHtmlItemNotFound(WhereCondition);
            string PreviewHtml = string.Empty;
            PreviewHtml = ds.Tables[0].Rows[0][PreviewHtmlValueColumnName].ToString();
            PreviewHtml = ReplacePlaceHolders(PreviewHtml, FormParameters, EmptyStringPlaceHolder);
            string fileUrl = string.Empty;
            if (!string.IsNullOrEmpty(selectedImageGuid))
            {
                if (isCalledFromCmsDesk)
                    fileUrl = EmergeMediaFileInfoProvider.GetMediaFileAbsoluteUrl(selectedImageGuid);
                else
                    fileUrl = EmergeMediaLibraryHelper.GetMediaFileUrl(selectedImageGuid, EmergeSiteInfoProvider.CurrentSiteName);
                PreviewHtml = PreviewHtml.Replace(PreviewHtmlCheerCardImagePlaceHolder, fileUrl);
            }
            return PreviewHtml;
        }

        /// <summary>
        /// method to delete cheer card preview image.
        /// </summary>
        public void DeleteImage(string imagePath)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(imagePath))) { File.Delete(HttpContext.Current.Server.MapPath(imagePath)); }
        }

        /// <summary>
        /// method to Replace place holders in the Source string.
        /// </summary>
        /// <param name="sourceString">Source String.</param>
        /// <param name="parameters">Dictionary Items. Source String will be searched with the Keys in the Dictionary  and replace with values in the Dictionary</param>
        /// <param name="emptyStringPlaceHolder">Empty String placeholder.</param>
        /// <returns>string after replacements.</returns>
        public string ReplacePlaceHolders(string sourceString, Dictionary<string, object> parameters, string emptyStringPlaceHolder)
        {
            string CheerCardRequestCustomtTableCodename = string.Format(CheerCardConstants.CUSTOMTABLE_CODENAME_FOR_CHEER_CARD_REQUESTS, EmergeSiteInfoProvider.CurrentSiteName);
            StringBuilder destinationString = new StringBuilder(sourceString);
            foreach (string keyInFormFields in parameters.Keys)
            {
                if (destinationString.ToString().ToLower().Contains("[" + keyInFormFields.ToLower() + "]"))
                {
                    string valueInFormField = parameters.Where(x => x.Key.ToLower().Equals(keyInFormFields.ToLower())).First().Value.ToString();
                    string returnValue = EmergeRelationHelper.GetRelationColumnValue(CheerCardRequestCustomtTableCodename, keyInFormFields, valueInFormField);
                    destinationString.Replace("[" + keyInFormFields + "]", returnValue.Trim().Equals(string.Empty) ? (valueInFormField.Trim().Equals(string.Empty) ? emptyStringPlaceHolder : valueInFormField.Trim()) : returnValue);
                }
            }
            return destinationString.ToString();
        }

        /// <summary>
        /// method to Replace place holders in the Source string.
        /// </summary>
        /// <param name="sourceString">Source String.</param>
        /// <param name="dt">datatable containing values to replace. Source String will be searched with the column names in the datatble and replace with values in the datatable</param>
        /// <param name="emptyStringPlaceHolder">Empty String placeholder.</param>
        /// <returns>string after replacements.</returns>
        public string ReplacePlaceHolders(string sourceString, DataTable dt, string emptyStringPlaceHolder)
        {
            return ReplacePlaceHolders(sourceString, GetDictionary(dt), emptyStringPlaceHolder);
        }

        private Dictionary<string, object> GetDictionary(DataTable dt)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (DataColumn column in dt.Columns)
            {
                parameters.Add(column.ColumnName, dt.Rows[0][column.ColumnName] == null ? string.Empty : dt.Rows[0][column.ColumnName].ToString());
            }
            return parameters;
        }
        /// <summary>
        /// method to Get custom table Items.
        /// </summary>
        /// <param name="CustomTableCodeName">Code Name of the Custom Table.</param>
        /// <param name="WhereCondition">Where condition on the cheer card items.</param>
        /// <returns>dataset containing Custom Table items.</returns>
        private DataSet GetCustomTableItemsByCriteria(string CustomTableCodeName, string WhereCondition)
        {
            if (CustomTableDataHelper.HasStatusField(CustomTableCodeName))
            {
                if (string.IsNullOrEmpty(WhereCondition))
                    WhereCondition = Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
                else
                    WhereCondition += Constants.WHERE_CONDITION_OPERATOR_AND + Constants.WHERE_CONDITION_FOR_CUSTOM_TABLE_ITEMS_WITH_ACTIVE_STATUS;
            }
            return CustomTableDataHelper.GetCustomTableItemsByCondition(CustomTableCodeName, WhereCondition, string.Empty);
        }

        /// method to write text on the background image.
        /// </summary>
        /// <param name="ds">dataset containing configuration records like what to write and position to write.</param>
        /// <param name="g">g.</param>
        /// <param name="FormParameters">Form Parameters</param>
        private void WriteMessageOnBackgroundImage(Graphics g, Dictionary<string, object> FormParameters, bool isCheerCardImageSelected, string emptyStringPlaceholder)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            emptyStringPlaceholder = emptyStringPlaceholder.Trim().Equals(CheerCardConstants.CHEER_CARD_CONFIG_EMPTY_STRING_PLACEHOLDER) ? string.Empty : emptyStringPlaceholder;
            List<CheerCardAttachementImageConfigurationInfo> MessageConfigurations = CheerCardAttachmentConfigurationService.LoadMessageConfigurations(isCheerCardImageSelected);
            foreach (CheerCardAttachementImageConfigurationInfo MessageConfiguration in MessageConfigurations)
            {
                if (!MessageConfiguration.CaptionText.Trim().Equals(string.Empty))
                {
                    if (IsDrawRectangle(MessageConfiguration.CaptionBlockHeight.ToString(), MessageConfiguration.CaptionBlockWidth.ToString()))
                    {
                        RectangleF rectangle = new RectangleF(MessageConfiguration.CaptionXPosition, MessageConfiguration.CaptionYPosition, MessageConfiguration.CaptionBlockWidth, MessageConfiguration.CaptionBlockHeight);
                        g.DrawString(MessageConfiguration.CaptionText, MessageConfiguration.CaptionFont, MessageConfiguration.CaptionBrush, rectangle, new StringFormat(StringFormatFlags.FitBlackBox));
                    }
                    else
                        g.DrawString(MessageConfiguration.CaptionText, MessageConfiguration.CaptionFont, MessageConfiguration.CaptionBrush, MessageConfiguration.CaptionXPosition, MessageConfiguration.CaptionYPosition);
                }
                if (!MessageConfiguration.ValueText.Trim().Equals(string.Empty))
                {
                    MessageConfiguration.ValueText = ReplacePlaceHolders(MessageConfiguration.ValueText, FormParameters, emptyStringPlaceholder).Trim();
                    if (IsDrawRectangle(MessageConfiguration.ValueBlockHeight.ToString(), MessageConfiguration.ValueBlockWidth.ToString()))
                    {
                        RectangleF rectangle = new RectangleF(MessageConfiguration.ValueXPosition, MessageConfiguration.ValueYPosition, MessageConfiguration.ValueBlockWidth, MessageConfiguration.ValueBlockHeight);
                        g.DrawString(MessageConfiguration.ValueText, MessageConfiguration.ValueFont, MessageConfiguration.ValueBrush, rectangle, new StringFormat(StringFormatFlags.FitBlackBox));
                    }
                    else
                        g.DrawString(MessageConfiguration.ValueText, MessageConfiguration.ValueFont, MessageConfiguration.ValueBrush, MessageConfiguration.ValueXPosition, MessageConfiguration.ValueYPosition);
                }
            }
        }

        /// method to write selected cheer card image on the background image.
        /// </summary>
        /// <param name="selectedCardGuid">Selected Cheer card image.</param>
        /// <param name="g">g.</param>
        private void WriteCheerCardImageOnBackgroundImage(string selectedCardGuid, Graphics g, CheerCardAttachementImageConfigurationInfo AttachmentImageConfigurationInfo)
        {
            Bitmap bmpSelectedCheerCard = new Bitmap(HttpUtility.UrlDecode(HttpContext.Current.Server.MapPath(EmergeMediaLibraryHelper.GetMediaFileUrl(selectedCardGuid, EmergeSiteInfoProvider.CurrentSiteName))));
            double scaleFactorH = 1.0d;
            double scaleFactorW = 1.0d;
            double scaleFactorSmallest = 1.0d;
            scaleFactorH = GetScaleValue(bmpSelectedCheerCard.Height, AttachmentImageConfigurationInfo.ImageMaxHeight);
            scaleFactorW = GetScaleValue(bmpSelectedCheerCard.Width, AttachmentImageConfigurationInfo.ImageMaxWidth);
            scaleFactorSmallest = scaleFactorH >= scaleFactorW ? scaleFactorW : scaleFactorH;
            if (scaleFactorSmallest != 1.0d)
            {
                int newHeight = (int)(bmpSelectedCheerCard.Height * scaleFactorSmallest);
                int newWidth = (int)(bmpSelectedCheerCard.Width * scaleFactorSmallest);
                Bitmap thumbnailImg;
                using (bmpSelectedCheerCard)
                {
                    thumbnailImg = new Bitmap(newWidth, newHeight);
                    var thumbGraph = Graphics.FromImage(thumbnailImg);
                    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(bmpSelectedCheerCard, imageRectangle);
                }
                bmpSelectedCheerCard = thumbnailImg;
            }
            Point p = new Point(AttachmentImageConfigurationInfo.ImageXPosition, AttachmentImageConfigurationInfo.ImageYPosition);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(bmpSelectedCheerCard, p);
        }

        /// <summary>
        /// method to Check whether to draw a rectangle shape on Image. If Height and width available, then this will return true.
        /// </summary>
        /// <param name="Height">Height.</param>
        /// <param name="Width">Width.</param>
        /// <returns>True if Height and Width both contains Value.</returns>
        private bool IsDrawRectangle(string Height, string Width)
        {
            if (Height.Trim().Equals(string.Empty)) return false;
            if (Height.Trim().Equals("0")) return false;
            if (Width.Trim().Equals(string.Empty)) return false;
            if (Width.Trim().Equals("0")) return false;
            return true;
        }

        /// <summary>
        /// method to return the scaling factior for Image.
        /// </summary>
        /// <param name="origionalDimension">origional Dimension( Height or width) of Image.</param>
        /// <param name="maxDimension">max Dimension ( Height or width) of Image.</param>
        /// <returns>scaling factor</returns>
        private double GetScaleValue(int origionalDimension, int maxDimension)
        {
            double newScale = 1.0d;
            newScale = Convert.ToDouble(maxDimension) / origionalDimension;
            return newScale;
        }
        #endregion
    }
}
