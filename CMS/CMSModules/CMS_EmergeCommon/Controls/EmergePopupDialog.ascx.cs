using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Web;

public partial class CMSModules_CMS_EmergeCommon_Controls_EmergePopupDialog : EmergeBaseCMSUserControl
{
    public event EventHandler OnOKButtonClick;
    public event EventHandler OnCancelButtonClick;

    #region Properties

    /// <summary>
    /// Gets or sets the text for the OK button.
    /// </summary>
    public string OKButtonText
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the text for the Cancel button.
    /// </summary>
    public string CancelButtonText
    {
        get;
        set;
    }

    /// <summary>
    /// Gets the control to add the content, controls, text, etc in the Popup.
    /// </summary>
    public Control Body
    {
        get
        {
            return InnerPanel;
        }
    }

    /// <summary>
    /// Gets or sets the Width of the popup.
    /// </summary>
    public string Width
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or set the height of the popup.
    /// </summary>
    public string Height
    {
        get;
        set;
    }

    private bool _showCancelButton = true;
    /// <summary>
    /// Gets or sets whether to show Cancel button.
    /// </summary>
    public bool ShowCancelButton
    {
        get
        {
            return _showCancelButton;
        }
        set
        {
            _showCancelButton = value;
        }
    }

    private bool _showOKButton = true;
    /// <summary>
    /// Gets or sets whether to show OK button.
    /// </summary>
    public bool ShowOKButton
    {
        get
        {
            return _showOKButton;
        }
        set
        {
            _showOKButton = value;
        }
    }

    /// <summary>
    /// Gets or sets the header text for the popup.
    /// </summary>
    public string HeaderText
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the X Coordinate for the popup.
    /// </summary>
    public int XCoordiante
    {
        get
        {
            return this.ModalPopupExtender.X;
        }
        set
        {
            this.ModalPopupExtender.X = value;
        }
    }

    /// <summary>
    /// Gets or sets the Y Coordinate for the popup.
    /// </summary>
    public int YCoordiante
    {
        get
        {
            return this.ModalPopupExtender.Y;
        }
        set
        {
            this.ModalPopupExtender.Y = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControl();
        RegisterEvents();
    }

    private void SetupControl()
    {
        OKButton.Text = OKButtonText;
        CancelButton.Text = CancelButtonText;
        PopupPanel.Width = new Unit(Width);
        PopupPanel.Height = new Unit(Height);
        OKButton.Visible = ShowOKButton;
        CancelButton.Visible = ShowCancelButton;
        this.PopupHeader.InnerHtml = HeaderText;
        this.PopupPanel.Style.Add("display", "none");
    }

    private void RegisterEvents()
    {
        OKButton.Click += OKButton_Click;
        CancelButton.Click += CancelButton_Click;
    }

    void CancelButton_Click(object sender, EventArgs e)
    {
        if (OnCancelButtonClick != null)
            OnCancelButtonClick(sender, e);
    }

    void OKButton_Click(object sender, EventArgs e)
    {
        if (OnOKButtonClick != null)
            OnOKButtonClick(sender, e);
    }

    /// <summary>
    /// Displays the popup.
    /// </summary>
    public void Show()
    {
        this.PopupPanel.Style.Add("display", "");// = true;
        this.ModalPopupExtender.Show();
    }

    /// <summary>
    /// Hides the popup.
    /// </summary>
    public void Hide()
    {
        this.PopupPanel.Style.Add("display", "none");
        this.ModalPopupExtender.Hide();
    }
}