using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Common.Dto;
using UpWork.Api.Attributes;
using UpWork.Common.Enums;
using UpWork.Api.Extensions;

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

        [HttpGet("GetUsersByOrganizationId")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.BasicRead)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<PaginatedResult<UserModel>> GetUsersByOrganizationId(Guid organizationId, int skip = 0, int take = 10)
        {
            var res = _usersService.GetUsersByOrganizationId(organizationId, skip, take);

            return Ok(res);
        }

        [HttpGet("GetOwnersByOrganizationId")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<PaginatedResult<UserModel>> GetOwnersByOrganizationId(Guid organizationId, int skip = 0, int take = 10)
        {
            var res = _usersService.GetOwnersByOrganizationId(organizationId, skip, take);

            return Ok(res);
        }

        [HttpGet("UsersWithSupervisors")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.BasicRead)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<PaginatedResult<UserWithSupervisorDto>> UsersWithSupervisors(Guid organizationId, int skip = 0, int take = 10)
        {
            PaginatedResult<UserWithSupervisorDto> res = _usersService.UsersWithSupervisors(organizationId, skip, take);

            return Ok(res);
        }

        [HttpGet("GetSupervisors")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.BasicRead)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<PaginatedResult<UserModel>> GetSupervisors(Guid organizationId, int skip = 0, int take = 10)
        {
            //Guid organizationId = (Guid)User.Identity.GetOrganizationId();
            var res = _usersService.GetSupervisors(organizationId, skip, take);

            return Ok(res);
        }

        [HttpGet("LoadUsersWithPermissions")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.GrantPermissions)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<PaginatedResult<UserWithPermissionsDto>> LoadUsersWithPermissions(Guid organizationId, int skip = 0, int take = 10)
        {
            PaginatedResult<UserWithPermissionsDto> res = _usersService.LoadUsersWithPermissions(organizationId, skip, take);
            return Ok(res);
        }

    }
}
