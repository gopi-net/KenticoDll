using System;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Common.Exceptions;
using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.LicenseManager.Exceptions;
using System.Web.UI.WebControls;
using System.Net.Mail;
using CMS.SiteProvider;
using System.Web.UI;
using System.Linq;
using System.Collections.Generic;
using Bluespire.Emerge.Web.Controls;
using CMS.PortalEngine.Web.UI;
using CMS.DocumentEngine.Web.UI;

namespace Bluespire.Emerge.Web.WebParts.BaseWebParts
{
    /// <summary>
    /// Base Class for all the webparts to be created in Emerge 2.0
    /// </summary>
    public class EmergeBaseWebPart : CMSAbstractWebPart
    {
        /// <summary>
        /// Gets or sets the name of the Module
        /// </summary>
        protected Constants.Modules Module
        {
            get;
            set;
        }

        /// <summary>
        /// Panel Containing form fields
        /// </summary>
        protected Panel ControlPanel { get; set; }

        /// <summary>
        /// Datastore for Form Parameters.
        /// </summary>
        Dictionary<string, object> _FormParameters;

        /// <summary>
        /// Gets or sets Form Parameters.
        /// </summary>
        protected Dictionary<string, object> FormParameters
        {
            get { return _FormParameters; }
            set { _FormParameters = value; }
        }

        protected Constants.Environments Environment
        {
            get;
            set;
        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                ConfigurationHelper.CheckWebsiteConfiguration(Module, string.Empty);
            }
            catch (SystemInMaintenanceModeException systemInMaintenance)
            {
                OnError(systemInMaintenance);
            }
            catch (ModuleInMaintenanceModeException moduleInMaintenance)
            {
                OnError(moduleInMaintenance);
            }
            catch (ModuleNotPurchasedException moduleNotPurchased)
            {
                OnError(moduleNotPurchased);
            }
            catch (MissingLicenseKeyException missingLicenseKey)
            {
                OnError(missingLicenseKey);
            }
            catch (ExpiredLicenseKeyException expiredLicenseKey)
            {
                OnError(expiredLicenseKey);
            }
            catch (InvalidDomainNameException invalidDomainName)
            {
                OnError(invalidDomainName);
            }
            catch (LicenseKeyException LicenseKeyException)
            {
                OnError(LicenseKeyException);
            }
        }

        /// <summary>
        /// OnError method.
        /// </summary>
        /// <param name="ex"></param>
        protected void OnError(Exception ex)
        {
            string errorMessage = string.Empty;
            ExceptionHandler.HandleException(ex, ref errorMessage);
            ShowError(errorMessage);

            StopProcessing = true;
        }
        #region "protected Methods"



        /// <summary>
        /// Method to load datasource into ListControl
        /// </summary>
        protected void LoadListControls()
        {
            LoadListControls(true);
        }

        /// <summary>
        /// Method to load datasource into ListControl
        /// </summary>
        /// <param name="setDefaultValue">allow to set default value</param>

        protected void LoadListControls(bool allowDefault)
        {
            Control datasource = null;
            if (ControlPanel != null)
            {
                foreach (Control control in ControlPanel.Controls.OfType<ListControl>())
                {

                    string ID = ((ListControl)control).ID + "_" + "Datasource";
                    datasource = ControlPanel.FindControl(ID);
                    // string.Format(((CMSQueryDataSource)datasource).QueryName, CMS.CMSHelper.CMSContext.CurrentSiteName);
                    if (datasource != null)
                    {
                        ((CMSQueryDataSource)datasource).QueryName = EmergeStaticHelper.SetSiteName(((CMSQueryDataSource)datasource).QueryName);
                        ((ListControl)control).DataSource = ((CMSQueryDataSource)datasource).DataSource;
                        ((ListControl)control).DataBind();
                        if (allowDefault)
                            ((ListControl)control).Items.Insert(0, new ListItem(Constants.DROPDOWN_DEFAULT_TEXT, Constants.DROPDOWN_DEFAULT_VALUE));
                    }
                }
            }
        }

        /// <summary>
        /// Method to fill the Dictionary instance with all the parameters which are available on the Form panel.
        /// </summary>
        protected void CreateFormParameters()
        {
            FormParameters = new Dictionary<string, object>();

            foreach (Control control in ControlPanel.Controls.OfType<Control>())
            {
                string key = control.ID;
                object value = string.Empty;

                if (control is TextBox)
                {
                    value = ((TextBox)control).Text;
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is DropDownList)
                {
                    value = ((DropDownList)control).SelectedItem.Value;
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is CheckBoxList)
                {
                    foreach (ListItem li in ((CheckBoxList)control).Items)
                    {
                        if (li.Selected)
                            value += li.Value + Constants.MULTI_VALUE_SEPERATOR;
                    }
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is RadioButtonList)
                {
                    //foreach (ListItem li in ((RadioButtonList)control).Items)
                    //{
                    //    if (li.Selected)
                    //        value += li.Value + Constants.MULTI_VALUE_SEPERATOR;
                    //}
                    value = ((RadioButtonList)control).SelectedValue;
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is ListBox)
                {
                    foreach (ListItem li in ((ListBox)control).Items)
                    {
                        if (li.Selected)
                            value += li.Value + Constants.MULTI_VALUE_SEPERATOR;
                    }
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is RadioButton)
                {
                    value = ((RadioButton)control).Checked ? true : false;
                    FormParameters.Add(key, value);
                    continue;
                }

                if (control is CheckBox)
                {
                    value = ((CheckBox)control).Checked ? true : false;
                    FormParameters.Add(key, value);
                    continue;
                }
                if (control is EmergeDateTimePickerUserControl)
                {
                    value = ((EmergeDateTimePickerUserControl)control).SelectedDateTime;
                    FormParameters.Add(key, value);
                    continue;
                }
                if (control is EmergeDatePickerUserControl)
                {
                    value = ((EmergeDatePickerUserControl)control).SelectedDateTime;
                    if (value.ToString() == System.DateTime.Today.AddYears(-125).ToString())
                    {
                        value = new DateTime();
                    }
                    FormParameters.Add(key, value);
                    continue;
                }
                if (control is HiddenField)
                {
                    value = ((HiddenField)control).Value;
                    if (value.ToString().ToLower() == "yes")
                        value = true;
                    else if (value.ToString().ToLower() == "no")
                        value = false;
                    FormParameters.Add(key, value);                  
                    continue;
                }
            }
        }
        /// <summary>
        /// Method to fill the Dictionary instance with all the parameters which are available on the Form panel.
        /// </summary>
        protected void AddFormParameters()
        {
            foreach (Control control in ControlPanel.Controls.OfType<Control>())
            {
                string key = control.ID;
                object value = string.Empty;

                if (control is TextBox)
                {
                    value = ((TextBox)control).Text;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is DropDownList)
                {
                    value = ((DropDownList)control).SelectedItem.Value;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is CheckBoxList)
                {
                    foreach (ListItem li in ((CheckBoxList)control).Items)
                    {
                        if (li.Selected)
                            value += li.Value + Constants.MULTI_VALUE_SEPERATOR;
                    }
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is RadioButtonList)
                {
                    value = ((RadioButtonList)control).SelectedValue;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is ListBox)
                {
                    foreach (ListItem li in ((ListBox)control).Items)
                    {
                        if (li.Selected)
                            value += li.Value + Constants.MULTI_VALUE_SEPERATOR;
                    }
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is RadioButton)
                {
                    value = ((RadioButton)control).Checked ? true : false;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }

                if (control is CheckBox)
                {
                    value = ((CheckBox)control).Checked ? true : false;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }
                if (control is EmergeDateTimePickerUserControl)
                {
                    value = ((EmergeDateTimePickerUserControl)control).SelectedDateTime;
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }
                if (control is EmergeDatePickerUserControl)
                {
                    value = ((EmergeDatePickerUserControl)control).SelectedDateTime;
                    if (value.ToString() == System.DateTime.Today.AddYears(-125).ToString())
                        value = new DateTime();
                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }
                if (control is HiddenField)
                {
                    value = ((HiddenField)control).Value;
                    if (value.ToString().ToLower() == "yes")
                        value = true;
                    else if (value.ToString().ToLower() == "no")
                        value = false;

                    if (FormParameters.ContainsKey(key))
                        FormParameters[key] = value;
                    else
                        FormParameters.Add(key, value);
                    continue;
                }
            }
        }


        /// <summary>
        /// method to clear the Fields.
        /// </summary>
        protected void ClearFormFields()
        {
            foreach (Control control in ControlPanel.Controls.OfType<Control>())
            {
                string key = control.ID;
                object value = string.Empty;

                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                    continue;
                }

                if (control is DropDownList)
                {
                    ((DropDownList)control).SelectedIndex = -1;
                    continue;
                }

                if (control is CheckBoxList)
                {
                    foreach (ListItem li in ((CheckBoxList)control).Items)
                    {
                        li.Selected = false;
                    }
                    continue;
                }

                if (control is RadioButtonList)
                {
                    foreach (ListItem li in ((RadioButtonList)control).Items)
                    {
                        li.Selected = false;
                    }
                    continue;
                }

                if (control is ListBox)
                {
                    foreach (ListItem li in ((ListBox)control).Items)
                    {
                        li.Selected = false;
                    }
                    continue;
                }

                if (control is RadioButton)
                {
                    ((RadioButton)control).Checked = false;
                    continue;
                }

                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                    continue;
                }
                if (control is EmergeDateTimePickerUserControl)
                {
                    ((EmergeDateTimePickerUserControl)control).SelectedDateTime = new DateTime();
                }
                if (control is EmergeDatePickerUserControl)
                {
                    ((EmergeDatePickerUserControl)control).SelectedDateTime = new DateTime();
                }
            }
        }
        /// <summary>
        /// Method to set Fields on the form with the Values in the Dictinary Instance.
        /// </summary>
        /// <param name="formFields">Dictionary Instance with which, fields on the Form needs to be set.</param>
        protected void SetFormFieldsFromDictionary(Dictionary<string, object> formFields)
        {
            try
            {
                foreach (Control control in ControlPanel.Controls.OfType<Control>())
                {

                    string key = control.ID;
                    object value = string.Empty;

                    if (key != null && formFields.ContainsKey(key))
                    {
                        value = formFields.Where(x => x.Key == key).First().Value;

                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = value.ToString();
                            continue;
                        }

                        if (control is DropDownList)
                        {
                            ((DropDownList)control).SelectedIndex = -1;
                            if (null != ((DropDownList)control).Items.FindByValue(value.ToString()))
                            {
                                ((DropDownList)control).Items.FindByValue(value.ToString()).Selected = true;
                            }

                            continue;
                        }

                        if (control is CheckBoxList)
                        {
                            foreach (ListItem li in ((CheckBoxList)control).Items)
                            {
                                li.Selected = false;
                            }
                            string[] selectedValues = value.ToString().Split(Constants.MULTI_VALUE_SEPERATOR);

                            foreach (string selectedValue in selectedValues)
                            {
                                if (null != ((CheckBoxList)control).Items.FindByValue(selectedValue))
                                {
                                    ((CheckBoxList)control).Items.FindByValue(selectedValue).Selected = true;
                                }
                            }

                            continue;
                        }

                        if (control is RadioButtonList)
                        {

                            foreach (ListItem li in ((RadioButtonList)control).Items)
                            {
                                li.Selected = false;
                            }
                            string[] selectedValues = value.ToString().Split(Constants.MULTI_VALUE_SEPERATOR);

                            foreach (string selectedValue in selectedValues)
                            {
                                if (null != ((RadioButtonList)control).Items.FindByValue(selectedValue))
                                {
                                    ((RadioButtonList)control).Items.FindByValue(selectedValue).Selected = true;
                                }
                            }

                            continue;
                        }

                        if (control is ListBox)
                        {
                            foreach (ListItem li in ((ListBox)control).Items)
                            {
                                li.Selected = false;
                            }
                            string[] selectedValues = value.ToString().Split(Constants.MULTI_VALUE_SEPERATOR);

                            foreach (string selectedValue in selectedValues)
                            {
                                if (null != ((ListBox)control).Items.FindByValue(selectedValue))
                                {
                                    ((ListBox)control).Items.FindByValue(selectedValue).Selected = true;
                                }
                            }

                            continue;
                        }

                        if (control is RadioButton)
                        {
                            ((RadioButton)control).Checked = (bool)value;
                            continue;
                        }

                        if (control is CheckBox)
                        {
                            ((CheckBox)control).Checked = (bool)value;
                            continue;
                        }

                        if (control is Label)
                        {
                            ((Label)control).Text = value.ToString();
                        }
                        if (control is EmergeDateTimePickerUserControl)
                        {
                            ((EmergeDateTimePickerUserControl)control).SelectedDateTime = (DateTime)value;
                        }
                        if (control is EmergeDatePickerUserControl)
                        {
                            ((EmergeDatePickerUserControl)control).SelectedDateTime = (DateTime)value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// returns get display value for each field and returns dictinary Item.
        /// </summary>
        /// <param name="CustomTableCodeName">Foreign Table code name</param>
        /// <param name="FormFields">Dictionary</param>
        /// <returns>Returns Dictionary Items with their respective display values </returns>
        protected Dictionary<string, object> GetFormFieldsForDisplay(string CustomTableCodeName, Dictionary<string, object> FormFields)
        {
            Dictionary<string, object> FormFieldsForDisplay = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> formField in FormFields)
            {

                object returnValue = EmergeRelationHelper.GetRelationColumnValue(CustomTableCodeName, formField.Key, formField.Value.ToString());
                returnValue = String.IsNullOrEmpty(returnValue.ToString()) ? formField.Value : returnValue;
                FormFieldsForDisplay.Add(formField.Key, returnValue);
            }

            return FormFieldsForDisplay;
        }
        #endregion
    }
}
