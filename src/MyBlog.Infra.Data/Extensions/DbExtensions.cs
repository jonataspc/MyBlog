using Microsoft.AspNetCore.Builder;
using MyBlog.Infra.Data.Helpers;

namespace MyBlog.Infra.Data.Extensions
{
    public static class DbExtensions
    {
        public static async Task UseDbMigrationHelperAsync(this WebApplication app)
        {
            await DbMigrationHelpers.EnsureSeedDataAsync(app);
        }
    }
}