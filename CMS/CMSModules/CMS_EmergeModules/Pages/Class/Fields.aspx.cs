using System;

using CMS.Base;
using CMS.Core;
using CMS.DataEngine;
using CMS.Base.Web.UI.ActionsConfig;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.Modules;
using CMS.UIControls;
using CMS.Base.Web.UI;

[UIElement(ModuleName.CMS, "Fields")]
public partial class CMSModules_CMS_EmergeModules_Pages_Class_Fields : GlobalAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControls();

        if (!URLHelper.IsPostback() && QueryHelper.GetBoolean("gen", false))
        {
            fieldEditor.ShowInformation(GetString("EditTemplateFields.FormDefinitionGenerated"));
        }

        ScriptHelper.HideVerticalTabs(this);
    }


    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        // Get info on the class
        DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(QueryHelper.GetInteger("classid", 0));
        if (dci != null)
        {
            if (dci.ClassIsDocumentType && !dci.ClassIsCoupledClass)
            {
                ShowError(GetString("EditTemplateFields.ErrorIsNotCoupled"));
            }
            else
            {
                fieldEditor.Visible = true;
                fieldEditor.ClassName = dci.ClassName;

                ResourceInfo resource = ResourceInfoProvider.GetResourceInfo(QueryHelper.GetInteger("moduleid", 0));
                bool isEditable = ((resource != null) && resource.IsEditable) || dci.ClassShowAsSystemTable;

                // Allow development mode only for non-system tables
                fieldEditor.DevelopmentMode = !dci.ClassShowAsSystemTable;
                fieldEditor.Enabled = isEditable;
                fieldEditor.Mode = dci.ClassShowAsSystemTable ? FieldEditorModeEnum.SystemTable : FieldEditorModeEnum.ClassFormDefinition;

                fieldEditor.HeaderActions.AddAction(new HeaderAction()
                {
                    Text = GetString("EditTemplateFields.GenerateFormDefinition"),
                    Tooltip = GetString("EditTemplateFields.GenerateFormDefinition"),
                    OnClientClick = "if (!confirm('" + GetString("EditTemplateFields.GenerateFormDefConfirmation") + "')) {{ return false; }}",
                    Visible = !dci.ClassIsDocumentType,
                    CommandName = "gendefinition",
                    Enabled = isEditable
                });

                fieldEditor.HeaderActions.ActionPerformed += (s, ea) => { if (ea.CommandName == "gendefinition") GenerateDefinition(); };
            }
        }
    }


    /// <summary>
    /// Generates default form definition.
    /// </summary>
    private void GenerateDefinition()
    {
        // Get info on the class
        DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(QueryHelper.GetInteger("classid", 0));
        if (dci != null)
        {
            TableManager tm = new TableManager(dci.ClassConnectionString);

            // Get the XML schema
            dci.ClassXmlSchema = tm.GetXmlSchema(dci.ClassTableName);
            FormInfo fi = new FormInfo();
            fi.LoadFromDataStructure(dci.ClassTableName, tm, true);
            dci.ClassFormDefinition = fi.GetXmlDefinition();
            DataClassInfoProvider.SetDataClassInfo(dci);

            URLHelper.Redirect(URLHelper.AddParameterToUrl(RequestContext.CurrentURL, "gen", "1"));
        }
    }
}