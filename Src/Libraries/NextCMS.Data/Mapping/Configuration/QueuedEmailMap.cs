using NextCMS.Core.Domain.Configuration;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Configuration
{
    public partial class QueuedEmailMap : EntityTypeConfiguration<QueuedEmail>
    {
        public QueuedEmailMap()
        {
            this.ToTable("Configuration_QueuedEmail");
            this.HasKey(t => t.Id);

            this.Property(t => t.From).HasMaxLength(200);
            this.Property(t => t.FromName).HasMaxLength(200);
            this.Property(t => t.To).IsRequired().HasMaxLength(200);
            this.Property(t => t.ToName).HasMaxLength(200);
            this.Property(t => t.ReplyTo).HasMaxLength(200);
            this.Property(t => t.ReplyToName).HasMaxLength(200);
            this.Property(t => t.CC).HasMaxLength(200);
            this.Property(t => t.Bcc).HasMaxLength(200);
            this.Property(t => t.Subject).HasMaxLength(200);
            this.Property(t => t.AttachmentFilePath).HasMaxLength(500);
            this.Property(t => t.AttachmentFileName).HasMaxLength(500);

            this.HasRequired(pr => pr.EmailAccount)
                .WithMany()
                .HasForeignKey(pr => pr.EmailAccountId);
        }
    }
}
