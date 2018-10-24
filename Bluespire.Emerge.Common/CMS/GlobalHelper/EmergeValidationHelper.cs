using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Helpers;
using CMS.DataEngine;

namespace Bluespire.Emerge.Common.CMS.GlobalHelper
{
    public static class EmergeValidationHelper
    {
        public static int GetMaxIntLength()
        {
            return Constants.KENTICO_MAX_INT_LENGTH;
        }

        public static int GetMaxLongIntLength()
        {
            return Constants.KENTICO_MAX_LONGINT_LENGTH;
        }

        public static string GetString(object value, string defaultValue)
        {
            return ValidationHelper.GetString(value, defaultValue);
        }
        
        public static string GetString(object value, string defaultValue, string culture)
        {
            return ValidationHelper.GetString(value, defaultValue,culture);
        }
        
        public static string GetString(object value, string defaultValue, string culture, string format)
        {
            return ValidationHelper.GetString(value, defaultValue, culture, format);
        }

        public static int GetInteger(object value, int defaultValue)
        {
            return ValidationHelper.GetInteger(value, defaultValue);
        }

        public static bool GetBoolean(object value, bool defaultValue)
        {
            return ValidationHelper.GetBoolean(value, defaultValue);
        }

        public static double GetDouble(object value, double defaultValue)
        {
            return ValidationHelper.GetDouble(value, defaultValue);
        }

        public static Guid GetGuid(object value, Guid defaultValue)
        {
            return ValidationHelper.GetGuid(value, defaultValue);
        }

    }
}
