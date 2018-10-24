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
using EmergeWebsiteHotFixApp.Resources;
using Emerge.WebsiteSetupManager;
using System.ComponentModel;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace EmergeWebsiteHotFixApp
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
                WebsitePathTextBox.Text = folderDialog.SelectedPath;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InstallHotfix();
        }

        private void InstallHotfix()
        {
            try
            {
                WebsiteDetailsPanel.IsEnabled = StartButton.IsEnabled = false;
                if (ValidateInputs())
                {
                    LogProgress(Messages.StartMessage);
                    WebsiteProcessor processor = new WebsiteProcessor(buildSettingsObject());
                    processor.OnLogProgress += LogProgress;
                    processor.InstallHotfix();
                    onOperationSuccess();
                }
            }
            catch (Exception ex)
            {
                LogProgress(ex.ToString());
                MessageBox.Show(Messages.OperationNotSuccessful);
            }
            WebsiteDetailsPanel.IsEnabled = StartButton.IsEnabled = true;
        }

        private void LogProgress(string message)
        {
            ProgressText.Text = Environment.NewLine + ProgressText.Text;
            ProgressText.Text = message + ProgressText.Text;
            System.Windows.Forms.Application.DoEvents();
        }

        private void onOperationSuccess()
        {
            MessageBoxResult result = MessageBox.Show(Messages.OperationSuccessful, "Success", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
                saveDialog.FileName = "WebsiteHotfixLog.txt";
                System.Windows.Forms.DialogResult dresult = saveDialog.ShowDialog();
                if (dresult == System.Windows.Forms.DialogResult.OK)
                    File.WriteAllText(saveDialog.FileName, ProgressText.Text);
            }
            //this.Close();
        }

        private WebsiteSettings buildSettingsObject()
        {
            return WebsiteHelper.BuildWebsiteSettingsFromConfig(WebsitePathTextBox.Text.Trim());
        }

        private bool ValidateInputs()
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(WebsitePathTextBox.Text.Trim()))
            {
                MessageBox.Show(Messages.WebsitePathValidation);
                return false;
            }
            return isValid;
        }
    }
}
