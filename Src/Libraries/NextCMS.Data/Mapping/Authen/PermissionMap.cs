using NextCMS.Core.Domain.Authen;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Authen
{
    public partial class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            this.ToTable("Authen_Permission");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.Property(t => t.Area).HasMaxLength(50);
            this.Property(t => t.Controller).HasMaxLength(50);
            this.Property(t => t.Action).HasMaxLength(50);
            this.Property(t => t.Icon).HasMaxLength(50);

            this.HasMany(pr => pr.Roles)
                .WithMany(cr => cr.Permissions)
                .Map(m => 
                { 
                    m.ToTable("Authen_RolePermission_Mapping");
                    m.MapLeftKey("PermissionId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}