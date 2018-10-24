using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.LicenseManager;
using EmergeLicenseGenerator.Exceptions;
namespace EmergeLicenseGenerator
{
    /// <summary>
    /// Class declared to handle Data access functinalities.
    /// </summary>
    
    class LicenseGeneratorDAL
    {


        /// <summary>
        /// function to Handle Insert License.
        /// </summary>

        public static LicenseGeneratorConstants.OperationStatusEnum INSERTLicense(LicenseInfo licenseInfo)
        {
            int result = 0;

            LicenseGeneratorConstants.OperationStatusEnum oprStatus;

            try
            {
                using (SqlConnection connection = new SqlConnection(LicenseGeneratorConstants.CONNECTION_STRING))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(LicenseGeneratorConstants.INSERT_LICENSE_SP, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.INSERT_LICENSE_PARAM_DOMAINNAME, licenseInfo.DomainName);

                    string allModules = string.Empty;

                    foreach (string moduleName in licenseInfo.ModuleNames)
                    {
                        allModules += moduleName + LicenseConstants.MODULE_NAME_SEPARATOR_IN_LICENSE_KEY;
                    }

                    allModules = allModules.Substring(0, allModules.LastIndexOf(LicenseConstants.MODULE_NAME_SEPARATOR_IN_LICENSE_KEY));

                    command.Parameters.AddWithValue(LicenseGeneratorConstants.INSERT_LICENSE_PARAM_MODULENAMES, allModules);
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.INSERT_LICENSE_PARAM_LICENSEKEY, licenseInfo.Key);
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.INSERT_LICENSE_PARAM_DATEEXPIRATION, licenseInfo.ExpirationDate);

                    SqlParameter sqlParam = new SqlParameter();
                    sqlParam.Direction = ParameterDirection.Output;
                    sqlParam.DbType = DbType.Int32;
                    sqlParam.Value = 0;
                    sqlParam.ParameterName = LicenseGeneratorConstants.INSERT_LICENSE_PARAM_LICENSEID;

                    command.Parameters.Add(sqlParam);


                    command.ExecuteNonQuery();
                    result = Int32.Parse(command.Parameters[LicenseGeneratorConstants.INSERT_LICENSE_PARAM_LICENSEID].Value.ToString());

                    oprStatus = result == 0 ? LicenseGeneratorConstants.OperationStatusEnum.Failure : LicenseGeneratorConstants.OperationStatusEnum.Success;
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {

                oprStatus = LicenseGeneratorConstants.OperationStatusEnum.Failure;
                EmergeLogWriter.LogError(ex, "INSERTLicense");
                throw new LicenseGeneratorSqlException(ex.ToString()); ;
            }
            return oprStatus;
        }

        /// <summary>
        /// function to read License of a domain name from Database.
        /// </summary>
       
        public static DataTable GetLicenseByDomainName(string DomainName)
        {

            DataTable dtLicense = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(LicenseGeneratorConstants.CONNECTION_STRING))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(LicenseGeneratorConstants.GET_LICENSE_BY_DOMAINNAME_SP, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.GET_LICENSE_BY_DOMAINNAM_PARAM_DOMAINNAME, DomainName);

                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(dtLicense);
                    
                }


            }
            catch (SqlException ex)
            {
                EmergeLogWriter.LogError(ex, "GetLicenseByDomainName");
                throw new LicenseGeneratorSqlException(ex.ToString()); ;
            }

            return dtLicense;
        }

    }
}
