﻿using System;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.DocumentEngine.Web.UI;

public partial class CMSModules_CMS_EmergeModules_Controls_Class_Filter : CMSAbstractBaseFilterControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the filter condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GetWhereCondition();
            return base.WhereCondition;
        }
        set
        {
            base.WhereCondition = value;
        }
    }


    /// <summary>
    /// Determines whether filter is set.
    /// </summary>
    public override bool FilterIsSet
    {
        get
        {
            return !string.IsNullOrEmpty(txtClassDisplayName.Text) || !string.IsNullOrEmpty(txtClassTableName.Text);
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize reset link button
        UniGrid grid = FilteredControl as UniGrid;
        if (grid == null || !grid.RememberState)
        {
            btnReset.Visible = false;
        }
        if (grid != null)
        {
            grid.HideFilterButton = true;
        }
    }


    /// <summary>
    /// Applies filter on associated UniGrid control.
    /// </summary>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        UniGrid grid = FilteredControl as UniGrid;
        if (grid != null)
        {
            grid.ApplyFilter(sender, e);
        }
    }


    /// <summary>
    /// Resets the associated UniGrid control.
    /// </summary>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        UniGrid grid = FilteredControl as UniGrid;
        if (grid != null)
        {
            grid.Reset();
        }
    }

    #endregion


    #region "State management"

    /// <summary>
    /// Resets filter to the default state.
    /// </summary>
    public override void ResetFilter()
    {
        txtClassDisplayName.Text = String.Empty;
        txtClassTableName.Text = String.Empty;
    }

    #endregion


    #region "Private methods"

    private string GetWhereCondition()
    {
        string condition = String.Empty;

        // Filter checking
        if (!String.IsNullOrEmpty(txtClassDisplayName.Text))
        {
            condition = "(ClassDisplayName LIKE '%" + txtClassDisplayName.Text.Trim().Replace("'", "''") + "%') OR (ClassName LIKE '%" + SqlHelper.GetSafeQueryString(txtClassDisplayName.Text, false) + "%')";
        }

        if (!String.IsNullOrEmpty(txtClassTableName.Text))
        {
            condition = SqlHelper.AddWhereCondition(condition, "(ClassTableName LIKE '%" + txtClassTableName.Text.Trim().Replace("'", "''") + "%')");
        }

        return condition;
    }

    #endregion

}