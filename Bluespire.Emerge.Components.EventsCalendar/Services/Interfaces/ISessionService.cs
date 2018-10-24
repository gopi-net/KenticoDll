using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    public interface ISessionService
    {
        IList<EventSession> GetSessionsByOccurence(int occurenceID);

        IList<EventSession> GetSessionsBySchedule(int scheduleID);

        void DeleteSessionsByOccurenceID(int occurenceID);

        void DeleteSessionsByScheduleID(int scheduleID);

        void DeleteSessionByID(int sessionID);

        IList<EventSession> GetSessionsByCondition(string whereCondition);
    }
}
