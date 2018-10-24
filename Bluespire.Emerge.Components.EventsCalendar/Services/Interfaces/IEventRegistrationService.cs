using System;
using System.Collections.Generic;
using Bluespire.Emerge.Components.EventsCalendar.Common;
using Bluespire.Emerge.Components.EventsCalendar.Entities;

namespace Bluespire.Emerge.Components.EventsCalendar.Services.Interfaces
{
    interface IEventRegistrationService
    {
        IEnumerable<EventRegistration> GetAllRegistrationsByScheduleID(int scheduleID, EventRegistrationStatus status);

        IEnumerable<EventRegistration> GetRegistrationsByOccurenceID(int occurenceID, EventRegistrationStatus status);

        EventRegistration GetEventRegistrationByID(int registrationID);

        void UpdateStartTimeForRegistration(int registrationID);

        void UpdateOccurenceForRegistration(int registrationID, int occurenceID);

        void MoveRegistrationsForSchedule(int scheduleID, IDictionary<string, string> registrations);

        IEnumerable<EventRegistration> GetRegistrationsByEventID(int eventID, EventRegistrationStatus status);

        void UpdateRegistrationForVolunteerUser(int registrationID, int userID);

        bool AllowEmailSend(RegistrationEmailMode mode);
        SaveRegistrationStatus SaveEventRegistrations(List<Dictionary<string, object>> registrations, ref List<int> savedItemIDs);
        SaveRegistrationStatus ValidateRegistrations(List<Dictionary<string, object>> registrations);
        IEnumerable<EventRegistration> GetRegistrationsBySessionID(int sessionID, EventRegistrationStatus status);

        void DeleteRegistrationsByScheduleID(int scheduleID);

        void DeleteRegistrationsByOccurrenceID(int occurrenceID);
    }
}
