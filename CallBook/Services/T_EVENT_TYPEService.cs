using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallBook.Services
{
    public class T_EVENT_TYPEService
    {
        public static T_EVENT_TYPE GetEventTypeByEventID(MyModel db, string eventTypeID)
        {
            return db.T_EVENT_TYPE.FirstOrDefault(n => n.EVENT_ID == eventTypeID);
        }
    }
}