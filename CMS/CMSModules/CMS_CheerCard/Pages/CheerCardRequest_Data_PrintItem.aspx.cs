using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.CheerCard;
using Bluespire.Emerge.Components.CheerCard.Services;
using CMS.SiteProvider;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.PortalEngine;
using Bluespire.Emerge.Components.Career;

public partial class CMSModules_CMS_CheerCard_Pages_CheerCardRequest_Data_PrintItem : CareerDataViewItemPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();
            int itemId = QueryHelper.GetInteger("ItemID", 0);
            string customTableCodeName = EmergeStaticHelper.SetSiteName(QueryHelper.GetString("CustomTableName", string.Empty));

            CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(itemId, customTableCodeName);

            Dictionary<string, object> customTableItemDict = new Dictionary<string, object>();

            foreach (string columnName in item.ColumnNames)
            {
                customTableItemDict.Add(columnName, item.GetValue(columnName));
            }
            string imageGUID = string.Empty;

            if (customTableItemDict.ContainsKey(CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID))
            {
                imageGUID = customTableItemDict[CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID] == null || String.IsNullOrEmpty(customTableItemDict[CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID].ToString()) ? CheerCardConstants.NO_CHEERCARD_SELECTED_TEXT : customTableItemDict[CheerCardConstants.FIELDS_CHEERCARDREQUEST_SELCTEDIMAGEGUID].ToString();
            }
            
            CheerCardService cheerCardService = new CheerCardService();
            contentToPrint.Text = cheerCardService.GetCheerCardPreviewHtml(customTableItemDict, Bluespire.Emerge.Common.Constants.Environments.Desktop, imageGUID, CheerCardConstants.CHEERCARD_PREVIEWHTML_DEFAULTEMPTYSTRINGPLACEHOLDER, true);
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    protected override void OnPreInit(EventArgs e)
    {
        this.Page.Theme = PortalContext.CurrentSiteStylesheetName;
    }
}