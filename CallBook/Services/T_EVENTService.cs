using System;
using System.Linq;

namespace CallBook.Services
{
    public static class T_EVENTService
    {
        public static IQueryable<T_EVENT> GetEventsByCallId(MyModel db, int eventId)
        {
            return db.T_EVENT.Where(item => item.CALL_ID == eventId);
        }

        public static IQueryable<int> GetCallerCallID(MyModel db, int caller)
        {
            return db.T_CALL.Where(record => record.CALLER == caller).Select(n => n.RECORD_ID).Distinct();
        }

        public static IQueryable<T_EVENT> GetAllCallerCalls(MyModel db, int caller)
        {
            IQueryable<int> callsID = GetCallerCallID(db, caller);
            return db.T_EVENT.Where(n => db.T_CALL.Where(t => t.CALLER == caller).Select(k => k.RECORD_ID).Contains(n.CALL_ID));
        }

        public static T_EVENT GetParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID)
        {
            return tEvent.FirstOrDefault(recStart => recStart.CALL_ID == callID);
        }

        public static T_EVENT GetParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID, string eventName)
        {
            return tEvent.Where(recStart => recStart.CALL_ID == callID && recStart.RECORD_EVENT_ID.ToLower().Contains(eventName)).FirstOrDefault();
        }

        public static IQueryable<T_EVENT> GetEventsByFilters(MyModel db, string filterCaller, string filterReceiver)
        {
            return db.T_EVENT.Where(item => item.T_CALL.CALLER.ToString().Contains(filterCaller))
                .Where(item => item.T_CALL.RECIEVER.ToString().Contains(filterReceiver));
        }

        public static IQueryable<T_EVENT> GetEventsByFilters(MyModel db, string filterCaller, string filterReceiver, string filterType)
        {
            return GetEventsByFilters(db, filterCaller, filterReceiver).Where(item => item.T_EVENT_TYPE.EVENT_NAME == filterType);
        }

    }
}