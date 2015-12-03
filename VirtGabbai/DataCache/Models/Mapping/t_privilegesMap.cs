using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataCache.Models.Mapping
{
    public class t_privilegesMap : EntityTypeConfiguration<t_zl_privileges>
    {
        public t_privilegesMap()
        {
            // Primary Key
            this.HasKey(t => t.C_id);

            // Properties
            this.Property(t => t.C_id);

            this.Property(t => t.privilege_name)
                .HasMaxLength(45);

            // Table & Column Mappings
            this.ToTable("zl_privileges", "zera_levi");
            this.Property(t => t.C_id).HasColumnName("_id");
            this.Property(t => t.privilege_name).HasColumnName("privilege_name");
        }
    }
}