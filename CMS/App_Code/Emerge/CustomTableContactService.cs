using Bluespire.Emerge.Common.CMS.GlobalHelper;
using Bluespire.Emerge.CommonService;
using CMS.DataEngine;
using CMS.FormEngine;
using GlobalLink.ProjectDirector.WebServices.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using CMS.Base;
using CMS.OnlineMarketing;
using CMS.Globalization;
using CMS.ContactManagement;

/// <summary>
/// Summary description for CustomTableContactService
/// </summary>
public class CustomTableContactService
{
    static ContactInfo objContact = ContactManagementContext.CurrentContact;    
     const string COLUMN_CONTACT_COLUMNMAPPING = "ColumnMappings";
     const string COLUMN_CONTACT_ALLOWOVERWRITE = "AllowOverwrite";
     const string COLUMN_CONTACT_ISLOGGINGENABLED = "IsLoggingEnabled";
     const string COLUMN_CONTACT_CUSTOMTABLEID = "CustomTableID";
    
     const string COLUMN_STATEMASTER_CUSTOMTABLECLASSNAME = "CustomTableClassName";
     const string COLUMN_STATEMASTER_MASTERTABLENAME = "MasterTableName";
     const string COLUMN_STATEMASTER_MASTERTABLESTATENAMECOLUMNNAME = "MasterTableStateNameColumnName";
     const string COLUMN_STATEMASTER_ITEMID = "ItemID";

     const string TABLE_CONTACT_COLUMNMAPPING = "customtable.Emerge_OM_CustomTableContactMapping";
     const string TABLE_STATEMASTER_MAPPING = "customtable.Emerge_OM_CustomTableStateMasterMapping";
    public static void UpdateContactInformation(object editedObject)
    {
        string columnMappings = string.Empty;
        bool OverriteContactData = false;
        BaseInfo obj = (BaseInfo)editedObject as BaseInfo;
        DataSet ds = GetCustomTableConfiguration(obj.TypeInfo.ObjectClassName);
        if (!EmergeDataHelper.DataSourceIsEmpty(ds))
        {
            columnMappings = Convert.ToString(ds.Tables[0].Rows[0][COLUMN_CONTACT_COLUMNMAPPING]);
            OverriteContactData = Convert.ToBoolean(ds.Tables[0].Rows[0][COLUMN_CONTACT_ALLOWOVERWRITE]);
        }
        SaveContactData(columnMappings, obj, OverriteContactData);
    }
    public static bool IsTableAllowedForLoggingActivity(string ClassNameOfTableBeingChanged)
    {
        bool isLoggingEnabled = false;
        DataSet ds = GetCustomTableConfiguration(ClassNameOfTableBeingChanged);
        if (!EmergeDataHelper.DataSourceIsEmpty(ds))
        {
            isLoggingEnabled = Convert.ToBoolean(ds.Tables[0].Rows[0][COLUMN_CONTACT_ISLOGGINGENABLED]);
        }
        return isLoggingEnabled;
    }

    private static DataSet GetCustomTableConfiguration(string ClassNameOfTableBeingChanged)
    {
        int CustomTableId = CustomTableDataHelper.GetCustomTableClassInfo(ClassNameOfTableBeingChanged).ClassID;
        return CustomTableDataHelper.GetCustomTableItemsByCondition(TABLE_CONTACT_COLUMNMAPPING, COLUMN_CONTACT_CUSTOMTABLEID + "=" + CustomTableId, string.Empty);
    }

    private static string GetStringValue(string value, string newValue, bool overwrite)
    {
        if (overwrite)
            return newValue;
        if (value.Trim() == string.Empty)
            return newValue;
        return value;
    }
    private static int GetStateValue(string className, int stateId)
    {
        string masterTableName = string.Empty;
        string masterTableStateNameColumnName = string.Empty;
        string stateName = string.Empty;
        StateInfo stateInfo = null;

        DataSet ds = CustomTableDataHelper.GetCustomTableItemsByCondition(TABLE_STATEMASTER_MAPPING, COLUMN_STATEMASTER_CUSTOMTABLECLASSNAME + "='" + className + "'", string.Empty);
        if (EmergeDataHelper.DataSourceIsEmpty(ds))
            return 0;
        masterTableName = Convert.ToString(ds.Tables[0].Rows[0][COLUMN_STATEMASTER_MASTERTABLENAME]);
        masterTableStateNameColumnName = Convert.ToString(ds.Tables[0].Rows[0][COLUMN_STATEMASTER_MASTERTABLESTATENAMECOLUMNNAME]);

        DataSet dsInner = CustomTableDataHelper.GetCustomTableItemsByCondition(masterTableName, COLUMN_STATEMASTER_ITEMID + "=" + stateId, string.Empty);
        if (EmergeDataHelper.DataSourceIsEmpty(dsInner))
            return 0;
        if (masterTableStateNameColumnName == string.Empty)
            return 0;
        stateName = Convert.ToString(dsInner.Tables[0].Rows[0][masterTableStateNameColumnName]).Replace(" ", "");
        if (stateName == string.Empty)
            return 0;
        stateInfo = StateInfoProvider.GetStateInfo(stateName);
        if (stateInfo == null)
            return 0;
        //set country to USA if not already set
        CountryInfo info = CountryInfoProvider.GetCountryInfo("USA");
        objContact.ContactCountryID = GetIntegerValue(objContact.ContactCountryID, info.CountryID, false);
        return stateInfo.StateID;
    }
    private static int GetIntegerValue(int value, int newValue, bool overwrite)
    {
        if (overwrite)
            return newValue;
        if (value <= 0)
            return newValue;
        return value;
    }
    private static int GetGenderID(string gender)
    {
        switch (gender.ToLowerCSafe())
        {
            case "m":
            case "male":
                return 1;
            case "f":
            case "female":
                return 2;
            default:
                return 0;
        }

    }
    private static DateTime GetDateTimeValue(DateTime value, DateTime newValue, bool overwrite)
    {
        if (overwrite)
            return newValue;
        if (value == null)
            return newValue;
        if (value == DateTime.MinValue)
            return newValue;
        return value;
    }
    private static void SaveContactData(string mapping, BaseInfo editedObject, bool OverriteContactData)
    {
        if (!string.IsNullOrEmpty(mapping))
        {
            FormInfo mapInfo = new FormInfo(mapping);
            if (mapInfo.ItemsList.Count > 0)
            {
                var fields = mapInfo.GetFields(true, true);
                foreach (FormFieldInfo ffi in fields)
                {
                    switch (ffi.Name.ToLowerCSafe())
                    {
                        case "contactaddress1":
                            objContact.ContactAddress1 = GetStringValue(objContact.ContactAddress1, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;                        
                        case "contactbirthday":
                            objContact.ContactBirthday = GetDateTimeValue(objContact.ContactBirthday, Convert.ToDateTime(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactbusinessphone":
                            objContact.ContactBusinessPhone = GetStringValue(objContact.ContactBusinessPhone, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactcity":
                            objContact.ContactCity = GetStringValue(objContact.ContactCity, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactcountryid":
                           //Country ID
                            CountryInfo info = CountryInfoProvider.GetCountryInfo(editedObject.GetStringValue(ffi.MappedToField, string.Empty));
                            objContact.ContactCountryID = GetIntegerValue(objContact.ContactCountryID, info.CountryID, OverriteContactData);
                            break;
                        case "contactcompanyname":
                            objContact.ContactCompanyName = GetStringValue(objContact.ContactCompanyName, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactemail":
                            objContact.ContactEmail = GetStringValue(objContact.ContactEmail, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactfirstname":
                            objContact.ContactFirstName = GetStringValue(objContact.ContactFirstName, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactgender":
                            //Gender ID
                            int genderId = GetGenderID(Convert.ToString(editedObject.GetValue(ffi.MappedToField)));
                            objContact.ContactGender = GetIntegerValue(objContact.ContactGender, genderId, OverriteContactData);
                            break;                       
                        case "contactjobtitle":
                            objContact.ContactJobTitle = GetStringValue(objContact.ContactJobTitle, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactlastname":
                            objContact.ContactLastName = GetStringValue(objContact.ContactLastName, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactmiddlename":
                            objContact.ContactMobilePhone = GetStringValue(objContact.ContactMobilePhone, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        case "contactmobilephone":
                            objContact.ContactMobilePhone = GetStringValue(objContact.ContactMobilePhone, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;                       
                        case "contactstateid":
                            //State ID
                            int stateId = GetStateValue(editedObject.TypeInfo.ObjectClassName, Convert.ToInt32(editedObject.GetValue(ffi.MappedToField)));
                            objContact.ContactStateID = GetIntegerValue(objContact.ContactStateID, stateId, OverriteContactData);
                            break;                        
                        case "contactzip":
                            objContact.ContactZIP = GetStringValue(objContact.ContactZIP, Convert.ToString(editedObject.GetValue(ffi.MappedToField)), OverriteContactData);
                            break;
                        default:

                            // ... contact's custom fields
                            break;
                    }
                }
                objContact.Update();
            }
        }
    }
}