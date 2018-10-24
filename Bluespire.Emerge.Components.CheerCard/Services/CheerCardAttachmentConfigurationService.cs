using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Bluespire.Emerge.Common;

namespace Bluespire.Emerge.Components.CheerCard.Services
{
    /// <summary>
    /// Cheer card Configuration Service used to get message, images configurations which will be used to form cheer card Image.
    /// </summary>
    public class CheerCardAttachmentConfigurationService
    {
        /// <summary>
        /// Method to get Configuration settings to write CHEER CARD Message on background Image.
        /// </summary>
        /// <param name="Environment">enum which indicates the environment to which webpart belogs to. (enviroment contains Mobile, Desktop..)</param>
        /// <param name="isCheerCardImageSelected">true if Cheer card image is selected. </param>
        /// <returns>List of Cheercard configurations</returns>
        public static List<CheerCardAttachementImageConfigurationInfo> LoadMessageConfigurations(bool isCheerCardImageSelected)
        {
            List<CheerCardAttachementImageConfigurationInfo> MessageConfigurations = new List<CheerCardAttachementImageConfigurationInfo>();
            ICheerCardsService CheerCardService = new CheerCardService();
            string Key = isCheerCardImageSelected ? CheerCardConstants.CHEER_CARD_ATTACHMENT_MESSAGE_CONFIGURATION_KEYVALUE_FOR_WITHCCIMAGE : CheerCardConstants.CHEER_CARD_ATTACHMENT_MESSAGE_CONFIGURATION_KEYVALUE_FOR_WITHNOCCIMAGE;
            DataSet ds = CheerCardService.GetCheerCardMessageConfigurationsByCriteria(CheerCardConstants.CHEERCARD_ATTACHMENTIMAGECONFIGURATION_KEY_COLUMNNAME + Constants.WHERE_CONDITION_OPERATOR_EQUAL + Constants.WHERE_CONDITION_SINGLE_QUOTE + Key + Constants.WHERE_CONDITION_SINGLE_QUOTE);
            foreach (DataRow drLine in ds.Tables[0].Rows)
            {
                CheerCardAttachementImageConfigurationInfo MessageConfiguration = new CheerCardAttachementImageConfigurationInfo();
                if (!drLine[CheerCardConstants.CAPTION_COLUMNNAME].ToString().Trim().Equals(string.Empty))
                {
                    MessageConfiguration.CaptionText = drLine[CheerCardConstants.CAPTION_COLUMNNAME].ToString().Trim();
                    FontStyle fontStyleCaption = new FontStyle();
                    foreach (string style in drLine[CheerCardConstants.CAPTION_FONT_STYLE_COLUMNNAME].ToString().Trim().Split(',').ToArray())
                    {
                        fontStyleCaption |= GetFontStyle(style);
                    }
                    MessageConfiguration.CaptionXPosition = Int32.Parse(drLine[CheerCardConstants.CAPTION_COORDINATE_X_COLUMNNAME].ToString());
                    MessageConfiguration.CaptionYPosition = Int32.Parse(drLine[CheerCardConstants.CAPTION_COORDINATE_Y_COLUMNNAME].ToString());
                    MessageConfiguration.CaptionBlockHeight = Int32.Parse(drLine[CheerCardConstants.CAPTION_BLOCK_HEIGHT_COLUMNNAME].ToString().Trim().Equals(string.Empty) ? "0" : drLine[CheerCardConstants.CAPTION_BLOCK_HEIGHT_COLUMNNAME].ToString());
                    MessageConfiguration.CaptionBlockWidth = Int32.Parse(drLine[CheerCardConstants.CAPTION_BLOCK_WIDTH_COLUMNNAME].ToString().Trim().Equals(string.Empty) ? "0" : drLine[CheerCardConstants.CAPTION_BLOCK_WIDTH_COLUMNNAME].ToString());
                    MessageConfiguration.CaptionBrush = new SolidBrush(Color.FromArgb(Int32.Parse(drLine[CheerCardConstants.CAPTION_BRUSHCOLOR_R_COLUMNNAME].ToString()), Int32.Parse(drLine[CheerCardConstants.CAPTION_BRUSHCOLOR_G_COLUMNNAME].ToString()), Int32.Parse(drLine[CheerCardConstants.CAPTION_BRUSHCOLOR_B_COLUMNNAME].ToString())));
                    MessageConfiguration.CaptionFont = new Font(drLine[CheerCardConstants.CAPTION_FONT_NAME_COLUMNNAME].ToString().Trim(), int.Parse(drLine[CheerCardConstants.CAPTION_FONT_SIZE_COLUMNNAME].ToString().Trim()), fontStyleCaption, GraphicsUnit.Pixel);
                }
                if (!drLine[CheerCardConstants.VALUE_COLUMNNAME].ToString().Trim().Equals(string.Empty))
                {
                    MessageConfiguration.ValueText = drLine[CheerCardConstants.VALUE_COLUMNNAME].ToString();
                    FontStyle fontStyleText = new FontStyle();
                    foreach (string style in drLine[CheerCardConstants.VALUE_FONT_STYLE_COLUMNNAME].ToString().Trim().Split(',').ToArray())
                    {
                        fontStyleText |= GetFontStyle(style);
                    }
                    MessageConfiguration.ValueBrush = new SolidBrush(Color.FromArgb(Int32.Parse(drLine[CheerCardConstants.VALUE_BRUSHCOLOR_R_COLUMNNAME].ToString()), Int32.Parse(drLine[CheerCardConstants.VALUE_BRUSHCOLOR_G_COLUMNNAME].ToString()), Int32.Parse(drLine[CheerCardConstants.VALUE_BRUSHCOLOR_B_COLUMNNAME].ToString())));
                    MessageConfiguration.ValueFont = new Font(drLine[CheerCardConstants.VALUE_FONT_NAME_COLUMNNAME].ToString().Trim(), float.Parse(drLine[CheerCardConstants.VALUE_FONT_SIZE_COLUMNNAME].ToString().Trim()), fontStyleText, GraphicsUnit.Pixel);
                    MessageConfiguration.ValueXPosition = Int32.Parse(drLine[CheerCardConstants.VALUE_COORDINATE_X_COLUMNNAME].ToString());
                    MessageConfiguration.ValueYPosition = Int32.Parse(drLine[CheerCardConstants.VALUE_COORDINATE_Y_COLUMNNAME].ToString());
                    MessageConfiguration.ValueBlockHeight = Int32.Parse(drLine[CheerCardConstants.VALUE_BLOCK_HEIGHT_COLUMNNAME].ToString().Trim().Equals(string.Empty) ? "0" : drLine[CheerCardConstants.VALUE_BLOCK_HEIGHT_COLUMNNAME].ToString());
                    MessageConfiguration.ValueBlockWidth = Int32.Parse(drLine[CheerCardConstants.VALUE_BLOCK_WIDTH_COLUMNNAME].ToString().Trim().Equals(string.Empty) ? "0" : drLine[CheerCardConstants.VALUE_BLOCK_WIDTH_COLUMNNAME].ToString());
                }
                MessageConfigurations.Add(MessageConfiguration);
            }
            return MessageConfigurations;
        }

        /// <summary>
        /// method to return FontStyle enum value depending upon string in cheer card preview configuration custom table.
        /// </summary>
        /// <param name="fontStyle">fontStyle.</param>
        /// <returns>FontStyle enum.</returns>
        private static FontStyle GetFontStyle(string fontStyle)
        {
            switch (fontStyle.ToString().ToLower())
            {
                case "bold": return FontStyle.Bold;
                case "italic": return FontStyle.Italic;
                case "regular": return FontStyle.Regular;
                case "strikeout": return FontStyle.Strikeout;
                case "underline": return FontStyle.Underline;
                default: return FontStyle.Regular;
            }
        }
    }
}