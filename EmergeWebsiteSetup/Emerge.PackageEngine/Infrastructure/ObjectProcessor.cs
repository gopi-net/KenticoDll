using Emerge.WebsiteSetupHelper.Delegates;
using Emerge.PackageEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emerge.PackageEngine.Infrastructure
{
    /// <summary>
    /// Abstract class for all the objects to be processed.
    /// </summary>
    public abstract class ObjectProcessor
    {
        protected string FileName { get; set; }
        public abstract void ProcessXML();

        public event LogProgressHandler OnLogProgress;

        public virtual string GetNewValue(string value)
        {
            string oldWebsite = PackageHelper.OldSiteName;
            value = Regex.Replace(value, oldWebsite + "_", PackageHelper.NewSiteName + "_", RegexOptions.IgnoreCase);
            return value;
        }

        public void LogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }

    }
}
