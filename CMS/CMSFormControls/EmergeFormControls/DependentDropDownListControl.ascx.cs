using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.FormEngine;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using CMS.Helpers;
using CMS.CustomTables;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.DataEngine;
using CMS.FormEngine.Web.UI;

public partial class CMSFormControls_EmergeFormControls_DependentDropDownListControl : EmergeBaseFormEngineUserControl
{
    #region "Variables"

    private string selectedValue = null;
    private bool? mEditText;
    private bool mFirstAsDefault = true;
    private bool valueSet = false;

    const string DEPENDENTID = "DependentID";
    #endregion


    #region "Properties"


    #region [Emerge Properties]

    /// <summary>
    /// get or set default text for drop down(first selected value)
    /// </summary>
    public string DropDownDefaultText
    {

        get
        {
            return ValidationHelper.GetString(GetValue("DropDownDefaultText"), string.Empty);
        }
        set
        {
            ValidationHelper.GetString(GetValue("DropDownDefaultText"), string.Empty);
        }
    }

    /// <summary>
    /// get or set drop down default value
    /// </summary>
    public string DropDownDefaultValue
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DropDownDefaultValue"), string.Empty);
        }
        set
        {
            SetValue(("DropDownDefaultValue"), string.Empty);
        }
    }

    /// <summary>
    /// get or set is default value set or not
    /// </summary>
    public bool IsSetDefaultValue
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("IsSetDefaultValue"), false);
        }
        set
        {
            SetValue(("IsSetDefaultValue"), false);
        }
    }


    /// <summary>
    /// get or set depending field name 
    /// </summary>
    public string DependingFieldName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependingFieldName"), string.Empty);
        }
        set
        {
            SetValue(("DependingFieldName"), false);
        }
    }


    /// <summary>
    /// get or set Dependent custom table class name
    /// </summary>
    public string DependentCustomTable
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentCustomTable"), string.Empty);
        }
        set
        {
            SetValue(("DependentCustomTable"), false);
        }
    }


    public string DependentDropdownValueColumn
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentDropdownValueColumn"), string.Empty);
        }
        set
        {
            SetValue(("DependentDropdownValueColumn"), false);
        }
    }

    public string DependentDropdownTextColumn
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentDropdownTextColumn"), string.Empty);
        }
        set
        {
            SetValue(("DependentDropdownTextColumn"), string.Empty);
        }
    }

    public string DependentWhereConditionColumn
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DependentWhereConditionColumn"), string.Empty);
        }
        set
        {
            SetValue(("DependentWhereConditionColumn"), string.Empty);
        }
    }

    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WhereCondition"), string.Empty);
        }
        set
        {
            SetValue(("WhereCondition"), string.Empty);
        }
    }


    #endregion



    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return dropDownList.Enabled;
        }
        set
        {
            dropDownList.Enabled = value;
        }
    }

   
    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Return selected value
            // add dependent value with the | seperator
            return dropDownList.SelectedValue;
        }
        set
        {
            object convertedValue = value;
            if ((value != null) || ((FieldInfo != null) && FieldInfo.AllowEmpty))
            {
                if (FieldInfo != null)
                {
                    // Convert default boolean value to proper format
                    if (FieldInfo.DataType == FieldDataType.Boolean)
                    {
                        convertedValue = ValidationHelper.GetBoolean(value, false);
                    }
                    // Ensure rendering of decimal in current culture format
                    else if (FieldInfo.DataType == FieldDataType.Decimal)
                    {
                        convertedValue = ValidationHelper.GetDouble(value, 0.0);
                    }
                    // Ensure rendering of datetime in current culture format
                    else if (FieldInfo.DataType == FieldDataType.DateTime)
                    {
                        convertedValue = ValidationHelper.GetDateTime(value, System.DateTime.MinValue);
                    }
                    
                }

                selectedValue = ValidationHelper.GetString(convertedValue, null);

                //LoadAndSelectList();

                //if (!String.IsNullOrEmpty(selectedValue))
                    //dropDownList.SelectedValue = selectedValue;


                // Indicates that a value was set from outside
                valueSet = true;
            }
        }
    }


    /// <summary>
    /// Returns display name of the value.
    /// </summary>
    public override string ValueDisplayName
    {
        get
        {
            return (EditText || (dropDownList.SelectedItem == null) ? txtCombo.Text : dropDownList.SelectedItem.Text);
        }
    }


    /// <summary>
    /// Gets or sets selected value.
    /// </summary>
    public string SelectedValue
    {
        get
        {
            if (EditText)
            {
                return txtCombo.Text;
            }
            else
            {
                return dropDownList.SelectedValue;
            }
        }
        set
        {
            if (EditText)
            {
                txtCombo.Text = value;
            }
            else
            {
                dropDownList.SelectedValue = value;
            }

            // Indicates that a value was set from outside
            valueSet = true;
        }
    }


    /// <summary>
    /// Gets or sets selected index. Returns -1 if no element is selected.
    /// </summary>
    public int SelectedIndex
    {
        get
        {
            if (EditText)
            {
                if (dropDownList.Items.FindByValue(txtCombo.Text) != null)
                {
                    return dropDownList.SelectedIndex;
                }
                return -1;
            }
            else
            {
                return dropDownList.SelectedIndex;
            }
        }
        set
        {
            dropDownList.SelectedIndex = value;
            if (EditText)
            {
                txtCombo.Text = dropDownList.SelectedValue;
            }
        }
    }


    /// <summary>
    /// Enables to edit text from textbox and select values from dropdownlist.
    /// </summary>
    public bool EditText
    {
        get
        {
            return mEditText ?? ValidationHelper.GetBoolean(GetValue("edittext"), false);
        }
        set
        {
            mEditText = value;
        }
    }


    /// <summary>
    /// Gets dropdown list control.
    /// </summary>
    public DropDownList DropDownList
    {
        get
        {
            return dropDownList;
        }
    }


    /// <summary>
    /// Gets textbox control.
    /// </summary>
    public TextBox TextBoxControl
    {
        get
        {
            return txtCombo;
        }
    }


    /// <summary>
    /// Indicates whether or not to use first value as default if default value is empty.
    /// </summary>
    public bool FirstAsDefault
    {
        get
        {
            return mFirstAsDefault;
        }
        set
        {
            mFirstAsDefault = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {

        // Apply CSS class
        if (!String.IsNullOrEmpty(CssClass))
        {
            dropDownList.CssClass = CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(dropDownList.CssClass))
        {
            dropDownList.CssClass = "DropDownField form-control";
        }
        if (!String.IsNullOrEmpty(ControlStyle))
        {
            dropDownList.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckRegularExpression = true;
        CheckFieldEmptiness = true;
        dropDownList.AppendDataBoundItems = true;

        if (ViewState[DEPENDENTID] == null || (string)ViewState[DEPENDENTID] != Convert.ToString(Form.GetFieldValue(DependingFieldName)))
        {
            ViewState[DEPENDENTID] = Convert.ToString(Form.GetFieldValue(DependingFieldName));
            LoadAndSelectList();
            if (!String.IsNullOrEmpty(selectedValue) && dropDownList.Items.FindByValue(selectedValue) != null)
                dropDownList.SelectedValue = selectedValue;
        }
    }

    /// <summary>
    /// Loads and selects control.
    /// </summary>
    private void LoadAndSelectList()
    {

        //string options = ValidationHelper.GetString(GetValue("options"), null);
        //string query = ValidationHelper.GetString(GetValue("query"), null);
        dropDownList.Items.Clear();
        try
        {
            // FormHelper.LoadItemsIntoList(options, query, dropDownList.Items, FieldInfo);
            string value = Convert.ToString(Form.GetFieldValue(DependingFieldName));
            FillDependentDropDown(value);
            //insert default value and text at 0th index.

        }
        catch (Exception ex)
        {
            DisplayException(ex);
        }


        FormControlsHelper.SelectSingleValue(selectedValue, dropDownList);


    }




    /// <summary>
    /// Displays exception control with current error.
    /// </summary>
    /// <param name="ex">Thrown exception</param>
    private void DisplayException(Exception ex)
    {
        FormControlError ctrlError = new FormControlError();
        ctrlError.InnerException = ex;
        Controls.Add(ctrlError);
        dropDownList.Visible = false;

    }



    #endregion


    #region "emerge Private functions"

    private void FillDependentDropDown(string value)
    {
        string where = DependentWhereConditionColumn + " = '" + value + "' ";
        if (!String.IsNullOrEmpty(WhereCondition))
            where += " AND " + WhereCondition;

        DataSet ds = CustomTableItemProvider.GetItems(DependentCustomTable, where, string.Empty);
        
        if (ds != null && !DataHelper.DataSourceIsEmpty(ds))
        {
            dropDownList.DataSource = ds;
            if (ds.Tables[0].Columns.Contains(DependentDropdownTextColumn) && ds.Tables[0].Columns.Contains(DependentDropdownValueColumn))
            {
                dropDownList.DataTextField = DependentDropdownTextColumn;
                dropDownList.DataValueField = DependentDropdownValueColumn;
            }

            dropDownList.DataBind();
        }
        if (IsSetDefaultValue)
        {
            dropDownList.Items.Insert(0, new ListItem(DropDownDefaultText, DropDownDefaultValue));
        }


    }


    #endregion


}