﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IPermissionsService _permissionsService;

        public UsersController(IUsersService usersService, IPermissionsService permissionsService)
        {
            _usersService = usersService;
            _permissionsService=permissionsService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            var res = _usersService.GetUsers();
            return Ok(res);
        }

        [HttpGet("{OrganizationId}")]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<IEnumerable<UserModel>> GetUsersByOrganizationId(Guid OrganizationId)
        {
            var res = _usersService.GetUsersByOrganizationId(OrganizationId);

            return Ok(res);
        }

    }
}
