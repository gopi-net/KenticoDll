using System;
using System.Data;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.License;

public partial class CMSModules_License_Tools_License_List : LicenseListPage
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
            EmergeGridControl.UniGrid.WhereCondition = GetWhereCondition(Constants.WHERECONDITION_LICENSE_LIST);
            EmergeGridControl.AfterActionRedirectTo = "License_Data_List.aspx";
            
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
