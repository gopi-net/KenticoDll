using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bluespire.Emerge.Common.Exceptions;
using CMS.Helpers;

namespace Bluespire.Emerge.CommonService
{
    public class UniGridTemplateField : ITemplate
    {
        private ListItemType _UniGridListItemType;
        private Control _control;


        public UniGridTemplateField(ListItemType uniGridItemType, Control control)
        {
            _UniGridListItemType = uniGridItemType;
            _control = control;
        }


        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            if (_UniGridListItemType == ListItemType.Header)
                container.Controls.Add(_control);
            else
            {
                Type itemType = _control.GetType();


                switch (itemType.Name)
                {
                    case "CheckBox":
                        CheckBox chkBox = new CheckBox();
                        chkBox.CssClass = ((CheckBox)_control).CssClass;
                        chkBox.ID = ((CheckBox)_control).ID;
                        container.Controls.Add(chkBox);
                        break;
                    default:
                        throw new TemplateFieldNotImplementedException(String.Format(ResHelper.GetString("Emerge.Exception"), itemType.Name));
                }
            }
        }

        #endregion
    }
}
