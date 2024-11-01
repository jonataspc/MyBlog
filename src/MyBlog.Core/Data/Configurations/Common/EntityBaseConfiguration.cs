using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Core.Entities.Common;

namespace MyBlog.Core.Data.Configurations.Common
{
    internal abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
            where TEntity : EntityBase

    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired()
                .HasPrecision(0);

            builder.Property(e => e.ModifiedAt)
                .HasPrecision(0);
        }
    }
}