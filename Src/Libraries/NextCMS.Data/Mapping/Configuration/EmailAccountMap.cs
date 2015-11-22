using NextCMS.Core.Domain.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Configuration
{
    public partial class EmailAccountMap : EntityTypeConfiguration<EmailAccount>
    {
        public EmailAccountMap()
        {
            this.ToTable("Configuration_EmailAccount");
            this.HasKey(t => t.Id);
            
            this.Property(t => t.DisplayName).HasMaxLength(200);
            this.Property(t => t.Email).IsRequired().HasMaxLength(200);
            this.Property(t => t.Host).IsRequired().HasMaxLength(200);
            this.Property(t => t.UserName).IsRequired().HasMaxLength(200);
            this.Property(t => t.Password).IsRequired().HasMaxLength(200);

        }
    }
}
