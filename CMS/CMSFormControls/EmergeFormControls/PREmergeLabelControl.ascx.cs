using System;
using CMS.Helpers;
using CMS.UIControls;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.PreRegistration;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using System.Data;
using System.Collections;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using CMS.FormEngine.Web.UI;
public partial class CMSFormControls_Basic_PREmergeLabelControl : ReadOnlyFormEngineUserControl
{
    #region "Variables"

    private object mValue = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return label.Enabled;
        }
        set
        {
            label.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the transformation code to use to transform the value.
    /// </summary>
    public string Transformation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Transformation"), "");
        }
        set
        {
            SetValue("Transformation", value);
        }
    }


    /// <summary>
    /// Gets or sets the output format which can contains macros.
    /// </summary>
    public string OutputFormat
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OutputFormat"), "");
        }
        set
        {
            SetValue("OutputFormat", value);
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = ValidationHelper.GetString(value, String.Empty);
            string txt;
            if (!String.IsNullOrEmpty(OutputFormat))
            {
                txt = OutputFormat;
            }
            else
            {
                // Try to find the transformation
                if (!string.IsNullOrEmpty(Transformation) && UniGridTransformations.Global.ExecuteTransformation(label, Transformation, ref value))
                {
                    txt = ValidationHelper.GetString(value, "");
                }
                else if (FieldInfo != null)
                {
                    // Convert the value to a proper type
                    mValue = ConvertInputValue(value);
                    txt = GetValueBasedOnFieldInfo(mValue);
                }
                else
                {
                    txt = ValidationHelper.GetString(value, "");
                }
            }
            // Resolve macros
            label.Text = ContextResolver.ResolveMacros(txt);
        }

    }

    #endregion


    #region "Methods"
    private string GetValueBasedOnFieldInfo(object mValue)
    {
        string txt = string.Empty;
        if (mValue != null)
        {
            if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_STATE || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_RESIDENCYSTATE
                                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_GISTATE || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_NRSTATE
                                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_ECSTATE || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PISTATE
                                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SISTATE)
            {
                txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETSTATENAMEBYID);
            }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_MARITALSTATUS
                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_GIMARITALSTATUS)
            {
                txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETMARITALSTATUSBYID);
            }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_RACE
                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_GIRACE || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_BPIRACE
                || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PICERACE)
            { txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETRACEBYID); }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_HOWYOUFINDUS)
            { txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETHOWFINDOUTABOUTUS_BYID); }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PIRELATIONTOPOLICYHOLDER || FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SIRELATIONTOPOLICYHOLDER)
            { txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETRELATIONTO_POLITYHOLDER_BYID); }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_NRRELATIONTOPATIENT)
            { txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETRELATIONTO_PATIENTNR_BYID); }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_ECRELATIONTOPATIENT)
            {
                txt = FunctionToGetValueById(mValue, PreRegistrationConstants.QUERY_GETRELATIONTO_PATIENTEC_BYID);
            }
            else if (FieldInfo.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_MOTHERPARTICIPATEDIN)
            {
                string val = string.Empty;
                string[] values;
                values = mValue.ToString().Split('|');
                foreach (string item in values)
                {
                    if (item != string.Empty)
                        val = val + FunctionToGetValueById(item, PreRegistrationConstants.QUERY_GETMOTHERPARTICIPATEDIN_BYID) + ", ";
                }
                txt = val.Trim().Trim(',');
            }
            else
            {
                txt = ValidationHelper.GetString(mValue, "");
            }
        }
        return txt;
    }
    private string FunctionToGetValueById(object mValue, string query)
    {
        string txt = string.Empty;
        if (mValue != null)
            txt = GetFieldValueById(Convert.ToInt32(ValidationHelper.GetString(mValue, "")), query);
        else
            txt = string.Empty;
        return txt;
    }
    private string GetFieldValueById(Int32 Id, string queryname)
    {
        string value = string.Empty;
        DataSet ds = new DataSet();
        ds = GetFieldValueByFieldId(Id, queryname);
        if (ds != null && ds.Tables[0] != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
                value = Convert.ToString(ds.Tables[0].Rows[0][0]);
        }
        return value;
    }
    protected DataSet GetFieldValueByFieldId(Int32 Id, string query)
    {
        string value = string.Empty;
        Hashtable parameters = new Hashtable();
        string queryName = string.Format(query, EmergeCMSContext.CurrentSiteName);
        parameters.Add("@" + PreRegistrationConstants.QUERY_FIELD_PARAMETER, Id);
        return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // Apply styles to control
        if (!String.IsNullOrEmpty(CssClass))
        {
            label.CssClass = CssClass;
            CssClass = null;
        }
        else if (String.IsNullOrEmpty(label.CssClass))
        {
            label.CssClass = "LabelField form-control-text";
        }

        if (!String.IsNullOrEmpty(ControlStyle))
        {
            label.Attributes.Add("style", ControlStyle);
            ControlStyle = null;
        }

        CheckFieldEmptiness = false;
    }

    #endregion
}