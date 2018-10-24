using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.SiteProvider;
using System.Data;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.LicenseManager;
using Bluespire.Emerge.LicenseManager.Exceptions;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;
using CMS.Helpers;
using CMS.CustomTables;
using CMS.Membership;

namespace Bluespire.Emerge.CommonService.License
{
    /// <summary>
    /// Class declare to get the License Key from Custom table and communicate with the License Manager Layer to Validate the License.
    /// </summary>
    /// 
    public class LicenseProvider
    {

        /// <summary>
        /// Function to get the License Key based on domain name from License Custom Table.
        /// </summary>
        private static string GetLicenseKey()
        {
            string currentHost = string.Empty;
            currentHost = RequestContext.CurrentDomain;
            currentHost = currentHost.Contains("localhost:") ? "localhost" : currentHost;

            string Key = string.Empty;

            string where = string.Format(Constants.WHERECONDITION_FOR_LICENSE_KEY, currentHost);

            DataSet dsLicense = CustomTableItemProvider.GetItems(EmergeStaticHelper.SetSiteName(Constants.LICENSE_CUSTOM_TABLE_CODE_NAME), where, Constants.ORDERBY_FOR_LICENSE_KEY);

            if (dsLicense.Tables[0].Rows.Count > 0)
                Key = dsLicense.Tables[0].Rows[0][Constants.LICENSE_KEY_CUSTOM_TABLE_COLUMN_NAME].ToString();
            if (string.IsNullOrEmpty(Key)) throw new MissingLicenseKeyException("License Key is missing.");
            return Key;
        }

        /// <summary>
        /// Function to validate the License with respect to Module name. 
        /// This function will cached the Valid Module names against the domain name and returns boolean value.
        /// </summary>
        /// <exception cref="MissingLicenseKeyException"> Thrown in case of empty Lincense key.</exception>
        /// <exception cref="ExpiredLicenseKeyException"> Thrown if  License key expired.</exception>
        /// <exception cref="InvalidDomainNameException"> Thrown if  Domain name not matching with the License key domain name.</exception>
        /// <exception cref="UnknownLicenseFormatException"> Thrown if  Invalid or Unknown License Format</exception>
        /// <exception cref="LicenseKeyException"> Thrown in case of ArgumentNullException, ArgumentException, FormatException, or exception in Encrypt/decrypt method.</exception>
        public static bool ValidateLicense(string ModuleName)
        {

            if (!IsLicenseCheckRequiredForModule(ModuleName)) return true;
            
            string Key = string.Empty;
            try
            {
                               
                if (!isLicenseModulesCached())
                {
                    
                    Key = GetLicenseKey();
                    List<string> ValidModules = new List<string>();
                    ValidModules = LicenseBAL.GetValidModules(Key, RequestContext.CurrentDomain);

                    CacheLicenseModules(ValidModules);


                }
            }
            catch (ModuleNotPurchasedException moduleNotPurchased)
            {
                EmergeLogWriter.WriteInformation("Method Name: LicenseBAL.ValidateLicense", EventCode.EMERGE_LICENSE_NOTPURCHASED, moduleNotPurchased.ToString());
                throw moduleNotPurchased;
            }
            catch (MissingLicenseKeyException missingLicenseKey)
            {
                EmergeLogWriter.WriteError("Method Name: GetLicenseKey", EventCode.EMERGE_LICENSE_MISSINGLICENSEKEY, missingLicenseKey.ToString());
                throw missingLicenseKey;
            }
            catch (ExpiredLicenseKeyException expiredLicenseKey)
            {
                EmergeLogWriter.WriteInformation("Method Name: LicenseBAL.ValidateLicense", EventCode.EMERGE_LICENSE_EXPIREDLICENSEKEY, expiredLicenseKey.ToString());
                throw expiredLicenseKey;
            }
            catch (InvalidDomainNameException invalidDomainName)
            {
                EmergeLogWriter.WriteInformation("Method Name: LicenseBAL.ValidateLicense", EventCode.EMERGE_LICENSE_INVALIDDOMAIN, invalidDomainName.ToString());
                throw invalidDomainName;
            }
            catch (LicenseKeyException licenseKeyException)
            {
                EmergeLogWriter.WriteError("Method Name: LicenseBAL.ValidateLicense", EventCode.EMERGE_LICENSE_UNKNOWNLICENSE, licenseKeyException.ToString());
                throw licenseKeyException;
            }
            
            return (getCachedLicenseModules()).Any(x => x.ToLower() == ModuleName.ToLower());
        }

        
        
        /// <summary>
        /// Function to check if module requires license check or not. 
        /// This function will cached the Valid Module names against the domain name and returns boolean value.
        /// </summary>
        /// <param name="ModuleName">Module Name.</param>
        private static bool IsLicenseCheckRequiredForModule(string ModuleName)
        {
            if (Constants.MODULES_EXCLUDED_FROM_LICENSE_CHECK.Split(new string[] { Constants.MODULES_EXCLUDED_FROM_LICENSE_CHECK_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries).Any(x => x.ToLower().Equals(ModuleName.ToLower())))
                return false;
            else
                return true;
        }

        
        private static bool isLicenseModulesCached()
        {
            return CacheHelper.GetItem(RequestContext.CurrentDomain + Constants.LICENSED_MODULES_CACHE_KEY_SUFFIX) == null ? false : true;
        }

        private static List<string> getCachedLicenseModules()
        {
            return (List<string>)CacheHelper.GetItem(RequestContext.CurrentDomain + Constants.LICENSED_MODULES_CACHE_KEY_SUFFIX);
        }

        private static void CacheLicenseModules(List<string> ValidModules)
        {
            CacheHelper.Add(RequestContext.CurrentDomain + Constants.LICENSED_MODULES_CACHE_KEY_SUFFIX, ValidModules, null, DateTime.Now.AddMinutes(Constants.LICENSE_KEY_CACHE_EXPIRATION_TIME), System.Web.Caching.Cache.NoSlidingExpiration);
        }
                
        public static void ClearCachedLicenseModules()
        {
           if(isLicenseModulesCached())
               CacheHelper.Remove(RequestContext.CurrentDomain + Constants.LICENSED_MODULES_CACHE_KEY_SUFFIX);
        }
    }
}
