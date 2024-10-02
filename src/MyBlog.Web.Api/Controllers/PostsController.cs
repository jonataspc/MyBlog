using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Entities;
using MyBlog.Domain.Exceptions;
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
        ILogger<PostsController> logger,
        IConfiguration configuration) : AppControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<Ok<GetPostsResponseViewModel>, BadRequest<string>>> GetAllPosts([FromQuery] GetPostsRequestViewModel getPostsRequest)
        {
            int pageSize = configuration.GetValue<int>("Posts:PageSize");
            IPaginatedList<Post> posts = await postService.GetAvailablePostsPaginatedAsync(getPostsRequest.PageNumber ?? 1, pageSize);

            var pagingDetails = posts.Adapt<GetPostsResponseViewModel>();
            var response = pagingDetails with
            {
                Posts = posts.Adapt<IEnumerable<PostResponseViewModel>>()
            };

            if (getPostsRequest.PageNumber > pagingDetails.TotalPages)
            {
                return TypedResults.BadRequest($"{nameof(getPostsRequest.PageNumber)} is greater than {nameof(pagingDetails.TotalPages)}");
            }

            return TypedResults.Ok(response);
        }

        [HttpGet("{postId:guid}", Name = nameof(GetPostById))]
        [AllowAnonymous]
        public async Task<Results<Ok<PostResponseViewModel>, NotFound>> GetPostById(Guid postId)
        {
            var post = await postService.GetByIdAsync(postId);

            if (post is null)
            {
                return TypedResults.NotFound();
            }

            await postService.IncrementViewsAsync(postId);
            return TypedResults.Ok(post.Adapt<PostResponseViewModel>());
        }

        [HttpGet("{postId:guid}/comments", Name = nameof(GetPostComments))]
        [AllowAnonymous]
        public async Task<Results<Ok<IEnumerable<CommentResponseViewModel>>, NotFound>> GetPostComments(Guid postId)
        {
            var post = await postService.GetByIdAsync(postId);

            if (post is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(post.Comments.Adapt<IEnumerable<CommentResponseViewModel>>());
        }

        [HttpGet("{postId:guid}/comments/{commentId:guid}", Name = nameof(GetCommentById))]
        [AllowAnonymous]
        public async Task<Results<Ok<CommentResponseViewModel>, NotFound>> GetCommentById(Guid postId, Guid commentId)
        {
            var comment = await commentService.GetByIdAsync(commentId);

            if (comment is null || comment.PostId != postId)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(comment.Adapt<CommentResponseViewModel>());
        }

        [HttpPost]
        public async Task<CreatedAtRoute<PostResponseViewModel>> CreatePost([FromBody] PostRequestViewModel newPost)
        {
            Post post = newPost.Adapt<Post>();
            await postService.AddAsync(post, appIdentityUser.GetUserId());
            return TypedResults.CreatedAtRoute(post.Adapt<PostResponseViewModel>(), nameof(GetPostById), new { postId = post.Id });
        }

        [HttpPost("{postId:guid}/comments")]
        public async Task<CreatedAtRoute<CommentResponseViewModel>> CreateComment(Guid postId, [FromBody] CommentRequestViewModel newCommentViewModel)
        {
            Comment comment = newCommentViewModel.Adapt<Comment>();
            comment.UserId = appIdentityUser.GetUserId();
            comment.PostId = postId;
            await commentService.AddAsync(comment);
            return TypedResults.CreatedAtRoute(comment.Adapt<CommentResponseViewModel>(), nameof(GetCommentById), new { postId = postId, commentId = comment.Id });
        }

        [HttpDelete("{postId:guid}")]
        public async Task<Results<NoContent, ForbidHttpResult, NotFound>> DeletePost(Guid postId)
        {
            var post = await postService.GetByIdAsync(postId);

            if (post is null)
            {
                return TypedResults.NotFound();
            }

            try
            {
                await postService.RemoveAsync(postId);
            }
            catch (NotAllowedOperationException e)
            {
                logger.LogError(e, e.Message);
                return TypedResults.Forbid();
            }

            return TypedResults.NoContent();
        }

        [HttpDelete("{postId:guid}/comments/{commentId:guid}")]
        public async Task<Results<NoContent, ForbidHttpResult, NotFound>> DeleteComment(Guid postId, Guid commentId)
        {
            var comment = await commentService.GetByIdAsync(commentId);

            if (comment is null || comment.PostId != postId)
            {
                return TypedResults.NotFound();
            }

            try
            {
                await commentService.RemoveAsync(commentId);
            }
            catch (NotAllowedOperationException e)
            {
                logger.LogError(e, e.Message);
                return TypedResults.Forbid();
            }

            return TypedResults.NoContent();
        }

        [HttpPut("{postId:guid}")]
        public async Task<Results<NoContent, ForbidHttpResult, NotFound>> EditPost(Guid postId, [FromBody] PostRequestViewModel editPost)
        {
            if (await postService.GetByIdAsync(postId) is null)
            {
                return TypedResults.NotFound();
            }

            editPost.Id = postId;

            try
            {
                await postService.UpdateAsync(editPost.Adapt<Post>());
            }
            catch (NotAllowedOperationException e)
            {
                logger.LogError(e, e.Message);
                return TypedResults.Forbid();
            }

            return TypedResults.NoContent();
        }
    }
}