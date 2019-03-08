using System;
using System.Linq;

namespace CallBook.Services
{
    public static class T_EVENTService
    {
        public static IQueryable<T_EVENT> GetEventById(MyModel db, string eventId)
        {
            return db.T_EVENT.Where(item => item.T_CALL.RECORD_ID == int.Parse(eventId));
        }

        public static IQueryable<int> GetCallerCallID(MyModel db, string caller)
        {
            return db.T_CALL.Where(record => record.CALLER == int.Parse(caller)).Select(n => n.RECORD_ID).Distinct();
        }

        public static IQueryable<T_EVENT> GetAllCallerCalls(MyModel db, string caller)
        {
            IQueryable<int> callsID = GetCallerCallID(db, caller);
            return db.T_EVENT.Where(n => db.T_CALL.Where(t => t.CALLER == int.Parse(caller)).Select(k => k.RECORD_ID).Contains(n.CALL_ID));
        }

        public static IQueryable<T_EVENT> ParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID)
        {
            return tEvent.Where(recStart => recStart.CALL_ID.Equals(callID));
        }

        public static IQueryable<T_EVENT> ParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID, string eventName)
        {
            return tEvent.Where(recStart => recStart.CALL_ID == callID && recStart.RECORD_EVENT_ID.ToLower().Contains(eventName));
        }

        public static IQueryable<T_EVENT> EventsByFilters(MyModel db, string filterCaller, string filterReceiver)
        {
            return db.T_EVENT.Where(item => item.T_CALL.CALLER.ToString().Contains(filterCaller))
                .Where(item => item.T_CALL.RECIEVER.ToString().Contains(filterReceiver));
        }

        public static IQueryable<T_EVENT> EventsByFilters(MyModel db, string filterCaller, string filterReceiver, string filterType)
        {
            return EventsByFilters(db, filterCaller, filterReceiver).Where(item => item.T_EVENT_TYPE.EVENT_NAME == filterType);
        }

    }
}