using NextCMS.Core.Domain.Authen;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Authen
{
    public partial class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("Authen_User");
            this.HasKey(t => t.Id);

            this.Property(t => t.UserName).IsRequired().HasMaxLength(100);
            this.Property(t => t.Email).IsRequired().HasMaxLength(100);
            this.Property(t => t.Password).IsRequired().HasMaxLength(200);
            this.Property(t => t.Phone).HasMaxLength(20);
            this.Property(t => t.LastIpAddress).HasMaxLength(100);

            this.HasMany(c => c.Roles)
                .WithMany(t => t.Users)
                .Map(m =>
                {
                    m.ToTable("Authen_UserRole_Mapping");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}
