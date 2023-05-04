using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<IEnumerable<UserModel>> GetUsers(int skip = 0, int take = 10)
        {
            var res = _usersService.GetUsers(skip, take);
            return Ok(res);
        }

        [HttpGet("{OrganizationId}")]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<IEnumerable<UserModel>> GetUsersByOrganizationId(Guid OrganizationId, int skip = 0, int take = 10)
        {
            var res = _usersService.GetUsersByOrganizationId(OrganizationId, skip, take);

            return Ok(res);
        }

    }
}
