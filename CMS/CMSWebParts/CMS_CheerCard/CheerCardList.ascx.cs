using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.Components.CheerCard.Services;
using Bluespire.Emerge.Components.CheerCard.WebParts;
using CMS.Base.Web.UI;
using CMS.DocumentEngine.Web.UI;

public partial class CMSWebParts_CMS_CheerCard_CheerCardList : CheerCardListingWebPart
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


    # region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = panCheerCardList;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            panCheerCardList.Visible = false;
            return;
        }
        if (!string.IsNullOrEmpty(Request.QueryString[CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD]) && string.IsNullOrEmpty(SelectedImageGUID))
            SelectedImageGUID = Request.QueryString[CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD];

        Page.DataBind();
        panCheerCardList.Visible = true;

        SetupEventHandlers();
        SetCheerCardList();
    }
    # endregion "Page Events"

    # region "Control Events"
    void NextButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnSelectedImageGuid.Value))
            SelectedImageGUID = hdnSelectedImageGuid.Value;

        if (IsValidInput())
        {

            string QueryString = string.Empty;

            QueryString = FormQueryString();

            EmergeURLHelper.Redirect(CheerCardFormURL + QueryString);
        }
        else
        {
            ShowError(EmergeResHelper.GetString(CheerCardConstants.STRINGCODE_NOCHEERCARDSELECTEDMESSAGE));
        }

    }
    # endregion "Control Events"

    # region "Private methods"
    /// <summary>
    /// Function to check for valid input.
    /// </summary>
    /// <returns>true, if the cheer card image is selected else, false</returns>
    private bool IsValidInput()
    {
        return SelectedImageGUID.Trim().Equals(string.Empty) ? false : true;
    }

    private void SetupEventHandlers()
    {
        repCheerCardCategories.ItemDataBound += repCheerCardCategories_ItemDataBound;
        NextButton.Click += NextButton_Click;
    }

    # endregion "Private methods"

    # region "repeater events"
    void repCheerCardCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        const string HiddenFieldCategoryIDInTransformation = "hdnCategoryID";
        const string RepeaterCheerCards = "repCheerCard";

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ICheerCardsService cheerCardService = new CheerCardService();
            

            if (null != e.Item.Controls[0].FindControl(HiddenFieldCategoryIDInTransformation))
            {
                string whereCondition = CreateGetImagesByCategoryWhereCondition(((LocalizedHidden)e.Item.Controls[0].FindControl(HiddenFieldCategoryIDInTransformation)).Value.Trim());

                if (null != ((CMSRepeater)e.Item.Controls[0].FindControl(RepeaterCheerCards)))
                {

                    ((CMSRepeater)e.Item.Controls[0].FindControl(RepeaterCheerCards)).DataSource = cheerCardService.GetCheerCardImagesByCriteria(whereCondition);
                    ((CMSRepeater)e.Item.Controls[0].FindControl(RepeaterCheerCards)).DataBind();

                    if (((CMSRepeater)e.Item.Controls[0].FindControl(RepeaterCheerCards)).Items.Count == 0)
                    {
                        e.Item.Visible = false;
                    }

                }
            }
        }
    }

    private string CreateGetImagesByCategoryWhereCondition(string categoryName)
    {
        string whereCondition = string.Empty;
        whereCondition = " CategoryName = '" + categoryName + "'";
        whereCondition += " AND " + Constants.CUSTOM_TABLE_STATUS_COLUMNNAME + " = " + 1;
        return whereCondition;
    }
    # endregion "repeater events"
}