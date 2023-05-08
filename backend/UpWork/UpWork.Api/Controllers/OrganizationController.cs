using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Dto;
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

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpPost]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<OrganizationModel> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
        {
            OrganizationModel res = _organizationService.CreateOrganization(createOrganizationDto);

            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<OrganizationModel> GetOrganization(Guid id)
        {
            var res = _organizationService.GetOrganization(id);

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
