using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.Web;
using System.Globalization;
using Bluespire.Emerge.LicenseManager.Exceptions;

namespace Bluespire.Emerge.LicenseManager
{
    /// <summary>
    /// Class declared to Create new licenses based on domain name, slected modules, and expiration policies.
    /// Class also validate the given license.
    /// </summary>

    public class LicenseBAL
    {
        /// <summary>
        /// function to Create Encrypted License Key.
        /// </summary>
        public static void CreateLicenseKey(LicenseInfo LicenseInfo)
        {
            try
            {
                LicenseInfo.Key = string.Empty;

                string Licensekey = string.Empty;
                Licensekey = LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY + LicenseConstants.DOMAIN_NAME_LABEL_IN_LICENSE_KEY + LicenseInfo.DomainName.Trim() + LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY;
                string allModules = string.Empty;

                foreach (string moduleName in LicenseInfo.ModuleNames)
                {
                    allModules += moduleName + LicenseConstants.MODULE_NAME_SEPARATOR_IN_LICENSE_KEY;
                }

                allModules = allModules.Substring(0, allModules.LastIndexOf(LicenseConstants.MODULE_NAME_SEPARATOR_IN_LICENSE_KEY));

                Licensekey += LicenseConstants.MODULE_NAME_LABEL_IN_LICENSE_KEY + allModules + LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY;
                Licensekey += LicenseConstants.EXPIRATION_DATE_LABEL_IN_LICENSE_KEY + (LicenseInfo.ExpirationDate == null ? "Unlimited" : LicenseInfo.ExpirationDate.Value.ToString("MM/dd/yyyy"));

                Utilities.AESEncDecString obj = new AESEncDecString(LicenseConstants.ENCRYPTION_ALGO_KEY);
                Licensekey = obj.Encrypt(Licensekey);


                Licensekey += LicenseConstants.LICENSE_KEY_GROUP_SEPARATOR + LicenseInfo.DomainName.Trim();

                LicenseInfo.Key = Licensekey;
            }
            catch (ArgumentNullException ex)
            {
                throw new LicenseKeyException(ex.ToString(),ex);
            }
            catch (ArgumentException ex)
            {
                throw new LicenseKeyException(ex.ToString(),ex);
            }
            catch (FormatException ex)
            {
                throw new LicenseKeyException(ex.ToString(),ex);
            }
        }

        /// <summary>
        /// function to Validate License Key.
        /// </summary>
        /// <exception cref="MissingLicenseKeyException"> Thrown in case of empty Lincense key.</exception>
        /// <exception cref="LicenseKeyException"> Thrown in case of ArgumentNullException, ArgumentException, FormatException, or exception in Encrypt/decrypt method.</exception>
        private static LicenseConstants.LicenseValidationEnum ValidateLicense(string Key, string CurrentDomainName)
        {
            if (string.IsNullOrEmpty(Key)) throw new MissingLicenseKeyException("License Key is empty.");

            LicenseConstants.LicenseValidationEnum valEnum;

            try
            {
                string decryptedKey = string.Empty;

                Key = Key.Split(new string[] { LicenseConstants.LICENSE_KEY_GROUP_SEPARATOR }, StringSplitOptions.None)[0].ToString();



                Utilities.AESEncDecString obj = new AESEncDecString(LicenseConstants.ENCRYPTION_ALGO_KEY);
                decryptedKey = obj.Decrypt(Key);

                string moduleNames = string.Empty;
                string expirationDate = string.Empty;
                string domainName = string.Empty;

                domainName = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[1].ToString();
                domainName = domainName.Replace(LicenseConstants.DOMAIN_NAME_LABEL_IN_LICENSE_KEY, "");

                moduleNames = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[2].ToString();
                moduleNames = moduleNames.Replace(LicenseConstants.MODULE_NAME_LABEL_IN_LICENSE_KEY , "");

                expirationDate = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[3].ToString();
                expirationDate = expirationDate.Replace(LicenseConstants.EXPIRATION_DATE_LABEL_IN_LICENSE_KEY , "");


                if (domainName.Trim().Equals(string.Empty) || moduleNames.Trim().Equals(string.Empty) || expirationDate.Trim().Equals(string.Empty))
                {
                    valEnum = LicenseConstants.LicenseValidationEnum.Unknown;
                    return valEnum;
                }


                if (!expirationDate.ToLower().Equals("unlimited"))
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(expirationDate);

                        if (dt < DateTime.Now)
                        {
                            valEnum = LicenseConstants.LicenseValidationEnum.Expired;
                            return valEnum;
                        }

                    }
                    catch (FormatException ex)
                    {
                        throw new LicenseKeyException("Invalid License Expiration Date.", ex.InnerException);
                    }
                }

                //string currentHost = string.Empty;
                //currentHost = HttpContext.Current.Request.Url.Host.ToString().ToLower();

                if (CurrentDomainName.Contains("localhost:"))
                    CurrentDomainName = CurrentDomainName.Substring(0, CurrentDomainName.IndexOf(":"));

                if (!CurrentDomainName.ToLower().Equals(domainName.ToLower()))
                {
                    valEnum = LicenseConstants.LicenseValidationEnum.InvalidDomainName;
                    return valEnum;
                }


                valEnum = LicenseConstants.LicenseValidationEnum.Valid;

            }
            catch (Exception ex)
            {
                throw new LicenseKeyException(ex.ToString(),ex.InnerException);
            }
            return valEnum;
        }

        /// <summary>
        /// function to get Valid License modules.
        /// </summary>
        /// <param name="Key">License key</param>
        /// <param name="CurrentDomainName">Current Domain Name</param>
        /// <exception cref="MissingLicenseKeyException"> Thrown in case of empty Lincense key.</exception>
        /// <exception cref="ExpiredLicenseKeyException"> Thrown if  License key expired.</exception>
        /// <exception cref="InvalidDomainNameException"> Thrown if  Domain name not matching with the License key domain name.</exception>
        /// <exception cref="UnknownLicenseFormatException"> Thrown if  Invalid or Unknown License Format</exception>
        /// <exception cref="LicenseKeyException"> Thrown in case of ArgumentNullException, ArgumentException, FormatException, or exception in Encrypt/decrypt method.</exception>
        public static List<string> GetValidModules(string Key, string CurrentDomainName)
        {
            LicenseInfo licInfo = new LicenseInfo();
            licInfo = GetLicenseInfo(Key, CurrentDomainName);
            return licInfo.ModuleNames;
        }

        /// <summary>
        /// function to get License info object from Key.
        /// </summary>
        /// <param name="Key">License key</param>
        /// <param name="CurrentDomainName">Current Domain Name</param>
        /// <exception cref="ExpiredLicenseKeyException"> Thrown if  License key expired.</exception>
        /// <exception cref="InvalidDomainNameException"> Thrown if  Domain name not matching with the License key domain name.</exception>
        /// <exception cref="UnknownLicenseFormatException"> Thrown if  Invalid or Unknown License Format</exception>
        /// <exception cref="MissingLicenseKeyException"> Thrown in case of empty Lincense key.</exception>
        /// <exception cref="LicenseKeyException"> Thrown in case of ArgumentNullException, ArgumentException, FormatException, or exception in Encrypt/decrypt method.</exception>
        public static LicenseInfo GetLicenseInfo(string Key, string CurrentDomainName)
        {
            LicenseConstants.LicenseValidationEnum licEnum = ValidateLicense(Key, CurrentDomainName);
            LicenseInfo licInfo = new LicenseInfo();

            switch (licEnum)
            {
                case LicenseConstants.LicenseValidationEnum.Expired: throw new ExpiredLicenseKeyException("License Key Expired.");
                case LicenseConstants.LicenseValidationEnum.InvalidDomainName: throw new InvalidDomainNameException("Invalid Domain Name.");
                case LicenseConstants.LicenseValidationEnum.Unknown: throw new UnknownLicenseFormatException("Invalid or Unknown License Format.");
                case LicenseConstants.LicenseValidationEnum.Valid:

                    string decryptedKey = string.Empty;
                    string orgKey = string.Empty;


                    orgKey = Key;

                    Key = Key.Split(new string[] { LicenseConstants.LICENSE_KEY_GROUP_SEPARATOR }, StringSplitOptions.None)[0].ToString();

                    Utilities.AESEncDecString obj = new AESEncDecString(LicenseConstants.ENCRYPTION_ALGO_KEY);
                    decryptedKey = obj.Decrypt(Key);

                    string moduleNames = string.Empty;
                    string expirationDate = string.Empty;
                    string domainName = string.Empty;

                    domainName = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[1].ToString();
                    domainName = domainName.Replace(LicenseConstants.DOMAIN_NAME_LABEL_IN_LICENSE_KEY, "");

                    licInfo.DomainName = domainName;


                    expirationDate = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[3].ToString();
                    expirationDate = expirationDate.Replace(LicenseConstants.EXPIRATION_DATE_LABEL_IN_LICENSE_KEY, "");

                    if (!expirationDate.ToLower().Equals("unlimited"))
                    {
                        //IFormatProvider culture = new CultureInfo("en-GB", true);

                        DateTime dt = Convert.ToDateTime(expirationDate);
                        licInfo.ExpirationDate = dt;
                    }
                    else
                        licInfo.ExpirationDate = null;


                    moduleNames = decryptedKey.Split(new string[] { LicenseConstants.SECTION_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None)[2].ToString();
                    moduleNames = moduleNames.Replace(LicenseConstants.MODULE_NAME_LABEL_IN_LICENSE_KEY, "");

                    string[] validModules = moduleNames.Split(new string[] { LicenseConstants.MODULE_NAME_SEPARATOR_IN_LICENSE_KEY }, StringSplitOptions.None);

                    licInfo.ModuleNames = validModules.OfType<string>().ToList();
                    licInfo.Key = orgKey;


                    break;
            }



            return licInfo;

        }

    }
}
