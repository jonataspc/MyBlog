using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Services;
using MyBlog.Web.Api.Controllers.Base;
using MyBlog.Web.Api.Models;

namespace MyBlog.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class PostsController(
        IAppIdentityUser appIdentityUser,
        IPostService postService,
        ICommentService commentService,
        IConfiguration configuration) : AppControleBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<Ok<GetPostsResponseDto>, BadRequest<string>>> GetAllPosts([FromQuery] GetPostsRequestDto getPostsRequestDto)
        {
            int pageSize = configuration.GetValue<int>("Posts:PageSize");
            IPaginatedList<Post> posts = await postService.GetAvailablePostsPaginatedAsync(getPostsRequestDto.PageNumber ?? 1, pageSize);

            var pagingDetails = posts.Adapt<GetPostsResponseDto>();
            var response = pagingDetails with
            {
                Posts = posts.Adapt<IEnumerable<PostViewModel>>()
            };

            if (getPostsRequestDto.PageNumber > pagingDetails.TotalPages)
            {
                return TypedResults.BadRequest($"{nameof(getPostsRequestDto.PageNumber)} is greater than {nameof(pagingDetails.TotalPages)}");
            }

            return TypedResults.Ok(response);
        }

        [HttpGet("{postId:guid}", Name = nameof(GetPostById))]
        [AllowAnonymous]
        public async Task<Results<Ok<PostViewModel>, NotFound>> GetPostById(Guid postId)
        {
            var post = await postService.GetByIdAsync(postId);

            if (post is null)
            {
                return TypedResults.NotFound();
            }

            await postService.IncrementViewsAsync(postId);
            return TypedResults.Ok(post.Adapt<PostViewModel>());
        }

        [HttpGet("{postId:guid}/comments", Name = nameof(GetPostComments))]
        [AllowAnonymous]
        public async Task<Ok<IEnumerable<CommentViewModel>>> GetPostComments(Guid postId)
        {
            var comments = (await postService.GetByIdAsync(postId))?.Comments;
            return TypedResults.Ok(comments.Adapt<IEnumerable<CommentViewModel>>());
        }

        [HttpGet("{postId:guid}/comments/{commentId:guid}", Name = nameof(GetCommentById))]
        [AllowAnonymous]
        public async Task<Results<Ok<CommentViewModel>, NotFound>> GetCommentById(Guid postId, Guid commentId)
        {
            var comment = await commentService.GetByIdAsync(commentId);

            if (comment is null || comment.PostId != postId)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(comment.Adapt<CommentViewModel>());
        }

        [HttpPost]
        public async Task<CreatedAtRoute<PostViewModel>> CreatePost([FromBody] NewPostViewModel newPostViewModel)
        {
            Post post = newPostViewModel.Adapt<Post>();
            await postService.AddAsync(post, appIdentityUser.GetUserId());
            return TypedResults.CreatedAtRoute(post.Adapt<PostViewModel>(), nameof(GetPostById), new { postId = post.Id });
        }

        [HttpPost("{postId:guid}/comments")]
        public async Task<CreatedAtRoute<CommentViewModel>> CreateComment(Guid postId, [FromBody] NewCommentViewModel newCommentViewModel)
        {
            Comment comment = newCommentViewModel.Adapt<Comment>();
            comment.UserId = appIdentityUser.GetUserId();
            comment.PostId = postId;
            await commentService.AddAsync(comment);
            return TypedResults.CreatedAtRoute(comment.Adapt<CommentViewModel>(), nameof(GetCommentById), new { postId = postId, commentId = comment.Id });
        }

        //TODO: edit, delete
    }
}