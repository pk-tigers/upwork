using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpWork.Api.Attributes;
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

        [HttpPost]
        public ActionResult<AbsenceModel> CreateAbsenceRequestForUser([FromBody] CreateAbsenceRequestDto requestDto)
        {
            Guid userId = User.Identity.GetUserId();

            AbsenceModel createdRequest = _absenceService.CreateAbsenceRequestForUser(userId, requestDto);
            return Ok(createdRequest);
        }


        [HttpDelete("{id}")]
        public ActionResult<bool> CancelRequestForUser(Guid id)
        {
            var userId = User.Identity.GetUserId();

            bool isCancelled = _absenceService.CancelRequestForUser(id, userId);

            if (isCancelled)
                return Ok(true);
            else
                return NotFound();
        }

        [HttpPut]
        public ActionResult<AbsenceModelDto> UpdateAbsenceForUser([FromBody] UpdateAbsenceDto updateAbsenceDto)
        {
            var userId = User.Identity.GetUserId();
            var absence = _absenceService.UpdateAbsence(userId, updateAbsenceDto);

            return Ok(absence);
        }

    }
}
