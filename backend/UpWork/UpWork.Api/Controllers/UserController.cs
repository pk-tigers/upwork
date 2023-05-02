using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Extensions;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPermissionsService _permissionsService;

        public UserController(IUserService userService, IPermissionsService permissionsService)
        {
            _userService = userService;
            _permissionsService = permissionsService;
        }

        [HttpPost]
        [Authorize(Policy = IdentityData.MatchOrganizationIdBodyPolicy)]
        public ActionResult<UserModel> CreateUser([FromBody] RegisterDto registerDto)
        {
            _permissionsService.VerifyPermissionDatabase(User.Identity.GetUserId(), PermissionType.CreateUser, registerDto.OrganizationId);

            UserModel res = _userService.CreateUser(registerDto);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUser(Guid Id)
        {
            var res = _userService.GetUser(Id);

            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid Id)
        {
            var user = _userService.GetUser(Id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(Id);
            return Ok("User has been deleted");
        }
    }
}
