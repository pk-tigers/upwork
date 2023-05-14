using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Extensions;
using UpWork.Common.Dto;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IPermissionsService _permissionsService;

        public OrganizationController(IOrganizationService organizationService, IPermissionsService permissionsService)
        {
            _organizationService = organizationService;
            _permissionsService = permissionsService;
        }

        [HttpPost]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<OrganizationModel> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
        {
            OrganizationModel res = _organizationService.CreateOrganization(createOrganizationDto);

            return Ok(res);
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<OrganizationModel> GetOrganization([FromQuery] Guid organizationId)
        {
            var res = _organizationService.GetOrganization(organizationId);

            if (res == null)
            {
                return NotFound("Organization does not exist");
            }

            return Ok(res);
        }

        [HttpGet("GetOrganizationByUrlName")]
        public ActionResult<OrganizationModel> GetOrganizationByUrlName(string urlName)
        {
            OrganizationModel res = _organizationService.GetOrganizationByUrlName(urlName);

            var userId = User.Identity.GetUserId();
            _permissionsService.VerifyPermissionDatabase(userId, PermissionType.BasicRead, res.Id);

            if (res == null)
            {
                return NotFound("Organization does not exist");
            }

            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<bool> DeleteOrganization(Guid id)
        {
            return _organizationService.DeleteOrganization(id);
        }

    }
}
