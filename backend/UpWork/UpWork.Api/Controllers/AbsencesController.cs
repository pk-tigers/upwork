using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Extensions;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsencesService _absencesService;

        public AbsencesController(IAbsencesService absencesService)
        {
            _absencesService = absencesService;
        }

        [HttpGet("GetAbsencesByOrganizationId")]
        public ActionResult<PaginatedResult<AbsenceModel>> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByOrganizationId(organizationId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet("GetAbsencesByUserId")]
        public ActionResult<PaginatedResult<AbsenceModel>> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByUserId(userId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet("GetPendingAbsencesRequestsBySupervisorId")]
        public ActionResult<PaginatedResult<AbsenceModel>> GetPendingAbsencesRequestsBySupervisorId(Guid supervisorId, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetPendingAbsencesRequestsBySupervisorId(supervisorId, skip, take);
            return Ok(res);
        }

        [HttpGet("GetSupervisedAbsencesRequestsBySupervisorId")]
        public ActionResult<PaginatedResult<AbsenceModel>> GetSupervisedAbsencesRequestsBySupervisorId(Guid supervisorId, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetSupervisedAbsencesRequestsBySupervisorId(supervisorId, skip, take);
            return Ok(res);
        }

        [HttpGet("GetCurrentYearAbsenceDays")]
        public ActionResult<int> GetCurrentYearAbsenceDays()
        {
            var userId = User.Identity.GetUserId();
            var res = _absencesService.GetCurrentYearAbsenceDays(userId);
            return res;
        }

        [HttpGet("GetAbsenceTypesForUser")]
        //przenies do absence types bo rafal sue przypierdala 
        public ActionResult<PaginatedResult<AbsenceTypeModel>> GetAbsenceTypesForUser(int skip = 0, int take = 10)
        {
            var userId = User.Identity.GetUserId();

            var res = _absencesService.GetAbsenceTypesForUser(userId, skip, take);
            return res;
        }

        [HttpGet("GetUserAbsences")]
        public ActionResult<PaginatedResult<AbsenceModel>> GetUserAbsences(int skip = 0, int take = 10)
        {
            var userId = User.Identity.GetUserId();

            var res = _absencesService.GetUserAbsences(userId, skip, take);
            return res;
        }
    }
}
