using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergeLicenseGenerator
{
    public static class EmergeLogWriter
    {

        /// <summary>
        /// function to log Erros in case of exceptions.
        /// </summary>
        public static void LogError(Exception ex, string MethodName)
        {


            int rowsAffected = 0;

            SqlConnection conn = new SqlConnection(LicenseGeneratorConstants.CONNECTION_STRING);
            SqlCommand command = new SqlCommand(LicenseGeneratorConstants.LOG_ENTRY_SP, conn);

            try
            {

                using (conn)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.LOG_ENTRY_PARAM_MESSAGE, ex.Message);
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.LOG_ENTRY_PARAM_TITLE, MethodName);
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.LOG_ENTRY_PARAM_TIMESTAMP, DateTime.Now.Date.ToString());
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.LOG_ENTRY_PARAM_ERRORSOURCE, ex.Source);
                    command.Parameters.AddWithValue(LicenseGeneratorConstants.LOG_ENTRY_PARAM_INNEREXCEPTION, ex.InnerException);
                    conn.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
