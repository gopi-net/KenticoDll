using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.FormEngine;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Controls;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using CMS.Helpers;
using CMS.FormEngine.Web.UI;

public partial class CMSModules_CMS_EventsCalendar_FormControls_OccurenceDropDownList : EmergeBaseFormEngineUserControl
{
    #region "Variables"

    private string selectedValue = null;
    private bool? mEditText;
    private bool mFirstAsDefault = true;
    private bool valueSet = false;

    const string DEPENDENTID = "DependentID";
    #endregion
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

    public override object Value
    {
        get
        {
            return dropDownList.SelectedValue;
        }
        set
        {
            object convertedValue = value;
            if ((value != null) || ((FieldInfo != null) && FieldInfo.AllowEmpty))
            {
                selectedValue = ValidationHelper.GetString(convertedValue, null);
            }
        }
    }

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

    private void LoadAndSelectList()
    {
        dropDownList.Items.Clear();
        try
        {
            string value = Convert.ToString(Form.GetFieldValue(DependingFieldName));
            FillDropDown(value);
        }
        catch (Exception ex)
        {
            DisplayException(ex);
        }
        FormControlsHelper.SelectSingleValue(selectedValue, dropDownList);
    }

    private void FillDropDown(string value)
    {
        int scheduleID = ValidationHelper.GetInteger(value, 0);
        if (scheduleID == 0)
            return;
        EventSchedule schedule = EventsCalendarHelper.GetScheduleByScheduleID(scheduleID);
        List<EventOccurence> occurences = schedule.Occurences.OrderBy(a => a.OccurenceDate).ToList<EventOccurence>();
        if (occurences.Count == 0)
            return;
        if (schedule.IsSeries)
        {
            if (occurences[occurences.Count - 1].OccurenceDate < DateTime.Now.Date)
                occurences.RemoveAll(a => a.OccurenceDate < DateTime.Now.Date);
            else
                occurences.RemoveRange(1, occurences.Count - 1);
        }
        else
            occurences.RemoveAll(a => a.OccurenceDate < DateTime.Now.Date);

        if (!DataHelper.DataSourceIsEmpty(occurences))
        {
            dropDownList.DataSource = occurences;
            dropDownList.DataTextFormatString = "{0:" + Constants.EMERGE_DATEFORMAT + "}";
            dropDownList.DataTextField = EventsConstants.FIELDS_EVENTOCCURENCES_OCCURENCEDATE;
            dropDownList.DataValueField = "OccurenceID";
            dropDownList.DataBind();
        }
    }
}