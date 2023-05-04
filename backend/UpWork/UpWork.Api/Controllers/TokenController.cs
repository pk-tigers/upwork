using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Dto;
using UpWork.Common.Interfaces;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        [HttpPost]
        public ActionResult Login([FromBody] LoginDto loginData)
        {
            if (loginData is null)
                return BadRequest("Invalid client request");

            var isAuthenticated = _tokenService.TryAuthenticateUser(loginData, out var token);

            if (!isAuthenticated)
                return Unauthorized();

            var authResponse = new AuthenticatedResponseDto() { Token = token };
            return Ok(authResponse);
        }
    }
}
