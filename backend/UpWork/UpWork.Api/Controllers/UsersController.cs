using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            var res = _usersService.GetUsers();
            return Ok(res);
        }

        [HttpGet("{OrganizationId}")]
        public ActionResult<IEnumerable<UserModel>> GetUsersByOrganizationId(Guid OrganizationId)
        {
            var res = _usersService.GetUsersByOrganizationId(OrganizationId);

            return Ok(res);
        }

    }
}
