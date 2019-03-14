using CallBook.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CallBook.MyClasses
{

    public class TaskDBData
    {
        private static int GetPhoneNumber()
        {
            int phoneNumber = 0;
            Random random = new Random(Guid.NewGuid().GetHashCode());
            //digits of start phone number
            List<int> startDigits = new List<int>() { 3, 5, 8 };
            phoneNumber = startDigits[random.Next(startDigits.Count)];

            for (int i = 0; i < 5; i++)
            {
                Int32.TryParse(phoneNumber.ToString() + random.Next(0, 9), out phoneNumber);
            }

            return phoneNumber;
        }

        public static void GenerateDBData(int callsCount)
        {
            const int cancelCallCountPercent = 15;
            const int fullCallCountPercent = 80;
            MyModel context = new MyModel();

            List<T_EVENT_TYPE> eventTypes = new List<T_EVENT_TYPE>()
            {
                new T_EVENT_TYPE(){EVENT_ID = "EVENT_PICK_UP", EVENT_NAME = "Pick-up", Description="Generated when user pick ups the phone."},
                new T_EVENT_TYPE(){EVENT_ID = "EVENT_DIAL", EVENT_NAME = "Dialling", Description="Generated upon the start of the call."},
                new T_EVENT_TYPE(){EVENT_ID = "EVENT_CALL_ESTABLISHED", EVENT_NAME = "Call Established", Description="Generated when the reciever answers the call."},
                new T_EVENT_TYPE(){EVENT_ID = "EVENT_CALL_END", EVENT_NAME = "Call End", Description="Generated when one of the party cancels the call."},
                new T_EVENT_TYPE(){EVENT_ID = "EVENT_HANG_UP", EVENT_NAME = "Hang-up", Description="Generated when user hangs up the phone."},
            };

            if (!context.T_EVENT_TYPE.Any())
            {
                context.T_EVENT_TYPE.AddRange(eventTypes);
                context.SaveChanges();
            }

            if (!context.T_EVENT.Any())
            {
                double num = (Convert.ToDouble(callsCount) * fullCallCountPercent) / 100;
                int fullCallCount = Convert.ToInt32(Math.Round(num, MidpointRounding.AwayFromZero));
                num = (Convert.ToDouble(callsCount) * cancelCallCountPercent) / 100;
                int cancelCallCount = Convert.ToInt32(Math.Round(num, MidpointRounding.AwayFromZero));
                int nonDialledCallCount = callsCount - fullCallCount - cancelCallCount;

                RecordCallToDB(context, nonDialledCallCount, 1, eventTypes);
                RecordCallToDB(context, cancelCallCount, 2, eventTypes);
                RecordCallToDB(context, fullCallCount, 4, eventTypes);
            }
        }

        private static void RecordCallToDB(MyModel db, int recCount, int eventsCount, List<T_EVENT_TYPE> eventTypes)
        {
            MyModel context = new MyModel();
            const int eventCallEndIndex = 3; //EVENT_CALL_END index from eventTypes list
            const int totalCallEvents = 1; //caller make only EVENT_PICK_UP

            Random random = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < recCount; i++)
            {
                T_CALL newCall = null;
                //only event EVENT_PICK_UP is missing receiver number, other events have it
                if (eventsCount == totalCallEvents)
                {
                    newCall = db.T_CALL.Add(new T_CALL() { CALLER = GetPhoneNumber() });
                }
                else
                {
                    newCall = db.T_CALL.Add(new T_CALL() { CALLER = GetPhoneNumber(), RECIEVER = GetPhoneNumber() });
                }

                DateTime currentTime = DateTime.Now;
                for (int j = 0; j <= eventsCount; j++)
                {                  
                    currentTime = currentTime.AddMilliseconds(1000 - currentTime.Millisecond);
                    string eventTypeID = string.Empty;
                    //if index is last of events, that's means it's EVENT_HANG_UP
                    if (j == eventsCount)
                    {
                        eventTypeID = eventTypes.Select(x => x.EVENT_ID).Last();
                    }
                    else
                    {
                        eventTypeID = eventTypes.Select(x => x.EVENT_ID).ToList()[j];
                    }

                    T_EVENT_TYPE eventTypeRec = T_EVENT_TYPEService.GetEventTypeByEventID(db,eventTypeID);
                    //add some random time to EVENT_CALL_END(index = 3 in eventTypes)
                    if (j == eventCallEndIndex)
                    {
                        currentTime = currentTime.AddMinutes(random.Next(1, 30));
                    }
                    else
                    {
                        currentTime = currentTime.AddMinutes(random.Next(0, j));
                    }

                    db.T_EVENT.Add(new T_EVENT() { RECORD_EVENT_ID = eventTypeID, RECORD_DATE = currentTime, CALL_ID = newCall.RECORD_ID, T_CALL = newCall, T_EVENT_TYPE = eventTypeRec });
                }
               
            }
            db.SaveChanges();
        }
    }
}