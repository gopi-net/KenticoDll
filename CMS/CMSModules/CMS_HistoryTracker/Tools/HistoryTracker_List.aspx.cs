using System;
using System.Data;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.HistoryTracker;

public partial class CMSModules_CMS_HistoryTracker_Tools_HistoryTracker_List : HistoryTrackerListPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();

            // Initialize unigrid
            
            //uniGrid.OnAction += uniGrid_OnAction;
            EmergeGridControl.ZeroRowsText = GetString("customtable.notable");
            //uniGrid.ZeroRowsText = GetString("customtable.notable");
            EmergeGridControl.OnAfterRetrieveData += EmergeUniGrid_OnAfterRetrieveData;
            EmergeGridControl.UniGrid.WhereCondition = GetWhereCondition(Constants.WHERECONDITION_HISTORYTRACKER_LIST);
            EmergeGridControl.AfterActionRedirectTo = "HistoryTracker_Data_List.aspx";
            
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
    }

    #endregion


    #region "Unigrid events"

    private DataSet EmergeUniGrid_OnAfterRetrieveData(DataSet ds)
    {
        DataSet newDataSet = null;
        try
        {
            newDataSet = OnAfterRetrieveData(ds);
        }
        catch (Exception ex)
        {
            OnError(ex,true);
        }
        return newDataSet;
    }


    #endregion
}
