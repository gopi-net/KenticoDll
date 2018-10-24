using Bluespire.Emerge.Common.CMS.GlobalHelper;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bluespire.Emerge.Components.StaffDirectory.WebParts
{
    public class StaffDirectoryAdvanceSearchWebpart : StaffDirectoryWebPart
    {
        protected const string MILES_CONTROL_ID = "Miles";
        protected const string ZIP_CONTROL_ID = "Zip";
        protected IDictionary<string, string> SetSearchParameters(IDictionary<string, string> parameters)
        {
            foreach (Control control in ControlPanel.Controls.OfType<WebControl>())
            {
                string key = control.ID;
                string value = string.Empty;
                int intValue = 0;

                if (control is TextBox)
                {
                    value = ((TextBox)control).Text;
                    if (value != string.Empty)
                        parameters.Add(key, value);
                }

                else if (control is DropDownList)
                {
                    value = ((DropDownList)control).SelectedItem.Value;
                    int.TryParse(value, out intValue);
                    if (intValue > 0)
                        parameters.Add(key, ((DropDownList)control).SelectedItem.Text);
                }

                else if (control is RadioButtonList)
                {
                    value = ((RadioButtonList)control).SelectedValue;
                    int.TryParse(value, out intValue);
                    if (intValue > 0)
                        parameters.Add(key, ((RadioButtonList)control).SelectedItem.Value);
                }

            }
            return parameters;
        }
        protected void SaveSearchParametersToSession(IDictionary<string, string> parameters)
        {
            EmergeSessionHelper.SetValue(StaffDirectoryConstants.STAFF_SESSION_SEARCH_PARAMETERS, parameters);
        }
    }
}
