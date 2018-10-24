using System;
using System.Data;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.Maintenance;

public partial class CMSModules_Maintenance_Tools_Maintenance_List : MaintenanceListPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //EditPageUrl = "~/CMSModules/CMS_Maintenance/Tools/Maintenance_Data_List.aspx";
            base.OnPageLoad();

            // Initialize unigrid
            //uniGrid.OnAction += uniGrid_OnAction;
            //uniGrid.ZeroRowsText = GetString("customtable.notable");
            //uniGrid.OnAfterRetrieveData += uniGrid_OnAfterRetrieveData;
            //uniGrid.WhereCondition = GetWhereCondition(Constants.WHERECONDITION_MAINTENANCE_LIST);
            EmergeGridControl.ZeroRowsText = GetString("customtable.notable");
            EmergeGridControl.OnAfterRetrieveData += uniGrid_OnAfterRetrieveData;
            EmergeGridControl.UniGrid.WhereCondition = GetWhereCondition(Constants.WHERECONDITION_MAINTENANCE_LIST);
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    #endregion


    #region "Unigrid events"

    private DataSet uniGrid_OnAfterRetrieveData(DataSet ds)
    {
        DataSet newDataSet = null;
        try
        {
            newDataSet = OnAfterRetrieveData(ds);
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
        return newDataSet;
    }

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    //protected void uniGrid_OnAction(string actionName, object actionArgument)
    //{
    //    try
    //    {
    //        OnGridAction(actionName, actionArgument);
    //    }
    //    catch (Exception ex)
    //    {
    //        OnError(ex, true);
    //    }
    //}

    #endregion
}
