namespace CallBook
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_EVENT_TYPE
    {
        [Key]
        [StringLength(50)]
        public string EVENT_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string EVENT_NAME { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public virtual ICollection<T_EVENT> T_EVENT { get; set; }
    }
}
