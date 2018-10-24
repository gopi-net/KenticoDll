using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common.CMS.CMSHelper;
using Bluespire.Emerge.Common.CMS.GlobalHelper;
using CMS.MediaLibrary;

namespace Bluespire.Emerge.Common.CMS.MediaLibrary
{
    public class EmergeMediaFileInfoProvider
    {
        public static string GetMediaFileAbsoluteUrl(string fileGuid)
        {
            string fileURL = string.Empty;
            MediaFileInfo mediaFile = MediaFileInfoProvider.GetMediaFileInfo(EmergeValidationHelper.GetGuid(fileGuid, Guid.Empty), EmergeCMSContext.CurrentSiteName);

            if (mediaFile != null)
            {
                fileURL = MediaFileInfoProvider.GetMediaFileAbsoluteUrl(mediaFile.FileGUID, mediaFile.FileName);
            }

            return fileURL;
        }
    }
}
