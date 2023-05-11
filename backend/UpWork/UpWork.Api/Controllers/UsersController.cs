using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Common.Dto;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<PaginatedResult<UserModel>> GetUsers(int skip = 0, int take = 10)
        {
            var res = _usersService.GetUsers(skip, take);
            return Ok(res);
        }

        [HttpGet("{OrganizationId}")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<PaginatedResult<UserModel>> GetUsersByOrganizationId(Guid organizationId, int skip = 0, int take = 10)
        {
            var res = _usersService.GetUsersByOrganizationId(organizationId, skip, take);

            return Ok(res);
        }

        [HttpGet("UsersWithSupervisors")]
        public ActionResult<PaginatedResult<UserWithSupervisorDto>> UsersWithSupervisors(Guid organizationId, int skip = 0, int take = 10)
        {
            PaginatedResult<UserWithSupervisorDto> res = _usersService.UsersWithSupervisors(organizationId, skip, take);

            return Ok(res);
        }

    }
}
