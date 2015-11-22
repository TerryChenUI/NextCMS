using NextCMS.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Catalog
{
    public partial class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            this.ToTable("Catalog_Category");
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(100);
            this.Property(t => t.MetaKeywords).HasMaxLength(200);
            this.Property(t => t.MetaDescription).HasMaxLength(200);
            this.Property(t => t.MetaTitle).HasMaxLength(200);

        }
    }
}
