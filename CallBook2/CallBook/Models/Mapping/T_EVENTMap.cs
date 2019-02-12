using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CallBook.Models.Mapping
{
    public class T_EVENTMap : EntityTypeConfiguration<T_EVENT>
    {
        public T_EVENTMap()
        {
            // Primary Key
            this.HasKey(t => t.RECORD_ID);

            // Properties
            this.Property(t => t.RECORD_EVENT_ID)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_EVENT");
            this.Property(t => t.RECORD_ID).HasColumnName("RECORD_ID");
            this.Property(t => t.RECORD_EVENT_ID).HasColumnName("RECORD_EVENT_ID");
            this.Property(t => t.RECORD_DATE).HasColumnName("RECORD_DATE");
            this.Property(t => t.CALL_ID).HasColumnName("CALL_ID");
        }
    }
}
