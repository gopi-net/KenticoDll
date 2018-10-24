using CMS.IO;
using Emerge.WebsiteSetupHelper;
using Emerge.WebsiteSetupHelper.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Emerge.DatabaseEngine
{
    public class SQLScriptsProcessor
    {
        public event LogProgressHandler OnLogProgress;
        private string packageFileName = string.Empty;
        public string PackageFileName
        {
            get
            {
                return packageFileName;
            }
        }

        private string packageFilePath = string.Empty;
        public string PackageFilePath
        {
            get
            {
                return packageFilePath;
            }
        }

        private string targetFolder = string.Empty;
        public string TargetFolder
        {
            get
            {
                return targetFolder;
            }
        }

        private string newPackagePath = string.Empty;
        public string NewPackagePath
        {
            get
            {
                return newPackagePath;
            }
        }

        public SQLScriptsProcessor(string scriptsPath)
        {
            SetPathAndFileName(scriptsPath);
        }

        private void SetPathAndFileName(string scriptsPath)
        {
            string[] elements = scriptsPath.Split(new char[] { '\\' });
            packageFileName = elements.Last().Replace("[", "").Replace("]", "");
            packageFilePath = scriptsPath.Substring(0, scriptsPath.LastIndexOf('\\') + 1);
            targetFolder = PackageFilePath + "Temp" + "\\";
        }

        public bool ProcessScripts(List<string> websites)
        {
            LogProgress("Uncompressing the Hotifx SQL zip package...");
            if (ZIPHelper.UnCompress(PackageFilePath, PackageFileName, TargetFolder))
            {
                string hotfixFiles = DatabaseSettingsHelper.GetHotfixFiles();
                string[] files = hotfixFiles.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string file in files)
                    ProcessFile(file, websites);

                LogProgress("Compressing the Hotifx SQL zip package...");
                string newPackageFileName = getNewPackageFileName();
                newPackagePath = ZIPHelper.CompressPackage(PackageFilePath, newPackageFileName, TargetFolder);
                newPackagePath = newPackagePath.Replace(newPackageFileName, "[" + newPackageFileName + "]");
            }
            return true;
        }

        private string getNewPackageFileName()
        {
            return "EmergeHotfix_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".zip";
        }

        private void ProcessFile(string file, List<string> websites)
        {
            LogProgress(string.Format("Processing for file - {0}", file));
            List<string> fileList = LoadScriptsFromFile(DirectoryHelper.CombinePath(TargetFolder, file));
            List<string> newFileList = new List<string>();
            
            foreach (string website in websites)
            {
                LogProgress(string.Format("Processing for website - {0}", website));
                foreach (string objFile in fileList)
                {
                    string fileName = processFile(objFile, website);
                    if (!newFileList.Contains(fileName))
                        newFileList.Add(fileName);
                }
            }
            SaveFileList(newFileList, DirectoryHelper.CombinePath(TargetFolder, file));
            DeleteFiles(fileList);
        }

        private string processFile(string file, string websiteName)
        {
            LogProgress(string.Format("Processing file - {0}", file));
            string path = DirectoryHelper.CombinePath(TargetFolder, file);
            string text = GetSQLQueryText(path);
            text = GetNewValue(text, websiteName);

            string fileName = getNewFileName(file, websiteName);
            System.IO.File.WriteAllText(DirectoryHelper.CombinePath(TargetFolder, fileName), text);
            return fileName;
        }

        private string getNewFileName(string file, string websiteName)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string newfileName = fileName + "_" + websiteName;
            return file.Replace(fileName, newfileName);
        }

        private void DeleteFiles(List<string> fileList)
        {
            foreach (string file in fileList)
                System.IO.File.Delete(DirectoryHelper.CombinePath(TargetFolder, file));
        }

        private void SaveFileList(List<string> fileList, string path)
        {
            System.IO.File.Delete(path);
            using (StreamWriter str = File.CreateText(path))
            {
                foreach (string file in fileList)
                    str.WriteLine(file);
                str.Flush();
                str.Close();
            }
        }

        private string GetNewValue(string value, string newWebsiteName)
        {
            string oldWebsite = DatabaseSettingsHelper.GetOldWebsiteName();
            value = Regex.Replace(value, oldWebsite , newWebsiteName , RegexOptions.IgnoreCase);
            return value;
        }

        private string GetSQLQueryText(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(File.ReadAllText(path));
            return sb.ToString();
        }

        private List<string> LoadScriptsFromFile(string path)
        {
            List<string> result = new List<string>();
            using (StreamReader str = File.OpenText(path))
            {
                while (!str.EndOfStream)
                {
                    string line = str.ReadLine();
                    if (!String.IsNullOrEmpty(line))
                    {
                        line = line.Trim();
                        if (!String.IsNullOrEmpty(line) && !line.StartsWith("//"))
                        {
                            result.Add(line);
                        }
                    }
                }
            }
            return result;
        }


        private void LogProgress(string message)
        {
            if (OnLogProgress != null)
            {
                OnLogProgress(message);
            }
        }
        
    }
}
