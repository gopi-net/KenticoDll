using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CMS.FormEngine;
using CMS.MembershipProvider;
using Bluespire.Emerge.Components.EventsCalendar.Pages;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Components.EventsCalendar.Helpers;
using Bluespire.Emerge.Common;
using System.Web.UI.HtmlControls;
using Bluespire.Emerge.Components.EventsCalendar;
using System.Web.Security;
using Bluespire.Emerge.Common.Logging;
using CMS.DataEngine;
using CMS.Base;
using CMS.Membership;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using CMS.CustomTables;
using CMS.Helpers;

public partial class CMSModules_CMS_EventsCalendar_Pages_EventsCalendar_Events_VolunteerUsers : EventsCalendarDataEditItemPage
{
    private string editItemPage = EventsConstants.PAGEURL_NEW_SCHEDULE;
    private DataClassInfo dci = null;
    

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            ListPage = EventsConstants.PAGEURL_DATA_LIST;
            NewItemPage = EventsConstants.PAGEURL_NEW_VOLUNTEERUSERS;
            
            RegisterEvents();
            
            OnPageLoad();

            if (CheckForPermissions())
            {
                plcContent.Visible = false;
            }

            if (AccessGranted)
            {
                EventFormControl.CustomTableId = CustomTableID;
                EventFormControl.ItemId = ItemID;
            }
            if (CustomTableID > 0)
            {
                dci = CustomTableDataHelper.GetCustomTableClassInfo(CustomTableID);
            }
        }
        catch (Exception ex)
        {
            OnError(ex, true);
        }
    }

    private void RegisterEvents()
    {
        EventFormControl.OnBeforeSave += EventFormControl_OnBeforeSave;
        SaveButton.Click += SaveButton_Click;
    }

    void EventFormControl_OnBeforeSave(object sender, EventArgs e)
    {
        IDataContainer data = EventFormControl.CustomTableForm.Data;
        try
        {
            if (ItemID > 0)
            {
                UserInfo user = updateUser();
                setPassword(user);
            }
            else
            {
                bool userExists = CheckIfUserUniqueness(0);
                if (!userExists)
                {
                    UserInfo user = createUser();
                    addUserToVolunteerRole(user.UserID);
                    EventFormControl.CustomTableForm.Data.SetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERID, user.UserID);
                    setPassword(user);
                }
            }
        }
        catch (UserWithEmailExistsException)
        {
            ShowError(String.Format("User with the email {0} already exists.", Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_EMAIL))));
            EventFormControl.CustomTableForm.StopProcessing = true;
        }
        catch (UserWithUserNameExistsException)
        {
            ShowError(String.Format("User with the user name {0} already exists.", Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERNAME))));
            EventFormControl.CustomTableForm.StopProcessing = true;
        }
        catch (Exception ex)
        {
            EmergeLogWriter.WriteError("VolunteerUsers:Save", EventCode.EMERGE_ADD, ex.ToString());
            EventFormControl.CustomTableForm.StopProcessing = true;
            OnError(ex, true);
        }
    }

    private void setPassword(UserInfo user)
    {
        EventFormControl.CustomTableForm.Data.SetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PASSWORD, user.GetValue("UserPassword").ToString());
    }

    void SaveButton_Click(object sender, EventArgs e)
    {
        if (EventFormControl.CustomTableForm.ValidateData())
        {
            EventFormControl.CustomTableForm.SaveData(URLHelper.GetAbsoluteUrl(EventFormControl.EditItemPage) + "?customtableid=" + EventFormControl.CustomTableId + "&itemid=" + EventFormControl.CustomTableForm.ItemID);
        }
    }

    private UserInfo createUser()
    {
        IDataContainer data = EventFormControl.CustomTableForm.Data;
        UserInfo user = new UserInfo();
        
        setUserInfoObject(user);
        UserInfoProvider.SetUserInfo(user);
        if (Constants.HIDDENPASSWORD != Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PASSWORD)))
            UserInfoProvider.SetPassword(user.UserName, Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PASSWORD)));
        UserSiteInfoProvider.AddUserToSite(user.UserID, EmergeCMSContext.CurrentSiteID);
        user = UserInfoProvider.GetUserInfo(user.UserID);
        return user;
    }

    private UserInfo updateUser()
    {
        IDataContainer data = EventFormControl.CustomTableForm.Data;

        CustomTableItem item = CustomTableDataHelper.GetCustomTableItem(ItemID, String.Format(EventsConstants.CUSTOMTABLE_EVENT_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteName));
        int userID = Convert.ToInt32(item.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERID));
        CheckIfUserUniqueness(userID);
        UserInfo user = UserInfoProvider.GetUserInfo(userID);

        setUserInfoObject(user);
        user.Update();
        if (Constants.HIDDENPASSWORD != Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PASSWORD)))
            UserInfoProvider.SetPassword(user.UserName, Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_PASSWORD)));

        user = UserInfoProvider.GetUserInfo(userID);
        return user;
    }

    private void setUserInfoObject(UserInfo user)
    {
        IDataContainer data = EventFormControl.CustomTableForm.Data;
        user.FirstName = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_FIRSTNAME));
        user.LastName = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_LASTNAME));
        user.UserName = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERNAME));
        user.Email = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_EMAIL));
        user.FullName = user.FirstName + " " + user.LastName;
        user.Enabled = true;
    }

    private void addUserToVolunteerRole(int userID)
    {
        string volunteerRole = EventsConstants.ROLE_VOLUNTEERUSERS;
        RoleInfo roleInfo;

        if (!RoleInfoProvider.RoleExists(volunteerRole, EmergeCMSContext.CurrentSiteName))
            roleInfo = createVolunteerRole();
        
        roleInfo = RoleInfoProvider.GetRoleInfo(volunteerRole, EmergeCMSContext.CurrentSiteID);
        UserRoleInfoProvider.AddUserToRole(userID, roleInfo.RoleID); 
    }

    private RoleInfo createVolunteerRole()
    {
        return EmergeStaticHelper.CreateRole("Emerge Volunteer Users", EventsConstants.ROLE_VOLUNTEERUSERS, EmergeCMSContext.CurrentSiteID);
    }

    private bool CheckIfUserUniqueness(int userID)
    {
        IDataContainer data = EventFormControl.CustomTableForm.Data;
        string userName = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_USERNAME));
        string email = Convert.ToString(data.GetValue(EventsConstants.FIELDS_VOLUNTEERUSERS_EMAIL));

        EmergeStaticHelper.CheckUserbyUserName(userName, userID);
        EmergeStaticHelper.CheckUserByEmail(email, userID);
        return false;
    }
}