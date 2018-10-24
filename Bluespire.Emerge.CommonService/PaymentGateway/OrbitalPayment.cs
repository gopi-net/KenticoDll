using Bluespire.Emerge.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.CommonService.PaymentGateway
{
    /// <summary>
    /// Accept Orbital payment details
    /// </summary>
    public class OrbitalPayment : Payment
    {
        public string MerchantId { get; set; }
        public CurrencyCode currencyCode { get; set; }


        #region [Public Functions]
        /// <summary>
        /// Validate Orbital required fields
        /// </summary>
        public override void Validate()
        {
            
            StringBuilder errorColumn= new StringBuilder();

            if (string.IsNullOrEmpty(MerchantId)) { errorColumn.Append("MerchantId, "); }
            if (string.IsNullOrEmpty(currencyCode.ToString())) { errorColumn.Append("CurrencyCode, "); }
            base.result = errorColumn;
            base.Validate();
        }
        #endregion
    }
}
