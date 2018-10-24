using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.MediaLibrary;
using CMS.Helpers;
namespace Bluespire.Emerge.Common.CMS.MediaLibrary
{
    public class EmergeMediaLibraryHelper
    {
        public static string GetMediaFileUrl(string fileGuid, string siteName)
        {
            return MediaLibraryHelper.GetMediaFileUrl(ValidationHelper.GetGuid(fileGuid, Guid.Empty), siteName);
        }
    }
}
