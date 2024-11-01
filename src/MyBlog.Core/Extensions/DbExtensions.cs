using Microsoft.AspNetCore.Builder;
using MyBlog.Core.Data.Helpers;

namespace MyBlog.Core.Extensions
{
    public static class DbExtensions
    {
        public static async Task UseDbMigrationHelperAsync(this WebApplication app)
        {
            await DbMigrationHelpers.EnsureSeedDataAsync(app);
        }
    }
}