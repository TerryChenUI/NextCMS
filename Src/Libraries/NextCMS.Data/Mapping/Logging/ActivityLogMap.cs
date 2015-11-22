using System.Data.Entity.ModelConfiguration;
using NextCMS.Core.Domain.Common;

namespace NextCMS.Data.Mapping.Logging
{
    public partial class ActivityLogMap : EntityTypeConfiguration<ActivityLog>
    {
        public ActivityLogMap()
        {
            this.ToTable("Common_ActivityLog");
            this.HasKey(t => t.Id);
            this.Property(t => t.Comment).IsRequired();


            this.HasRequired(t => t.ActivityLogType)
                .WithMany()
                .HasForeignKey(t => t.ActivityLogTypeId);
            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);
        }
    }
}
