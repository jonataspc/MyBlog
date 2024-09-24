using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Infra.Identity;

namespace MyBlog.Infra.Data.Configurations
{
    internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(e => e.FullName)
                   .HasMaxLength(256)
                   .IsRequired();
        }
    }
}