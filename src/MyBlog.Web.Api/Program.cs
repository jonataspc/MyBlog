using MyBlog.Core.Extensions;
using MyBlog.Core.IoC;
using MyBlog.Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterDependencies(builder.Configuration, builder.Environment);
builder.Services.RegisterWebApiServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddAuthorizationBuilder();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
await app.UseDbMigrationHelperAsync();
await app.RunAsync();