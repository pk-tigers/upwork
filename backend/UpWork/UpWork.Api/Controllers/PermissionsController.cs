using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Dto;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
        }

        [HttpPost("UpdatePermissions")]
        [Authorize(Policy = IdentityData.AdminUserPolicy)]
        public ActionResult<bool> UpdatePermissions([FromBody] UpdatePermissionsDto updatePermissionsDto)
        {
            bool res = _permissionsService.UpdatePermissions(updatePermissionsDto);

            return Ok(res);
        }
    }
}
