using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Data;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Entities.Common;
using MyBlog.Domain.Exceptions;
using MyBlog.Infra.Data.Extensions;
using MyBlog.Infra.Identity;
using System.Reflection;

namespace MyBlog.Infra.Data.Context
{
    public class MyBlogDbContext : IdentityDbContext<ApplicationUser>, IUnitOfWork
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> BlogUsers { get; set; }

        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // fix precision for decimal data types
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(12, 2)");
            }

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.RemovePluralizingTableNameConvention();

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateModifiedAt();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateModifiedAt();
            return base.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (UniqueConstraintException e)
            {
                throw new DataUniqueConstraintException(e.Message, e);
            }
        }

        private void UpdateModifiedAt()
        {
            var entries = ChangeTracker
                            .Entries()
                            .Where(e => e.Entity is EntityBase && (e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).ModifiedAt = DateTime.Now;
            }
        }
    }
}