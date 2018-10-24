using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.CommonService.License;
using CMS.MacroEngine;
using CMS.Helpers;
using CMS.Base;

namespace Bluespire.Emerge.CommonService.CustomMacroMethods
{
    public static class CommonCustomMacroMethods
    {
        public static void RegisterMacroMethods()
        {
            MacroMethod getDomainWithPort = new MacroMethod("GetDomainWithPort", GetDomainWithPort)
            {
                Comment = ResHelper.GetString(Constants.STRINGCODE_COMMENT_GETDOMAINWITHPORT),
                Type = typeof(string),
                AllowedTypes = new List<Type>() { },
                MinimumParameters = 0

            };
            MacroMethods.RegisterMethod(getDomainWithPort);

            MacroMethod isModuleLicensedMethod = new MacroMethod("IsModuleLicensed", IsModuleLicensed)
            {
                Comment = ResHelper.GetString(Constants.STRINGCODE_COMMENT_ISMODULELICENSED),
                Type = typeof(bool),
                AllowedTypes = new List<Type>() { typeof(string) },
                MinimumParameters = 1

            };
            MacroMethods.RegisterMethod(isModuleLicensedMethod);
        }

        public static object IsModuleLicensed(params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 1:
                    return LicenseProvider.ValidateLicense(ValidationHelper.GetString(parameters[0], string.Empty));
                default:
                    throw new NotSupportedException();
            }
        }

        public static object GetDomainWithPort(params object[] parameters)
        {
            switch (parameters.Length)
            {
                case 0:
                    return URLHelper.GetDomain(RequestContext.URL.ToString()) + SystemContext.ApplicationPath.Trim();
                default:
                    throw new NotSupportedException();
            }
        }

    }
}
