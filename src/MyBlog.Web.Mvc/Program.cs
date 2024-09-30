using MyBlog.Infra.Data.Extensions;
using MyBlog.IoC;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.AddControllersWithViews();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.UseDbMigrationHelperAsync();
await app.RunAsync();