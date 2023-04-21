using Microsoft.AspNetCore.Mvc;
using UpWork.Common.DTO;
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
        [HttpGet]
        public ActionResult Login(LoginDTO loginData)
        {
            if (loginData is null)
                return BadRequest("Invalid client request");

            var isAuthenticated = _tokenService.TryAuthenticateUser(loginData, out var token);

            if (!isAuthenticated)
                return Unauthorized();

            var authResponse = new AuthenticatedResponseDTO() { Token = token };
            return Ok(authResponse);
        }
    }
}
