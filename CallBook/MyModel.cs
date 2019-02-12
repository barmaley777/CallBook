namespace CallBook
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyModel : DbContext
    {
        public MyModel()
            : base("name=MyModel")
        {
        }

        public virtual DbSet<T_CALL> T_CALL { get; set; }
        public virtual DbSet<T_EVENT> T_EVENT { get; set; }
        public virtual DbSet<T_EVENT_TYPE> T_EVENT_TYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
