using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Base;

namespace Bluespire.Emerge.CommonService.CustomMacroMethods
{
    [CommonMacroMethodLoader]
    public partial class CMSModuleLoader
    {
        /// <summary>
        /// Attribute class ensuring the registration of custom macro methods.
        /// </summary>
        private class CommonMacroMethodLoader : CMSLoaderAttribute
        {
            /// <summary>
            /// Called automatically when the application starts.
            /// </summary>
            public override void Init()
            {
                CommonCustomMacroMethods.RegisterMacroMethods();
            }
        }
    }
}
