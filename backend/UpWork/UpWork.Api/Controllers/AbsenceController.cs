using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Common.Dto;
using UpWork.Common.Enums;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpPost("SetAbsenceApprovalState")]
        public ActionResult<AbsenceModel> SetAbsenceApprovalState([FromBody] AbsenceApprovalStateDto absenceApprovalState)
        {
            var res = _absenceService.SetAbsenceApprovalState(absenceApprovalState);
            return Ok(res);
        }
    }
}
