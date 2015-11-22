using NextCMS.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Catalog
{
    public partial class ArticleMap : EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            this.ToTable("Catalog_Article");
            this.HasKey(t => t.Id);

            this.Property(t => t.Title).IsRequired().HasMaxLength(100);
            this.Property(t => t.ShortDescription).HasMaxLength(500);

            this.Property(t => t.MetaKeywords).HasMaxLength(200);
            this.Property(t => t.MetaDescription).HasMaxLength(200);
            this.Property(t => t.MetaTitle).HasMaxLength(200);

            this.HasOptional(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId).WillCascadeOnDelete(false);

            this.HasMany(pr => pr.Tags)
                .WithMany(cr => cr.Articles)
                .Map(m =>
                {
                    m.ToTable("Catalog_ArticleTag_Mapping");
                    m.MapLeftKey("ArticleId");
                    m.MapRightKey("TagId");
                });
        }
    }
}
