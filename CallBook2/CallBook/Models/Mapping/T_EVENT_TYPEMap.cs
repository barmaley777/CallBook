using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CallBook.Models.Mapping
{
    public class T_EVENT_TYPEMap : EntityTypeConfiguration<T_EVENT_TYPE>
    {
        public T_EVENT_TYPEMap()
        {
            // Primary Key
            this.HasKey(t => t.EVENT_ID);

            // Properties
            this.Property(t => t.EVENT_ID)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EVENT_NAME)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("T_EVENT_TYPE");
            this.Property(t => t.EVENT_ID).HasColumnName("EVENT_ID");
            this.Property(t => t.EVENT_NAME).HasColumnName("EVENT_NAME");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
