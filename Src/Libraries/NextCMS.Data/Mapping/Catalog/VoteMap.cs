using NextCMS.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Catalog
{
    public partial class VoteMap : EntityTypeConfiguration<Vote>
    {
        public VoteMap()
        {
            this.ToTable("Catalog_Vote");
            this.HasKey(t => t.Id);

            this.Property(t => t.IPAddress).IsRequired().HasMaxLength(100);

            this.HasRequired(pr => pr.Article)
                .WithMany()
                .HasForeignKey(pr => pr.ArticleId);
        }
    }
}
