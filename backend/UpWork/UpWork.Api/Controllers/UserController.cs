using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Extensions;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
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
        [Authorize]
        public ActionResult<UserModel> CreateUser([FromBody] RegisterDto registerDto)
        {
            _permissionsService.VerifyPermission(User.Identity.GetUserId(), PermissionType.CreateUser, registerDto.OrganizationId);

            UserModel res = _userService.CreateUser(registerDto);

            return Ok(res);
        }
    }
}
