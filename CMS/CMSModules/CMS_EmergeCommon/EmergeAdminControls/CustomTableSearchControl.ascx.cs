using CMS.UIControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using CMS.SiteProvider;
using CMS.FormEngine;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Web.Controls;

using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Web;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.Base;
using Bluespire.Emerge.Components.PreRegistration;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.Base.Web.UI;
using CMS.FormEngine.Web.UI;
public partial class CMSFormControls_EmergeAdminControls_CustomTableSearchControl : EmergeBaseCMSUserControl
{

    /// <summary>
    /// Gets an DataClassInfo instance of the CustomTable .
    /// </summary>

    private DataClassInfo mCustomTableClassInfo = null;
    public DataClassInfo CustomTableClassInfo
    {
        get
        {
            if (mCustomTableClassInfo == null)
            {
                if (EmergeQueryHelper.GetInteger("customtableid", 0) == 0)
                    throw new CustomTableIdNotFoundException("CustomTableID not Found.");
                else
                    mCustomTableClassInfo = CustomTableDataHelper.GetCustomTableClassInfo(EmergeQueryHelper.GetInteger("customtableid", 0));

            }

            return mCustomTableClassInfo;
        }
        set
        {
            mCustomTableClassInfo = value;
        }
    }

    //private string _Where = string.Empty;

    /// <summary>
    /// Messages placeholder
    /// </summary>
    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }

    /// <summary>
    /// Holds an instance of the UniGrid control.
    /// </summary>
    public UniGrid UniGrid
    {
        get;
        set;
    }

    /// <summary>
    /// Holds an instance of the Where Clause.
    /// </summary>
    public string Where
    {
        get { return ViewState["Where"] == null ? string.Empty : ViewState["Where"].ToString(); }
        private set { ViewState["Where"] = value; }
    }


    public int CurrentPage
    {
        get { return EmergeValidationHelper.GetInteger(UniGrid.Pager.UniPager.CurrentPage, 0); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["Original"] = UniGrid.WhereCondition == string.Empty ? null : UniGrid.WhereCondition;
        SetupControl();

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        panSearchFields.Controls.Clear();
        Where = string.Empty;
        SetupControl();
        btnSearch_Click(null, null);
    }

    /// <summary>
    /// Setup the Custom table search block and Grid.
    /// </summary>
    private void SetupControl()
    {
        SetupSearchPanel();
        SetupEventHandlers();

        if (!string.IsNullOrEmpty(Where))
        {
            UniGrid.WhereCondition = string.IsNullOrEmpty(UniGrid.WhereCondition) ? Where : UniGrid.WhereCondition + " AND " + Where;
        }
        //UniGrid.WhereCondition = string.Empty;
        //SetWhereCondition();
    }

    private void SetupEventHandlers()
    {
        btnSearch.Click += btnSearch_Click;
        btnClear.Click += btnClear_Click;


    }

    private void SetupSearchPanel()
    {
        FormInfo fi = FormHelper.GetFormInfo(CustomTableClassInfo.ClassName, false);
        List<FormFieldInfo> formFields = new List<FormFieldInfo>();
        formFields = fi.GetFields(true, true, true);

        SearchSettings searchSettings;
        searchSettings = CustomTableClassInfo.ClassSearchSettingsInfos;
        Dictionary<string, string> searchableDictionaryItems = searchSettings.Items.TypedValues.Where(x => x.Searchable == true).ToDictionary(kx => kx.ID.ToString(), kx => kx.Name);
        HtmlTable searchtable = new HtmlTable();
        searchtable.ID = "searchControlTable";
        if (searchableDictionaryItems.Count == 0)
        {
            btnSearch.Visible = false;
            btnClear.Visible = false;
        }
        else
        {
            btnSearch.Visible = true;
            btnClear.Visible = true;
        }
        foreach (FormFieldInfo ffi in formFields)
        {
            if (ffi != null)
            {
                if (searchableDictionaryItems.ContainsValue(ffi.Name))
                {
                    AddControlToPanel(ffi, searchtable);
                }
            }
        }
        panSearchFields.Controls.Add(searchtable);
    }



    /// <summary>
    /// Function to create and add instance of a control .
    /// </summary>
    private void AddControlToPanel(FormFieldInfo ffi, HtmlTable searchtable)
    {

        string controlname = ffi.Name;
        LocalizedLabel lbl;

        CMSTextBox txtbox;
        HtmlGenericControl div;


        LocalizedDropDownList ddl;
        LocalizedCheckBoxList chkList;
        LocalizedRadioButtonList rbList;
        ListBox listBox;
        CheckBox chkBox;
        HtmlTableRow searchRow = new HtmlTableRow();
        HtmlTableCell labelCol = new HtmlTableCell();
        HtmlTableCell contrlCol = new HtmlTableCell();

        if (controlname.ToLower() == "isactive")
        {
            lbl = new LocalizedLabel();
            lbl.ID = "lblIsActive";
            lbl.Text = ffi.Caption + ":";
            ddl = new LocalizedDropDownList();
            ddl.ID = controlname;
            ddl.Items.Insert(0, new ListItem("All", ""));
            ddl.Items.Insert(1, new ListItem("True", "1"));
            ddl.Items.Insert(2, new ListItem("False", "0"));
            ddl.SelectedIndex = 0;
            labelCol.Controls.Add(lbl);
            contrlCol.Controls.Add(ddl);

        }
        else if (!string.IsNullOrEmpty(Convert.ToString(ffi.Settings["controlname"])))
        {
            switch (ffi.Settings["controlname"].ToString().ToLower())
            {
                case "htmlareacontrol":
                case "textboxcontrol":
                case "textareacontrol":
                case "longnumbertextBox":
                case "decimalnumbertextBox":
                case "emailinput":
                case "mandatorytextbox":
                case "mandatoryemail":
                case "uszipcode":
                case "cms.emerge_formcontrols_uniquetextbox":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    txtbox = new CMSTextBox();
                    txtbox.ID = controlname;
                    div = new HtmlGenericControl("DIV");
                    div.Attributes.Add("style", "width:100%;margin-bottom:10px");
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(txtbox);
                    break;
                case "calendarcontrol":
                case "cms.emerge_formcontrols_emergecalendar":
                case "labelcontrol":
                    if (ffi.DataType == FieldDataType.DateTime)
                    {
                        lbl = new LocalizedLabel();
                        lbl.Text = ffi.Caption + ":";
                        EmergeRangeDateTimePicker calendar = new EmergeRangeDateTimePicker("From", "To");
                        calendar.UseDynamicDefaultTime = true;
                        calendar.EditTime = false;
                        calendar.ID = controlname;
                        calendar.PostbackOnOK = false;
                        labelCol.Controls.Add(lbl);
                        contrlCol.Controls.Add(calendar);
                    }
                    else if (CustomTableClassInfo.ClassName == string.Format(PreRegistrationConstants.CUSTOMTABLE_PREREGISTRATIONINFORMATION, EmergeCMSContext.CurrentSiteName))
                    {
                        lbl = new LocalizedLabel();
                        lbl.ID = "lbl" + controlname;
                        lbl.Text = ffi.Caption + ":";
                        txtbox = new CMSTextBox();
                        txtbox.ID = controlname;
                        div = new HtmlGenericControl("DIV");
                        div.Attributes.Add("style", "width:100%;margin-bottom:10px");
                        labelCol.Controls.Add(lbl);
                        contrlCol.Controls.Add(txtbox);
                    }
                    break;

                case "dropdownlistcontrol":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    ddl = new LocalizedDropDownList();
                    ddl.ID = controlname;
                    GetValueSet(ffi, ddl);
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(ddl);
                    break;

                case "cms.emergedropdownlist":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    ddl = new LocalizedDropDownList();
                    ddl.ID = controlname;
                    GetValueSet(ffi, ddl);
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(ddl);
                    break;
                case "multiplechoicecontrol":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    chkList = new LocalizedCheckBoxList();
                    chkList.ID = controlname;
                    GetValueSet(ffi, chkList);
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(chkList);
                    break;

                case "radiobuttonscontrol":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    rbList = new LocalizedRadioButtonList();
                    rbList.ID = controlname;
                    GetValueSet(ffi, rbList);
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(rbList);
                    break;

                case "listboxcontrol":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    listBox = new ListBox();
                    listBox.ID = controlname;
                    GetValueSet(ffi, listBox);
                    listBox.CssClass = "ListBoxField";
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(listBox);
                    break;

                case "checkboxcontrol":
                    lbl = new LocalizedLabel();
                    lbl.ID = "lbl" + controlname;
                    lbl.Text = ffi.Caption + ":";
                    chkBox = new CheckBox();
                    chkBox.ID = controlname;
                    labelCol.Controls.Add(lbl);
                    contrlCol.Controls.Add(chkBox);
                    break;
            }
        }
        searchRow.Cells.Add(labelCol);
        searchRow.Cells.Add(contrlCol);
        searchtable.Rows.Add(searchRow);
    }

    /// <summary>
    /// Function to get value set for dropdownlist.
    /// </summary>
    private void GetValueSet(FormFieldInfo ffi, LocalizedDropDownList ddl)
    {
        string options = EmergeValidationHelper.GetString(ffi.Settings["Options"], null);
        string query = EmergeValidationHelper.GetString(ffi.Settings["Query"], null);

        new SpecialFieldsDefinition()
        {
            FieldInfo = ffi
        }
                 .LoadFromText(options)
                 .LoadFromQuery(query)
                 .FillItems(ddl.Items);
        ddl.Items.Insert(0, new ListItem("All", "0"));
        ddl.SelectedIndex = -1;
        ddl.SelectedIndex = 0;
    }

    /// <summary>
    /// Function to get value set for Checkboxlist.
    /// </summary>
    private void GetValueSet(FormFieldInfo ffi, LocalizedCheckBoxList chkList)
    {
        string options = EmergeValidationHelper.GetString(ffi.Settings["Options"], null);
        string query = EmergeValidationHelper.GetString(ffi.Settings["Query"], null);
        string direction = EmergeValidationHelper.GetString(ffi.Settings["RepeatDirection"], "");
        if (direction.ToLowerCSafe() == "horizontal")
        {
            chkList.RepeatDirection = RepeatDirection.Horizontal;
        }
        else
        {
            chkList.RepeatDirection = RepeatDirection.Vertical;
        }
        //FormHelper.LoadItemsIntoList(options, query, chkList.Items, ffi);  
        new SpecialFieldsDefinition()
        {
            FieldInfo = ffi
        }
                 .LoadFromText(options)
                 .LoadFromQuery(query)
                 .FillItems(chkList.Items);        
    }
    /// <summary>
    /// Function to get value set for radiobutton list.
    /// </summary>
    private void GetValueSet(FormFieldInfo ffi, LocalizedRadioButtonList rbList)
    {
        string direction = EmergeValidationHelper.GetString(ffi.Settings["RepeatDirection"], "");
        if (direction.ToLowerCSafe() == "horizontal")
        {
            rbList.RepeatDirection = RepeatDirection.Horizontal;
        }
        else
        {
            rbList.RepeatDirection = RepeatDirection.Vertical;
        }
        string options = EmergeValidationHelper.GetString(ffi.Settings["Options"], null);
        string query = EmergeValidationHelper.GetString(ffi.Settings["Query"], null);
        //FormHelper.LoadItemsIntoList(options, query, rbList.Items, ffi);

        new SpecialFieldsDefinition()
        {
            FieldInfo = ffi
        }
                 .LoadFromText(options)
                 .LoadFromQuery(query)
                 .FillItems(rbList.Items);
    }

    /// <summary>
    /// Function to get value set for Listbox.
    /// </summary>
    private void GetValueSet(FormFieldInfo ffi, ListBox listBox)
    {
        bool allowmultiple = EmergeValidationHelper.GetBoolean(ffi.Settings["AllowMultiplechoices"], true);
        if (allowmultiple)
        {
            listBox.SelectionMode = ListSelectionMode.Multiple;
        }
        else
        {
            listBox.SelectionMode = ListSelectionMode.Single;
        }
        string options = EmergeValidationHelper.GetString(ffi.Settings["Options"], null);
        string query = EmergeValidationHelper.GetString(ffi.Settings["Query"], null);
        //FormHelper.LoadItemsIntoList(options, query, listBox.Items, ffi);
        new SpecialFieldsDefinition()
        {
            FieldInfo = ffi
        }
                 .LoadFromText(options)
                 .LoadFromQuery(query)
                 .FillItems(listBox.Items);
    }

    void btnSearch_Click(object sender, EventArgs e)
    {
        if (!IsValidSearchInput()) return;
        Where = string.Empty;
        SetWhereCondition();
        if (null != this.Parent.FindControl("customTableDataList"))
        {
            if (this.Parent.FindControl("customTableDataList").GetType().GetMethod("SetGridData") != null)
            {
                this.Parent.FindControl("customTableDataList").GetType().GetMethod("SetGridData").Invoke(this.Parent.FindControl("customTableDataList"), null);
            }
        }

    }

    /// <summary>
    /// Function to create Where Clause.
    /// </summary>
    private void SetWhereCondition()
    {
        HtmlTable table = (HtmlTable)panSearchFields.FindControl("searchControlTable");
        foreach (HtmlTableRow row in table.Rows)
        {
            foreach (HtmlTableCell cell in row.Cells)
            {
                foreach (Control ctrl in cell.Controls.OfType<WebControl>())
                {
                    string searchParameter = GetSearchParameter(ctrl);

                    if (!string.IsNullOrEmpty(searchParameter.Trim()))
                        Where += searchParameter + " AND ";
                }
            }
        }

        if (Where != null && Where.EndsWith("AND "))
            Where = Where.Substring(0, Where.LastIndexOf("AND"));
        if (ViewState["Original"] == null)
        {
            UniGrid.WhereCondition = Where;
        }
        else
        {
            string wherec = Convert.ToString(ViewState["Original"]);
            string finalWhere = wherec + " AND " + Where;
            if (finalWhere != null && finalWhere.EndsWith("AND "))
                finalWhere = finalWhere.Substring(0, finalWhere.LastIndexOf("AND"));
            UniGrid.WhereCondition = finalWhere;
        }
    }

    /// <summary>
    /// Function to check for Valid Input. Returns false in case of invalid input.
    /// </summary>
    bool IsValidSearchInput()
    {
        HtmlTable table = (HtmlTable)panSearchFields.FindControl("searchControlTable");
        foreach (HtmlTableRow row in table.Rows)
        {
            foreach (HtmlTableCell cell in row.Cells)
            {
                foreach (Control ctrl in cell.Controls.OfType<WebControl>())
                {
                    if (ctrl is EmergeRangeDateTimePicker)
                    {
                        if (!((EmergeRangeDateTimePicker)ctrl).ValidateDateTimeRange())
                        {
                            ShowError(EmergeResHelper.GetString("Emerge.CustomTableSearchControl.ErrorMessage.InvalidDateRange"));
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Function to get Search parameter( will be used to form the search query.
    /// </summary>
    private string GetSearchParameter(Control control)
    {
        string key = control.ID;
        string value = string.Empty;
        if (control is TextBox)
        {
            if (!((TextBox)control).Text.Trim().Equals(string.Empty))
            {
                value = " [" + control.ID + "] like " + "'%" + ((TextBox)control).Text.Trim().Replace("'", "''") + "%'";
                return value;
            }
        }

        if (control is EmergeRangeDateTimePicker)
        {
            string FromDate = string.Empty;
            string ToDate = string.Empty;

            FromDate = ((EmergeRangeDateTimePicker)control).DateTimeTextBox.Text.Trim().ToLower().Equals(((EmergeRangeDateTimePicker)control).FromWaterMarkText.ToLower()) ? string.Empty : ((EmergeRangeDateTimePicker)control).DateTimeTextBox.Text.Trim();
            ToDate = ((EmergeRangeDateTimePicker)control).AlternateDateTimeTextBox.Text.Trim().ToLower().Equals(((EmergeRangeDateTimePicker)control).ToWaterMarkText.ToLower()) ? string.Empty : ((EmergeRangeDateTimePicker)control).AlternateDateTimeTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(ToDate))
                ToDate = SetEndTimeOfDay(Convert.ToDateTime(ToDate));
            if (!FromDate.Equals(string.Empty) && ToDate.Equals(string.Empty))
            {
                value = " DATEDIFF(second, " + control.ID + ", '" + FromDate + "' ) <= 0 ";
                return value;
            }
            else if (FromDate.Equals(string.Empty) && !ToDate.Equals(string.Empty))
            {
                value = " DATEDIFF(second, " + control.ID + ", '" + ToDate + "' ) >= 0 ";
                return value;
            }
            else if (!FromDate.Equals(string.Empty) && !ToDate.Equals(string.Empty))
            {
                value = " CONVERT(DATETIME, CONVERT(VARCHAR, " + control.ID + " , 20),20) BETWEEN CONVERT(DATETIME, '" + FromDate + "', 20) AND  CONVERT(DATETIME, '" + ToDate + "', 20)";
                return value;
            }
        }
        if (control is LocalizedDropDownList)
        {
            if (((DropDownList)control).SelectedIndex != -1 && !((DropDownList)control).SelectedItem.Text.ToLower().Equals("select") && !((DropDownList)control).SelectedItem.Text.ToLower().Equals("all"))
            {
                value = "[" + control.ID + "] = " + "'" + ((DropDownList)control).SelectedItem.Value.Replace("'", "''") + "'";
                return value;
            }
        }
        if (control is LocalizedCheckBoxList)
        {
            bool isFirst = true;
            foreach (ListItem li in ((LocalizedCheckBoxList)control).Items)
            {
                if (li.Selected)
                {
                    if (isFirst)
                    {
                        value += " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + control.ID + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + li.Value + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                        isFirst = false;
                    }
                    else
                    {
                        value += " OR ";
                        value += " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + control.ID + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + li.Value + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                    }
                }
            }
            return value;
        }
        if (control is LocalizedRadioButtonList)
        {
            foreach (ListItem li in ((LocalizedRadioButtonList)control).Items)
            {
                if (li.Selected)
                {
                    value += " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + control.ID + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + li.Value + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                    break;
                }
            }
            return value;
        }
        if (control is ListBox)
        {
            bool isFirst = true;
            foreach (ListItem li in ((ListBox)control).Items)
            {
                if (li.Selected)
                {
                    if (isFirst)
                    {
                        value += " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + control.ID + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + li.Value + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                        isFirst = false;
                    }
                    else
                    {
                        value += " OR ";
                        value += " '" + Constants.MULTI_VALUE_SEPERATOR + "'+" + control.ID + "+'" + Constants.MULTI_VALUE_SEPERATOR + "' like '%" + Constants.MULTI_VALUE_SEPERATOR + "" + li.Value + "" + Constants.MULTI_VALUE_SEPERATOR + "%'";
                    }
                }
            }
            return value;
        }
        if (control is CheckBox)
        {
            if (((CheckBox)control).Checked)
            {
                value = " " + control.ID + " = " + "1";
                return value;
            }
        }
        return value;
    }

    string SetEndTimeOfDay(DateTime date)
    {
        if (date.Hour == 0 && date.Minute == 0 && date.Second == 0)
        {
            date = date.AddHours(23);
            date = date.AddMinutes(59);
            date = date.AddSeconds(59);
        }
        return date.ToString();
    }
}