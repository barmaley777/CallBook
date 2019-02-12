using System;
using System.Collections.Generic;

namespace CallBook.Models
{
    public partial class T_EVENT
    {
        public int RECORD_ID { get; set; }
        public string RECORD_EVENT_ID { get; set; }
        public System.DateTime RECORD_DATE { get; set; }
        public int CALL_ID { get; set; }
    }
}
