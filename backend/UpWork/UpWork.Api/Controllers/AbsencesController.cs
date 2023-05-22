using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Attributes;
using UpWork.Api.Extensions;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsencesService _absencesService;

        public AbsencesController(IAbsencesService absencesService)
        {
            _absencesService = absencesService;
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<PaginatedResult<AbsenceModel>> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByOrganizationId(organizationId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet]
        [Authorize(Policy = IdentityData.MatchOrganizationIdQueryPolicy)]
        public ActionResult<IEnumerable<object>> GetUsersAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to)
        {
            var res = _absencesService.GetUsersAbsencesByOrganizationId(organizationId, from, to);
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<PaginatedResult<AbsenceModel>> GetAbsencesByDateForUser(DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var userId = User.Identity.GetUserId();
            var res = _absencesService.GetAbsencesByUserId(userId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<PaginatedResult<AbsenceModel>> GetAbsencesForUser(int skip = 0, int take = 10)
        {
            var userId = User.Identity.GetUserId();

            var res = _absencesService.GetAbsencesForUser(userId, skip, take);
            return res;
        }

        [HttpGet]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.CanSupervise)]
        public ActionResult<PaginatedResult<AbsenceModel>> GetPendingAbsencesRequestsForSupervisor(int skip = 0, int take = 10)
        {
            var supervisorId = User.Identity.GetUserId();
            var res = _absencesService.GetPendingAbsencesRequestsForSupervisor(supervisorId, skip, take);
            return Ok(res);
        }

        [HttpGet]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.CanSupervise)]
        public ActionResult<PaginatedResult<AbsenceModel>> GetSupervisedAbsencesRequestsForSupervisor(int skip = 0, int take = 10)
        {
            var supervisorId = User.Identity.GetUserId();
            var res = _absencesService.GetSupervisedAbsencesRequestsForSupervisor(supervisorId, skip, take);
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<int> GetYearAbsenceCountForUser()
        {
            var userId = User.Identity.GetUserId();

            var res = _absencesService.GetYearAbsenceCountForUser(userId);
            return res;
        }
    }
}
