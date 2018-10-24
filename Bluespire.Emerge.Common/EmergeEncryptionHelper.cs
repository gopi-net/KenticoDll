using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.Common.Logging;

namespace Bluespire.Emerge.Common
{
    public class EmergeEncryptionHelper
    {
        private const string INIT_VECTOR = "@1B2c3D4e5F6g7H8";
        private const string KEY_VAULE = "EmergeBluespire";

        /// <summary>
        /// Encrypts the source string.
        /// </summary>
        /// <param name="sourceString">string to be encrypted.</param>
        /// <returns></returns>
        public static string EncryptData(string sourceString)
        {
            try
            {
                AESEncDecString encryptObj = new AESEncDecString(KEY_VAULE, INIT_VECTOR);
                return encryptObj.Encrypt(sourceString);
            }
            catch(Exception ex)
            {
                EmergeLogWriter.WriteError("ENCRYPTDECRYPT", EventCode.EMERGE_ENCDEC, ex.ToString());
                throw new DataEncryptDecryptException("There has been an error while encrypting data.", ex);

            }
        }

        /// <summary>
        /// Decrypts the source string
        /// </summary>
        /// <param name="sourceString">string to be decrypted.</param>
        /// <returns></returns>
        public static string DecryptData(string sourceString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sourceString)) return string.Empty;
 
                AESEncDecString encryptObj = new AESEncDecString(KEY_VAULE, INIT_VECTOR);
                return encryptObj.Decrypt(sourceString);
            }
            catch (Exception ex)
            {
                EmergeLogWriter.WriteError("ENCRYPTDECRYPT", EventCode.EMERGE_ENCDEC, ex.ToString());
                throw new DataEncryptDecryptException("There has been an error while decrypting data.", ex);
            }
        }
    }
}
