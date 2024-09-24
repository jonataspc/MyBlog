using MyBlog.Infra.Data.Extensions;
using MyBlog.IoC;
using MyBlog.Web.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDependencies(builder.Configuration);
builder.Services.RegisterLocalServiceDependencies();
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
    pattern: "{controller=Posts}/{action=Index}/{id?}");
app.MapRazorPages();

await app.UseDbMigrationHelperAsync();

await app.RunAsync();