using System;
using System.Linq;

namespace CallBook.Services
{
    public static class T_EVENTService
    {
        public static IQueryable<T_EVENT> GetEventById(MyModel db, string eventId)
        {
            return db.T_EVENT.Where(item => item.T_CALL.RECORD_ID.ToString().Contains(eventId));
        }

        public static IQueryable<int> GetCallerCallID(MyModel db, string caller)
        {
            return db.T_CALL.Where(record => record.CALLER.ToString().Equals(caller)).Select(n => n.RECORD_ID).Distinct();
        }

        public static IQueryable<T_EVENT> GetAllCallerCalls(MyModel db, string caller)
        {
            IQueryable<int> callsID = GetCallerCallID(db, caller);
            return db.T_EVENT.Where(n => db.T_CALL.Where(t => t.CALLER.ToString().Equals(caller)).Select(k => k.RECORD_ID).Contains(n.CALL_ID));
        }

        public static IQueryable<T_EVENT> ParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID)
        {
            return tEvent.Where(recStart => recStart.CALL_ID.Equals(callID));
        }

        public static IQueryable<T_EVENT> ParticipantsByCallID(IQueryable<T_EVENT> tEvent, int callID, string eventName)
        {
            return tEvent.Where(recStart => recStart.CALL_ID.Equals(callID) && recStart.RECORD_EVENT_ID.ToLower().Contains(eventName));
        }
    }
}