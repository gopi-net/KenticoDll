using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.GiftShop.DL;

namespace Bluespire.Emerge.Components.GiftShop.BL
{
    public static class TaxCalculator
    {

        public static double GetTaxAmount(double amount)
        {
            return (amount * TaxCalculatorDAL.GetTaxPercentage() / 100 );
        }

        public static double GetTaxPercentage()
        { return TaxCalculatorDAL.GetTaxPercentage(); }
        


    }
}
