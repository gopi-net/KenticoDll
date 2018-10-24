using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.LicenseManager
{
    /// <summary>
    /// License constants.
    /// </summary>
    public class LicenseConstants
    {
        public const string ENCRYPTION_ALGO_KEY = "@1B2c3D4e5F6g7H8";

        public enum ModuleNamesEnum
        {

            /// <summary>
            /// GiftShop.
            /// </summary>
            GiftShop,
            /// <summary>
            /// Rates.
            /// </summary>
            Rates,
            /// <summary>
            /// Staff Directory.
            /// </summary>
            StaffDirectory,
            /// <summary>
            /// Events Calendar.
            /// </summary>
            EventsCalendar,
            /// <summary>
            /// Cheer Card.
            /// </summary>
            CheerCard,
            /// <summary>
            /// Maintenance.
            /// </summary>
            Maintenance,

            /// <summary>
            /// Career.
            /// </summary>
            Career,
            /// <summary>
            /// License.
            /// </summary>
            License,
            /// <summary>
            /// Location.
            /// </summary>
            Location,
            /// <summary>
            /// Donation.
            /// </summary>
            Donation,
             /// <summary>
            /// PreRegistration
            /// </summary>
            PreRegistration,

            /// <summary>
            /// History Tracker.
            /// </summary>
            HistoryTracker
           
        }

        public enum LicenseValidationEnum
        {
            /// <summary>
            /// Valid.
            /// </summary>
            Valid,
            /// <summary>
            /// Invalid.
            /// </summary>
            InvalidDomainName,
            /// <summary>
            /// Expired.
            /// </summary>
            Expired,
            /// <summary>
            /// Format Unknown.
            /// </summary>
            Unknown

        }

        /// <summary>
        /// Separator used between module names (in case of multiple modules purchased).
        /// </summary>
        public const string MODULE_NAME_SEPARATOR_IN_LICENSE_KEY = "$";
        public const string SECTION_SEPARATOR_IN_LICENSE_KEY = "$$$";
        public const string DOMAIN_NAME_LABEL_IN_LICENSE_KEY = "DomainName:";
        public const string MODULE_NAME_LABEL_IN_LICENSE_KEY = "ModuleName:";
        public const string EXPIRATION_DATE_LABEL_IN_LICENSE_KEY = "ExpirationDate:";

        public const string LICENSE_KEY_GROUP_SEPARATOR = "Domain Name: ";


    }
}
