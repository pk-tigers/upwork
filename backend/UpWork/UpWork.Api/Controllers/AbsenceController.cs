using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Extensions;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
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

        [HttpPost("CreateAbsenceRequest")]
        public ActionResult<AbsenceModel> CreateAbsenceRequest([FromBody] CreateAbsenceRequestDto requestDto)
        {
            Guid userId = User.Identity.GetUserId();

            AbsenceModel createdRequest = _absenceService.CreateAbsenceRequest(userId, requestDto);
            return Ok(createdRequest);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> CancelRequest(Guid id)
        {
            bool isCancelled = _absenceService.CancelRequestIfNotStarted(id);

            if (isCancelled)
                return Ok(true);
            else
                return NotFound();
        }

    }
}
