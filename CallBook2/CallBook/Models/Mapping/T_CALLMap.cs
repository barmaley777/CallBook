using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CallBook.Models.Mapping
{
    public class T_CALLMap : EntityTypeConfiguration<T_CALL>
    {
        public T_CALLMap()
        {
            // Primary Key
            this.HasKey(t => t.RECORD_ID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_CALL");
            this.Property(t => t.RECORD_ID).HasColumnName("RECORD_ID");
            this.Property(t => t.CALLER).HasColumnName("CALLER");
            this.Property(t => t.RECIEVER).HasColumnName("RECIEVER");
        }
    }
}
