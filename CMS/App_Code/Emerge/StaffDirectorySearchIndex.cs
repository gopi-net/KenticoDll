using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.StaffDirectory;
using CMS.DataEngine;
using Lucene.Net.Documents;
using CMS.Search;
using CMS.CustomTables;
using CMS.Helpers;
using CMS.Base;

/// <summary>
/// Summary description for StaffDirectorySearchIndex
/// </summary>
public class StaffDirectorySearchIndex : ICustomSearchIndex
{
    public StaffDirectorySearchIndex()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region ICustomSearchIndex Members


    /// <summary>
    /// Implementation of rebuild method.
    /// </summary>
    /// <param name="srchInfo">Search index info</param>
    public void Rebuild(SearchIndexInfo srchInfo)
    {
        SearchIndexProvider srchProvider = new SearchIndexProvider(srchInfo);
        // Check whether index info object is defined
        if (srchInfo != null)
        {
            // Get index writer for current index
            IIndexWriter iw = srchProvider.GetWriter(true);

            // Check whether writer is defined
            if (iw != null)
            {
                try
                {
                    //Check whether exist index settings
                    if ((srchInfo.IndexSettings != null))
                    {
                        // Get settings info
                        SearchIndexSettingsInfo sisi = srchInfo.IndexSettings.Items[SearchHelper.CUSTOM_INDEX_DATA];
                        if (sisi != null)
                        {
                            string staffSiteName = EmergeStaticHelper.SetSiteName(StaffDirectoryConstants.CUSTOMTABLE_CODENAME_SD_STAFF);
                            DataClassInfo dci = DataClassInfoProvider.GetDataClassInfo(staffSiteName);
                            CustomTableItemProvider itemProvider = new CustomTableItemProvider();

                            if (dci != null)
                            {


                                // Get primary key
                                //string primaryKey = SqlGenerator.GetPKName(dci.ClassXmlSchema);
                                string primaryKey = StaffDirectoryConstants.ItemID;
                                if (!String.IsNullOrEmpty(primaryKey))
                                {


                                    string queryName = dci.ClassName + ".selectall";
                                    string idColumn = Convert.ToString(primaryKey);
                                    int batchSize = srchInfo.IndexBatchSize;
                                    bool multiplePKs = idColumn.Contains(";");

                                    // Last item id
                                    int lastId = 0;

                                    List<ISearchDocument> documents = null;

                                    // Get forum iDocuments
                                    while ((documents != null) || (lastId == 0))
                                    {
                                        documents = null;

                                        DataSet ds = null;
                                        // If the table has multiple PKs, disable the batch size
                                        ds = ConnectionHelper.ExecuteQuery(queryName, null, string.Empty, "", -1, null);
                                        if (!DataHelper.DataSourceIsEmpty(ds))
                                        {
                                            documents = new List<ISearchDocument>();
                                            foreach (DataRow dr in ds.Tables[0].Rows)
                                            {
                                                SearchDocumentParameters srchDocumentParams = new SearchDocumentParameters();
                                                srchDocumentParams.Type = SearchHelper.CUSTOM_SEARCH_INDEX;
                                                srchDocumentParams.Id = Convert.ToString(dr[StaffDirectoryConstants.ItemID]);
                                                srchDocumentParams.SiteName = SearchHelper.INVARIANT_FIELD_VALUE;
                                                srchDocumentParams.Created = Convert.ToDateTime(dr[StaffDirectoryConstants.ITEM_CREATED_WHEN]);
                                                srchDocumentParams.Culture = SearchHelper.INVARIANT_FIELD_VALUE;
                                                
                                                ISearchDocument doc = SearchHelper.CreateDocument(srchDocumentParams);
                                                
                                                foreach (DataColumn dl in ds.Tables[0].Columns)
                                                {
                                                    // Adds a content field. The value of this field is used for the search result excerpt.
                                                    SearchHelper.AddGeneralField(doc, dl.ColumnName, dr[dl.ColumnName], true, false);
                                                }
                                                SearchParameters obj = new SearchParameters();
                                                documents.Add(doc);
                                            }
                                            if ((documents != null) && (documents.Count > 0))
                                            {
                                                // Get last ID  (document id)
                                                lastId = ValidationHelper.GetInteger(documents[documents.Count - 1].Get(SearchFieldsConstants.ID), 1);

                                                // Loop thru all iDocuments
                                                foreach (ISearchDocument doc in documents)
                                                {
                                                    iw.AddDocument(doc);
                                                }

                                                iw.Flush();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            // Optimize index
                            srchInfo.IndexStatus = IndexStatusEnum.OPTIMIZING;

                            if (SystemContext.IsRunningOnAzure)
                            {
                                SearchIndexInfoProvider.SetSearchIndexInfo(srchInfo);
                            }

                            iw.Optimize();
                        }
                    }
                }
                catch (Exception ex)
                {
                    EmergeLogWriter.WriteError("Search Index", EventCode.EMERGE_UPDATE, ex.ToString());
                    // Set statuses to error
                    srchInfo.IndexStatus = IndexStatusEnum.ERROR;

                    if (SystemContext.IsRunningOnAzure)
                    {
                        SearchIndexInfoProvider.SetSearchIndexInfo(srchInfo);
                    }
                }
                finally
                {
                    if (iw != null)
                    {
                        // Close index
                        iw.Close();
                    }
                }
            }
        }
    }

    #endregion
}