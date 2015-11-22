using NextCMS.Core.Domain.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace NextCMS.Data.Mapping.Catalog
{
    public partial class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            this.ToTable("Catalog_Comment");
            this.HasKey(t => t.Id);

            this.Property(t => t.UserName).HasMaxLength(100);

            this.HasRequired(pr => pr.Article)
                .WithMany(cr => cr.Comments)
                .HasForeignKey(pr => pr.ArticleId);
        }
    }
}
