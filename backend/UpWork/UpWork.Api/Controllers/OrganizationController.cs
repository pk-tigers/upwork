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
        public IActionResult DeleteOrganization(Guid id)
        {
            var organization = _organizationService.GetOrganization(id);

            if (organization == null)
            {
                return NotFound("No organization to delete");
            }

            _organizationService.DeleteOrganization(id);

            return Ok("Organization deleted");
        }

        [HttpPost("addUser")]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<OrganizationModel> AddUserToOrganization([FromBody] UserOrganizationDto userOrganizationDto)
        {
            var user = _userService.GetUser(userOrganizationDto.UserId);
            if(user == null)
            {
               return NotFound("User does not exist");
            }

            var organization = _organizationService.GetOrganizationWithUsers(userOrganizationDto.OrganizationId);
            if( organization == null)
            {
                return NotFound("Organization not found");
            }

            if (organization.Users.Contains(user))
            {
                return BadRequest("User already exists in the organization");
            }

            _organizationService.AddUserToOrganization(userOrganizationDto);
            return Ok("User added to organization");
        }

        [HttpDelete("deleteUser")]
        [Authorize(Policy = IdentityData.AdminUserClaimName)]
        public ActionResult<OrganizationModel> DeleteUserFromOrganization([FromBody] UserOrganizationDto userOrganizationDto)
        {
            var user = _userService.GetUser(userOrganizationDto.UserId);
            if (user == null)
            {
                return NotFound("User does not exist");
            }

            var organization = _organizationService.GetOrganizationWithUsers(userOrganizationDto.OrganizationId);
            if (organization == null)
            {
                return NotFound("Organization not found");
            }

            if (!organization.Users.Contains(user))
            {
                return BadRequest("User has been removed");
            }


            _organizationService.DeleteUserFromOrganization(userOrganizationDto);
            return Ok("User removed from organization");
        }




    }
}
