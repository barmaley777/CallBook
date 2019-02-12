using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CallBook.Models.Mapping;

namespace CallBook.Models
{
    public partial class callBookDBContext : DbContext
    {
        static callBookDBContext()
        {
            Database.SetInitializer<callBookDBContext>(null);
        }

        public callBookDBContext()
            : base("Name=callBookDBContext")
        {
        }

        public DbSet<T_CALL> T_CALL { get; set; }
        public DbSet<T_EVENT> T_EVENT { get; set; }
        public DbSet<T_EVENT_TYPE> T_EVENT_TYPE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_CALLMap());
            modelBuilder.Configurations.Add(new T_EVENTMap());
            modelBuilder.Configurations.Add(new T_EVENT_TYPEMap());
        }
    }
}
