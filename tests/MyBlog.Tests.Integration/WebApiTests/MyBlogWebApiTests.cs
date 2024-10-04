using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using MyBlog.Domain.StaticValues;
using MyBlog.Web.Api.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;
using Xunit.Priority;

namespace MyBlog.Tests.Integration.WebApiTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class MyBlogWebApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private string? _jwtToken = null;

        public MyBlogWebApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact, Priority(0)]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var validCredentials = new AuthenticationRequestDto(DefaultUserCredentials.OrdinaryPassword, DefaultUserCredentials.OrdinaryEmail);
            var requestUri = "/api/authentication/login";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, validCredentials);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResponseDto>();
            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();
            result.WebApiUser.Username.Should().Be(validCredentials.Username);
            _jwtToken = result.Token;
        }

        [Fact, Priority(1)]
        public async Task Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var invalidCredentials = new AuthenticationRequestDto("wrongPassword", DefaultUserCredentials.OrdinaryEmail);
            var requestUri = "/api/authentication/login";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, invalidCredentials);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        private async Task<PostResponseViewModel> GetSamplePostWithCommentsAsync()
        {
            var requestUri = "/api/posts";
            var response = await _client.GetAsync(requestUri);

            return (await response.Content.ReadFromJsonAsync<GetPostsResponseViewModel>())!.Posts.First(c => c.CommentsCount > 0);
        }

        private async Task<CommentResponseViewModel> GetSampleCommentAsync()
        {
            var requestUri = $"/api/posts/{(await GetSamplePostWithCommentsAsync()).Id}/comments";
            var response = await _client.GetAsync(requestUri);

            return (await response.Content.ReadFromJsonAsync<IEnumerable<CommentResponseViewModel>>())!.First();
        }

        [Fact, Priority(0)]
        public async Task LoginOrdinaryUserAsync()
        {
            // Arrange
            var validCredentials = new AuthenticationRequestDto(DefaultUserCredentials.OrdinaryPassword, DefaultUserCredentials.OrdinaryEmail);
            var requestUri = "/api/authentication/login";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, validCredentials);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResponseDto>();

            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();

            _jwtToken = result!.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }

        [Fact, Priority(0)]
        public async Task LoginAdminUserAsync()
        {
            // Arrange
            var validCredentials = new AuthenticationRequestDto(DefaultUserCredentials.AdminPassword, DefaultUserCredentials.AdminEmail);
            var requestUri = "/api/authentication/login";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, validCredentials);

            // Assert
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResponseDto>();

            result.Should().NotBeNull();
            result!.Token.Should().NotBeNullOrEmpty();

            _jwtToken = result!.Token;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);
        }

        [Fact, Priority(2)]
        public async Task GetAllPosts_ShouldReturnOk()
        {
            // Arrange
            var requestUri = "/api/posts";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<GetPostsResponseViewModel>();
            result.Should().NotBeNull();
            result!.Posts.Should().NotBeEmpty();
        }

        [Fact, Priority(3)]
        public async Task GetPostById_ShouldReturnOk_WhenPostExists()
        {
            // Arrange
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<PostResponseViewModel>();
            result.Should().NotBeNull();
            result!.Id.Should().Be(postId);
        }

        [Fact, Priority(4)]
        public async Task GetPostById_ShouldReturnNotFound_WhenPostDoesNotExist()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact, Priority(5)]
        public async Task GetPostComments_ShouldReturnOk_WhenPostExists()
        {
            // Arrange
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var requestUri = $"/api/posts/{postId}/comments";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<CommentResponseViewModel>>();
            result.Should().NotBeNull();
        }

        [Fact, Priority(6)]
        public async Task GetPostComments_ShouldReturnNotFound_WhenPostDoesNotExist()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var requestUri = $"/api/posts/{postId}/comments";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact, Priority(7)]
        public async Task GetCommentById_ShouldReturnOk_WhenCommentExists()
        {
            // Arrange
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var commentId = (await GetSampleCommentAsync()).Id;
            var requestUri = $"/api/posts/{postId}/comments/{commentId}";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<CommentResponseViewModel>();
            result.Should().NotBeNull();
            result!.Id.Should().Be(commentId);
        }

        [Fact, Priority(8)]
        public async Task GetCommentById_ShouldReturnNotFound_WhenCommentDoesNotExist()
        {
            // Arrange
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var commentId = Guid.NewGuid();
            var requestUri = $"/api/posts/{postId}/comments/{commentId}";

            // Act
            var response = await _client.GetAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact, Priority(9)]
        public async Task CreatePost_ShouldReturnUnauthorized_WhenNotLoggedIn()
        {
            // Arrange
            var newPost = new PostRequestViewModel
            {
                Title = "New Post",
                Summary = "Summary of the new post",
                Content = "Content of the new post",
                PublishDate = DateTime.UtcNow
            };
            var requestUri = "/api/posts";

            // Act
            var response = await _factory.CreateClient().PostAsJsonAsync(requestUri, newPost);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact, Priority(9)]
        public async Task CreatePost_ShouldReturnCreated_WhenLoggedIn()
        {
            // Arrange
            await LoginOrdinaryUserAsync();
            var newPost = new PostRequestViewModel
            {
                Title = "New Post",
                Summary = "Summary of the new post",
                Content = "Content of the new post",
                PublishDate = DateTime.UtcNow
            };
            var requestUri = "/api/posts";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, newPost);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = await response.Content.ReadFromJsonAsync<PostResponseViewModel>();
            result.Should().NotBeNull();
            result!.Title.Should().Be(newPost.Title);
        }

        [Fact, Priority(10)]
        public async Task CreateComment_ShouldReturnCreated()
        {
            // Arrange
            await LoginOrdinaryUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var newComment = new CommentRequestViewModel 
            { 
                Content = "This is a comment",
                Id = postId,
            };
            var requestUri = $"/api/posts/{postId}/comments";

            // Act
            var response = await _client.PostAsJsonAsync(requestUri, newComment);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var result = await response.Content.ReadFromJsonAsync<CommentResponseViewModel>();
            result.Should().NotBeNull();
            result!.Content.Should().Be(newComment.Content);
        }

        [Fact, Priority(11)]
        public async Task DeletePost_ShouldReturnNoContent_WhenPostExists()
        {
            // Arrange
            await LoginAdminUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(12)]
        public async Task DeletePost_ShouldReturnNotFound_WhenPostDoesNotExist()
        {
            // Arrange
            await LoginOrdinaryUserAsync();
            var postId = Guid.NewGuid(); // Use a non-existing post ID
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact, Priority(13)]
        public async Task DeleteComment_ShouldReturnNoContent_WhenCommentExists()
        {
            // Arrange
            await LoginAdminUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var commentId = (await GetSampleCommentAsync()).Id;
            var requestUri = $"/api/posts/{postId}/comments/{commentId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(13)]
        public async Task DeleteComment_ShouldReturnForbidden_WhenUserIsNotAllowed()
        {
            // Arrange
            await LoginOrdinaryUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var commentId = (await GetSampleCommentAsync()).Id;
            var requestUri = $"/api/posts/{postId}/comments/{commentId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact, Priority(14)]
        public async Task DeleteComment_ShouldReturnNotFound_WhenCommentDoesNotExist()
        {
            // Arrange
            await LoginAdminUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var commentId = Guid.NewGuid(); // Use a non-existing comment ID
            var requestUri = $"/api/posts/{postId}/comments/{commentId}";

            // Act
            var response = await _client.DeleteAsync(requestUri);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact, Priority(15)]
        public async Task EditPost_ShouldReturnNoContent_WhenPostExists()
        {
            // Arrange
            await LoginAdminUserAsync();
            var postId = (await GetSamplePostWithCommentsAsync()).Id;
            var editPost = new PostRequestViewModel
            {
                Title = "Updated Post",
                Summary = "Updated summary",
                Content = "Updated content",
                PublishDate = DateTime.UtcNow
            };
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.PutAsJsonAsync(requestUri, editPost);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact, Priority(16)]
        public async Task EditPost_ShouldReturnNotFound_WhenPostDoesNotExist()
        {
            // Arrange
            await LoginAdminUserAsync();
            var postId = Guid.NewGuid();
            var editPost = new PostRequestViewModel
            {
                Title = "Updated Post",
                Summary = "Updated summary",
                Content = "Updated content",
                PublishDate = DateTime.UtcNow
            };
            var requestUri = $"/api/posts/{postId}";

            // Act
            var response = await _client.PutAsJsonAsync(requestUri, editPost);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}