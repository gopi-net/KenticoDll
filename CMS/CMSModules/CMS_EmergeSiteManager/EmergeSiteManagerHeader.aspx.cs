using System;
using System.Web;

using CMS.Ecommerce;
using CMS.UIControls;
using CMS.Helpers;
using CMS.Core;
using CMS.Base;
public partial class CMSModules_Emerge_SiteManager_Pages_Tools_Header : CMSDeskPage
{
    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        uiToolbarElem.OnButtonCreating += uiToolbarElem_OnButtonCreating;
        uiToolbarElem.OnButtonFiltered += uiToolbarElem_OnButtonFiltered;
    }

    #endregion


    #region "Event handlers"

    private void uiToolbarElem_OnButtonCreating(object sender, UniMenuArgs eventArgs)
    {
        string url = eventArgs.UIElement.ElementTargetURL;
        url = URLHelper.EnsureHashToQueryParameters(url);
        eventArgs.UIElement.ElementTargetURL = url;
    }


    private bool uiToolbarElem_OnButtonFiltered(object sender, UniMenuArgs eventArgs)
    {
        // Hide reports button when reporting module is not loaded
        if (eventArgs.UIElement.ElementName.CompareToCSafe("ecreports", true) == 0)
        {
            return ModuleEntryManager.IsModuleLoaded(ModuleName.REPORTING);
        }

        return true;
    }

    #endregion
}