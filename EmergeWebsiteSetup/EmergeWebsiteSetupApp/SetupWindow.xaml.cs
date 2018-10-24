using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EmergeWebsiteSetupApp.Resources;
using Emerge.WebsiteSetupManager;
using System.ComponentModel;
using System.IO;

namespace EmergeWebsiteSetupApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void WebsitePathButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                WebsitePathTextBox.Text = folderDialog.SelectedPath;
                setDatabaseDetails(folderDialog.SelectedPath);
            }
        }
        private void setDatabaseDetails(string websitePath)
        {
            try
            {
                WebsiteSettings settings = WebsiteHelper.BuildWebsiteSettingsFromConfig(websitePath);
                DatabaseNameTextBox.Text = settings.DatabaseName;
                ServerNameTextBox.Text = settings.DatabaseServerName;
                UserNameTextBox.Text = settings.UserName;
                PasswordTextBox.Password = settings.Password;
                DatabaseDetailsPanel.IsEnabled = String.IsNullOrEmpty(DatabaseNameTextBox.Text) || String.IsNullOrEmpty(ServerNameTextBox.Text)
                    || String.IsNullOrEmpty(UserNameTextBox.Text) || String.IsNullOrEmpty(PasswordTextBox.Password);
            }
            catch
            {
                MessageBox.Show(Messages.WebsitePathValidation);
                WebsitePathTextBox.Text = string.Empty;
            }
        }
        private void PackagePathButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Title = "Select Package";
            bool? result = openDialog.ShowDialog();
            if (result == true)
                PackagePathTextBox.Text = openDialog.FileName;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            process();
        }

        public void process()
        {
            try
            {
                WebsiteDetailsPanel.IsEnabled = DatabaseDetailsPanel.IsEnabled = StartButton.IsEnabled = false;
                if (ValidateInputs())
                {
                    LogProgress(Messages.StartMessage);
                    WebsiteProcessor processor = new WebsiteProcessor(buildSettingsObject());
                    processor.OnLogProgress += LogProgress;
                    processor.StartProcess();
                    onOperationSuccess();
                }
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
                MessageBox.Show(Messages.OperationNotSuccessful);
            }
            WebsiteDetailsPanel.IsEnabled = DatabaseDetailsPanel.IsEnabled = StartButton.IsEnabled = true;
        }

        private void onOperationSuccess()
        {
            MessageBoxResult result = MessageBox.Show(Messages.OperationSuccessful, "Success", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
                saveDialog.FileName = "WebsiteSetupLog.txt";
                System.Windows.Forms.DialogResult dresult = saveDialog.ShowDialog();
                if (dresult == System.Windows.Forms.DialogResult.OK)
                    File.WriteAllText(saveDialog.FileName, ProgressText.Text);
            }
            //this.Close();
        }

        private void LogProgress(string message)
        {
            ProgressText.Text = Environment.NewLine + ProgressText.Text;
            ProgressText.Text = message + ProgressText.Text;
            System.Windows.Forms.Application.DoEvents();
        }

        private WebsiteSettings buildSettingsObject()
        {
            WebsiteSettings settings = new WebsiteSettings();
            settings.DatabaseName = DatabaseNameTextBox.Text.Trim();
            settings.DatabaseServerName = ServerNameTextBox.Text.Trim();
            settings.PackagePath = PackagePathTextBox.Text.Trim();
            settings.NewPackagePath = settings.PackagePath;
            settings.Password = PasswordTextBox.Password.Trim();
            settings.UserName = UserNameTextBox.Text.Trim();
            settings.WebsiteCodeName = SiteCodeNameTextBox.Text.Trim();
            settings.WebsiteDisplayName = SiteDisplayNameTextBox.Text.Trim();
            settings.WebsiteDomain = SiteDomainTextBox.Text.Trim();
            settings.WebsitePath = WebsitePathTextBox.Text.Trim();
            return settings;
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(SiteCodeNameTextBox.Text.Trim()) || SiteCodeNameTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.WebsiteNameInputValidation);
                return false;
            }

            if (String.IsNullOrEmpty(DatabaseNameTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.DatabaseNameValidation);
                return false;
            }

            if (String.IsNullOrEmpty(ServerNameTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.DatabaseServerNameValidation);
                return false;
            }

            if (String.IsNullOrEmpty(PackagePathTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.PackagePathValidation);
                return false;
            }

            if (String.IsNullOrEmpty(PasswordTextBox.Password.Trim()) || PasswordTextBox.Password.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.PasswordValidation);
                return false;
            }

            if (String.IsNullOrEmpty(UserNameTextBox.Text.Trim()) || UserNameTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.UserNamevalidation);
                return false;
            }

            if (String.IsNullOrEmpty(SiteDisplayNameTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.WebsiteDisplayNameValidation);
                return false;
            }

            if (String.IsNullOrEmpty(SiteDomainTextBox.Text.Trim()) || SiteDomainTextBox.Text.Trim().Contains(' '))
            {
                MessageBox.Show(Messages.DomainNameValidation);
                return false;
            }

            if (String.IsNullOrEmpty(WebsitePathTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.WebsitePathValidation);
                return false;
            }
            return isValid;
        }
    }
}
