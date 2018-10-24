using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emerge.WebsiteSetupManager;
using System.Threading;
using EmergeWebsiteSetupApp.Resources;

namespace EmergeImportApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            

        }

        private void SelectPackageButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenPackageDialog.ShowDialog();
                PackagePathTextBox.Text = OpenPackageDialog.FileName;
                if (!String.IsNullOrEmpty(OpenPackageDialog.FileName))
                    ProcessImportButton.Enabled = true;
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
            }
        }

        private void WebsitePathBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog.ShowDialog();
                WebsitePathTextBox.Text = FolderBrowserDialog.SelectedPath;
                if (!String.IsNullOrEmpty(FolderBrowserDialog.SelectedPath))
                    ProcessImportButton.Enabled = true;
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
            }
        }

        private void ProcessImportButton_Click(object sender, EventArgs e)
        {
            process();
        }

        public void process()
        {
            try
            {
                WebsiteGroupBox.Enabled = DatabaseGroupBox.Enabled = false;
                if (ValidateInputs())
                {
                    LogProgress(Messages.StartMessage);
                    WebsiteProcessor processor = new WebsiteProcessor(buildSettingsObject());
                    processor.OnLogProgress += LogProgress;
                    processor.StartProcess();
                    MessageBox.Show(Messages.OperationSuccessful);
                }
                else
                    WebsiteGroupBox.Enabled = DatabaseGroupBox.Enabled = true;
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
                MessageBox.Show(Messages.OperationNotSuccessful);
                WebsiteGroupBox.Enabled = DatabaseGroupBox.Enabled = true;
            }
        }

        private void LogProgress(string message)
        {
            ProgressBox.Text = Environment.NewLine + ProgressBox.Text;
            ProgressBox.Text = message + ProgressBox.Text;
            ProgressBox.Refresh();
        }

        private WebsiteSettings buildSettingsObject()
        {
            WebsiteSettings settings = new WebsiteSettings();
            settings.DatabaseName = DatabaseNameTextBox.Text.Trim();
            settings.DatabaseServerName = DBServerName.Text.Trim();
            settings.PackagePath = PackagePathTextBox.Text.Trim();
            settings.NewPackagePath = settings.PackagePath;
            settings.Password = PasswordTextBox.Text.Trim();
            settings.UserName = UserNameTextBox.Text.Trim();
            settings.WebsiteCodeName = WebSiteNameTextBox.Text.Trim();
            settings.WebsiteDisplayName = WebsiteDisplayNameTextBox.Text.Trim();
            settings.WebsiteDomain = DomainTextBox.Text.Trim();
            settings.WebsitePath = WebsitePathTextBox.Text.Trim();
            return settings;
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(WebSiteNameTextBox.Text.Trim()) || WebSiteNameTextBox.Text.Trim().Contains(' '))
            {
               MessageBox.Show(Messages.WebsiteNameInputValidation);
                return false;
            }

            if (String.IsNullOrEmpty(DatabaseNameTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.DatabaseNameValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(DBServerName.Text.Trim()))
            {
                MessageBox.Show(Messages.DatabaseServerNameValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(PackagePathTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.PackagePathValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(PasswordTextBox.Text.Trim()) || PasswordTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.PasswordValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(UserNameTextBox.Text.Trim()) || UserNameTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.UserNamevalidation);
                return  false;
            }

            if (String.IsNullOrEmpty(WebsiteDisplayNameTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.WebsiteDisplayNameValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(DomainTextBox.Text.Trim()) || DomainTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.DomainNameValidation);
                return  false;
            }

            if (String.IsNullOrEmpty(WebsitePathTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.WebsitePathValidation);
                return  false;
            }
            return isValid;
        }

        

        
    }
}
