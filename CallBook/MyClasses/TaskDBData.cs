using System;
using System.Collections.Generic;
using System.Linq;

namespace CallBook.MyClasses
{
    public class TaskDBData
    {
        private int PhoneNumber()
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

        public void GenerateDBData(int callsCount)
        {
            MyModel context = new MyModel();
            Random random = new Random(Guid.NewGuid().GetHashCode());

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

            if (context.T_EVENT.Count() < 1)
            {
                double num = (Convert.ToDouble(callsCount) * 80) / 100;
                int fullCallCount = Convert.ToInt32(Math.Round(num, MidpointRounding.AwayFromZero));
                num = (Convert.ToDouble(callsCount) * 15) / 100;
                int cancelCallCount = Convert.ToInt32(Math.Round(num, MidpointRounding.AwayFromZero));
                int nonDialledCallCount = callsCount - fullCallCount - cancelCallCount;

                for (int i = 0; i < nonDialledCallCount; i++)
                {
                    T_CALL newCall = context.T_CALL.Add(new T_CALL() { CALLER = PhoneNumber() });
                    context.SaveChanges();

                    for (int j = 0; j < 1; j++)
                    {
                        DateTime currentTime = DateTime.Now;
                        currentTime = currentTime.AddMilliseconds(1000 - currentTime.Millisecond);
                        String eventType = eventTypes[j].EVENT_ID;
                        T_EVENT_TYPE eventTypeRec = context.T_EVENT_TYPE.FirstOrDefault(n => n.EVENT_ID == eventType);
                        context.T_EVENT.Add(new T_EVENT() { RECORD_EVENT_ID = eventType, RECORD_DATE = currentTime.AddMinutes(j), CALL_ID = newCall.RECORD_ID, T_CALL = newCall, T_EVENT_TYPE = eventTypeRec });
                    }
                    context.SaveChanges();
                }

                for (int i = 0; i < cancelCallCount; i++)
                {
                    T_CALL newCall = context.T_CALL.Add(new T_CALL() { CALLER = PhoneNumber() });
                    context.SaveChanges();

                    for (int j = 0; j < 2; j++)
                    {
                        DateTime currentTime = DateTime.Now;
                        currentTime = currentTime.AddMilliseconds(1000 - currentTime.Millisecond);
                        String eventType = eventTypes[j].EVENT_ID;
                        T_EVENT_TYPE eventTypeRec = context.T_EVENT_TYPE.FirstOrDefault(n => n.EVENT_ID == eventType);
                        context.T_EVENT.Add(new T_EVENT() { RECORD_EVENT_ID = eventType, RECORD_DATE = currentTime.AddMinutes(j), CALL_ID = newCall.RECORD_ID, T_CALL = newCall, T_EVENT_TYPE = eventTypeRec });
                    }
                    context.SaveChanges();
                }

                for (int i = 0; i < fullCallCount; i++)
                {
                    T_CALL newCall = context.T_CALL.Add(new T_CALL() { CALLER = PhoneNumber(), RECIEVER = PhoneNumber() });
                    context.SaveChanges();

                    for (int j = 0; j < eventTypes.Count; j++)
                    {
                        DateTime currentTime = DateTime.Now;
                        currentTime = currentTime.AddMilliseconds(1000 - currentTime.Millisecond);
                        String eventType = eventTypes[j].EVENT_ID;
                        T_EVENT_TYPE eventTypeRec = context.T_EVENT_TYPE.FirstOrDefault(n => n.EVENT_ID == eventType);
                        if (j == 4)
                        {
                            context.T_EVENT.Add(new T_EVENT() { RECORD_EVENT_ID = eventType, RECORD_DATE = currentTime.AddMinutes(random.Next(1, 30)), CALL_ID = newCall.RECORD_ID, T_CALL = newCall, T_EVENT_TYPE = eventTypeRec });
                        }
                        else
                        {
                            context.T_EVENT.Add(new T_EVENT() { RECORD_EVENT_ID = eventType, RECORD_DATE = currentTime.AddMinutes(j), CALL_ID = newCall.RECORD_ID, T_CALL = newCall, T_EVENT_TYPE = eventTypeRec });
                        }
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}