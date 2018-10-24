using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using CultureInfo = System.Globalization.CultureInfo;
using ZipEntry = ICSharpCode.SharpZipLib.Zip.ZipEntry;

namespace Emerge.WebsiteSetupHelper
{

    /// <summary>
    /// Helper class for zip
    /// </summary>
    public static class ZIPHelper
    {
        /// <summary>
        /// Method to uncompress the files
        /// </summary>
        /// <returns>true if successful</returns>
        public static bool UnCompress(string packageFilePath, string packageFileName, string targetFolder)
        {
            bool isSuccess = false;
            FileStream fileStream = null;
            ZipInputStream s = null;

            try
            {
                // Create output stream
                fileStream = File.OpenRead(packageFilePath + packageFileName);
                s = new ZipInputStream(fileStream);

                ZipEntry theEntry = null;

                EnsureDiskPathInternal(targetFolder, packageFilePath);
                // For all the entries
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    // Ensure disk path

                    EnsureDiskPathInternal(targetFolder + directoryName.TrimStart(new char[] { '\\' }) + "\\", packageFilePath);
                    // Directory.CreateDirectory(targetFolder);

                    if (fileName != "")
                    {
                        // Create stream for the file
                        FileStream streamWriter = File.Create(targetFolder + theEntry.Name);

                        int size = 1024 * 1024;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
                isSuccess = true;
            }
            finally
            {
                // Close the streams
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }

                if (s != null)
                {
                    s.Close();
                    s.Dispose();
                }
            }
            if (!isSuccess)
                throw new Exception("Resources.Messages.UncompressFailed");
            return isSuccess;
        }

        /// <summary>
        /// Compresses and creates new package
        /// </summary>
        public static string CompressPackage(string packageFilePath, string newPackageName, string targetFolder)
        {
            ZipOutputStream outStream = null;
            try
            {
                Crc32 crc = new Crc32();
                string newPackageFilePath = packageFilePath + newPackageName;
                FileStream fs = File.Create(newPackageFilePath);
                outStream = new ZipOutputStream(fs);
                outStream.SetLevel(6);
                CompressFolder(outStream, targetFolder, targetFolder, crc);
                //PackageHelper.NewPackageFilePath = newPackageFilePath;
                return newPackageFilePath;
            }
            finally
            {
                if (outStream != null)
                {
                    outStream.Finish();
                    outStream.Close();
                    try
                    {
                        Directory.Delete(targetFolder, true);
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Compress specified folder.
        /// </summary>
        /// <param name="settings">Export settings</param>
        /// <param name="outStream">Output stream</param>
        /// <param name="path">Destination path</param>
        /// <param name="tmpPath">Temporary files path</param>
        /// <param name="crc">CRC</param>
        private static void CompressFolder(ZipOutputStream outStream, string path, string tmpPath, Crc32 crc)
        {

            foreach (string dir in Directory.GetDirectories(path))
            {
                CompressFolder(outStream, dir, tmpPath, crc);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                // Add file
                CompressEntry(outStream, file, tmpPath, crc);
            }
        }


        /// <summary>
        /// Compress entry of the ZIP export package file.
        /// </summary>
        /// <param name="outStream">Output stream</param>
        /// <param name="entryPath">Path of the entry</param>
        /// <param name="tmpPath">Temporary files path</param>
        /// <param name="crc">CRC</param>
        private static void CompressEntry(ZipOutputStream outStream, string entryPath, string tmpPath, Crc32 crc)
        {
            FileStream fs = File.OpenRead(entryPath);

            // Create new entry
            string entryName = entryPath.Substring(tmpPath.Length).Replace('\\', '/');
            ZipEntry entry = new ZipEntry(entryName);

            crc.Reset();

            entry.DateTime = DateTime.Now;
            entry.Size = fs.Length;

            int bytesLeft = (int)fs.Length;
            int bufferSize = 1024 * 1024;

            byte[] buffer = new byte[bufferSize];

            outStream.PutNextEntry(entry);

            while (bytesLeft > 0)
            {
                int length = bytesLeft;
                if (length > bufferSize)
                {
                    length = bufferSize;
                }

                // Read next block
                fs.Read(buffer, 0, length);

                crc.Update(buffer, 0, length);
                outStream.Write(buffer, 0, length);

                bytesLeft -= length;
            }

            entry.Crc = crc.Value;

            // Close the file
            fs.Close();
            fs.Dispose();
        }

        
        /// <summary>
        /// Creates the directories for the new path
        /// </summary>
        /// <param name="path">path whose directories are to be created</param>
        /// <param name="startingPath">Starting path to create directories</param>
        public static void EnsureDiskPathInternal(string path, string startingPath)
        {
            string folderPath = null;
            int folderIndex = 0;
            string currentPath = null;
            string[] pathArray = null;
            int startingIndex = 0;

            // Prepare the starting path
            if (startingPath == null)
            {
                startingPath = "";
            }
            if (startingPath.EndsWith("\\"))
            {
                startingPath = startingPath.Substring(0, startingPath.Length - 1);
            }
            // If path outside of the application folder, ignore the starting path
            if (!path.StartsWith(startingPath, StringComparison.CurrentCultureIgnoreCase))
            {
                startingPath = "";
            }

            bool networkDirectory = path.StartsWith("\\\\", StringComparison.CurrentCultureIgnoreCase);

            // Remove file name from the path
            folderPath = path.Substring(0, path.LastIndexOf(@"\"));
            pathArray = folderPath.Split('\\');
            currentPath = pathArray[0];

            // If starting path available, get starting index
            if ((startingPath != "") && folderPath.ToLower().Trim().StartsWith(startingPath.ToLower().Trim()))
            {
                startingIndex = startingPath.Split('\\').GetUpperBound(0);
            }

            for (folderIndex = 1; folderIndex <= pathArray.GetUpperBound(0); folderIndex++)
            {
                currentPath += @"\" + pathArray[folderIndex];
                if ((startingIndex < folderIndex) && (!networkDirectory || (folderIndex > 2)))
                {
                    if (!Directory.Exists(currentPath))
                    {
                        Directory.CreateDirectory(currentPath);
                    }
                }
            }
        }
    }
}
