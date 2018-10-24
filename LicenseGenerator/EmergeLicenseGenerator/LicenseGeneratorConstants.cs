using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace EmergeLicenseGenerator.common
//{
    public class LicenseGeneratorConstants
    {
        public const string CONNECTION_STRING = "Persist Security Info=False;database=EmergeLicenseDb ;server=win2008sql2012;user id=bluespire;password=bluespire;Current Language=English;Connection Timeout=240;";

        public enum OperationStatusEnum
        {
            /// <summary>
            /// Type of Query Result Enum.
            /// </summary>
            /// <summary>
            /// Success.
            /// </summary>
            Success,
            /// <summary>
            /// Fail.
            /// </summary>
            Failure
        }

        public const string INSERT_LICENSE_SP = "sp_InsertLicense";
        public const string INSERT_LICENSE_PARAM_DOMAINNAME = "@DomainName";
        public const string INSERT_LICENSE_PARAM_MODULENAMES = "@ModuleNames";
        public const string INSERT_LICENSE_PARAM_LICENSEKEY = "@LicenseKey";
        public const string INSERT_LICENSE_PARAM_DATEEXPIRATION = "@DateExpiration";
        public const string INSERT_LICENSE_PARAM_LICENSEID = "@LicenseID";

        public const string LOG_ENTRY_SP = "usp_LogEntry";
        public const string LOG_ENTRY_PARAM_MESSAGE = "@Message";
        public const string LOG_ENTRY_PARAM_TITLE = "@Title";
        public const string LOG_ENTRY_PARAM_TIMESTAMP = "@TimeStamp";
        public const string LOG_ENTRY_PARAM_ERRORSOURCE = "@ErrorSource";
        public const string LOG_ENTRY_PARAM_INNEREXCEPTION = "@InnerException";

        public const string GET_LICENSE_BY_DOMAINNAME_SP = "sp_GetLicenseByDomainName";
        public const string GET_LICENSE_BY_DOMAINNAM_PARAM_DOMAINNAME = "@DomainName";

        public const string LICENSE_KEY_COLUMN_NAME = "LicenseKey";

        public enum ExpirationPolicyEnum
        {
            /// <summary>
            /// No license expiration Policy Selected.
            /// </summary>
            NO_EXPIRATION_POLICY = -1,
            /// <summary>
            /// Unlimited Expiration Policy Selected.
            /// </summary>
            UNLIMITED_EXPIRATION_POLICY=0,
            /// <summary>
            /// Fix time Expiration Policy Selected.
            /// </summary>
            FIX_TIME_EXPIRATION_POLICY = 1,

        }

       

    }
//}
