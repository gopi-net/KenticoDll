using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Components.CheerCard.Services;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Components.CheerCard.WebParts
{
    public class CheerCardListingWebPart : CheerCardWebPart
    {
        const string CHEERCARDCATEGORIESREPEATER_CONTROL_ID = "repCheerCardCategories";

        CMSRepeater cheercardCategoriesRepeater
        { get { return (CMSRepeater)ControlPanel.FindControl(CHEERCARDCATEGORIESREPEATER_CONTROL_ID); } }

        # region "Webpart Properties"
        public string CheerCardFormURL
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("CheerCardFormPageUrl"), "");
            }
            set
            {
                SetValue("CheerCardFormPageUrl", value);
            }
        }

        public string TransformationName
        {
            get
            {
                return EmergeValidationHelper.GetString(GetValue("TransformationName"), cheercardCategoriesRepeater.TransformationName);
            }
            set
            {
                SetValue("TransformationName", value);
                cheercardCategoriesRepeater.TransformationName = value;
            }
        }
        # endregion "Webpart Properties"

        string _SelectedImageGUID = string.Empty;
        protected string SelectedImageGUID
        {
            get { return _SelectedImageGUID; }
            set {_SelectedImageGUID = value;}
        }

        /// <summary>
        /// Property to check if image selected is of "No Card"
        /// </summary>
        protected bool IsNoImageSelected
        {
            get { return SelectedImageGUID.ToLower().Equals(CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT.ToLower()) ? true : false; }
        }

        protected string FormQueryString()
        {
            string parameter = string.Empty;
            string QueryString = string.Empty;
            parameter = CheerCardConstants.QUERYSTRING_PARAMETER_NAME_FOR_SELECTED_CHEERCARD + "," + SelectedImageGUID;
            QueryString = EmergeQueryHelper.BuildQuery(parameter.Split(new char[] { ',' }));
            return QueryString;
        }

        protected void SetCheerCardList()
        {
            cheercardCategoriesRepeater.TransformationName = TransformationName;
            ICheerCardsService cheerCardService = new CheerCardService();
            cheercardCategoriesRepeater.DataSource = cheerCardService.GetCheerCardCategoriesByCriteria(Constants.CUSTOM_TABLE_STATUS_COLUMNNAME + " = " + 1);
            cheercardCategoriesRepeater.DataBind();
        }
    }
}
