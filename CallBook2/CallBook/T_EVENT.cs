namespace CallBook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EVENT
    {
        [Key]
        public int RECORD_ID { get; set; }

        [Required]
        [StringLength(50)]
        [ForeignKey("T_EVENT_TYPE")]
        public string RECORD_EVENT_ID { get; set; }

        public DateTime RECORD_DATE { get; set; }

        [ForeignKey("T_CALL")]
        public int CALL_ID { get; set; }

        public virtual T_EVENT_TYPE T_EVENT_TYPE { get; set; }
        public virtual T_CALL T_CALL { get; set; }
    }
}
