using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Core.Data.Configurations.Common;
using MyBlog.Core.Entities;

namespace MyBlog.Core.Data.Configurations
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