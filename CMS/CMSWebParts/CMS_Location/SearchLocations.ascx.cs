using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
//using Bluespire.Emerge.Common.CMS;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
//using Bluespire.Emerge.Common.CMS.MediaLibrary;
//using Bluespire.Emerge.Common.CMS.SettingsProvider;
//using Bluespire.Emerge.Common.CMS.WebAnalytics;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.Location;
using Bluespire.Emerge.Components.Location.WebParts;
using CMS.Base.Web.UI;
//using Bluespire.Emerge.Web.Controls;

public partial class CMSWebParts_CMS_Location_SearchLocations : SearchLocationsWebPart
{

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

    #region "Page Events"
    
    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = SearchPanel;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            SearchPanel.Visible = false;
            return;
        }

        SetupControl();

        if (!EmergeRequestHelper.IsPostBack())
        {
            EmergeSessionHelper.Remove(LocationConstants.SESSION_LOCATIONS);
            BindLocations(LoadLocations(string.Empty));
            Page.DataBind();
            LoadListControls(true);  
        }
       
       
    }
    
    #endregion 
    
    #region "Private Methods"
    private void SetupControl()
    {
        LocationRepeater.TransformationName = TransformationName;
        LocationRepeater.ZeroRowsText = GetString(LocationConstants.STRINGCODE_LOCATIONREPEATER_ZEROROWSTEXT);
        btnClear.Click += btnClear_Click;
        btnSearch.Click += btnSearch_Click;
    }
    #endregion

    #region "Control Events"
    void btnSearch_Click(object sender, EventArgs e)
    {
        EmergeSessionHelper.Remove(LocationConstants.SESSION_LOCATIONS);
        BindLocations(LoadLocations(GetWhereCondition()));
        Page.DataBind();
    }

    void btnClear_Click(object sender, EventArgs e)
    {
        EmergeSessionHelper.Remove(LocationConstants.SESSION_LOCATIONS);
        ClearFormFields();
        BindLocations(LoadLocations(string.Empty));
        Page.DataBind();
    }
    #endregion


}
