using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using UpWork.Api.Attributes;
>>>>>>> dev
using UpWork.Api.Extensions;
using UpWork.Common.Dto;
using UpWork.Common.DTO;
using UpWork.Common.Enums;
using UpWork.Common.Identity;
using UpWork.Common.Interfaces;
using UpWork.Common.Models.DatabaseModels;
using UpWork.Database;

namespace UpWork.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpPost]
        [RequireClaim(IdentityData.PermissionsClaimName, PermissionType.CanSupervise)]
        public ActionResult<AbsenceModel> SetAbsenceApprovalStateForSupervisor([FromBody] AbsenceApprovalStateDto absenceApprovalState)
        {
            var supervisorId = User.Identity.GetUserId();

            var res = _absenceService.SetAbsenceApprovalState(absenceApprovalState, supervisorId);
            return Ok(res);
        }

<<<<<<< HEAD
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
=======
        [HttpPost]
        public ActionResult<AbsenceModel> CreateAbsenceRequestForUser([FromBody] CreateAbsenceRequestDto requestDto)
        {
            Guid userId = User.Identity.GetUserId();

            AbsenceModel createdRequest = _absenceService.CreateAbsenceRequestForUser(userId, requestDto);
            return Ok(createdRequest);
        }

        [HttpDelete]
        public ActionResult<bool> CancelRequestForUser(Guid absenceId)
        {
            var userId = User.Identity.GetUserId();

            bool isCancelled = _absenceService.CancelRequestForUser(absenceId, userId);
>>>>>>> dev

            if (isCancelled)
                return Ok(true);
            else
                return NotFound();
        }

    }
}
