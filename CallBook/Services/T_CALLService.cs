using System.Linq;

namespace CallBook.Services
{
    public class T_CALLService
    {
        public static IQueryable<T_CALL> ReceiverByCallID(MyModel db, int callID)
        {
            return db.T_CALL.Where(record => record.RECORD_ID.Equals(callID));
        }
    }
}