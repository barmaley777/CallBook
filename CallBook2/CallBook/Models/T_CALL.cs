using System;
using System.Collections.Generic;

namespace CallBook.Models
{
    public partial class T_CALL
    {
        public int RECORD_ID { get; set; }
        public int CALLER { get; set; }
        public Nullable<int> RECIEVER { get; set; }
    }
}
