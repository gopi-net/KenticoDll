using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Common;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;
using Bluespire.Emerge.Web.WebParts.BaseWebParts;

namespace Bluespire.Emerge.Components.EventsCalendar.WebParts
{
    /// <summary>
    /// Base Class for all the webparts to be created for EventsCalendar module.
    /// </summary>
    public class EventsCalendarWebPart : EmergeBaseWebPart
    {
        /// <summary>
        /// OnInit Method will be used to set the module name to which webpart belongs to.
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            StopProcessing = false;
            Module = Constants.Modules.EventsCalendar;
            base.OnInit(e);
        }



    }


}
