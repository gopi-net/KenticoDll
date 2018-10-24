using Bluespire.Emerge.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.PaymentGateway
{
    /// <summary>
    /// Accept Authorize.NET payment details
    /// </summary>
    public class AuthorizeNetPayment : Payment
    {
        #region [Properties]
        /// <summary>
        /// Authorize .net Required Data
        /// </summary>
        [Description("Authorize.net login id.Retrive value from authrize.net account. ")]
        public string LoginID { get; set; }
        [Description("Retrive value from authrize.net account.")]
        public string TransactionKey { get; set; }
        #endregion


        #region [Public Functions]
        /// <summary>
        /// Validate Authorize.NET required field
        /// </summary>
        public override void Validate()
        {
            
            StringBuilder result = new StringBuilder();

            if (string.IsNullOrEmpty(LoginID)) { result.Append("LoginID, "); }
            if (string.IsNullOrEmpty(TransactionKey)) { result.Append("TransactionKey, "); }
            base.result = result;
            base.Validate();
            
        }
        #endregion


    }
}
