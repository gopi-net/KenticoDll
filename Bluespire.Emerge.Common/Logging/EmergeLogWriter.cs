using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using CMS.EventLog;
using CMS.IO;
using CMS.Helpers;
using CMS.Base;


namespace Bluespire.Emerge.Common.Logging
{

    public enum EventCode
    {
        EMERGE_GET,
        EMERGE_UPDATE,
        EMERGE_DELETE,
        EMERGE_ADD,
        EMERGE_ENCDEC,
        EMERGE_EMAIL,
        EMERGE_PROCESS_PAYMENTGATEWAY,
        EMERGE_PAGENOTFOUND,
        EMERGE_LICENSE_NOTPURCHASED,
        EMERGE_LICENSE_MISSINGLICENSEKEY,
        EMERGE_LICENSE_EXPIREDLICENSEKEY,
        EMERGE_LICENSE_INVALIDDOMAIN,
        EMERGE_LICENSE_UNKNOWNLICENSE,
        EMERGE_ACTION_PERMISSIONMISSING,
        EMERGE_MODULECODENAME_MISSING,
        EMERGE_PERMISSIONMISSING,
        EMERGE_PERMISSIONCONFIGFILEMISSING,
        EMERGE_UNITYCONFIGURATION,
        EMERGE_EVENTSOCCURENCES_SAVE,
        EMERGE_UNHANDLED,
        EMERGE_USEREXISTS,
        EMERGE_INVALIDUSER
    }
    

    /// <summary>
    /// Writes the entries to event log table.
    /// </summary>
    public abstract class EmergeLogWriter
    {
        /// <summary>
        /// Logs file path.
        /// </summary>
        private static string mLogFile = null;

        private static string LogFile
        {
            get
            {
                if (mLogFile == null)
                {
                    mLogFile = SystemContext.WebApplicationPhysicalPath + "\\App_Data\\EmergeEvents.log";
                    DirectoryHelper.EnsureDiskPath(mLogFile, SystemContext.WebApplicationPhysicalPath);
                }

                return mLogFile;
            }
        }

        private static void WriteEventLog(string eventType, string source, EventCode eventCode , string eventDescription)
        {
            try
            {
                EventLogProvider.LogEvent(eventType, source, eventCode.ToString(), eventDescription);
                
            }
            catch
            {
                LogEventToFile(eventType, source, eventCode, eventDescription);
            }

        }

        private static void LogEventToFile(string eventType, string source, EventCode eventCode, string eventDescription)
        {
            string entry = String.Format("Event type: {0}, Source: {1}, EventCode: {2}, Description {3}", eventType, source, eventCode.ToString(), eventDescription);
            System.IO.File.AppendAllText(LogFile, entry, Encoding.UTF8);
        }


        /// <summary>
        /// Writes Informative events to the event log table
        /// </summary>
        /// <param name="source">Source of the event</param>
        /// <param name="eventCode">Event code of event</param>
        /// <param name="eventDescription">Description of event</param>
        public static void WriteInformation(string source, EventCode eventCode, string eventDescription)
        {
            EventLogProvider.LogInformation( source, eventCode.ToString(), eventDescription);
        }

        /// <summary>
        /// Writes Error events to the event log table
        /// </summary>
        /// <param name="source">Source of the event</param>
        /// <param name="eventCode">Event code of event</param>
        /// <param name="eventDescription">Description of event</param>
        public static void WriteError(string source, EventCode eventCode, string eventDescription)
        {
            Exception ex = new Exception(eventDescription);
            EventLogProvider.LogException(source, eventCode.ToString(), ex);
        }

        /// <summary>
        /// Writes Warning events to the event log table
        /// </summary>
        /// <param name="source">Source of the event</param>
        /// <param name="eventCode">Event Coce of event</param>
        /// <param name="eventDescription">Description of event</param>
        public static void WriteWarning(string source, EventCode eventCode, string eventDescription)
        {
            Exception ex = new Exception(eventDescription);
            EventLogProvider.LogWarning(source, eventCode.ToString(), ex, Emerge.Common.CMS.CMSHelper.EmergeCMSContext.CurrentSiteID, eventDescription);
        }
       
    }
}
