using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bluespire.Emerge.LicenseManager;
using EmergeLicenseGenerator.Exceptions;
using Bluespire.Emerge.LicenseManager.Exceptions;

namespace EmergeLicenseGenerator
{
    public partial class LicenseGenerator : Form
    {
        public LicenseGenerator()
        {
            InitializeComponent();
            SetupFormFields();
        }

        private void SetupFormFields()
        {
            labelError.Text = string.Empty;
            labelMessage.Text = string.Empty;
            
            LabelLicensekey.Visible = false;
            TextboxKey.Visible = false;

            CheckedBoxListModuleNames.Items.Clear();
            foreach (LicenseConstants.ModuleNamesEnum modulename in Enum.GetValues(typeof(LicenseConstants.ModuleNamesEnum)))
            {
                if (IsModulePurchasable(modulename))
                    CheckedBoxListModuleNames.Items.Add(modulename.ToString());
            }
        }

        private bool IsModulePurchasable(LicenseConstants.ModuleNamesEnum modulename)
        {
            if ((modulename == LicenseConstants.ModuleNamesEnum.License) || (modulename == LicenseConstants.ModuleNamesEnum.HistoryTracker) || (modulename == LicenseConstants.ModuleNamesEnum.Maintenance))
                return false;
            return true;
        }

        private void cmdGenerate_Click(object sender, EventArgs e)
        {
            ResetErrorMessages();

            if (IsValidInput())
            {

                LicenseInfo LicenceInfo = new LicenseInfo();

                string domainName = TextboxDomainName.Text.Trim();
                domainName = domainName.Replace("http://", "");
                //domainName = domainName.Replace("www.", "");

                LicenceInfo.DomainName = domainName;


                CheckedListBox.CheckedIndexCollection checkedModuleIndeces;

                checkedModuleIndeces = CheckedBoxListModuleNames.CheckedIndices;

                List<string> lstSelectedModuleNames = new List<string>();



                foreach (int i in checkedModuleIndeces)
                {
                    lstSelectedModuleNames.Add(CheckedBoxListModuleNames.Items[i].ToString());
                }



                LicenceInfo.ModuleNames = lstSelectedModuleNames;


                if (!CboExpirationpolicy.SelectedItem.ToString().ToLower().Equals("unlimited"))
                    LicenceInfo.ExpirationDate = dateTimeExpiredon.Value;


                try
                {
                    if (LicenseGeneratorBAL.GenerateLicense(LicenceInfo) == LicenseGeneratorConstants.OperationStatusEnum.Failure)
                        labelError.Text = "Error in EmergeLicenseBAL.GenerateLicense. Please see log for more details.";
                    else
                    {
                        labelMessage.Text = "License Generated Successfully.";
                        LabelLicensekey.Visible = true;
                        TextboxKey.Visible = true;
                        TextboxKey.Text = LicenceInfo.Key;
                    }

                }

                catch (NullReferenceException ex)
                {
                    EmergeLogWriter.LogError(ex, "EmergeLicenseBAL.GenerateLicense");
                    labelError.Text = "Error in EmergeLicenseBAL.CreateLicenseKey. Please see log for more details.";
                }

                catch (LicenseKeyException ex)
                {
                    EmergeLogWriter.LogError(ex, "EmergeLicenseBAL.GenerateLicense");
                    labelError.Text = "Error in EmergeLicenseBAL.CreateLicenseKey. Please see log for more details.";
                }
                catch (LicenseGeneratorSqlException)
                {
                    labelError.Text = "Error in EmergeLicenseBAL.CreateLicenseKey. Please see log for more details";
                }


            }
        }

        private bool IsValidInput()
        {
            bool isValid = true;

            if (TextboxDomainName.Text.Trim().Equals(string.Empty))
            {
                LGErrorProvider.SetError(TextboxDomainName, "Please Enter Domain name.");
                isValid = false;
            }

            if (CheckedBoxListModuleNames.CheckedItems.Count == 0)
            {
                LGErrorProvider.SetError(CheckedBoxListModuleNames, "Please Select Module(s).");
                isValid = false;
            }

            if (CboExpirationpolicy.SelectedIndex == (int)LicenseGeneratorConstants.ExpirationPolicyEnum.NO_EXPIRATION_POLICY)
            {
                LGErrorProvider.SetError(CboExpirationpolicy, "Please Select Expiration Policy.");
                isValid = false;
            }

            if (CboExpirationpolicy.SelectedIndex == (int)LicenseGeneratorConstants.ExpirationPolicyEnum.FIX_TIME_EXPIRATION_POLICY)
            {
                if (dateTimeExpiredon.Value <= DateTime.Now)
                {
                    LGErrorProvider.SetError(dateTimeExpiredon, "Expired on date must be greater than today's date.");
                    isValid = false;
                }
            }

            return isValid;
        }

        protected void CboExpirationpolicy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CboExpirationpolicy.SelectedIndex == (int)LicenseGeneratorConstants.ExpirationPolicyEnum.FIX_TIME_EXPIRATION_POLICY)
            {
                LabelExpiredOn.Visible = true;
                dateTimeExpiredon.Visible = true;
            }
            else
            {
                LabelExpiredOn.Visible = false;
                dateTimeExpiredon.Visible = false;
            }

            
        }


        private void cmdGetLicenseDetails_Click(object sender, EventArgs e)
        {
            ResetErrorMessages();

            if (TextboxDomainName.Text.Trim().Equals(string.Empty))
            {
                LGErrorProvider.SetError(TextboxDomainName, "Please Enter Domain name.");
                return;
            }

            try
            {
                LicenseInfo licInfo = LicenseGeneratorBAL.GetLicenseByDomainName(TextboxDomainName.Text.Trim());

                CheckedBoxListModuleNames.ClearSelected();

                TextboxDomainName.Text = licInfo.DomainName;

                for (int rowCounter = 0; rowCounter < CheckedBoxListModuleNames.Items.Count; rowCounter++)
                {
                    CheckedBoxListModuleNames.SetItemChecked(rowCounter, false);
                }

                foreach (string moduleName in licInfo.ModuleNames)
                {
                    if (-1 != CheckedBoxListModuleNames.Items.IndexOf(moduleName))
                        CheckedBoxListModuleNames.SetItemChecked(CheckedBoxListModuleNames.Items.IndexOf(moduleName), true);
                }


                if (licInfo.ExpirationDate.HasValue)
                {
                    dateTimeExpiredon.Value = licInfo.ExpirationDate.Value;
                    CboExpirationpolicy.SelectedIndex = 1;
                }
                else
                    CboExpirationpolicy.SelectedIndex = 0;

                LabelLicensekey.Visible = true;
                TextboxKey.Visible = true;
                TextboxKey.Text = licInfo.Key;

            }
            catch (ExpiredLicenseKeyException)
            {
                labelError.Text = "License Expired for Domain Name: " + TextboxDomainName.Text.Trim();
            }
            catch (NoLicenseFoundException)
            {
                labelError.Text = "No License Available for Domain Name: " + TextboxDomainName.Text.Trim();
            }
            catch (LicenseGeneratorSqlException ex)
            {
                EmergeLogWriter.LogError(ex, "cmdGetModules_Click");
                labelError.Text = "Error in EmergeLicenseBAL.CreateLicenseKey. Please see log for more details.";
            }
            catch (LicenseKeyException ex)
            {
                EmergeLogWriter.LogError(ex, "cmdGetModules_Click");
                labelError.Text = "Error in EmergeLicenseBAL.CreateLicenseKey. Please see log for more details.";
            }


        }

        private void ResetErrorMessages()
        {
            LGErrorProvider.Clear();
            labelError.Text = string.Empty;
            labelMessage.Text = string.Empty;
        }

    }
}
