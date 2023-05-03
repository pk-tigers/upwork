using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.DTO;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IUserService _userService;

        public OrganizationController(IOrganizationService organizationService, IUserService userService)
        {
            _organizationService = organizationService;
            _userService=userService;
        }

        [HttpPost]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<OrganizationModel> CreateOrganization([FromBody] CreateOrganizationDTO createOrganizationDTO)
        {
            OrganizationModel res = _organizationService.CreateOrganization(createOrganizationDTO);

            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
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
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<bool> DeleteOrganization(Guid id)
        {
            return _organizationService.DeleteOrganization(id);
        }

    }
}
