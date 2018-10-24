using System;
using System.Data;
using System.Collections;
using CMS.UIControls;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Web.Pages.BasePages;
using CMS.SiteProvider;
using CMS.Membership;
using CMS.Helpers;
using CMS.DataEngine;

namespace Bluespire.Emerge.Web.Pages
{
    /// <summary>
    /// Base class for the List pages. All the pages those shows the list of custom tables should inherit from this class.
    /// </summary>
    public class EmergeListPage : EmergeToolsPage
    {
        /// <summary>
        /// get or set Edit page url
        /// </summary>
        public string EditPageUrl { get; set; }



        /// <summary>
        /// Returns the where condition for the unigrid to show the data.
        /// </summary>
        /// <param name="whereCondition">Additional where condition.</param>
        /// <returns>Updated the where condition</returns>
        protected string GetWhereCondition(string whereCondition)
        {
            return  "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + SiteContext.CurrentSite.SiteID  + ")" + whereCondition;
        }
        
        /// <summary>
        /// Init method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// OnPageLoad method. This method should be called from page_load event in the derived class.
        /// </summary>
        protected void OnPageLoad()
        {
            CurrentMaster.Title.TitleText = ResHelper.GetString("customtable.list.Title");
            //CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomTable/object.png");
            CurrentMaster.Title.HelpTopicName = "custom_tables_tools_list";
            CurrentMaster.Title.HelpName = "helpTopic";
        }

        /// <summary>
        /// Returns the dataset by filtering the data after authorization.
        /// </summary>
        /// <param name="ds">orginal dataset</param>
        /// <returns>filtered dataset</returns>
        protected DataSet OnAfterRetrieveData(DataSet ds)
        {
            // Check permission to each custom table if user is not autorized to read all (from module)
            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerResource("CMS.CustomTables", "Read"))
            {
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    ArrayList toDelete = new ArrayList();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        int customtableid = ValidationHelper.GetInteger(row["ClassID"], 0);
                        DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(customtableid);
                        if (dci != null)
                        {
                            if (!MembershipContext.AuthenticatedUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
                            {
                                toDelete.Add(row);
                            }
                        }
                    }

                    // Delete from DataSet
                    foreach (DataRow row in toDelete)
                    {
                        ds.Tables[0].Rows.Remove(row);
                    }

                    // Redirect to access denied page if user doesn't have permission to any custom table
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        MissingPermissionsRedirect();
                    }
                }
                else
                {
                    // Redirect to access denied page if user doesn't have permission to any custom table
                    MissingPermissionsRedirect();
                }
            }

            return ds;
        }

        /// <summary>
        /// Handles the UniGrid's OnAction event.
        /// </summary>
        /// <param name="actionName">Name of item (button) that throws event</param>
        /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
        protected void OnGridAction(string actionName, object actionArgument)
        {
            if (actionName == "edit")
            {
                int classId = ValidationHelper.GetInteger(actionArgument, 0);
                DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(classId);
                if (dci != null)
                {
                    // Check if custom table class hasn't set specific listing page
                    if (dci.ClassListPageURL != String.Empty)
                    {
                        URLHelper.Redirect(dci.ClassListPageURL + "?customtableid=" + classId);
                    }
                    else
                    {
                        URLHelper.Redirect(EditPageUrl+"?customtableid=" + classId);
                    }
                }
            }
        }

        /// <summary>
        /// Redirects to access denied page with appropriate message.
        /// </summary>
        private void MissingPermissionsRedirect()
        {
            RedirectToAccessDenied(ResHelper.GetString("customtable.anytablepermiss"));
        }
    }
}
