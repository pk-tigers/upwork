using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Attributes;
using UpWork.Api.Extensions;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.CreateUser)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdBodyPolicy)]
        public ActionResult<UserModel> CreateUser([FromBody] RegisterDto registerDto)
        {
            UserModel res = _userService.CreateUser(registerDto);

            return Ok(res);
        }

        [HttpPost("CreateOwner")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<UserModel> CreateOwner([FromBody] RegisterDto registerDto)
        {
            UserModel res = _userService.CreateUser(registerDto, Role.OrganizationOwner);

            return Ok(res);
        }

        [HttpGet("{id}")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.BasicRead)]
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
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult <bool> DeleteUser(Guid Id)
        {
            return Ok(_userService.DeleteUser(Id));
        }

        [HttpPost("UpdateUserSupervisor")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.ModifyUser)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdBodyPolicy)]
        public ActionResult<bool> UpdateUserSupervisor([FromBody] UpdateUserSupervisorDto updateUserSupervisor)
        {
            bool res = _userService.UpdateUserSupervisor(updateUserSupervisor);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.ModifyUser)]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<UserModel> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto, [FromQuery] Guid organizationId)
        {
            UserModel existingUser = _userService.GetUser(id, organizationId);

            if (existingUser == null)
            {
                return NotFound();
            }
            UserModel updatedUser = _userService.UpdateUser(existingUser, updateUserDto);
            return Ok(updatedUser);
       }
    }
}
