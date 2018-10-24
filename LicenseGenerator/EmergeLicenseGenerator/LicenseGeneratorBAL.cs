using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.LicenseManager;
using EmergeLicenseGenerator.Exceptions;

namespace EmergeLicenseGenerator
{
    /// <summary>
    /// Class declared to handle business layer functinalities for License Generator application.
    /// </summary>

    class LicenseGeneratorBAL
    {
        public static LicenseGeneratorConstants.OperationStatusEnum GenerateLicense(LicenseInfo LicenceInfo)
        {
            LicenseGeneratorConstants.OperationStatusEnum status = LicenseGeneratorConstants.OperationStatusEnum.Failure;
            LicenseBAL.CreateLicenseKey(LicenceInfo);
            status = INSERTLicense(LicenceInfo);

            return status;
        }


        /// <summary>
        /// function to insert License in database.
        /// </summary>
        private static LicenseGeneratorConstants.OperationStatusEnum INSERTLicense(LicenseInfo LicenceInfo)
        {
            return LicenseGeneratorDAL.INSERTLicense(LicenceInfo);
        }

        /// <summary>
        /// function to Get latest license by Domain Name.
        /// </summary>
        public static LicenseInfo GetLicenseByDomainName(string DomainName)
        {
            LicenseInfo licInfo = new LicenseInfo();
            licInfo.Key = string.Empty;

            DataTable dtLicenseInfo = new DataTable();

            dtLicenseInfo = LicenseGeneratorDAL.GetLicenseByDomainName(DomainName);

            if (dtLicenseInfo.Rows.Count == 0)
                throw new NoLicenseFoundException("License Not Found in Database.");

            if (dtLicenseInfo.Rows.Count > 0)
            {
                licInfo = LicenseBAL.GetLicenseInfo(dtLicenseInfo.Rows[0][LicenseGeneratorConstants.LICENSE_KEY_COLUMN_NAME].ToString(),DomainName);

            }

            return licInfo;
        }




    }
}
