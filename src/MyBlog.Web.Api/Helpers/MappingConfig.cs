using Mapster;
using MyBlog.Domain.Entities;
using MyBlog.Web.Api.Models;

namespace MyBlog.Web.Api.Helpers
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Post, PostViewModel>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.AuthorName, src => src.Author.User.FullName);

            config.NewConfig<Comment, CommentViewModel>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.UserName, src => src.User.FullName);
        }
    }
}