using System;

using CMS.Core;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.UIControls;
using CMS.Base.Web.UI;

[EditedObject(AlternativeFormInfo.OBJECT_TYPE, "objectid")]
[UIElement(ModuleName.CUSTOMTABLES, "AlternativeForm.General")]
public partial class CMSModules_CMS_EmergeCustomTables_CustomTables_AlternativeForms_AlternativeForms_Edit_General : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        altFormEdit.OnAfterSave += altFormEdit_OnAfterSave;
    }


    protected void altFormEdit_OnAfterSave(object sender, EventArgs e)
    {
        ScriptHelper.RefreshTabHeader(Page, ((BaseInfo)EditedObject).Generalized.ObjectDisplayName);
    }
}