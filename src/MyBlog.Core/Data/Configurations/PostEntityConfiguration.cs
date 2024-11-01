using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Core.Data.Configurations.Common;
using MyBlog.Core.Entities;

namespace MyBlog.Core.Data.Configurations
{
    internal class PostEntityConfiguration : EntityBaseConfiguration<Post>, IEntityTypeConfiguration<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Title)
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property(e => e.Summary)
                   .HasMaxLength(1024)
                   .IsRequired();

            builder.Property(e => e.Content)
                   .HasMaxLength(1024 * 5)
                   .IsRequired();

            builder.Property(e => e.ViewCount)
                   .HasDefaultValue(0);

            builder.Property(e => e.IsActive)
                   .HasDefaultValue(true);

            builder.Property(e => e.PublishDate)
                   .IsRequired();

            builder.HasQueryFilter(e => e.IsActive);

            builder.HasIndex(e => new { e.IsActive, e.PublishDate });
            builder.HasIndex(e => new { e.AuthorId, e.IsActive });

            builder.HasOne(e => e.Author)
                   .WithMany(a => a.Posts)
                   .HasForeignKey(e => e.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}