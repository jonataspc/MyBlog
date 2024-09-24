using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.Bus.Publishers;
using MyBlog.Application.Services;
using MyBlog.Domain.Bus;
using MyBlog.Domain.Services;

namespace MyBlog.IoC.Dependencies
{
    internal static class Services
    {
        public static void RegisterServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICommentService, CommentService>();


            // Bus
            services.AddSingleton<IGenericMessageBusPublisher, GenericMessageBusPublisherService>();

            services.AddMassTransit(m =>
            {
                m.AddConsumers(typeof(GenericMessageBusPublisherService).Assembly);
                m.SetKebabCaseEndpointNameFormatter();
                m.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.ConfigureJsonSerializerOptions(o =>
                    {
                        o.IncludeFields = true;
                        return o;
                    }
                    );
                });
            });
        }
    }
}