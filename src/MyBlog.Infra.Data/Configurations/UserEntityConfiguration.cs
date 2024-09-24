using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Domain.Entities;
using MyBlog.Infra.Data.Configurations.Common;

namespace MyBlog.Infra.Data.Configurations
{
    internal class UserEntityConfiguration : EntityBaseConfiguration<User>, IEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.FullName)
                   .HasMaxLength(256)
                   .IsRequired();
        }
    }
}