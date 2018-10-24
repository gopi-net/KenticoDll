using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.StaffDirectory.Services;
using CMS.GlobalHelper;
using CMS.SiteProvider;

namespace Bluespire.Emerge.Components.StaffDirectory.WebParts
{
    class StaffDirectoryDetailsWebpart : StaffDirectoryWebPart
    {

        #region "protected methods"

        /// <summary>
        /// Get staff member details by querystring
        /// querystring contain itemid or combination of firstname-middlename-lastname
        /// </summary>
        /// <returns>returrn physician details in dataset</returns>
        protected DataSet GetStaffMemberDetails()
        {
            DataSet ds = new DataSet();
            string source = System.Web.HttpUtility.UrlDecode(QueryHelper.EncodedQueryString);
            int queryItemId;
            StaffDirectoryService service = new StaffDirectoryService();
            if (Int32.TryParse(QueryHelper.GetString("ItemID", ""), out queryItemId))
            {

                int itemId = QueryHelper.GetInteger("ItemID", 0);

                ds = service.GetPhysicianByItemId(itemId);
            }
            else if (QueryHelper.GetString("Name", "") != string.Empty)
            {
                string name = System.Web.HttpUtility.UrlDecode(QueryHelper.GetString("Name", ""));
                string[] names = name.Split('-');
                string where = string.Empty;
                if (names.Length >= 3)
                {
                    where = " " + StaffDirectoryConstants.FIRST_NAME + "='" + names[0] + "' ";
                    where += " AND " + StaffDirectoryConstants.MIDDLE_NAME + "='" + names[1] + "' ";
                    where += " AND " + StaffDirectoryConstants.LAST_NAME + "='" + names[2] + "' ";
                }
                else if (names.Length >= 2)
                {
                    where = " " + StaffDirectoryConstants.FIRST_NAME + " ='" + names[0] + "' ";

                    where += " AND " + StaffDirectoryConstants.LAST_NAME + "='" + names[1] + "' ";
                }



                ds = service.GetPhysicianByCriteria(where);
            }
            else
            {
                throw new Exception("Physician id not in correct format.");
            }

            return ds;
        }

        #endregion
    }
}
