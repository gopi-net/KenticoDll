using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Helpers;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_CMS_EmergePortalEngine_Controls_Layout_PageTemplateSelector : CMSAdminControl
{
    #region "Variables"

    private bool mShowOnlySiteTemplates = true;

    #endregion


    #region "Page template selector properties"

    /// <summary>
    /// If enabled ad-hoc templates will be displayed in selector.
    /// </summary>
    public bool ShowAdHoc
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets a value indicating whether to show site page templates only.
    /// </summary>
    public bool ShowOnlySiteTemplates
    {
        get
        {
            return mShowOnlySiteTemplates;
        }
        set
        {
            mShowOnlySiteTemplates = value;
        }
    }


    /// <summary>
    /// Indicates whether only templates for product section should be displayed.
    /// </summary>
    public bool ShowOnlyProductSectionTemplates
    {
        get;
        set;
    }

    #endregion


    #region "Selector properties"

    /// <summary>
    /// Gets or sets the value that indicates whether empty categories should be displayed.
    /// </summary>
    public bool ShowEmptyCategories
    {
        get
        {
            return treeElem.ShowEmptyCategories;
        }
        set
        {
            treeElem.ShowEmptyCategories = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether stratup focus fuctionallity is enabled.
    /// </summary>
    public bool UseStartUpFocus
    {
        get
        {
            return flatElem.UniFlatSelector.UseStartUpFocus;
        }
        set
        {
            flatElem.UniFlatSelector.UseStartUpFocus = value;
        }
    }


    /// <summary>
    /// Gets or set the flat panel selected item.
    /// </summary>
    public string SelectedItem
    {
        get
        {
            return flatElem.SelectedItem;
        }
        set
        {
            flatElem.SelectedItem = value;
        }
    }


    /// <summary>
    /// Selected item for tree.
    /// </summary>
    public String TreeSelectedCategory
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets selected item in tree.
    /// </summary>
    public string SelectedCategory
    {
        get
        {
            return ValidationHelper.GetString(treeElem.SelectedItem, "");
        }
        set
        {
            treeElem.SelectedItem = value;
        }
    }


    /// <summary>
    /// Gets or sets name of javascript function used for passing selected value from flat selector.
    /// </summary>
    public string SelectFunction
    {
        get
        {
            return flatElem.UniFlatSelector.SelectFunction;
        }
        set
        {
            flatElem.UniFlatSelector.SelectFunction = value;
        }
    }


    /// <summary>
    /// If enabled, flat selector remembers selected item trough postbacks.
    /// </summary>
    public bool RememberSelectedItem
    {
        get
        {
            return flatElem.UniFlatSelector.RememberSelectedItem;
        }
        set
        {
            flatElem.UniFlatSelector.RememberSelectedItem = value;
        }
    }


    /// <summary>
    /// Enables  or disables stop processing.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            flatElem.StopProcessing = value;
            treeElem.StopProcessing = value;
            EnableViewState = !value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            treeElem.IsLiveSite = value;
            flatElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets document id.
    /// </summary>
    public int DocumentID
    {
        get;
        set;
    }


    /// <summary>
    /// Document node GUID to identify ad-hoc templates
    /// </summary>
    public Guid NodeGUID
    {
        get;
        set;
    }


    /// <summary>
    /// Whether selecting new page.
    /// </summary>
    public bool IsNewPage
    {
        get;
        set;
    }


    /// <summary>
    /// Root category
    /// </summary>
    public int RootCategory
    {
        get;
        set;
    }

    #endregion


    #region "Page methods and events"

    /// <summary>
    /// OnInit.
    /// </summary>    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        treeElem.SelectPath = "/";
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Preselect root category
        if (!RequestHelper.IsPostBack())
        {
            ResetToDefault();
        }
        else
        {
            LoadRootCategory();
        }

        if (!ShowAdHoc)
        {
            treeElem.ShowAdHocCategory = false;
            flatElem.ShowOnlyReusable = true;
        }

        // Offer only templates for product section
        if (ShowOnlyProductSectionTemplates)
        {
            flatElem.ShowOnlyProductSectionTemplates = true;
        }

        if (ShowOnlySiteTemplates)
        {
            // Show only templates belonging to current site
            flatElem.SiteId = SiteContext.CurrentSiteID;
        }

        treeElem.ShowOnlySiteTemplates = ShowOnlySiteTemplates;

        // Set node id
        flatElem.DocumentID = DocumentID;
        treeElem.DocumentID = DocumentID;

        flatElem.IsNewPage = IsNewPage;
        treeElem.IsNewPage = IsNewPage;

        if (!IsNewPage && (NodeGUID != Guid.Empty))
        {
            treeElem.ShowAdHocCategory = true;
            flatElem.NodeGUID = NodeGUID;
        }
    }


    /// <summary>
    /// PreRender.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // Pass currently selected category to flat selector
        if (RequestHelper.IsPostBack())
        {
            flatElem.TreeSelectedItem = treeElem.SelectedItem;
        }
    }


    /// <summary>
    /// On tree element item selected.
    /// </summary>
    /// <param name="selectedValue">Selected value</param> 
    protected void treeElem_OnItemSelected(string selectedValue)
    {
        flatElem.TreeSelectedItem = selectedValue;

        // Clear search box and pager
        flatElem.UniFlatSelector.ResetToDefault();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    /// <param name="reloadFlat">If true, flat selector is reloaded</param>
    public override void ReloadData(bool reloadFlat)
    {
        treeElem.ReloadData();
        if (reloadFlat)
        {
            flatElem.ReloadData();
        }
    }


    /// <summary>
    /// Sets the root category to the control
    /// </summary>
    protected PageTemplateCategoryInfo LoadRootCategory()
    {
        PageTemplateCategoryInfo ptci = null;

        if (RootCategory > 0)
        {
            ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(RootCategory);
            treeElem.MultipleRoots = false;
        }
        else
        {
            ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfoByCodeName("/");
        }
        if (ptci != null)
        {
            // Select and expand root node
            treeElem.RootPath = ptci.CategoryPath;
        }

        return ptci;
    }


    /// <summary>
    /// Selects root category in tree, clears search condition and resets pager to first page.
    /// </summary>
    public void ResetToDefault()
    {
        PageTemplateCategoryInfo ptci = LoadRootCategory();
        if (ptci != null)
        {
            flatElem.SelectedCategory = ptci;

            // Select and expand root node
            treeElem.SelectedItem = String.IsNullOrEmpty(TreeSelectedCategory) ? ptci.CategoryId.ToString() : TreeSelectedCategory;
            treeElem.SelectPath = ptci.CategoryPath;
        }

        // Clear search condition and resets pager to first page
        flatElem.UniFlatSelector.ResetToDefault();
    }


    /// <summary>
    /// Add a reload script to the page which will update the page size (items count) according to the window size.
    /// </summary>
    /// <param name="forceResize">Indicates whether to invoke resizing of the page before calculating the items count</param>
    public void RegisterRefreshPageSizeScript(bool forceResize)
    {
        flatElem.RegisterRefreshPageSizeScript(forceResize);
    }

    #endregion
}