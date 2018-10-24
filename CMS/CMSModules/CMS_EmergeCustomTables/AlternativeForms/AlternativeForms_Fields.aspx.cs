using System;

using CMS.Core;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.UIControls;
using CMS.Base.Web.UI;

[UIElement(ModuleName.CUSTOMTABLES, "AlternativeForm.Fields")]
public partial class CMSModules_CMS_EmergeCustomTables_CustomTables_AlternativeForms_AlternativeForms_Fields : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int altFormId = QueryHelper.GetInteger("objectid", 0);
        CurrentMaster.BodyClass += " FieldEditorBody";

        altFormFieldEditor.Mode = FieldEditorModeEnum.AlternativeCustomTable;
        altFormFieldEditor.AlternativeFormID = altFormId;
        altFormFieldEditor.DisplayedControls = FieldEditorControlsEnum.CustomTables;

        ScriptHelper.HideVerticalTabs(this);
    }
}