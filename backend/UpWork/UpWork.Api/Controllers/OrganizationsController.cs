﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Infrastucture.Services;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {

        private readonly IOrganizationsService _organizationsService;

        public OrganizationsController(IOrganizationsService organizationsService)
        {
            _organizationsService = organizationsService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<IEnumerable<OrganizationModel>> GetOrganizations()
        {
            var res = _organizationsService.GetOrganizations();

            return Ok(res);
        }
    }
}