namespace CallBook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_CALL
    {
        [Key]
        public int RECORD_ID { get; set; }

        public int CALLER { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RECIEVER { get; set; }

        public virtual ICollection<T_EVENT> T_EVENT { get; set; }
    }
}
