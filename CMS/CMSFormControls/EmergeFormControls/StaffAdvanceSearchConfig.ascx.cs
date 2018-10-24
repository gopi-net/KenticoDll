using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Web.Controls;
using CMS.DataEngine;
using CMS.Helpers;


public partial class CMSFormControls_EmergeFormControls_StaffAdvanceSearchConfig : EmergeBaseFormEngineUserControl
{
    #region "variables"
    Dictionary<string, List<string>> selectedValue = new Dictionary<string, List<string>>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSetColumnList.Click += btnSetColumnList_Click;
     
      
        if (!RequestHelper.IsPostBack())
        {
            LoadTables();
            LoadColumnList();
            SetColumnList();
            BindGrid();
            ViewState["IsPostBack"] = true;
        }
        if (RequestHelper.IsPostBack() && (ViewState["IsPostBack"]==null ||Convert.ToBoolean(ViewState["IsPostBack"]) ))
        {
            LoadTables();
            LoadColumnList();
            SetColumnList();
            BindGrid();
            ViewState["IsPostBack"] =false;
        }
       
    }



    #region
    public override object Value
    {
        get
        {
            string resultValue = string.Empty;

            selectedValue = (Dictionary<string, List<string>>)ViewState["selectedValue"];
            selectedValue = selectedValue.Where(t => t.Value.Count > 0).ToDictionary(t => t.Key, t => t.Value);
            bool isFirst = true;
            if (selectedValue != null)
            {
                foreach (var item in selectedValue)
                {
                    if (isFirst)
                    {
                        resultValue = item.Key;
                        isFirst = false;
                    }
                    else
                    {
                        resultValue = resultValue + Constants.MULTI_VALUE_SEPERATOR + item.Key;
                    }
                    resultValue += "-";
                    bool isFirstColumn = true;
                    foreach (var columnItem in item.Value)
                    {
                        if (isFirstColumn)
                        {
                            resultValue += columnItem;
                            isFirstColumn = false;
                        }
                        else
                        {
                            resultValue += "~" + columnItem;
                        }
                    }
                }
            }
            return resultValue;
        }

        set
        {
            selectedValue = new Dictionary<string, List<string>>();
            string resultValue = (string)value;
            if (!string.IsNullOrEmpty(resultValue))
            {
                string[] TableList = resultValue.Split(Constants.MULTI_VALUE_SEPERATOR);
                if (TableList != null)
                {
                    for (int i = 0; i < TableList.Length; i++)
                    {
                        string[] TableColumnList = TableList[i].Split('-');
                        if (TableColumnList.Length > 1)
                        {
                            string[] ColList = TableColumnList[1].Split('~');
                            List<string> columnItems = new List<string>();
                            for (int j = 0; j < ColList.Length; j++)
                            {
                                columnItems.Add(ColList[j]);
                            }
                            selectedValue.Add(TableColumnList[0], columnItems);
                        }
                    }
                }
            }
            selectedValue=selectedValue.Where(t=>t.Value.Count>0).ToDictionary(t=>t.Key,t=>t.Value);
            ViewState["selectedValue"] = selectedValue;

        }

    }
    public object SetControl
    {
        get
        {


            return (Dictionary<string, List<string>>)ViewState["selectedValue"];

        }

        set
        {
            ViewState["selectedValue"] = (Dictionary<string, List<string>>)value;

        }
    }

    #endregion

    #region "private methods"

    /// <summary>
    /// load tables which are related to staff custom table
    /// </summary>
    private void LoadTables()
    {
        OtherTables_DataSource.QueryName = EmergeStaticHelper.SetSiteName(OtherTables_DataSource.QueryName);
        OtherTables.DataSource = OtherTables_DataSource.DataSource;
        OtherTables.DataBind();
        OtherTables.Items.Insert(0, new ListItem(Constants.DROPDOWN_DEFAULT_TEXT, Constants.DROPDOWN_DEFAULT_VALUE));

    }


    /// <summary>
    /// load column checkbox list as per the selected table
    /// </summary>
    private void LoadColumnList()
    {

        string where = string.Empty;

        Columns_DataSource.QueryName = EmergeStaticHelper.SetSiteName("customtable.Emerge_{0}_SD_Staff.GetColumnList");

        string tableName = OtherTables.SelectedItem.Value.Replace("customtable.", "");
        if (tableName == Constants.DROPDOWN_DEFAULT_VALUE)
        {
            tableName = "-1";
        }
        //Columns_DataSource.WhereCondition = " table_name = '" + tableName + "'";
        QueryDataParameters param = new QueryDataParameters();
        param.Add("@table_name", tableName);
        Columns_DataSource.QueryParameters = param;

        Columns_DataSource.LoadData(true);
        ColumnList.DataSource = Columns_DataSource.DataSource;
        ColumnList.DataBind();

        if (ColumnList.Items.Count == 0)
        {
            btnSetColumnList.Visible = false;
        }
        else
        {
            btnSetColumnList.Visible = true;
        }
    }

    /// <summary>
    /// get control value and set checkbox list according to selected dropdown value
    /// </summary>
    private void SetColumnList()
    {
        string selectedTable = OtherTables.SelectedItem.Value;
        selectedValue = (Dictionary<string, List<string>>)SetControl;
        if (selectedValue != null)
        {
            if (selectedValue.ContainsKey(selectedTable))
            {
                List<string> checkedColumns = selectedValue[selectedTable];

                for (int i = 0; i < checkedColumns.Count; i++)
                {
                    for (int j = 0; j < ColumnList.Items.Count; j++)
                    {
                        if (ColumnList.Items[j].Text == checkedColumns[i])
                        {
                            ColumnList.Items[j].Selected = true;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// set control value
    /// </summary>
    private void SetControlValue()
    {
        string selectedTable = OtherTables.SelectedItem.Value;
        List<string> items = new List<string>();// ColumnList.Items.Cast<ListItem>().Where(li => li.Selected = true).Select(li => li.Text).ToList<string>();
        for (int i = 0; i < ColumnList.Items.Count; i++)
        {
            if (ColumnList.Items[i].Selected == true)
            {
                items.Add(ColumnList.Items[i].Text);
            }
        }

        selectedValue = (Dictionary<string, List<string>>)SetControl;
        if (selectedValue != null)
        {
            if (selectedValue.ContainsKey(selectedTable))
            {
                selectedValue[selectedTable] = items;
            }
            else
            {
               
                selectedValue.Add(selectedTable, items);
            }
        }
        else
        {
            selectedValue = new Dictionary<string, List<string>>();
            selectedValue.Add(selectedTable, items);
        }
        SetControl = selectedValue;
    }


    private void BindGrid()
    {
        selectedValue = (Dictionary<string, List<string>>)SetControl;
        selectedValue = selectedValue.Where(t => t.Value.Count > 0).ToDictionary(t => t.Key, t => t.Value);
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        dt.Columns.Add("ItemID");
        dt.Columns.Add("RelationalTable");
        dt.Columns.Add("Columns");
        int itemid=1;
        foreach (var tables in selectedValue)
	    {
		    
            foreach (var columList in tables.Value)
            {
                DataRow dr = dt.NewRow();
                dr[0] = itemid;
                dr[1] = tables.Key;
                dr[2] = columList;
                dt.Rows.Add(dr);
                itemid++;
            }
            
        }
        dt.AcceptChanges();
        ds.Tables.Add(dt);
        ds.AcceptChanges();
        CustomTableGrid.DataSource = ds;
        CustomTableGrid.DataBind();
        //CustomTableGrid.DataBind();
    }

   

    void CustomTableGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
           
        }
    }

    #endregion



    #region "Button Events"

    protected void btnSetColumnList_Click(object sender, EventArgs e)
    {
        SetControlValue();
        SetColumnList();
        BindGrid();
    }
    #endregion
    protected void OtherTables_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadColumnList();
        SetColumnList();
    }
}