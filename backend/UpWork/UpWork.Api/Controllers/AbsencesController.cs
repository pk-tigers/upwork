using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AbsencesController : ControllerBase
    {
        private readonly IAbsencesService _absencesService;

        public AbsencesController(IAbsencesService absencesService)
        {
            _absencesService = absencesService;
        }

        [HttpGet("GetAbsencesByOrganizationId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetAbsencesByOrganizationId(Guid organizationId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByOrganizationId(organizationId, from, to, skip, take);
            return Ok(res);
        }

        [HttpGet("GetAbsencesByUserId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetAbsencesByUserId(Guid userId, DateTime from, DateTime to, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesByUserId(userId, from, to, skip, take);
            return Ok();
        }

        [HttpGet("GetAbsencesRequestsBySupervisorId")]
        public ActionResult<IEnumerable<AbsenceModel>> GetAbsencesRequestsBySupervisorId(Guid supervisorId, ApprovalState? approvalState = null, int skip = 0, int take = 10)
        {
            var res = _absencesService.GetAbsencesRequestsBySupervisorId(supervisorId, approvalState, skip, take);
            return Ok();
        }
    }
}
