using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Domain.Entities;
using MyBlog.Infra.Data.Configurations.Common;

namespace MyBlog.Infra.Data.Configurations
{
    internal class AuthorEntityConfiguration : EntityBaseConfiguration<Author>, IEntityTypeConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            //builder.Property(e => e.Bio)
            //       .HasMaxLength(1024);

            //builder.Property(e => e.Slug)
            //       .HasMaxLength(256)
            //       .IsRequired();

            //builder.HasIndex(e => new { e.Slug })
            //       .IsUnique();

            
            builder.HasOne(e => e.User)
                   .WithOne()
                   .HasForeignKey<Author>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}