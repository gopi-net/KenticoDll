using System;
using Bluespire.Emerge.Components.Rates;
using Bluespire.Emerge.Components.GenericParsing;
using System.Data;
using System.IO;
using Bluespire.Emerge.CommonService;
using System.Collections.Generic;
using System.Linq;
using Bluespire.Emerge.Components.Rates.Services;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;
using CMS.CustomTables;
public partial class CMSModules_Rates_Tools_Rates_Data_List_Upload : RatesDataListPage
{
    IRatesService objRateSetvices = new RatesService();
    protected void Page_Init(object sender, EventArgs e)
    {
        RequireSite = false;
        customTableSearch.UniGrid = customTableDataList.UniGrid;
        ImportFile.Click += ImportFile_Click;
        ImportFile.Text = ResHelper.GetString("Emerge.RT.ImportData.Button.ImportData");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            NewItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";
            ListPage = "~/CMSModules/CMS_Rates/Tools/Rates_List.aspx";
            SelectFieldsPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_SelectFields.aspx";

            customTableDataList.EditItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_EditItem.aspx";
            customTableDataList.ViewItemPage = "~/CMSModules/CMS_Rates/Tools/Rates_Data_ViewItem.aspx";

            base.OnPageLoad();

            if (DataClassInfo != null)
            {
                //customTableDataList.UniGrid.WhereCondition = RatesConstants.ACTIVE_DATA_CONDITION;
                customTableDataList.CustomTableClassInfo = DataClassInfo;
                customTableDataList.GridName = "~/CMSModules/CMS_Rates/Tools/Rates_Data_List.xml";
                customTableDataList.EditItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                customTableDataList.ViewItemPageAdditionalParams = (IsSiteManager ? "sm=1" : String.Empty);
                // Set alternative form and data container
                customTableDataList.UniGrid.FilterFormName = DataClassInfo.ClassName + "." + "filter";

                customTableDataList.UniGrid.FilterFormData = CustomTableItem.New(DataClassInfo.ClassName);

                customTableDataList.ShowObjectMenu = false;
                // Set custom pages
                if (DataClassInfo.ClassEditingPageURL != String.Empty)
                {
                    customTableDataList.EditItemPage = DataClassInfo.ClassEditingPageURL;
                }
                if (DataClassInfo.ClassNewPageURL != String.Empty)
                {
                    NewItemPage = DataClassInfo.ClassNewPageURL;
                }
                if (DataClassInfo.ClassViewPageUrl != String.Empty)
                {
                    customTableDataList.ViewItemPage = DataClassInfo.ClassViewPageUrl;
                }
                if (CheckForPermissions())
                {
                    plcContent.Visible = false;
                }
                customTableSearch.CustomTableClassInfo = DataClassInfo;
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    protected void ImportFile_Click(object sender, EventArgs e)
    {
        if (IsUploadValid())
        {
            ProcessFile();
        }
    }

    private void ProcessFile()
    {
        string serverFile = string.Empty;
        try
        {
            EventArgs e = new EventArgs();
            serverFile = SaveFile();
            objRateSetvices.UpdateCustomTable(serverFile, txtSheetName.Text.Trim());
            ShowChangesSaved();

        }
        catch (RatesIncorrectMappingsException ex)
        {
            ShowError(ex.Message);
        }
        catch (RatesSheetNotFoundException ex)
        {
            ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            ExceptionHandler.HandleException(ex);
        }
        finally
        {
            if (File.Exists(serverFile))
                File.Delete(serverFile);
        }
    }

    private string SaveFile()
    {
        string path = Server.MapPath("~/" + RatesConstants.RATES_TEMPORARY_FILE_PATH);
        string serverFile = path + RatesConstants.RATES_TEMPORARY_FILE_NAME_PREFIX + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(fuFile.FileName);
        Directory.CreateDirectory(path);
        fuFile.SaveAs(serverFile);
        return serverFile;
    }

    private bool IsUploadValid()
    {
        if (fuFile.HasFile)
            return fuFile.HasFile && IsSheetNameProvided() && IsExtensionSupported();
        else
            ShowError(ResHelper.GetString("Emerge.RT.ImportData.Error.SelectFile"));
        return false;
    }
    
    private bool IsExtensionSupported()
    {
        if (Path.GetExtension(fuFile.FileName).Contains("xls"))
            return true;
        else
            ShowError(ResHelper.GetString("Emerge.RT.ImportData.Error.FileFormat"));
        return false;
    }

    private bool IsSheetNameProvided()
    {
        if (txtSheetName.Text.Trim() != string.Empty)
            return true;
        else
            ShowError(ResHelper.GetString("Emerge.RT.ImportData.Error.SheetName"));
        return false;
    }

}