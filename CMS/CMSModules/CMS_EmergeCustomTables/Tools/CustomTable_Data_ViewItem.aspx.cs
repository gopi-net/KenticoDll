using System;

using CMS.Core;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Membership;
using CMS.UIControls;
using CMS.DataEngine;

[UIElement(ModuleName.CUSTOMTABLES, "CustomTables", false, true)]
public partial class CMSModules_CMS_EmergeCustomTables_CustomTables_Tools_CustomTable_Data_ViewItem : CMSCustomTablesModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle.TitleText = GetString("customtable.data.viewitemtitle");
        Page.Title = PageTitle.TitleText;
        // Get custom table id from url
        int customTableId = QueryHelper.GetInteger("customtableid", 0);
        // Get custom table item id
        int itemId = QueryHelper.GetInteger("itemid", 0);

        DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(customTableId);
        // Set edited object
        EditedObject = dci;

        if (dci != null)
        {
            // Check 'Read' permission
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                !MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
            {
                RedirectToAccessDenied("cms.customtables", "Read");
            }

            CustomTableItem item = CustomTableItemProvider.GetItem(itemId, dci.ClassName);
            customTableViewItem.CustomTableItem = item;
        }
    }
}