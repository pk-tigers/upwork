using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private IConfiguration _configuration;
        public PingController(IConfiguration configuration) 
        {
            _configuration = configuration;

        }
        [HttpGet]
        public ActionResult Pong()
        {
            return Ok($"pong {_configuration["ConnectionStrings:Default"]}");
        }
        [HttpGet("auth"), Authorize]
        public ActionResult PongAuth()
        {
            return Ok(User.Identity?.IsAuthenticated);
        }
    }
}
