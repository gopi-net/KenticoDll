using System.Drawing;

namespace Bluespire.Emerge.Components.CheerCard.Services
{
    /// <summary>
    /// Class that represents cheer card configurations.
    /// </summary>
    public class CheerCardAttachementImageConfigurationInfo
    {
        #region variables
        int _ImageMaxHeight = 0;
        int _ImageMaxWidth = 0;
        int _ImageXPosition = 0;
        int _ImageYPosition = 0;
        string _backgroundImagePathWithCCImage = string.Empty;
        string _backgroundImagePathWithNoCCImage = string.Empty;
        string _CaptionText = string.Empty;
        int _CaptionXPosition = 0;
        int _CaptionYPosition = 0;
        int _CaptionBlockHeight = 0;
        int _CaptionBlockWidth = 0;
        string _ValueText = string.Empty;
        int _ValueXPosition = 0;
        int _ValueYPosition = 0;
        int _ValueBlockHeight = 0;
        int _ValueBlockWidth = 0;
        #endregion variables

        #region "image properties"
        public int ImageMaxHeight { get{ return _ImageMaxHeight;} set {_ImageMaxHeight=value;} }
        public int ImageMaxWidth { get { return _ImageMaxWidth; } set { _ImageMaxWidth = value; } }
        public int ImageXPosition { get { return _ImageXPosition; } set { _ImageXPosition = value; } }
        public int ImageYPosition { get { return _ImageYPosition; } set { _ImageYPosition = value; } }
        #endregion "image properties"

        #region "background image properties"
        public string BackgroundImagePathWithCCImage { get { return _backgroundImagePathWithCCImage; } set { _backgroundImagePathWithCCImage = value; } }
        public string BackgroundImagePathWithNoCCImage { get { return _backgroundImagePathWithNoCCImage; } set { _backgroundImagePathWithNoCCImage = value; } }
        #endregion

        #region "message properties"

        public string CaptionText { get { return _CaptionText; } set { _CaptionText = value; } }
        public Font CaptionFont { get; set; }
        public SolidBrush CaptionBrush { get; set; }
        public int CaptionXPosition { get { return _CaptionXPosition; } set { _CaptionXPosition = value; } }
        public int CaptionYPosition { get { return _CaptionYPosition; } set { _CaptionYPosition = value; } }
        public int CaptionBlockHeight { get { return _CaptionBlockHeight; } set { _CaptionBlockHeight = value; } }
        public int CaptionBlockWidth { get { return _CaptionBlockWidth; } set { _CaptionBlockWidth = value; } }
        public string ValueText { get { return _ValueText; } set { _ValueText = value; } }
        public Font ValueFont { get; set; }
        public SolidBrush ValueBrush { get; set; }
        public int ValueXPosition { get { return _ValueXPosition; } set { _ValueXPosition = value; } }
        public int ValueYPosition { get { return _ValueYPosition; } set { _ValueYPosition = value; } }
        public int ValueBlockHeight { get { return _ValueBlockHeight; } set { _ValueBlockHeight = value; } }
        public int ValueBlockWidth { get { return _ValueBlockWidth; } set { _ValueBlockWidth = value; } }
        #endregion "message properties"

    }
}
