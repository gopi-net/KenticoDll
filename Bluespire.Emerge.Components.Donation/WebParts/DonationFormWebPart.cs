using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using CMS.DataEngine;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;

namespace Bluespire.Emerge.Components.Donation.WebParts
{
    public class DonationFormWebPart : DonationBaseWebpart
    {
        protected void setData()
        {
            CreateFormParameters();
            EmergeSessionHelper.SetValue(DonationConstants.SESSIONKEY_DONATIONINFO, FormParameters);
        }

        protected DataSet GetDataForAmountLevels(string donationType)
        {
            string queryName;
            QueryDataParameters parameters;
            queryName = string.Format(DonationConstants.QUERY_GETAMOUNTINFO, EmergeCMSContext.CurrentSiteName);
            parameters = new QueryDataParameters();
            parameters.Add("@DonationType", donationType);
            DataSet ds = ConnectionHelper.ExecuteQuery(queryName, parameters, null, null);
            return ds;
        }
    }
}
