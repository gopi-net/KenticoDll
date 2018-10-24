using System;
using System.Collections.Generic;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Logging;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.CommonService.HistoryTracker;
using CMS.SiteProvider;
using CMS.Base;
using CMS.DataEngine;
using CMS.CustomTables;
using CMS.MacroEngine;
using Bluespire.Emerge.Components.EventsCalendar.CustomMacros;


/// <summary>

/// Exlude objects of the object type 'role' whose code name starts with 'CMS' from staging.

/// </summary>

[CustomObjectEvents]

public partial class CMSModuleLoader
{

    /// <summary>

    /// Ensure the loading of custom handlers

    /// </summary>

    

    private class CustomObjectEventsAttribute : CMSLoaderAttribute
    {
        List<HistoryTrackerInfo> trackingItems = new List<HistoryTrackerInfo>();
        //HistoryTrackerService historyService = new HistoryTrackerService();
        /// <summary>

        /// Called automatically when the application starts

        /// </summary>

        public override void Init()
        {


            ObjectEvents.Insert.After += new EventHandler<ObjectEventArgs>(InsertObject_After);
            ObjectEvents.Update.Before += new EventHandler<ObjectEventArgs>(UpdateObject_Before);
            ObjectEvents.Update.After += new EventHandler<ObjectEventArgs>(UpdateObject_After);
            ObjectEvents.Delete.Before += new EventHandler<ObjectEventArgs>(Delete_Before);
            Extend<StringNamespace>.With<CustomMacroMethods>();


        }


        private void InsertObject_After(object sender, ObjectEventArgs e)
        {
            var insertedItem = e.Object;

            if (IsCustomTable(insertedItem) && HistoryTrackerService.IsTableAllowedForHistoryTracking(insertedItem.TypeInfo.ObjectClassName))
            {
                //EmergeLogWriter.WriteError("InsertAfter", EventCode.EMERGE_ADD, "Before Generating History Items itemID:" + insertedItem.GetValue("itemid") + " CustomTableName:" + insertedItem.TypeInfo.ObjectClassName);
                GenerateHistoryTrackingItems(insertedItem, HistoryTrackerInfo.OperationType.INSERT);
                //EmergeLogWriter.WriteError("InsertAfter", EventCode.EMERGE_ADD, "After Generating History Items itemID:" + insertedItem.GetValue("itemid") + " CustomTableName:" + insertedItem.TypeInfo.ObjectClassName + " Generated Item Count: " +trackingItems.Count.ToString());
                
                HistoryTrackerService.TrackHistory(new List<HistoryTrackerInfo>(trackingItems));
                //EmergeLogWriter.WriteError("InsertAfter", EventCode.EMERGE_ADD, "After Tracking History Items itemID:" + insertedItem.GetValue("itemid") + " CustomTableName:" + insertedItem.TypeInfo.ObjectClassName);
            }
            if (IsCustomTable(insertedItem) && CustomTableContactService.IsTableAllowedForLoggingActivity(insertedItem.TypeInfo.ObjectClassName))
            {
                CustomTableContactService.UpdateContactInformation(insertedItem);
            }
        }

        private bool IsCustomTable(BaseInfo Item)
        {
            if (Item.TypeInfo.ModuleName != null && Item.TypeInfo.ModuleName.ToLower().Equals(Constants.CUSTOM_TABLE_MODULE_NAME.ToLower()))
                return true;
            return false;
        }

        private void UpdateObject_Before(object sender, ObjectEventArgs e)
        {
            var updatedItem = e.Object;

            trackingItems.Clear();

            if (IsCustomTable(updatedItem) && HistoryTrackerService.IsTableAllowedForHistoryTracking(updatedItem.TypeInfo.ObjectClassName))
            {
                GenerateHistoryTrackingItems(updatedItem, HistoryTrackerInfo.OperationType.UPDATE);
            }

        }

        private void UpdateObject_After(object sender, ObjectEventArgs e)
        {

            var updatedItem = e.Object;

            if (IsCustomTable(updatedItem) && HistoryTrackerService.IsTableAllowedForHistoryTracking(updatedItem.TypeInfo.ObjectClassName))
            {
                HistoryTrackerService.TrackHistory(new List<HistoryTrackerInfo>(trackingItems));
            }

            if (IsCustomTable(updatedItem) && CustomTableContactService.IsTableAllowedForLoggingActivity(updatedItem.TypeInfo.ObjectClassName))
            {
                CustomTableContactService.UpdateContactInformation(updatedItem);
            }
        }

        private void Delete_Before(object sender, ObjectEventArgs e)
        {

            var itemToDelete = e.Object;

            if (IsCustomTable(itemToDelete) && HistoryTrackerService.IsTableAllowedForHistoryTracking(itemToDelete.TypeInfo.ObjectClassName))
            {
                GenerateHistoryTrackingItems(itemToDelete, HistoryTrackerInfo.OperationType.DELETE);
                HistoryTrackerService.TrackHistory(new List<HistoryTrackerInfo>(trackingItems));
            }

        }

        /// <summary>
        /// method to add Tracking objects in the list.
        /// </summary>
        /// <param name="itemBeingChanged"> custom table item being changed.</param>
        /// <param name="Operation"> operation being perform on the custom table item.</param>
        private void GenerateHistoryTrackingItems(BaseInfo itemBeingChanged, HistoryTrackerInfo.OperationType Operation)
        {
            trackingItems.Clear();

            List<string> columns = new List<string>();
            columns = Operation == HistoryTrackerInfo.OperationType.UPDATE ? itemBeingChanged.ChangedColumns() : itemBeingChanged.ColumnNames;

            if (columns.Count == 0) return;

            CustomTableItem oldItem = null;
            oldItem = Operation == HistoryTrackerInfo.OperationType.UPDATE ? CustomTableDataHelper.GetCustomTableItem(((CustomTableItem)itemBeingChanged).ItemID, itemBeingChanged.TypeInfo.ObjectClassName) : null;
            DataClassInfo dcp = CustomTableDataHelper.GetCustomTableClassInfo(itemBeingChanged.TypeInfo.ObjectClassName);
            string groupGuid = Guid.NewGuid().ToString();
            string description = string.Empty;
            description = GetDescription(Operation);
            int itemID = Convert.ToInt32(itemBeingChanged.GetValue(Constants.CUSTOMTABLE_PRIMARY_KEY_COLUMNNAME));

            foreach (string columnName in columns)
            {
                string newValue = string.Empty;
                string oldValue = string.Empty;

                oldValue = Operation == HistoryTrackerInfo.OperationType.UPDATE ? (oldItem.GetValue(columnName) ?? string.Empty).ToString() : (Operation == HistoryTrackerInfo.OperationType.DELETE ? (itemBeingChanged.GetValue(columnName) ?? string.Empty).ToString() : string.Empty);
                newValue = Operation == HistoryTrackerInfo.OperationType.UPDATE ? (itemBeingChanged.GetValue(columnName) ?? string.Empty).ToString() : (Operation == HistoryTrackerInfo.OperationType.INSERT ? (itemBeingChanged.GetValue(columnName) ?? string.Empty).ToString() : string.Empty);

                HistoryTrackerInfo HistoryTracker = new HistoryTrackerInfo(itemBeingChanged.TypeInfo.ObjectType, dcp.ClassDisplayName, columnName, oldValue, newValue, description, groupGuid, itemID);
                trackingItems.Add(HistoryTracker);
            }
        }

        /// <summary>
        /// Returns description depending on Operation being perform on the custom table
        /// </summary>
        private string GetDescription(HistoryTrackerInfo.OperationType Operation)
        {
            return (Operation == HistoryTrackerInfo.OperationType.UPDATE ? Constants.DESCRIPTION_FOR_UPDATE_ITEM : (Operation == HistoryTrackerInfo.OperationType.INSERT ? Constants.DESCRIPTION_FOR_NEW_ITEM : Constants.DESCRIPTION_FOR_DELETE_ITEM));
        }

        



    }

}