using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Api.Controllers.Base;
using MyBlog.Web.Api.Models;
using MyBlog.Web.Api.Services;

namespace MyBlog.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController(IWebApiUserService webApiUserService, ITokenService tokenService) : AppControleBase
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<Results<Ok<AuthenticationResponseDto>, UnauthorizedHttpResult>> Login([FromBody] AuthenticationRequestDto authenticationRequestDto)
        {
            WebApiUser? webApiUser = await webApiUserService.AuthenticateAsync(authenticationRequestDto.Username, authenticationRequestDto.Password);

            return webApiUser is not null
                ? TypedResults.Ok(new AuthenticationResponseDto(tokenService.CreateToken(webApiUser), webApiUser))
                : TypedResults.Unauthorized();
        }
    }
}