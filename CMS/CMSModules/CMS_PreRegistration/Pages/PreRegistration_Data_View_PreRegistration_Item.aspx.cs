using System;
using Bluespire.Emerge.Components.Career;
using CMS.SiteProvider;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.CommonService;
using System.Collections.Generic;
using System.Data;
using CMS.DataEngine;
using CMS.CustomTables;
using Bluespire.Emerge.Components.PreRegistration.Pages;
using Bluespire.Emerge.Components.PreRegistration;
using System.Collections;
using Bluespire.Emerge.Common.CMS.SettingsProvider;
using System.Linq;
using Bluespire.Emerge.Common;

public partial class PreRegistration_Data_View_PreRegistration_Item : PreRegistrationDataItemViewPage
{
    int itemId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            base.OnPageLoad();
            CustomTableItem item = GetCustomTableItem();
            itemId = EmergeValidationHelper.GetInteger(item[PreRegistrationConstants.QUERY_FIELD_PARAMETER], 0);
            BindRepeater();
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    protected void BindRepeater()
    {
        DataTable dt = new DataTable();
        dt = GetPreRegistrationInfo();
        if (dt != null && dt.Rows.Count > 0)
        {
            PreRegistrationInfo.TransformationName = string.Format(PreRegistrationConstants.TRANSFORMATION_PREVIEW, EmergeCMSContext.CurrentSiteName);
            PreRegistrationInfo.DataSource = dt;
            PreRegistrationInfo.DataBind();
        }
    }
    protected DataTable GetPreRegistrationInfo()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        ds = GetInfoUsingQuery();
        if (ds != null)
        {
            dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (row[column] == string.Empty)
                            row[column] = "--";
                        else if (column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SSN || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_GISSN || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PIPOLICYNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PIGROUPNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_PISOCIALSECURITYNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SIPOLICYNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SIGROUPNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_SISOCIALSECURITYNUMBER || column.ToString() == PreRegistrationConstants.FIELD_PREREGINFO_BPISSN)
                            row[column] = EmergeEncryptionHelper.DecryptData(row[column].ToString());
                    }
                }
            }
        }
        return dt;
    }
    protected DataSet GetInfoUsingQuery()
    {
        Hashtable parameters = new Hashtable();
        string queryName = string.Format(PreRegistrationConstants.QUERY_GETPREREGISTRATIONINFO, EmergeCMSContext.CurrentSiteName);
        parameters.Add("@" + PreRegistrationConstants.QUERY_FIELD_PARAMETER, itemId);
        return EmergeSqlHelperClass.ExecuteQuery(queryName, parameters, null, null);
    }
}