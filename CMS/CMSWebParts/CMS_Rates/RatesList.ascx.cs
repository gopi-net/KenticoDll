using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.Rates;
using Bluespire.Emerge.Components.Rates.Services;
using Bluespire.Emerge.Components.Rates.WebParts;
using CMS.Base.Web.UI;


public partial class CMSWebParts_CMS_Rates_RatesList : RatesWebPart
{

    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    # region "Webpart Properties"
    public string TransformationName
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("TransformationName"), repRatesList.TransformationName);
        }
        set
        {
            SetValue("TransformationName", value);
            repRatesList.TransformationName = value;
        }
    }
    public string WhereCondition
    {
        get
        {
            return EmergeValidationHelper.GetString(GetValue("WhereCondition"), repRatesList.WhereCondition);
        }
        set
        {
            SetValue("WhereCondition", value);
            repRatesList.WhereCondition = value;
        }
    }
    # endregion

    # region "Page Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panelRatesList.Visible = false;
            return;
        }
        
        //repRatesList.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);
        repRatesList.TransformationName = TransformationName;
        IRatesService ratesService = new RatesService();
        if (!Page.IsPostBack)
        {
            repRatesList.DataSource = ratesService.GetRatesList(WhereCondition);
            repRatesList.DataBind();
        }


    }
    # endregion

}